using UnityEngine;

namespace Geo2D
{
    [ExecuteInEditMode]
    public class DebugTriangleRect : MonoBehaviour
    {
        public DrawTriangle _tri;
        public DrawRect _rect;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (!_rect || !_tri) return;

            Color color = new Color(1.0f, 1.0f, 0.0f);

            if (Intersect.Test(_tri._tri, _rect._rect))
            {
                _rect._color = Color.green;
            }
            else
            {
                _rect._color = Color.red;
            }
        }
    }
}