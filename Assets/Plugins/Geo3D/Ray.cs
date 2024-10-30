using System.Runtime.CompilerServices;
using Unity.Mathematics;

namespace Geo3D
{
    public struct Ray
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Ray(float3 origin, float3 dir)
        {
            this.origin = origin;
            this.dir = dir;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float3 CalcPos(float t)
        {
            return origin + dir * t;
        }

        public float3 origin;
        public float3 dir;
    }
}
