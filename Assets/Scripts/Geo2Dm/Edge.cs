using System.Runtime.CompilerServices;
using Unity.Mathematics;
using static Unity.Mathematics.math;

namespace Geo2Dm
{
    // A line segment connecting two points on the XY plane.
    public struct Edge
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Edge(float2 v0, float2 v1)
        {
            this.v0 = v0;
            this.v1 = v1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float2 CalcDirection()
        {
            return normalize(Axis);
        }

        public float2 Axis
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return v1 - v0; }
        }

        public float2 Centre
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return v0 + Axis * 0.5f; }
        }

        public float2 v0;
        public float2 v1;
    }
}
