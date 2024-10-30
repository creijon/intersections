using System.Runtime.CompilerServices;
using Unity.Mathematics;
using static Unity.Mathematics.math;

namespace Geo2D
{
    // A tri lying on the XY plane.
    public struct Triangle
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Triangle(float2 v0, float2 v1, float2 v2)
        {
            this.v0 = v0;
            this.v1 = v1;
            this.v2 = v2;
        }

        public Edge Edge0 => new Edge(v0, v1);
        public Edge Edge1 => new Edge(v1, v2);
        public Edge Edge2 => new Edge(v2, v0);

        public float2 CalcBarycentric(float2 p)
        {
            float2 e1 = v1 - v0;
            float2 e0 = v2 - v0;
            float2 e2 = p - v0;

            float dot00 = dot(e0, e0);
            float dot01 = dot(e0, e1);
            float dot02 = dot(e0, e2);
            float dot11 = dot(e1, e1);
            float dot12 = dot(e1, e2);

            float invDenom = 1.0f / (dot00 * dot11 - dot01 * dot01);
            float u = (dot11 * dot02 - dot01 * dot12) * invDenom;
            float v = (dot00 * dot12 - dot01 * dot02) * invDenom;

            return float2(u, v);
        }

        public float2 v0;
        public float2 v1;
        public float2 v2;
    }

}