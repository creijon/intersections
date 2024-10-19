using Geo2D;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

namespace Geo3D
{
    public static class Intersect
    {
        public static bool Test(Vector3 p, AABB aabb)
        {
            var cr = p - aabb._centre;

            if (Mathf.Abs(cr.x) > aabb._extents.x) return false;
            if (Mathf.Abs(cr.y) > aabb._extents.y) return false;
            if (Mathf.Abs(cr.z) > aabb._extents.z) return false;

            return true;
        }

        public static bool Test(AABB aabb1, AABB aabb2)
        {
            var cr = aabb1._centre - aabb2._centre;
            var e = aabb1._extents + aabb2._extents;

            if (Mathf.Abs(cr.x) > e.x) return false;
            if (Mathf.Abs(cr.y) > e.y) return false;
            if (Mathf.Abs(cr.z) > e.z) return false;

            return true;
        }

        public static bool Test(Edge line, AABB aabb)
        {
            var ha = line.Axis * 0.5f;
            var cr = line.Centre - aabb._centre;
            var e = aabb._extents;
            var ahax = Mathf.Abs(ha.x);          // Exploiting symmetry
            var ahay = Mathf.Abs(ha.y);
            var ahaz = Mathf.Abs(ha.z);

            if (Mathf.Abs(cr.x) > e.x + ahax) return false;
            if (Mathf.Abs(cr.y) > e.y + ahay) return false;
            if (Mathf.Abs(cr.z) > e.z + ahaz) return false;
            if (Mathf.Abs(ha.y * cr.z - ha.z * cr.y) > e.y * ahaz + e.z * ahay + Mathf.Epsilon) return false;
            if (Mathf.Abs(ha.z * cr.x - ha.x * cr.z) > e.z * ahax + e.x * ahaz + Mathf.Epsilon) return false;
            if (Mathf.Abs(ha.x * cr.y - ha.y * cr.x) > e.x * ahay + e.y * ahax + Mathf.Epsilon) return false;

            return true;
        }

        // Adapted from Moller-Trumbore solution:
        // https://en.wikipedia.org/wiki/M%C3%B6ller%E2%80%93Trumbore_intersection_algorithm
        public static bool Test(Ray ray, Triangle triangle, out float t)
        {
            var t0 = triangle._v0;
            var t1 = triangle._v1;
            var t2 = triangle._v2;
            var e1 = t1 - t0;
            var e2 = t2 - t0;
            var P = Vector3.Cross(ray._dir, e2);
            var det = Vector3.Dot(e1, P);
            t = 0.0f;

            if (det > -Mathf.Epsilon && det < Mathf.Epsilon) return false;

            float invDet = 1.0f / det;
            var T = ray._origin - t0;
            var u = Vector3.Dot(T, P) * invDet;
            if (u < 0.0f || u > 1.0f) return false;

            var Q = Vector3.Cross(T, e1);
            var v = Vector3.Dot(ray._dir, Q * invDet);
            if (v < 0.0f || u + v > 1.0f) return false;

            t = Vector3.Dot(e2, Q) * invDet;
            if (t > Mathf.Epsilon) return true;

            return false;
        }

        public static bool Test(Edge edge, Triangle triangle, out float t)
        {
            var d = edge.Axis;
            var ld = d.magnitude;
            var dir = d / ld;
            
            if (Test(new Ray(edge._v0, dir), triangle, out t))
            {
                if (t <= ld) return true;
            }

            return false;
        }

        public static bool Test(Ray ray, AABB aabb, out float t)
        {
            var invDir = Util.Multiply(Vector3.one, ray._dir);
            var rmin = Util.Multiply(aabb.Min - ray._origin, invDir);
            var rmax = Util.Multiply(aabb.Max - ray._origin, invDir);
            var tmax = Mathf.Min(Mathf.Min(Mathf.Max(rmin.x, rmax.x), Mathf.Max(rmin.y, rmax.y)), Mathf.Max(rmin.z, rmax.z));

            t = tmax;

            if (tmax < 0.0f) return false;

            var tmin = Mathf.Max(Mathf.Max(Mathf.Min(rmin.x, rmax.x), Mathf.Min(rmin.y, rmax.y)), Mathf.Min(rmin.z, rmax.z));

            if (tmin > tmax) return false;

            t = tmin;

            return true;
        }

