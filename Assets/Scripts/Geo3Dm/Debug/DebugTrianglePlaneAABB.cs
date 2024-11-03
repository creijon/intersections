using Geo3Dm;
using UnityEngine;
using static Unity.Mathematics.math;

namespace Geo3Dm
{
    [ExecuteInEditMode]
    public class DebugTrianglePlaneAABB : MonoBehaviour
    {
        public DrawTriangle _tri;
        public DrawAABB _aabb;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (!_aabb || !_tri) return;

            var n = _tri._tri.Cross();
            var r = dot(_aabb._aabb.extents, abs(n));
            var s = dot(n, _aabb._aabb.centre - _tri._tri.v0);

            bool result = (abs(s) <= r);

            _aabb._color = (result) ? Color.green : Color.red;
        }
    }
}