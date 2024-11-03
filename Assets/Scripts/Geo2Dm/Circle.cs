using System.Runtime.CompilerServices;
using Unity.Mathematics;

namespace Geo2Dm
{
    public struct Circle
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Circle(float2 centre, float radius)
        {
            this.centre = centre;
            this.radius = radius;
        }

        public float2 centre;
        public float radius;
    }
}
