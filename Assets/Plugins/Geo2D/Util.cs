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

    }
}
