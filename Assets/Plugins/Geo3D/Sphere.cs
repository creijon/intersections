using UnityEngine;

namespace Geo3D
{
    public class Sphere
    {
        public Sphere(Vector3 centre, float radius)
        {
            _centre = centre;
            _radius = radius;
        }

        public Vector2 _centre;
        public float _radius;
    }
}
