using Geo3D;
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

        public static bool Test(Triangle triangle, Rect rect)
        {
            // If any of the edges intersect then the triangle intersects.
            if (Test(triangle.Edge0, rect)) return true;
            if (Test(triangle.Edge1, rect)) return true;
            if (Test(triangle.Edge2, rect)) return true;

            return false;
        }
    }
}
