using UnityEngine;
using Unity.Collections;
using System.IO;
using Geo3D;
using Unity.Mathematics;
using static Unity.Mathematics.math;

[RequireComponent(typeof(MeshFilter))]
public class BoxTest : MonoBehaviour
{
    class Box
    {
        public Box(float3 min, float3 max)
        {
            _aabb = new AABB(min, max, true);
            _triCount = 0;
        }

        public AABB _aabb;
        public uint _triCount;
    }

    Mesh _mesh;
    float3 _min;
    float3 _max;
    public bool _mySolution = false;
    public bool _comparison = false;
    public int _randomSeed = 100;
    public int _boxCount = 1000;
    Box[] _boxes;

    void WriteResults(string filePath)
    {
        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(filePath, true);

        // Now output the results to terminal.
        for (int i = 0; i < _boxCount; ++i)
        {
            writer.WriteLine(i + "\t" + _boxes[i]._triCount);
        }

        writer.Close();
    }

    float3 RandomVector()
    {
        return new float3(UnityEngine.Random.Range(_min.x, _max.x),
                          UnityEngine.Random.Range(_min.y, _max.y),
                          UnityEngine.Random.Range(_min.z, _max.z));
    }

    // Start is called before the first frame update
    void Start()
    {
        _mesh = GetComponent<MeshFilter>().mesh;
        NativeArray<float3> vertices;
        NativeArray<ushort> indices;

        using (Mesh.MeshDataArray dataArray = Mesh.AcquireReadOnlyMeshData(_mesh))
        {
            var data = dataArray[0];

            vertices = new NativeArray<float3>(_mesh.vertexCount, Allocator.Persistent);
            data.GetVertices(vertices.Reinterpret<Vector3>());

            indices = new NativeArray<ushort>((int)_mesh.GetIndexCount(0), Allocator.Persistent);
            data.GetIndices(indices, 0);
        }

        _min = vertices[0];
        _max = vertices[0];
        _boxes = new Box[_boxCount];

        foreach (var vert in vertices)
        {
            _min = min(vert, _min);
            _max = max(vert, _max);
        }

        // Create a series of random boxes.
        UnityEngine.Random.InitState(_randomSeed);
        for (int i = 0; i < _boxCount; ++i)
        {
            var a = RandomVector();
            var b = RandomVector();

            var minV = min(a, b);
            var maxV = max(a, b);

            // Pick one of the three axes to snap to the outer box.
            int clampAxis = UnityEngine.Random.Range(0, 3);

            if (clampAxis == 0)
            {
                minV.x = 0.0f;
                maxV.x = _max.x;
            }
            else if (clampAxis == 1)
            {
                minV.y = 0.0f;
                maxV.y = _max.y;
            }
            else
            {
                minV.z = 0.0f;
                maxV.z = _max.z;
            }

            _boxes[i] = new Box(minV, maxV);
        }

        var stopWatch = new System.Diagnostics.Stopwatch();
        stopWatch.Start();

        uint totalIntersections = 0;

        for (int i = 0; i < _boxCount; ++i)
        {
            // Test all the triangles against the boxes.
            // Store the results.
            for (int j = 0; j < indices.Length; j += 3)
            {
                var t0 = vertices[indices[j + 0]];
                var t1 = vertices[indices[j + 1]];
                var t2 = vertices[indices[j + 2]];
                var tri = new Triangle(t0, t1, t2);

                bool test1 = false;
                
                if (_mySolution)
                {
                    test1 = Intersect.Test(tri, _boxes[i]._aabb);
                }
                else
                {
                    test1 = Intersect.TestSS(tri, _boxes[i]._aabb);

                    if (_comparison)
                    {
                        bool test2 = Intersect.Test(tri, _boxes[i]._aabb);

                        if (test1 != test2)
                        {
                            Debug.Log("Mismatch on box " + i + ", triangle " + j);
                            Debug.Log("Base:" + test1 + " Mine:" + test2);

                            var c = _boxes[i]._aabb.centre;
                            var e = _boxes[i]._aabb.extents;
                            Debug.Log("Centre: [" + c.x + "," + c.y + "," + c.z + "]");
                            Debug.Log("Extents: [" + e.x + "," + e.y + "," + e.z + "]");
                            Debug.Log("t0: [" + t0.x + "," + t0.y + "," + t0.z + "]");
                            Debug.Log("t1: [" + t1.x + "," + t1.y + "," + t1.z + "]");
                            Debug.Log("t2: [" + t2.x + "," + t2.y + "," + t2.z + "]");
                        }
                    }
                }

                if (test1)
                {
                    _boxes[i]._triCount += 1;
                }
            }

            totalIntersections += _boxes[i]._triCount;
        }

        stopWatch.Stop();

        var ts = stopWatch.Elapsed;

        // Format and display the TimeSpan value.
        var elapsedTime = System.String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);

        Debug.Log("Tests: " + _boxCount * (indices.Length / 3) + " Intersections: " + totalIntersections + " Runtime: " + elapsedTime);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
