using System.Runtime.CompilerServices;
using Unity.Mathematics;

namespace Geo2Dm
{
    // Oriented rectangle in the XY plane.
    // Stored as a centre position, X-axis and a pair of extents,
    // representing the half-width and half-height.
    public struct OrientedRect
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OrientedRect(float2 centre, float2 extents, float2 axis)
        {
            this.centre = centre;
            this.extents = extents;
            this.axis = axis;
        }

        // By adding an unused bool to the constructor we initialise from a min and max value.
        // Validity not checked, but it doesn't matter since extents can be negative.
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OrientedRect(float2 min, float2 max, float2 axis, bool minMax)
        {
            extents = (max - min) * 0.5f;
            centre = min + extents;
            this.axis = axis;
        }

        public float2 Min
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return centre - extents; }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set { SetMinMax(value, Max); }
        }

        public float2 Max
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return centre + extents; }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set { SetMinMax(Min, value); }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetMinMax(float2 min, float2 max)
        {
            extents = (max - min) * 0.5f;
            centre = min + extents;
        }

        public float2 centre;
        public float2 extents;
        public float2 axis;
    }
}
