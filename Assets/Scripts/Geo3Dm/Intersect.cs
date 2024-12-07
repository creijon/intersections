using Unity.Mathematics;
using static Unity.Mathematics.math;

namespace Geo3Dm
{
    public static class Intersect
    {
        public static bool Test(float3 p, AABB rect)
        {
            var acr = abs(p - rect.centre);

            if (any(acr > rect.extents)) return false;

            return true;
        }

        public static bool Test(AABB rect1, AABB rect2)
        {
            var e = rect1.extents + rect2.extents;
            var acr = abs(rect1.centre - rect2.centre);

            if (any(acr > e)) return false;

            return true;
        }

        public static bool Test(Ray ray, AABB aabb, out float t)
        {
            var invDir = 1.0f / ray.dir;
            var rmin = (aabb.Min - ray.origin) * invDir;
            var rmax = (aabb.Max - ray.origin) * invDir;
            var tmax = Util.MinCoefficient(max(rmin, rmax));
            t = tmax;
            if (tmax < 0.0f) return false;

            var tmin = Util.MaxCoefficient(min(rmin, rmax));
            if (tmin > tmax) return false;

            t = (tmin > 0.0f) ? tmin : tmax;

            return true;
        }

        public static bool Test(Edge edge, AABB aabb)
        {
            var ha = edge.Axis * 0.5f;          // Half axis
            var cr = edge.Centre - aabb.centre; // Centre relative
            var aha = abs(ha);                  // Abs half axis

            if (any(abs(cr) > aabb.extents + aha)) return false;

            var axis1 = abs(ha * cr.yzx - ha.yzx * cr.xyz);
            var axis2 = (aabb.extents * aha.yzx + aabb.extents.yzx * aha) + EPSILON;

            if (any(axis1 > axis2)) return false;

            return true;
        }

        // Adapted from Moller-Trumbore solution:
        // https://en.wikipedia.org/wiki/M%C3%B6ller%E2%80%93Trumbore_intersection_algorithm
        public static bool Test(Ray ray, Triangle tri, out float t)
        {
            var e1 = tri.v1 - tri.v0;
            var e2 = tri.v2 - tri.v0;
            var P = cross(ray.dir, e2);
            var det = dot(e1, P);
            t = 0.0f;

            if (det > -EPSILON && det < EPSILON) return false;

            float invDet = 1.0f / det;
            var T = ray.origin - tri.v0;
            var u = dot(T, P) * invDet;
            if (u < 0.0f || u > 1.0f) return false;

            var Q = cross(T, e1);
            var v = dot(ray.dir, Q * invDet);
            if (v < 0.0f || u + v > 1.0f) return false;

            t = dot(e2, Q) * invDet;
            if (t > EPSILON) return true;

            return false;
        }

        public static bool Test(Edge edge, Triangle tri, out float t)
        {
            var d = edge.Axis;
            var ld = length(d);
            var dir = d / ld;
            
            if (Test(new Ray(edge.v0, dir), tri, out t))
            {
                if (t <= ld) return true;
            }

            return false;
        }

        public static bool Test(float3 p, Triangle tri)
        {
            var e1 = tri.v2 - tri.v0;
            var e0 = tri.v1 - tri.v0;
            var eP = p - tri.v0;

            var dot01 = dot(e0, e1);
            var dot0P = dot(e0, eP);
            var dot11 = dot(e1, e1);
            var dot1P = dot(e1, eP);

            // Test edge1
            var u = dot11 * dot0P - dot01 * dot1P;
            if (u < 0.0f) return false;

            // Test edge0
            var dot00 = dot(e0, e0);
            var v = dot00 * dot1P - dot01 * dot0P;
            if (v < 0.0f) return false;

            var denom = dot00 * dot11 - dot01 * dot01;
            if (denom < u + v) return false;

            return true;
        }

        public static bool Test(Plane plane, AABB aabb)
        {
            var r = dot(aabb.extents, abs(plane.n));
            var s = plane.SignedDistance(aabb.centre);

            return abs(s) <= r;
        }

        public static bool Test(Edge edge, Plane plane)
        {
            var d0 = plane.SignedDistance(edge.v0);
            var d1 = plane.SignedDistance(edge.v1);

            return (d0 * d1 <= 0.0f);
        }

        public static bool Test(Edge edge, Plane plane, out float t)
        {
            t = 0.0f;
            var d0 = plane.SignedDistance(edge.v0);
            var d1 = plane.SignedDistance(edge.v1);

            if (d0 * d1 > 0.0f) return false;

            t = d0 / (d0 - d1);

            return true;
        }

