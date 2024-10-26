using System.Runtime.CompilerServices;
using UnityEngine;

namespace Geo2D
{
    public class Circle
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Circle(Vector2 centre, float radius)
        {
            this.centre = centre;
            this.radius = radius;
        }

        public Vector2 centre;
        public float radius;
    }
}
