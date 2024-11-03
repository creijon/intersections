using UnityEngine;

namespace Geo3D
{
    public class Sphere
    {
        public Sphere(Vector3 centre, float radius)
        {
            this.centre = centre;
            this.radius = radius;
        }

        public Vector2 centre;
        public float radius;
    }
}
