using UnityEngine;
using static Unity.Mathematics.math;

namespace Geo3Dm
{
    [ExecuteInEditMode]
    public class DebugTriangleEdge : MonoBehaviour
    {
        public DrawTriangle _tri;
        public DrawEdge _edge;

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
                _edge._color = Color.green;
            }
            else
            {
                _edge._color = Color.red;
            }

            var plane = _tri._tri.CalcPlane();
            var p = lerp(_edge._edge.v0, _edge._edge.v1, t);
            Debug.DrawLine(p, p + plane.n, _edge._color);
        }
    }

}