        // Adapted from Schwarz-Seidel triangle-box intersection:
        // https://michael-schwarz.com/research/publ/2010/vox/
        public static bool Test(Triangle triangle, AABB aabb)
        {
            if (!Test(triangle.CalcBounds(), aabb)) return false;
            if (!Test(triangle.CalcPlane(), aabb)) return false;

            if (!Geo2D.Intersect.Test(triangle.XY, aabb.XY)) return false;
            if (!Geo2D.Intersect.Test(triangle.YZ, aabb.YZ)) return false;
            if (!Geo2D.Intersect.Test(triangle.ZX, aabb.ZX)) return false;

            return true;
        }

        // This is my alternative approach to the triangle-AABB test.
        // It differs from SAT-derived solutions such as the Schwarz-Seidel algo above by finding
        // positive intersections early and returning.  SAT solutions only exit early on disjoint
        // cases.  This means that in situations where there are many intersecting triangles it 
        // will be significantly faster than the Schwarz-Seidel.
        public static bool Test2(Triangle triangle, AABB aabb)
        {
            // Early out if the AABB of the triangle is disjoint with the AABB.
            if (!Test(triangle.CalcBounds(), aabb)) return false;

            // Test three triangle edges against box.
            if (Test(triangle.Edge0, aabb)) return true;
            if (Test(triangle.Edge1, aabb)) return true;
            if (Test(triangle.Edge2, aabb)) return true;

            // If none of the edges of a degenerate triangle intersect then don't test any further.
            var n = triangle.Cross();
            if (n.sqrMagnitude < Mathf.Epsilon) return false;
            
            // Test the four internal diagonals of the box against the triangle.
            // This catches the situations where the middle of the triangle is intersected
            // by the box but not any of the edges.
            float t;
            var min = aabb.Min;
            var max = aabb.Max;
            var axis0 = max - min;
            var invLength = 1.0f / axis0.magnitude;

            if (Test(new Ray(min, axis0 * invLength), triangle, out t))
            {
                if (t * invLength <= 1.0f) return true;
            }

            var i1a = new Vector3(max.x, min.y, min.z);
            var i1b = new Vector3(min.x, max.y, max.z);
            var axis1 = i1b - i1a;

            if (Test(new Ray(i1a, axis1 * invLength), triangle, out t))
            {
                if (t * invLength <= 1.0f) return true;
            }

            var i2a = new Vector3(min.x, max.y, min.z);
            var i2b = new Vector3(max.x, min.y, max.z);
            var axis2 = i2b - i2a;

            if (Test(new Ray(i2a, axis2 * invLength), triangle, out t))
            {
                if (t * invLength <= 1.0f) return true;
            }

            var i3a = new Vector3(max.x, max.y, min.z);
            var i3b = new Vector3(min.x, min.y, max.z);
            var axis3 = i3b - i3a;

            if (Test(new Ray(i3a, axis3 * invLength), triangle, out t))
            {
                if (t * invLength <= 1.0f) return true;
            }

            return false;
        }


        public static bool Test(Plane plane, AABB aabb)
        {
            var r = Vector3.Dot(aabb._extents, Util.Abs(plane._n));
            var s = plane.SignedDistance(aabb._centre);

            return Mathf.Abs(s) <= r;
        }

        public static bool Test(Edge edge, Plane plane, out float t)
        {
            t = 0.0f;
            var d0 = plane.SignedDistance(edge._v0);
            var d1 = plane.SignedDistance(edge._v1);

            if (Mathf.Abs(d0 - d1) < Mathf.Epsilon) return true;
            if (Mathf.Sign(d0) == Mathf.Sign(d1)) return false;

            t = d0 / (d0 - d1);

            return true;
        }
    }
}

