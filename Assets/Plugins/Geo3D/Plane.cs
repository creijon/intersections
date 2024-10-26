using System.Runtime.CompilerServices;
using UnityEngine;

namespace Geo3D
{
    public class Plane
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Plane(Vector3 n, float d)
        {
            this.n = n;
            this.d = d;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float SignedDistance(Vector3 p)
        {
            Vector3 o = n * d;
            return Vector3.Dot(n, p - o);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3 Project(Vector3 p)
        {
            return p - (SignedDistance(p) * n);
        }

        public Vector3 n;
        public float d;
    }
}
