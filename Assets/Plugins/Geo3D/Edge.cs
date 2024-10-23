using System.Runtime.CompilerServices;
using UnityEngine;

namespace Geo3D
{
    public class Edge
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Edge(Vector3 v0, Vector3 v1)
        {
            _v0 = v0;
            _v1 = v1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3 CalcDirection()
        {
            return (_v1 - _v0).normalized;
        }

        public Vector3 Axis
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return _v1 - _v0; }
        }

        public Vector3 Centre
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return _v0 + Axis * 0.5f; }
        }

        public Vector3 _v0;
        public Vector3 _v1;
    }

}