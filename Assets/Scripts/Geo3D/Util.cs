using System.Runtime.CompilerServices;
using UnityEngine;

namespace Geo3D
{
    class Util
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 XY(Vector3 v) => new Vector2(v.x, v.y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 YZ(Vector3 v) => new Vector2(v.y, v.z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)] 
        public static Vector2 ZX(Vector3 v) => new Vector2(v.z, v.x);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Reciprocal(Vector3 v) => new Vector3(1.0f / v.x, 1.0f / v.y, 1.0f / v.z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Abs(Vector3 v) => new Vector3(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MinCoefficient(Vector3 v)
        {
            return (v.x < v.z) ? ((v.x < v.y) ? v.x : v.y) : ((v.y < v.z) ? v.y : v.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MaxCoefficient(Vector3 v)
        {
            return (v.x > v.z) ? ((v.x > v.y) ? v.x : v.y) : ((v.y > v.z) ? v.y : v.z);
        }
    }
}
