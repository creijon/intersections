using Unity.Mathematics;

namespace Geo3Dm
{
    public struct Sphere
    {
        public Sphere(float3 centre, float radius)
        {
            this.centre = centre;
            this.radius = radius;
        }

        public float3 centre;
        public float radius;
    }
}
