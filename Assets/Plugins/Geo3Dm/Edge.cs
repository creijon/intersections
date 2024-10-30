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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float3 CalcDirection()
        {
            return normalize(v1 - v0);
        }

        public float3 Axis
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return v1 - v0; }
        }

        public float3 Centre
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return v0 + Axis * 0.5f; }
        }

        public float3 v0;
        public float3 v1;
    }

}