        public static bool Test(Triangle tri, AABB aabb)
        {
            // Early out if the AABB of the triangle is disjoint with the AABB.
            if (!Test(tri.CalcBounds(), aabb)) return false;

            return TestNoBB(tri, aabb);
        }

        // Adapted from Schwarz-Seidel triangle-box intersection:
        // https://michael-schwarz.com/research/publ/2010/vox/
        // Provided for performance comparisons.
        public static bool TestSS(Triangle tri, AABB aabb)
        {
            var n = tri.Cross();
            var r = dot(aabb.extents, abs(n));
            var s = dot(n, aabb.centre - tri.v0);

            if (abs(s) > r) return false;

            if (!Geo2Dm.Intersect.Test(tri.XY, aabb.XY)) return false;
            if (!Geo2Dm.Intersect.Test(tri.YZ, aabb.YZ)) return false;
            if (!Geo2Dm.Intersect.Test(tri.ZX, aabb.ZX)) return false;

            return true;
        }


        // Separating Axis Theorem Solution
        // From Tomas Akenine-Moller 2001
        // https://fileadmin.cs.lth.se/cs/Personal/Tomas_Akenine-Moller/code/tribox_tam.pdf
        // Provided for performance comparisons.
        // NB: this returns false positives when the face of the triangle is very close to one corner of the box, for example:
        // t0 = { 0.07494, 0.070754, 0.028271 }
        // t1 = { 0.075958, 0.071995, 0.028739 }
        // t2 = { 0.075162, 0.071562, 0.029279 }
        // min = { 0.05576525, 0.0715705, 0.01354725 }
        // max = { 0.07522762, 0.09086225, 0.0286315 }
		// Also fails to detect intersections with degenerate triangles
        public static bool TestSAT(Triangle tri, AABB aabb)
        {
            float p0, p1, p2, r;

            var v0 = tri.v0 - aabb.centre;
            var v1 = tri.v1 - aabb.centre;
            var v2 = tri.v2 - aabb.centre;

            var f0 = v1 - v0;
            var f1 = v2 - v1;
            var f2 = v0 - v2;

            float3  a00 = new float3(0, -f0.z, f0.y),
                    a01 = new float3(0, -f1.z, f1.y),
                    a02 = new float3(0, -f2.z, f2.y),
                    a10 = new float3(f0.z, 0, -f0.x),
                    a11 = new float3(f1.z, 0, -f1.x),
                    a12 = new float3(f2.z, 0, -f2.x),
                    a20 = new float3(-f0.y, f0.x, 0),
                    a21 = new float3(-f1.y, f1.x, 0),
                    a22 = new float3(-f2.y, f2.x, 0);

            // Test axis a00
            p0 = dot(v0, a00);
            p1 = dot(v1, a00);
            p2 = dot(v2, a00);
            r = aabb.extents.y * abs(f0.z) + aabb.extents.z * abs(f0.y);

            if (any(max(-Util.Max(p0, p1, p2), Util.Min(p0, p1, p2)) > r))
            {
                return false;
            }

            // Test axis a01
            p0 = dot(v0, a01);
            p1 = dot(v1, a01);
            p2 = dot(v2, a01);
            r = aabb.extents.y * abs(f1.z) + aabb.extents.z * abs(f1.y);

            if (any(max(-Util.Max(p0, p1, p2), Util.Min(p0, p1, p2)) > r))
            {
                return false;
            }

            // Test axis a02
            p0 = dot(v0, a02);
            p1 = dot(v1, a02);
            p2 = dot(v2, a02);
            r = aabb.extents.y * abs(f2.z) + aabb.extents.z * abs(f2.y);

            if (any(max(-Util.Max(p0, p1, p2), Util.Min(p0, p1, p2)) > r))
            {
                return false;
            }

            // Test axis a10
            p0 = dot(v0, a10);
            p1 = dot(v1, a10);
            p2 = dot(v2, a10);
            r = aabb.extents.x * abs(f0.z) + aabb.extents.z * abs(f0.x);
            if (any(max(-Util.Max(p0, p1, p2), Util.Min(p0, p1, p2)) > r))
            {
                return false;
            }

            // Test axis a11
            p0 = dot(v0, a11);
            p1 = dot(v1, a11);
            p2 = dot(v2, a11);
            r = aabb.extents.x * abs(f1.z) + aabb.extents.z * abs(f1.x);

            if (any(max(-Util.Max(p0, p1, p2), Util.Min(p0, p1, p2)) > r))
            {
                return false;
            }

            // Test axis a12
            p0 = dot(v0, a12);
            p1 = dot(v1, a12);
            p2 = dot(v2, a12);
            r = aabb.extents.x * abs(f2.z) + aabb.extents.z * abs(f2.x);

            if (any(max(-Util.Max(p0, p1, p2), Util.Min(p0, p1, p2)) > r))
            {
                return false;
            }

            // Test axis a20
            p0 = dot(v0, a20);
            p1 = dot(v1, a20);
            p2 = dot(v2, a20);
            r = aabb.extents.x * abs(f0.y) + aabb.extents.y * abs(f0.x);

            if (any(max(-Util.Max(p0, p1, p2), Util.Min(p0, p1, p2)) > r))
            {
                return false;
            }

            // Test axis a21
            p0 = dot(v0, a21);
            p1 = dot(v1, a21);
            p2 = dot(v2, a21);
            r = aabb.extents.x * abs(f1.y) + aabb.extents.y * abs(f1.x);

            if (any(max(-Util.Max(p0, p1, p2), Util.Min(p0, p1, p2)) > r))
            {
                return false;
            }

            // Test axis a22
            p0 = dot(v0, a22);
            p1 = dot(v1, a22);
            p2 = dot(v2, a22);
            r = aabb.extents.x * abs(f2.y) + aabb.extents.y * abs(f2.x);

            if (any(max(-Util.Max(p0, p1, p2), Util.Min(p0, p1, p2)) > r))
            {
                return false;
            }

            if (any(Util.Max(v0.x, v1.x, v2.x) < -aabb.extents.x) || any(Util.Min(v0.x, v1.x, v2.x) > aabb.extents.x))
            {
                return false;
            }

            if (any(Util.Max(v0.y, v1.y, v2.y) < -aabb.extents.y) || any(Util.Min(v0.y, v1.y, v2.y) > aabb.extents.y))
            {
                return false;
            }

            if (any(Util.Max(v0.z, v1.z, v2.z) < -aabb.extents.z) || any(Util.Min(v0.z, v1.z, v2.z) > aabb.extents.z))
            {
                return false;
            }

            float3 normal = normalize(cross(f1, f0));
            float d = dot(normal, tri.v0);
            return Test(new Plane(normal, d), aabb);
        }

