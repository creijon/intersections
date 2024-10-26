using System.Runtime.CompilerServices;
using UnityEngine;

namespace Geo3D
{
    public class AABB
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AABB(Vector3 centre, Vector3 extents)
        {
            _centre = centre;
            _extents = extents;
        }

        // By adding an unused bool to the constructor we initialise from a min and max value.
        // Validity not checked, but it doesn't matter since extents can be negative.
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AABB(Vector3 min, Vector3 max, bool minMax)
        {
            SetMinMax(min, max);
        }

        public Vector3 Min
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return _centre - _extents; }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set { SetMinMax(value, Max); }
        }

        public Vector3 Max
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return _centre + _extents; }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set { SetMinMax(Min, value); }
        }

        public Vector3 Size
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return _extents * 2.0f; }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetMinMax(Vector3 min, Vector3 max)
        {
            _extents = (max - min) * 0.5f;
            _centre = min + _extents;
        }

        public void Include(Vector3 p)
        {
            SetMinMax(Vector3.Min(p, Min), Vector3.Max(p, Max));
        }

        public Geo2D.Rect XY => new Geo2D.Rect(Util.XY(_centre), Util.XY(_extents));
        public Geo2D.Rect YZ => new Geo2D.Rect(Util.YZ(_centre), Util.YZ(_extents));
        public Geo2D.Rect ZX => new Geo2D.Rect(Util.ZX(_centre), Util.ZX(_extents));

        public Vector3 _centre;
        public Vector3 _extents;
    }
}
