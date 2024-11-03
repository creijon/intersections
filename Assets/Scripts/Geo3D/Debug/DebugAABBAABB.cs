using UnityEngine;

namespace Geo3D
{
    [ExecuteInEditMode]
    public class DebugAABBAABB : MonoBehaviour
    {
        public DrawAABB _aabb1;
        public DrawAABB _aabb2;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (!_aabb1 || !_aabb2) return;

            Color color = new Color(1.0f, 1.0f, 0.0f);

            if (Intersect.Test(_aabb1._aabb, _aabb2._aabb))
            {
                _aabb1._color = Color.green;
            }
            else
            {
                _aabb1._color = Color.red;
            }
        }
    }
}