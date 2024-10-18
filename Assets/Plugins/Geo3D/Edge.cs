using UnityEngine;

namespace Geo3D
{
    public class Edge
    {
        public Edge(Vector3 v0, Vector3 v1)
        {
            _v0 = v0;
            _v1 = v1;
        }

        public Vector3 CalcDirection()
        {
            return (_v1 - _v0).normalized;
        }

        public Vector3 Axis
        {
            get { return _v1 - _v0; }
        }

        public Vector3 Centre
        {
            get { return _v0 + Axis * 0.5f; }
        }

        public Vector3 _v0;
        public Vector3 _v1;
    }

}