using UnityEngine;

namespace Geo3D
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

            DebugDraw(_aabb.Min, _aabb.Max, _color, Vector3.zero);
        }

        public static void DebugDraw(Vector3 min, Vector3 max, Color color, Vector3 translation)
        {
            min = min + translation;
            max = max + translation;

            Vector3 v001 = new Vector3(max.x, min.y, min.z);
            Vector3 v010 = new Vector3(min.x, max.y, min.z);
            Vector3 v011 = new Vector3(max.x, max.y, min.z);
            Vector3 v100 = new Vector3(min.x, min.y, max.z);
            Vector3 v101 = new Vector3(max.x, min.y, max.z);
            Vector3 v110 = new Vector3(min.x, max.y, max.z);

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