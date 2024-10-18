using UnityEngine;

namespace Geo2D
{
    // A line segment connecting two points on the XY plane.
    public class Edge
    {
        public Edge(Vector2 v0, Vector2 v1)
        {
            _v0 = v0;
            _v1 = v1;
        }

        public Vector2 CalcDirection()
        {
            return Axis.normalized;
        }

        public Vector2 Axis
        {
            get { return _v1 - _v0; }
        }

        public Vector2 Centre
        {
            get { return _v0 + Axis * 0.5f; }
        }

        public Vector2 _v0;
        public Vector2 _v1;
    }
}
