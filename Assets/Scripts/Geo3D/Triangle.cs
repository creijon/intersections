using System.Runtime.CompilerServices;
using UnityEngine;

namespace Geo3D
{
    public class Triangle
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Triangle(Vector3 v0, Vector3 v1, Vector3 v2)
        {
            this.v0 = v0;
            this.v1 = v1;
            this.v2 = v2;
        }

        public Edge Edge0 => new Edge(v0, v1);
        public Edge Edge1 => new Edge(v1, v2);
        public Edge Edge2 => new Edge(v2, v0); 
        public Geo2D.Triangle XY => new Geo2D.Triangle(Util.XY(v0), Util.XY(v1), Util.XY(v2));
        public Geo2D.Triangle YZ => new Geo2D.Triangle(Util.YZ(v0), Util.YZ(v1), Util.YZ(v2));
        public Geo2D.Triangle ZX => new Geo2D.Triangle(Util.ZX(v0), Util.ZX(v1), Util.ZX(v2));
        public Vector3 Cross => Vector3.Cross(v1 - v0, v1 - v2);

        public AABB CalcBounds()
        {
            var min = Vector3.Min(Vector3.Min(v0, v1), v2);
            var max = Vector3.Max(Vector3.Max(v0, v1), v2);

            return new AABB(min, max, true);
        }

        public Vector3 CalcNormal()
        {
            return Cross.normalized;
        }

        public Plane CalcPlane()
        {
            Vector3 n = CalcNormal();
            float d = Vector3.Dot(v0, n);

            return new Plane(n, d);
        }

        public Vector2 CalcBarycentric(Vector3 p)
        {
            Vector3 v1 = this.v1 - this.v0;
            Vector3 v0 = this.v2 - this.v0;
            Vector3 v2 = p - this.v0;

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

        public Vector3 v0;
        public Vector3 v1;
        public Vector3 v2;
    }
}
