using UnityEngine;

namespace Geo3D
{
    [ExecuteInEditMode]
    public class DrawTriangle : MonoBehaviour
    {
        public GameObject[] _verts = new GameObject[3];
        public Color _color;
        public Triangle _tri;

        void Reset()
        {
            if (_verts[0] && _verts[1] && _verts[2])
            {
                _tri = new Triangle(_verts[0].transform.position, _verts[1].transform.position, _verts[2].transform.position);
            }
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
            Debug.DrawLine(_tri.v0, _tri.v1, _color);
            Debug.DrawLine(_tri.v1, _tri.v2, _color);
            Debug.DrawLine(_tri.v2, _tri.v0, _color);
        }
    }
}