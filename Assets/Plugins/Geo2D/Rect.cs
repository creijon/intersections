using System.Runtime.CompilerServices;
using UnityEngine;

namespace Geo2D
{
    // Axis-aligned rectangle in the XY plane.
    // Stored as a centre position and a pair of extents,
    // representing the half-width and half-height.
    public class Rect
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Rect(Vector2 centre, Vector2 extents)
        {
            _centre = centre;
            _extents = extents;
        }

        // By adding an unused bool to the constructor we initialise from a min and max value.
        // Validity not checked, but it doesn't matter since extents can be negative.
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Rect(Vector2 min, Vector2 max, bool minMax)
        {
            SetMinMax(min, max);
        }

        public Vector2 Min
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return _centre - _extents; }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set { SetMinMax(value, Max); }
        }

        public Vector2 Max
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return _centre + _extents; }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set { SetMinMax(Min, value); }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetMinMax(Vector2 min, Vector2 max)
        {
            _extents = (max - min) * 0.5f;
            _centre = min + _extents;
        }

        public Vector2 _centre;
        public Vector2 _extents;
    }
}
