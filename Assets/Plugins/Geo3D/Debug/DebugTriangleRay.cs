using UnityEngine;

namespace Geo3D
{
    [ExecuteInEditMode]
    public class DebugTriangleRay : MonoBehaviour
    {
        public DrawTriangle _tri;
        public DrawRay _ray;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (!_tri || !_ray) return;

            Color color = new Color(1.0f, 1.0f, 0.0f);

            float t = 0.0f;

            if (Intersect.Test(_ray._ray, _tri._tri, out t))
            {
                _tri._color = Color.green;
            }
            else
            {
                _tri._color = Color.red;
            }

            Vector3 p = _ray._ray.CalcPos(t);
            Geo3D.Plane plane = _tri._tri.CalcPlane();
            Debug.DrawLine(p, p + plane.n, _ray._color);
        }
    }

}