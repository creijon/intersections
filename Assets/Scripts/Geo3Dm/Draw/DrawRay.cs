using UnityEngine;

namespace Geo3Dm
{
    [ExecuteInEditMode]
    public class DrawRay : MonoBehaviour
    {
        public Color _color;
        public Ray _ray;

        void Reset()
        {
            _ray = new Ray(transform.position, transform.forward);
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
            Vector3 end = _ray.origin + _ray.dir * 3f;
            Vector3 tip = _ray.origin + _ray.dir * 3.2f;
            Debug.DrawLine(_ray.origin, end, _color);
            Debug.DrawLine(end - transform.right * 0.1f, end + transform.right * 0.1f, _color);
            Debug.DrawLine(end - transform.right * 0.1f, tip, _color);
            Debug.DrawLine(end + transform.right * 0.1f, tip, _color);
        }
    }
}