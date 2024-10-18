using UnityEngine;

namespace Geo3D
{
    [ExecuteInEditMode]
    public class DebugEdgeTriangle : MonoBehaviour
    {
        public DrawEdge _edge;
        public DrawTriangle _triangle;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (!_triangle || !_edge) return;

            Color color = new Color(1.0f, 1.0f, 0.0f);
            float t = 0.0f;

            if (Intersect.Test(_edge._edge, _triangle._triangle, out t))
            {
                _triangle._color = Color.green;
            }
            else
            {
                _triangle._color = Color.red;
            }
        }
    }

}