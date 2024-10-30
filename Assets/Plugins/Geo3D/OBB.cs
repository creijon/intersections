using Unity.Mathematics;

namespace Geo3D
{
    public struct OBB
    {
        public OBB(float4x4 transform)
        {
            _transform = transform;
        }

        public float4x4 _transform;
    }
}
