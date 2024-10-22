using Geo3D;
using System;
using System.Drawing;
using TMPro;
using UnityEngine;

namespace Geo2D
{
    public static class Intersect
    {
        public static bool Test(Vector2 p, Rect rect)
        {
            var cr = p - rect._centre;

            if (Mathf.Abs(cr.x) > rect._extents.x) return false;
            if (Mathf.Abs(cr.y) > rect._extents.y) return false;

            return true;
        }

        public static bool Test(Rect rect1, Rect rect2)
        {
            var cr = rect1._centre - rect2._centre;
            var e = rect1._extents + rect2._extents;

            if (Mathf.Abs(cr.x) > e.x) return false;
            if (Mathf.Abs(cr.y) > e.y) return false;

            return true;
        }

        public static bool Test(Edge edge, Rect rect)
        {
            var cr = edge.Centre - rect._centre;
            var ha = edge.Axis * 0.5f;
            var e = rect._extents;
            var ahax = Mathf.Abs(ha.x);          // Exploiting symmetry
            var ahay = Mathf.Abs(ha.y);

            if (Mathf.Abs(cr.x) > e.x + ahax) return false;
            if (Mathf.Abs(cr.y) > e.y + ahay) return false;
            if (Mathf.Abs(ha.x * cr.y - ha.y * cr.x) > e.x * ahay + e.y * ahax + Mathf.Epsilon) return false;

            return true;
        }

        public static bool Test(Vector2 p, Triangle tri)
        {
            var e0 = tri._v1 - tri._v0;
            var e2 = tri._v0 - tri._v2;
            var r0 = p - tri._v0;
            var r2 = p - tri._v2;
            var s = e2.x * r2.y - e2.y * r2.x;
            var t = e0.x * r0.y - e0.y * r0.x;
            if ((s < 0.0f) != (t < 0.0f) && s != 0.0f && t != 0.0f) return false;

            var e1 = tri._v2 - tri._v1;
            var r1 = p - tri._v1;
            var d = e1.y * r1.x - e1.y * r1.x;
            return (d == 0.0f) || ((d < 0.0f) == (s + t <= 0.0f));
        }

        public static bool Test(Triangle triangle, Rect rect)
        {
            // If any of the edges intersect then the triangle intersects.
            if (Test(triangle.Edge0, rect)) return true;
            if (Test(triangle.Edge1, rect)) return true;
            if (Test(triangle.Edge2, rect)) return true;

            // If centre of the box is inside the triangle then it intersects.
            if (Test(rect._centre, triangle)) return true;

            return false;
        }
    }
}
