using UnityEngine;
using Unity.Collections;
using System.IO;
using Geo3Dm;
using Unity.Jobs;
using Unity.Mathematics;
using static Unity.Mathematics.math;
using Unity.Burst;

[RequireComponent(typeof(MeshFilter))]
public class BoxTest : MonoBehaviour
{
    [BurstCompile]
    public struct Job : IJob
    {
        public NativeArray<AABB> boxes;
        public NativeArray<uint> triCounts;
        public NativeArray<float3> vertices;
        public NativeArray<ushort> indices;
        public bool mySolution;

        public void Execute()
        {
            for (int i = 0; i < boxes.Length; ++i)
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

                    if (mySolution)
                    {
                        test1 = Intersect.Test(tri, boxes[i]);
                    }
                    else
                    {
                        test1 = Intersect.TestSS(tri, boxes[i]);
                    }

                    if (test1)
                    {
                        triCounts[i] += 1;
                    }
                }
            }
        }
    }

    Mesh _mesh;
    float3 _min;
    float3 _max;
    public bool _mySolution = false;
    public bool _comparison = false;
    public int _randomSeed = 100;
    public int _boxCount = 1000;

    Job _job;

    void WriteResults(string filePath)
    {
        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(filePath, true);

        // Now output the results to terminal.
        for (int i = 0; i < _job.triCounts.Length; ++i)
        {
            writer.WriteLine(i + "\t" + _job.triCounts[i]);
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
        _job = new Job();

        using (Mesh.MeshDataArray dataArray = Mesh.AcquireReadOnlyMeshData(_mesh))
        {
            var data = dataArray[0];

            _job.vertices = new NativeArray<float3>(_mesh.vertexCount, Allocator.Persistent);
            data.GetVertices(_job.vertices.Reinterpret<Vector3>());

            _job.indices = new NativeArray<ushort>((int)_mesh.GetIndexCount(0), Allocator.Persistent);
            data.GetIndices(_job.indices, 0);
        }

        _job.boxes = new NativeArray<AABB>(_boxCount, Allocator.Persistent);
        _job.triCounts = new NativeArray<uint>(_boxCount, Allocator.Persistent);
        _job.mySolution = _mySolution;

        _min = _job.vertices[0];
        _max = _job.vertices[0];

        foreach (var vert in _job.vertices)
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

            _job.boxes[i].SetMinMax(minV, maxV);
            _job.triCounts[i] = 0;
        }

        var stopWatch = new System.Diagnostics.Stopwatch();
        stopWatch.Start();

        uint totalIntersections = 0;


        JobHandle jobHandle;

        jobHandle = _job.Schedule();
        jobHandle.Complete();

        stopWatch.Stop();

        var ts = stopWatch.Elapsed;

        // Format and display the TimeSpan value.
        var elapsedTime = System.String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);

        Debug.Log("Tests: " + _boxCount * (_job.indices.Length / 3) + " Intersections: " + totalIntersections + " Runtime: " + elapsedTime);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
