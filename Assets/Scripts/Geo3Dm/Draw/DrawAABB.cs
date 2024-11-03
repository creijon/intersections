using UnityEngine;

namespace Geo3Dm
{
    [ExecuteInEditMode]
    public class DrawAABB : MonoBehaviour
    {
        public Color _color;
        public AABB _aabb;

        void Reset()
        {
            _aabb = new AABB(transform.position, transform.localScale);
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

            DebugDraw(_aabb.Min, _aabb.Max, _color, Matrix4x4.identity);
        }

        public static void DebugDraw(Vector3 min, Vector3 max, Color color, Matrix4x4 transform)
        {
            Vector3 v001 = new Vector3(max.x, min.y, min.z);
            Vector3 v010 = new Vector3(min.x, max.y, min.z);
            Vector3 v011 = new Vector3(max.x, max.y, min.z);
            Vector3 v100 = new Vector3(min.x, min.y, max.z);
            Vector3 v101 = new Vector3(max.x, min.y, max.z);
            Vector3 v110 = new Vector3(min.x, max.y, max.z);

            min = transform.MultiplyPoint(min);
            v001 = transform.MultiplyPoint(v001);
            v010 = transform.MultiplyPoint(v010);
            v011 = transform.MultiplyPoint(v011);
            v100 = transform.MultiplyPoint(v100);
            v101 = transform.MultiplyPoint(v101);
            v110 = transform.MultiplyPoint(v110);
            max = transform.MultiplyPoint(max);

            Debug.DrawLine(min,  v001, color);
            Debug.DrawLine(v001, v011, color);
            Debug.DrawLine(v011, v010, color);
            Debug.DrawLine(v010, min,  color);

            Debug.DrawLine(v100, v101, color);
            Debug.DrawLine(v101, max,  color);
            Debug.DrawLine(max,  v110, color);
            Debug.DrawLine(v110, v100, color);

            Debug.DrawLine(min,  v100, color);
            Debug.DrawLine(v001, v101, color);
            Debug.DrawLine(v010, v110, color);
            Debug.DrawLine(v011, max,  color);
        }
    }
}