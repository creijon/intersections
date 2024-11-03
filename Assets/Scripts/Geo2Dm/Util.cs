using System.Runtime.CompilerServices;
using Unity.Mathematics;

namespace Geo2Dm
{
    class Util
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MaxCoefficient(float2 v)
        {
            return (v.x > v.y) ? v.x : v.y;
        }

        public static float SignedTriArea(float2 a, float2 b, float2 c)
        {
            var ca = a - c;
            var cb = b - c;
            return ca.x * cb.y - ca.y * cb.x;
        }
    }
}
