using Unity.Mathematics;
using static Unity.Mathematics.math;

namespace Geo2D
{
    public static class Intersect
    {
        public static bool Test(float2 p, Rect rect)
        {
            var acr = abs(p - rect.centre);

            if (any(acr > rect.extents)) return false;

            return true;
        }

        public static bool Test(Rect rect1, Rect rect2)
        {
            var e = rect1.extents + rect2.extents;
            var acr = abs(rect1.centre - rect2.centre);

            if (any(acr > e)) return false;

            return true;
        }

        public static bool Test(Edge edge, Rect rect)
        {
            var cr = edge.Centre - rect.centre;
            var ha = edge.Axis * 0.5f;
            var e = rect.extents;
            var acr = abs(cr);
            var aha = abs(ha);

            if (any(acr > e + aha)) return false;
            if (abs(ha.x * cr.y - ha.y * cr.x) > e.x * aha.y + e.y * aha.x + EPSILON) return false;

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

        // Same as above but faster if the point of intersection isn't required.
        public static bool Test(Edge a, Edge b)
        {
            float a1 = Util.SignedTriArea(a.v0, a.v1, b.v1);
            float a2 = Util.SignedTriArea(a.v0, a.v1, b.v0);

            if (a1 * a2 < 0.0f)
            {
                float a3 = Util.SignedTriArea(b.v0, b.v1, a.v0);
                float a4 = a3 + a2 - a1;
                if (a3 * a4 < 0.0f) return true;
            }

            return false;
        }

        public static bool Test(float2 p, Triangle tri)
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
