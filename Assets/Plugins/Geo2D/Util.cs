using UnityEngine;

namespace Geo2D
{
    class Util
    {
        public static Vector3 XY(Vector2 v)
        {
            return v;
        }

        public static Vector3 YZ(Vector2 v)
        {
            return new Vector3(0.0f, v.x, v.y);
        }

        public static Vector2 ZX(Vector2 v)
        {
            return new Vector3(v.y, 0.0f, v.x);
        }

        // Component-wise multiplication of two vectors.
        // Formally known as the Hadamard or Schar product.
        public static Vector2 Multiply(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x * b.x, a.y * b.y);
        }

        public static Vector2 Abs(Vector2 v)
        {
            return new Vector2(Mathf.Abs(v.x), Mathf.Abs(v.y));
        }
    }
}
