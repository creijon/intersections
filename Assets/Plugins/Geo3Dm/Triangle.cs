using System.Runtime.CompilerServices;
using Unity.Mathematics;
using static Unity.Mathematics.math;

namespace Geo3Dm
{
    public struct Triangle
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Triangle(float3 v0, float3 v1, float3 v2)
        {
            this.v0 = v0;
            this.v1 = v1;
            this.v2 = v2;
        }

        public Edge Edge0 => new Edge(v0, v1);
        public Edge Edge1 => new Edge(v1, v2);
        public Edge Edge2 => new Edge(v2, v0); 
        public Geo2Dm.Triangle XY => new Geo2Dm.Triangle(v0.xy, v1.xy, v2.xy);
        public Geo2Dm.Triangle YZ => new Geo2Dm.Triangle(v0.yz, v1.yz, v2.yz);
        public Geo2Dm.Triangle ZX => new Geo2Dm.Triangle(v0.zx, v1.zx, v2.zx);

        public AABB CalcBounds()
        {
            return new AABB(Util.Min(v0, v1, v2), Util.Max(v0, v1, v2), true);
        }

        public float3 Cross()
        {
            var edge0 = v1 - v0;
            var edge1 = v1 - v2;

            return cross(edge0, edge1);
        }

        public float3 CalcNormal()
        {
            return normalize(Cross());
        }

        public Plane CalcPlane()
        {
            var n = CalcNormal();
            float d = dot(v0, n);

            return new Plane(n, d);
        }

        public float2 CalcBarycentric(float3 p)
        {
            var v1 = this.v1 - this.v0;
            var v0 = this.v2 - this.v0;
            var v2 = p - this.v0;

            var dot00 = dot(v0, v0);
            var dot01 = dot(v0, v1);
            var dot02 = dot(v0, v2);
            var dot11 = dot(v1, v1);
            var dot12 = dot(v1, v2);

            var invDenom = 1.0f / (dot00 * dot11 - dot01 * dot01);
            var u = (dot11 * dot02 - dot01 * dot12) * invDenom;
            var v = (dot00 * dot12 - dot01 * dot02) * invDenom;

            return new float2(u, v);
        }

        public float3 v0;
        public float3 v1;
        public float3 v2;
    }
}
