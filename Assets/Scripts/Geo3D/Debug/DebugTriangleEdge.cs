using UnityEngine;

namespace Geo3D
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
            if (_tri._tri == null) return;
            if (_edge._edge == null) return;

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
            var p = Vector3.Lerp(_edge._edge.v0, _edge._edge.v1, t);
            Debug.DrawLine(p, p + plane.n, _edge._color);
        }
    }

}