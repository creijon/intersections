using System.Runtime.CompilerServices;
using UnityEngine;

namespace Geo3D
{
    public class Plane
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Plane(Vector3 n, float d)
        {
            _n = n;
            _d = d;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float SignedDistance(Vector3 p)
        {
            Vector3 o = _n * _d;
            return Vector3.Dot(_n, p - o);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3 Project(Vector3 p)
        {
            return p - (SignedDistance(p) * _n);
        }

        public Vector3 _n;
        public float _d;
    }
}
