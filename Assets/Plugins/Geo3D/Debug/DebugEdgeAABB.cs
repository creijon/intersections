using Geo2D;
using UnityEngine;

namespace Geo3D
{
    [ExecuteInEditMode]
    public class DebugEdgeAABB : MonoBehaviour
    {
        public DrawEdge _edge;
        public DrawAABB _aabb;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (!_aabb || !_edge) return;

            Color color = new Color(1.0f, 1.0f, 0.0f);

            if (Intersect.Test(_edge._edge, _aabb._aabb))
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