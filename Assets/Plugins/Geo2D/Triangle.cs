using System.Runtime.CompilerServices;
using UnityEngine;

namespace Geo2D
{
    // A tri lying on the XY plane.
    public class Triangle
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Triangle(Vector2 v0, Vector2 v1, Vector2 v2)
        {
            _v0 = v0;
            _v1 = v1;
            _v2 = v2;
        }

        public Edge Edge0 => new Edge(_v0, _v1);
        public Edge Edge1 => new Edge(_v1, _v2);
        public Edge Edge2 => new Edge(_v2, _v0);

        public Vector2 CalcBarycentric(Vector2 p)
        {
            Vector2 v1 = _v1 - _v0;
            Vector2 v0 = _v2 - _v0;
            Vector2 v2 = p - _v0;

            float dot00 = Vector2.Dot(v0, v0);
            float dot01 = Vector2.Dot(v0, v1);
            float dot02 = Vector2.Dot(v0, v2);
            float dot11 = Vector2.Dot(v1, v1);
            float dot12 = Vector2.Dot(v1, v2);

            float invDenom = 1.0f / (dot00 * dot11 - dot01 * dot01);
            float u = (dot11 * dot02 - dot01 * dot12) * invDenom;
            float v = (dot00 * dot12 - dot01 * dot02) * invDenom;

            return new Vector2(u, v);
        }

        public Vector2 _v0;
        public Vector2 _v1;
        public Vector2 _v2;
    }

}