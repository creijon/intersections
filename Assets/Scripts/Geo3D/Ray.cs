using System.Runtime.CompilerServices;
using UnityEngine;

namespace Geo3D
{
    public class Ray
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Ray(Vector3 origin, Vector3 dir)
        {
            this.origin = origin;
            this.dir = dir;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3 CalcPos(float t)
        {
            return origin + dir * t;
        }

        public Vector3 origin;
        public Vector3 dir;
    }
}
