using System.Runtime.CompilerServices;
using UnityEngine;

namespace Geo2D
{
    // A ray on the XY plane.
    // Defined by an origin and a direction vector of unit length.
    public class Ray
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Ray(Vector2 origin, Vector2 dir)
        {
            this.origin = origin;
            this.dir = dir;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector2 CalcPos(float t)
        {
            return origin + dir * t;
        }

        public Vector2 origin;
        public Vector2 dir;
    }
}
