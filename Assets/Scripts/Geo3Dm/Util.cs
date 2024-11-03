using System.Runtime.CompilerServices;
using Unity.Mathematics;
using static Unity.Mathematics.math;

namespace Geo3Dm
{
    class Util
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 Min(float3 a, float3 b, float3 c)
        {
            return min(min(a, b), c);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 Max(float3 a, float3 b, float3 c)
        {
            return max(max(a, b), c);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MinCoefficient(float3 v)
        {
            return (v.x < v.z) ? ((v.x < v.y) ? v.x : v.y) : ((v.y < v.z) ? v.y : v.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MaxCoefficient(float3 v)
        {
            return (v.x > v.z) ? ((v.x > v.y) ? v.x : v.y) : ((v.y > v.z) ? v.y : v.z);
        }
    }
}
