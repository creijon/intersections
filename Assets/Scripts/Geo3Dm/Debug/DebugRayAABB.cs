using Unity.Mathematics;
using UnityEngine;

namespace Geo3Dm
{
    [ExecuteInEditMode]
    public class DebugRayAABB : MonoBehaviour
    {
        public DrawRay _ray;
        public DrawAABB _aabb;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (!_aabb || !_ray) return;

            Color color = new Color(1.0f, 1.0f, 0.0f);
            float t = 0.0f;

            if (Intersect.Test(_ray._ray, _aabb._aabb, out t))
            {
                _ray._color = Color.green;
                var hitPos = _ray._ray.CalcPos(t);
                DrawAABB.DebugDraw(hitPos - 0.01f, hitPos + 0.01f, Color.green, Matrix4x4.identity);
            }
            else
            {
                _ray._color = Color.red;
            }
        }
    }

}