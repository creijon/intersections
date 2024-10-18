using UnityEngine;

namespace Geo3D
{
    public class Plane
    {
        public Plane(Vector3 n, float d)
        {
            _n = n;
            _d = d;
        }

        public float SignedDistance(Vector3 p)
        {
            Vector3 o = _n * _d;
            return Vector3.Dot(_n, p - o);
        }

        public Vector3 Project(Vector3 p)
        {
            return p - (SignedDistance(p) * _n);
        }

        public Vector3 _n;
        public float _d;
    }
}
