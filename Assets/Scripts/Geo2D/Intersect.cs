using UnityEngine;

namespace Geo2D
{
    public static class Intersect
    {
        public static bool Test(Vector2 p, Rect rect)
        {
            var cr = p - rect.centre;

            if (Mathf.Abs(cr.x) > rect.extents.x) return false;
            if (Mathf.Abs(cr.y) > rect.extents.y) return false;

            return true;
        }

        public static bool Test(Rect rect1, Rect rect2)
        {
            var cr = rect1.centre - rect2.centre;
            var e = rect1.extents + rect2.extents;

            if (Mathf.Abs(cr.x) > e.x) return false;
            if (Mathf.Abs(cr.y) > e.y) return false;

            return true;
        }

        public static bool Test(Edge edge, Rect rect)
        {
            var cr = edge.Centre - rect.centre;
            var ha = edge.Axis * 0.5f;
            var e = rect.extents;
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
            float a1 = Util.SignedTriArea(a.v0, a.v1, b.v1);
            float a2 = Util.SignedTriArea(a.v0, a.v1, b.v0);

            if (a1 * a2 < 0.0f)
            {
                float a3 = Util.SignedTriArea(b.v0, b.v1, a.v0);
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
            var s = Util.SignedTriArea(tri.v0, p, tri.v2);
            var t = Util.SignedTriArea(tri.v1, p, tri.v0);
            if ((s < 0) != (t < 0) && s != 0 && t != 0) return false;

            var d = Util.SignedTriArea(tri.v2, p, tri.v1);
            return d == 0 || (d < 0) == (s + t <= 0);
        }

        public static bool Test(Triangle tri, Rect rect)
        {
            // If any of the edges intersect then the tri intersects.
            if (Test(tri.Edge0, rect)) return true;
            if (Test(tri.Edge1, rect)) return true;
            if (Test(tri.Edge2, rect)) return true;

            // If centre of the box is inside the tri then it intersects.
            if (Test(rect.centre, tri)) return true;

            return false;
        }
    }
}
