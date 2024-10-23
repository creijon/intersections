using UnityEngine;

namespace Geo3D
{
    [ExecuteInEditMode]
    public class DebugEdgePlane : MonoBehaviour
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

            var plane = _tri._tri.CalcPlane();
            float t = 0.0f;

            if (Intersect.Test(_edge._edge, plane, out t))
            {
                _edge._color = Color.green;
                Vector3 intersection = Vector3.Lerp(_edge._edge._v0, _edge._edge._v1, t);

                Debug.DrawLine(intersection, intersection + plane._n, _edge._color);
            }
            else
            {
                _edge._color = Color.red;
            }
        }
    }

}