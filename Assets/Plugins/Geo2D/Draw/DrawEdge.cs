using UnityEngine;
using Unity.Mathematics;
using Geo3D;

namespace Geo2D
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
            if (_v0 && _v1)
            {
                var p0 = new Vector2(_v0.transform.position.x, _v0.transform.position.y);
                var p1 = new Vector2(_v1.transform.position.x, _v1.transform.position.y);
                _edge = new Edge(p0, p1);
            }
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

            Debug.DrawLine(new float3(_edge.v0, 0.0f), new float3(_edge.v1, 0.0f), _color);
        }
    }
}