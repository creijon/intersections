using UnityEngine;

namespace Geo2Dm
{
    [ExecuteInEditMode]
    public class DebugRectRect : MonoBehaviour
    {
        public DrawRect _rect1;
        public DrawRect _rect2;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (!_rect1 || !_rect2) return;

            Color color = new Color(1.0f, 1.0f, 0.0f);

            if (Intersect.Test(_rect1._rect, _rect2._rect))
            {
                _rect1._color = Color.green;
            }
            else
            {
                _rect1._color = Color.red;
            }
        }
    }
}