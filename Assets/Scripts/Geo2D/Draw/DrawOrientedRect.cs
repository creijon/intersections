using UnityEngine;

namespace Geo2D
{
    [ExecuteInEditMode]
    public class DrawOrientedRect : MonoBehaviour
    {
        public Color _color;
        public OrientedRect _orientedRect;

        void Reset()
        {
            Vector2 axis = Geo3D.Util.XY(transform.right).normalized;
            _orientedRect = new OrientedRect(transform.position, axis, transform.localScale * 0.5f);
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