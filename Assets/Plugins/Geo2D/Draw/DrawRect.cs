using UnityEngine;

namespace Geo2D
{
    [ExecuteInEditMode]
    public class DrawRect : MonoBehaviour
    {
        public Color _color;
        public Rect _rect;

        void Reset()
        {
            _rect = new Rect(transform.position, transform.localScale * 0.5f);
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

            DebugDraw(_rect.Min, _rect.Max, _color, Vector3.zero);
        }

        public static void DebugDraw(Vector2 min, Vector2 max, Color color, Vector2 translation)
        {
            min = min + translation;
            max = max + translation;

            Vector2 v01 = new Vector2(max.x, min.y);
            Vector2 v10 = new Vector2(min.x, max.y);

            Debug.DrawLine(min, v01, color);
            Debug.DrawLine(v01, max, color);
            Debug.DrawLine(max, v10, color);
            Debug.DrawLine(v10, min, color);
        }
    }
}