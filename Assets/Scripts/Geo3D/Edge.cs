using System.Runtime.CompilerServices;
using UnityEngine;

namespace Geo3D
{
    public class Edge
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Edge(Vector3 v0, Vector3 v1)
        {
            this.v0 = v0;
            this.v1 = v1;
        }

        public Vector3 Axis => v1 - v0;
        public Vector3 Centre => v0 + Axis * 0.5f;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3 CalcDirection()
        {
            return Axis.normalized;
        }

        public Vector3 v0;
        public Vector3 v1;
    }
}