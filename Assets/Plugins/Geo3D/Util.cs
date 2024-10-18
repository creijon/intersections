using UnityEngine;

namespace Geo3D
{
    class Util
    {
        public static Vector2 XY(Vector3 v)
        {
            return new Vector2(v.x, v.y);
        }

        public static Vector2 YZ(Vector3 v)
        {
            return new Vector2(v.y, v.z);
        }

        public static Vector2 ZX(Vector3 v)
        {
            return new Vector2(v.z, v.x);
        }

        // Component-wise multiplication of two vectors.
        // Formally known as the Hadamard or Schar product.
        public static Vector3 Multiply(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
        }

        public static Vector3 Abs(Vector3 v)
        {
            return new Vector3(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z));
        }
    }
}
