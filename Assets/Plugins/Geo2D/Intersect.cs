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

        // From Real-Time Collision Detection by Christer Ericson
        // Published by Morgan Kaufmaan Publishers
        // © 2005 Elvesier Inc
        public static bool Test(Edge a, Edge b, out float t)
        {
            t = 0.0f;
            float a1 = Util.SignedTriArea(a._v0, a._v1, b._v1);
            float a2 = Util.SignedTriArea(a._v0, a._v1, b._v0);

            if (a1 * a2 < 0.0f)
            {
                float a3 = Util.SignedTriArea(b._v0, b._v1, a._v0);
                // Since area is constant a1 - a2 = a3 - a4, or a4 = a3 + a2 - a1
                float a4 = a3 + a2 - a1;

                // Points a and b on different sides of cd if areas have different signs
                if (a3 * a4 < 0.0f)
                {
                    // Segments intersect. Find intersection point along L(t) = a + t * (b - a).
                    t = a3 / (a3 - a4);
                    return true;
                }
            }

            // Segments not intersecting or colinear
            return false;
        }

        public static bool Test(Vector2 p, Triangle tri)
        {
            var s = (tri._v0.x - tri._v2.x) * (p.y - tri._v2.y) - (tri._v0.y - tri._v2.y) * (p.x - tri._v2.x);
            var t = (tri._v1.x - tri._v0.x) * (p.y - tri._v0.y) - (tri._v1.y - tri._v0.y) * (p.x - tri._v0.x);
            if ((s < 0) != (t < 0) && s != 0 && t != 0) return false;

            var d = (tri._v2.x - tri._v1.x) * (p.y - tri._v1.y) - (tri._v2.y - tri._v1.y) * (p.x - tri._v1.x);
            return d == 0 || (d < 0) == (s + t <= 0);
        }

        public static bool Test(Triangle tri, Rect rect)
        {
            // If any of the edges intersect then the tri intersects.
            if (Test(tri.Edge0, rect)) return true;
            if (Test(tri.Edge1, rect)) return true;
            if (Test(tri.Edge2, rect)) return true;

            // If centre of the box is inside the tri then it intersects.
            if (Test(rect._centre, tri)) return true;

            return false;
        }
    }
}
