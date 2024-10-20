using UnityEngine;

namespace Geo3D
{
    public class Triangle
    {
        public Triangle(Vector3 v0, Vector3 v1, Vector3 v2)
        {
            _v0 = v0;
            _v1 = v1;
            _v2 = v2;
        }

        public Edge Edge0 => new Edge(_v0, _v1);
        public Edge Edge1 => new Edge(_v1, _v2);
        public Edge Edge2 => new Edge(_v2, _v0); 
        public Geo2D.Triangle XY => new Geo2D.Triangle(Util.XY(_v0), Util.XY(_v1), Util.XY(_v2));
        public Geo2D.Triangle YZ => new Geo2D.Triangle(Util.YZ(_v0), Util.YZ(_v1), Util.YZ(_v2));
        public Geo2D.Triangle ZX => new Geo2D.Triangle(Util.ZX(_v0), Util.ZX(_v1), Util.ZX(_v2));

        public AABB CalcBounds()
        {
            var min = Vector3.Min(Vector3.Min(_v0, _v1), _v2);
            var max = Vector3.Max(Vector3.Max(_v0, _v1), _v2);

            return new AABB(min, max, true);
        }

        public Vector3 Cross()
        {
            var edge0 = _v1 - _v0;
            var edge1 = _v2 - _v1;

            return Vector3.Cross(edge0, -edge1);
        }

        public Vector3 CalcNormal()
        {
            return Cross().normalized;
        }

        public Plane CalcPlane()
        {
            Vector3 n = CalcNormal();
            float d = Vector3.Dot(_v0, n);

            return new Plane(n, d);
        }

        public Vector2 CalcBarycentric(Vector3 p)
        {
            Vector3 v1 = _v1 - _v0;
            Vector3 v0 = _v2 - _v0;
            Vector3 v2 = p - _v0;

            float dot00 = Vector3.Dot(v0, v0);
            float dot01 = Vector3.Dot(v0, v1);
            float dot02 = Vector3.Dot(v0, v2);
            float dot11 = Vector3.Dot(v1, v1);
            float dot12 = Vector3.Dot(v1, v2);

            float invDenom = 1.0f / (dot00 * dot11 - dot01 * dot01);
            float u = (dot11 * dot02 - dot01 * dot12) * invDenom;
            float v = (dot00 * dot12 - dot01 * dot02) * invDenom;

            return new Vector2(u, v);
        }

        public Vector3 _v0;
        public Vector3 _v1;
        public Vector3 _v2;
    }
}
