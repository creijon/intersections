using System.Runtime.CompilerServices;
using Unity.Mathematics;

namespace Geo2Dm
{
    // A ray on the XY plane.
    // Defined by an origin and a direction vector of unit length.
    public struct Ray
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Ray(float2 origin, float2 dir)
        {
            this.origin = origin;
            this.dir = dir;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float2 CalcPos(float t)
        {
            return origin + dir * t;
        }

        public float2 origin;
        public float2 dir;
    }
}
