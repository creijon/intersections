using UnityEngine;
using Unity.Mathematics;

namespace Geo2D
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
            if (!_point || !_tri) return;

            Color color = new Color(1.0f, 1.0f, 0.0f);
            float3 position = _point.transform.position;

            if (Intersect.Test(position.xy, _tri._tri))
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