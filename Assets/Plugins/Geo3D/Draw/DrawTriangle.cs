using UnityEngine;

namespace Geo3D
{
    [ExecuteInEditMode]
    public class DrawTriangle : MonoBehaviour
    {
        public GameObject[] _verts = new GameObject[3];
        public Color _color;
        public Triangle _triangle;

        void Reset()
        {
            _triangle = new Triangle(_verts[0].transform.position, _verts[1].transform.position, _verts[2].transform.position);
        }

        // Start is called before the first frame update
        void Start()
        {
            for (int i = 0; i < 3; ++i)
            {
                if (!_verts[i] && transform.GetChild(i))
                {
                    _verts[i] = transform.GetChild(i).gameObject;
                }
            }
            Reset();
        }

        // Update is called once per frame
        void Update()
        {
            Reset();
            Debug.DrawLine(_triangle._v0, _triangle._v1, _color);
            Debug.DrawLine(_triangle._v1, _triangle._v2, _color);
            Debug.DrawLine(_triangle._v2, _triangle._v0, _color);
        }
    }
}