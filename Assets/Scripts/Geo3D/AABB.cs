using System.Runtime.CompilerServices;
using UnityEngine;

namespace Geo3D
{
    public class AABB
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AABB(Vector3 centre, Vector3 extents)
        {
            this.centre = centre;
            this.extents = extents;
        }

        // By adding an unused bool to the constructor we initialise from a min and max value.
        // Validity not checked, but it doesn't matter since extents can be negative.
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AABB(Vector3 min, Vector3 max, bool minMax)
        {
            SetMinMax(min, max);
        }

        public float3 Size => extents * 2.0f;

        public Vector3 Min
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return centre - extents; }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set { SetMinMax(value, Max); }
        }

        public Vector3 Max
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return centre + extents; }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set { SetMinMax(Min, value); }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetMinMax(Vector3 min, Vector3 max)
        {
            extents = (max - min) * 0.5f;
            centre = min + extents;
        }

        public void Include(Vector3 p)
        {
            SetMinMax(Vector3.Min(p, Min), Vector3.Max(p, Max));
        }

        public Geo2D.Rect XY => new Geo2D.Rect(Util.XY(centre), Util.XY(extents));
        public Geo2D.Rect YZ => new Geo2D.Rect(Util.YZ(centre), Util.YZ(extents));
        public Geo2D.Rect ZX => new Geo2D.Rect(Util.ZX(centre), Util.ZX(extents));

        public Vector3 centre;
        public Vector3 extents;
    }
}
