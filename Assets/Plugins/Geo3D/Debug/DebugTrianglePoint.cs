using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Geo3D
{
    [ExecuteInEditMode]
    public class DebugTrianglePoint : MonoBehaviour
    {
        public DrawTriangle _triangle;
        public GameObject _point;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (!_triangle || !_point) return;

            var plane = _triangle._triangle.CalcPlane();
            Color color = new Color(1.0f, 1.0f, 0.0f);

            Debug.DrawLine(_point.transform.position, plane.Project(_point.transform.position), color);

            var tri = _triangle._triangle;

            if (false)//Intersect.TrianglePoint(tri._v0, tri._v1, tri._v2, _point.transform.position))
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