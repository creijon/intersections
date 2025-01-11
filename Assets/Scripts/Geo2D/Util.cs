using System.Runtime.CompilerServices;
using UnityEngine;

namespace Geo2D
{
    class Util
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 XY(Vector2 v) => v;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 YZ(Vector2 v) => new Vector3(0.0f, v.x, v.y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 ZX(Vector2 v) => new Vector3(v.y, 0.0f, v.x);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Abs(Vector2 v) => new Vector2(Mathf.Abs(v.x), Mathf.Abs(v.y));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MaxCoefficient(Vector2 v)
        {
            return (v.x > v.y) ? v.x : v.y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float SignedTriArea(Vector2 a, Vector2 b, Vector2 c)
        {
            var ca = a - c;
            var cb = b - c;
            return ca.x * cb.y - ca.y * cb.x;
        }
    }
}
