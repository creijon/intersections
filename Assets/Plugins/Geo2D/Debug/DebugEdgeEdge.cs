using UnityEngine;

namespace Geo2D
{
    [ExecuteInEditMode]
    public class DebugEdgeEdge : MonoBehaviour
    {
        public DrawEdge _edge0;
        public DrawEdge _edge1;

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            if (!_edge0 || !_edge1) return;

            Color color = new Color(1.0f, 1.0f, 0.0f);
            float t;

            if (Intersect.Test(_edge0._edge, _edge1._edge, out t))
            {
                _edge0._color = Color.green;
            }
            else
            {
                _edge0._color = Color.red;
            }
        }
    }
}