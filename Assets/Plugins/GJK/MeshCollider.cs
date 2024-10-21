using UnityEngine;

namespace GJK
{
    [RequireComponent(typeof(MeshFilter))]

    public class MeshCollider : MonoBehaviour
    {
        private Mesh _mesh;

        protected override void Awake()
        {
            base.Awake();

            var meshFilter = GetComponent<MeshFilter>();
            _mesh = meshFilter?.mesh;
        }

        public Vector3 FindFurthestPoint(Vector3 direction)
        {
            var dir = transform.InverseTransformDirection(direction);

            var furthestPoint = Vector3.zero;
            var maxDistance = float.MinValue;

            foreach (var vertex in _mesh.vertices)
            {
                var distance = Vector3.Dot(vertex, dir);

                if (distance > maxDistance)
                {
                    maxDistance = distance;
                    furthestPoint = vertex;
                }
            }

            return transform.TransformPoint(furthestPoint);
        }
    }

}