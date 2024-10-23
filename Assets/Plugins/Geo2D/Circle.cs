using System.Runtime.CompilerServices;
using UnityEngine;

namespace Geo2D
{
    public class Circle
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Circle(Vector2 centre, float radius)
        {
            _centre = centre;
            _radius = radius;
        }

        public Vector2 _centre;
        public float _radius;
    }
}
