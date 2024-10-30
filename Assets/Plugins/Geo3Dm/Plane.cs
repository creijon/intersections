using System.Runtime.CompilerServices;
using Unity.Mathematics;
using static Unity.Mathematics.math;

namespace Geo3Dm
{
    public struct Plane
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Plane(float3 n, float d)
        {
            this.n = n;
            this.d = d;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float SignedDistance(float3 p)
        {
            float3 o = n * d;
            return dot(n, p - o);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float3 Project(float3 p)
        {
            return p - (SignedDistance(p) * n);
        }

        public float3 n;
        public float d;
    }
}
