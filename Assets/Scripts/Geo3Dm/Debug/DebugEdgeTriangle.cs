using UnityEngine;

namespace Geo3Dm
{
    [ExecuteInEditMode]
    public class DebugEdgeTriangle : MonoBehaviour
    {
        public DrawEdge _edge;
        public DrawTriangle _tri;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (!_tri || !_edge) return;

            Color color = new Color(1.0f, 1.0f, 0.0f);
            float t = 0.0f;

            if (Intersect.Test(_edge._edge, _tri._tri, out t))
            {
                _tri._color = Color.green;
            }
            else
            {
                _tri._color = Color.red;
            }
        }
    }

}