using UnityEngine;

namespace Geo3Dm
{
    [ExecuteInEditMode]
    public class DebugPointTriangle : MonoBehaviour
    {
        public GameObject _point;
        public DrawTriangle _tri;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (!_tri || !_point) return;

            var plane = _tri._tri.CalcPlane();
            Color color = new Color(1.0f, 1.0f, 0.0f);

            Debug.DrawLine(_point.transform.position, plane.Project(_point.transform.position), color);

            if (Intersect.Test(_point.transform.position, _tri._tri))
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