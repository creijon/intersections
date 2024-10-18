using UnityEngine;

namespace Geo3D
{
    [ExecuteInEditMode]
    public class DebugTriangleRay : MonoBehaviour
    {
        public DrawTriangle _triangle;
        public DrawRay _ray;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (!_triangle || !_ray) return;

            Color color = new Color(1.0f, 1.0f, 0.0f);

            float t = 0.0f;

            if (Intersect.Test(_ray._ray, _triangle._triangle, out t))
            {
                _triangle._color = Color.green;
            }
            else
            {
                _triangle._color = Color.red;
            }

            Vector3 p = _ray._ray.CalcPos(t);
            Geo3D.Plane plane = _triangle._triangle.CalcPlane();
            Debug.DrawLine(p, p + plane._n, _ray._color);
        }
    }

}