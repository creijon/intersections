using Geo2D;
using UnityEngine;

namespace Geo3D
{
    public class Ray
    {
        public Ray(Vector3 origin, Vector3 dir)
        {
            _origin = origin;
            _dir = dir;
        }

        public Vector3 CalcPos(float t)
        {
            return _origin + _dir * t;
        }

        public Vector3 _origin;
        public Vector3 _dir;
    }
}
