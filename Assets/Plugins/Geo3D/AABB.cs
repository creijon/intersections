using UnityEngine;

namespace Geo3D
{
    public class AABB
    {
        public AABB(Vector3 centre, Vector3 extents)
        {
            _centre = centre;
            _extents = extents;
        }

        // By adding an unused bool to the constructor we initialise from a min and max value.
        // Validity not checked, but it doesn't matter since extents can be negative.
        public AABB(Vector3 min, Vector3 max, bool minMax)
        {
            SetMinMax(min, max);
        }

        public Vector3 Min
        {
            get { return _centre - _extents; }
            set { SetMinMax(value, Max); }
        }

        public Vector3 Max
        {
            get { return _centre + _extents; }
            set { SetMinMax(Min, value); }
        }

        public void SetMinMax(Vector3 min, Vector3 max)
        {
            _extents = (max - min) * 0.5f;
            _centre = min + _extents;
        }

        public Vector3 _centre;
        public Vector3 _extents;
    }
}
