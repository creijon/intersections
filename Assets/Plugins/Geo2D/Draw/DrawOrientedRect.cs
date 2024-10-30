using UnityEngine;
using Unity.Mathematics;
using static Unity.Mathematics.math;

namespace Geo2D
{
    [ExecuteInEditMode]
    public class DrawOrientedRect : MonoBehaviour
    {
        public Color _color;
        public OrientedRect _orientedRect;

        void Reset()
        {
            float3 position = transform.position;
            float3 right = transform.right;
            float2 axis = normalize(right.xy);
            float3 scale = transform.localScale * 0.5f;
            _orientedRect = new OrientedRect(position.xy, scale.xy, axis);
        }

        // Start is called before the first frame update
        void Start()
        {
            Reset();
        }

        // Update is called once per frame
        void Update()
        {
            Reset();

            DebugDraw(_orientedRect, _color);
        }

        public static void DebugDraw(OrientedRect rect, Color color)
        {
            Vector2 up = new Vector2(rect.axis.y, -rect.axis.x);
        }
    }
}