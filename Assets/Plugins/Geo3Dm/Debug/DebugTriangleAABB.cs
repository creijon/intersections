using UnityEngine;

namespace Geo3Dm
{
    [ExecuteInEditMode]
    public class DebugTriangleAABB : MonoBehaviour
    {
        public DrawTriangle _tri;
        public DrawAABB _aabb;
        public enum Version
        { 
            SAT,
            Siedel,
            Mine
        }

        public Version _version;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (!_aabb || !_tri) return;

            bool result = false;

            if (Intersect.Test(_tri._tri.CalcBounds(), _aabb._aabb))
            {
                if (_version == Version.Siedel)
                {
                    result = Intersect.TestSS(_tri._tri, _aabb._aabb);
                }
                else if (_version == Version.SAT)
                {
                    result = Intersect.TestSAT(_tri._tri, _aabb._aabb);
                }
                else
                {
                    result = Intersect.TestNoBB(_tri._tri, _aabb._aabb);
                }
            }

            _aabb._color = (result) ? Color.green : Color.red;
        }
    }
}