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
            _origin = origin;
            _dir = dir;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector2 CalcPos(float t)
        {
            return _origin + _dir * t;
        }

        public Vector2 _origin;
        public Vector2 _dir;
    }
}
