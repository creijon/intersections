using System.Runtime.CompilerServices;
using Unity.Mathematics;
using static Unity.Mathematics.math;

namespace Geo3D
{
    public struct AABB
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AABB(float3 centre, float3 extents)
        {
            this.centre = centre;
            this.extents = extents;
        }

        // By adding an unused bool to the constructor we initialise from a min and max value.
        // Validity not checked, but it doesn't matter since extents can be negative.
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AABB(float3 min, float3 max, bool minMax)
        {
            extents = (max - min) * 0.5f;
            centre = min + extents;
        }

        public float3 Min
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return centre - extents; }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set { SetMinMax(value, Max); }
        }

        public float3 Max
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return centre + extents; }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set { SetMinMax(Min, value); }
        }

        public float3 Size
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return extents * 2.0f; }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetMinMax(float3 min, float3 max)
        {
            extents = (max - min) * 0.5f;
            centre = min + extents;
        }

        public void Include(float3 p)
        {
            SetMinMax(min(p, Min), max(p, Max));
        }

        public Geo2D.Rect XY => new Geo2D.Rect(centre.xy, extents.xy);
        public Geo2D.Rect YZ => new Geo2D.Rect(centre.yz, extents.yz);
        public Geo2D.Rect ZX => new Geo2D.Rect(centre.zx, extents.zx);

        public float3 centre;
        public float3 extents;
    }
}
