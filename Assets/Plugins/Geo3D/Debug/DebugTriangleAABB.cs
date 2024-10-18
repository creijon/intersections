using UnityEngine;

namespace Geo3D
{
    [ExecuteInEditMode]
    public class DebugTriangleAABB : MonoBehaviour
    {
        public DrawTriangle _triangle;
        public DrawAABB _aabb;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (!_aabb || !_triangle) return;

            Color color = new Color(1.0f, 1.0f, 0.0f);

            if (Intersect.Test(_triangle._triangle, _aabb._aabb))
            {
                _aabb._color = Color.green;
            }
            else
            {
                _aabb._color = Color.red;
            }
        }
    }
}