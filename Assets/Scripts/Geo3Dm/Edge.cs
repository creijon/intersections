using System.Runtime.CompilerServices;
using Unity.Mathematics;
using static Unity.Mathematics.math;

namespace Geo3Dm
{
    public struct Edge
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Edge(float3 v0, float3 v1)
        {
            this.v0 = v0;
            this.v1 = v1;
        }

        public float3 Axis => v1 - v0;
        public float3 Centre => v0 + Axis * 0.5f;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float3 CalcDirection()
        {
            return normalize(Axis);
        }

        public float3 v0;
        public float3 v1;
    }

}