using UnityEngine;

namespace Geo2D
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
                var v0 = new Vector2(_verts[0].transform.position.x, _verts[0].transform.position.y);
                var v1 = new Vector2(_verts[1].transform.position.x, _verts[1].transform.position.y);
                var v2 = new Vector2(_verts[2].transform.position.x, _verts[2].transform.position.y);

                _tri = new Triangle(v0, v1, v2);
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

            Debug.DrawLine(_tri._v0, _tri._v1, _color);
            Debug.DrawLine(_tri._v1, _tri._v2, _color);
            Debug.DrawLine(_tri._v2, _tri._v0, _color);
        }
    }
}