        public static bool TestNoBB(Triangle tri, AABB aabb)
        {
            // Test three triangle edges against box.
            if (Test(tri.Edge0, aabb)) return true;
            if (Test(tri.Edge1, aabb)) return true;
            if (Test(tri.Edge2, aabb)) return true;

            // If none of the edges of a degenerate triangle intersect then don't test any further.
            var n = tri.Cross();
            if (dot(n, n) < EPSILON) return false;

            // Test if plane of triangle intersects the box.
            var r = dot(aabb.extents, abs(n));
            var s = dot(n, aabb.centre - tri.v0);
            if (abs(s) > r) return false;

            // Test the four internal diagonals of the box against the triangle.
            // This catches the situations where the middle of the triangle is intersected
            // by the box but not any of the edges.
            float t;
            var min = aabb.Min;
            var max = aabb.Max;
            var axis0 = max - min;
            var invLength = 1.0f / length(axis0);

            if (Test(new Ray(min, axis0 * invLength), tri, out t))
            {
                if (t * invLength <= 1.0f) return true;
            }

            var i1a = new float3(max.x, min.y, min.z);
            var i1b = new float3(min.x, max.y, max.z);
            var axis1 = i1b - i1a;

            if (Test(new Ray(i1a, axis1 * invLength), tri, out t))
            {
                if (t * invLength <= 1.0f) return true;
            }

            var i2a = new float3(min.x, max.y, min.z);
            var i2b = new float3(max.x, min.y, max.z);
            var axis2 = i2b - i2a;

            if (Test(new Ray(i2a, axis2 * invLength), tri, out t))
            {
                if (t * invLength <= 1.0f) return true;
            }

            var i3a = new float3(max.x, max.y, min.z);
            var i3b = new float3(min.x, min.y, max.z);
            var axis3 = i3b - i3a;

            if (Test(new Ray(i3a, axis3 * invLength), tri, out t))
            {
                if (t * invLength <= 1.0f) return true;
            }

            return false;
        }
    }
}
