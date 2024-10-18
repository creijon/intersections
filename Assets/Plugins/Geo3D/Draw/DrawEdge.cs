using UnityEngine;

namespace Geo3D
{
    [ExecuteInEditMode]
    public class DrawEdge : MonoBehaviour
    {
        public GameObject _v0;
        public GameObject _v1;
        public Color _color;
        public Edge _edge;

        void Reset()
        {
            _edge = new Edge(_v0.transform.position, _v1.transform.position);
        }

        // Start is called before the first frame update
        void Start()
        {
            if (!_v0 && transform.GetChild(0)) _v0 = transform.GetChild(0).gameObject;
            if (!_v1 && transform.GetChild(1)) _v1 = transform.GetChild(1).gameObject;
            Reset();
        }

        // Update is called once per frame
        void Update()
        {
            Reset();
            Debug.DrawLine(_edge._v0, _edge._v1, _color);
        }
    }
}