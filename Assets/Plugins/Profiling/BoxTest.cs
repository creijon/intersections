using UnityEngine;
using Unity.Collections;
using System.IO;
using Geo3D;

[RequireComponent(typeof(MeshFilter))]
public class BoxTest : MonoBehaviour
{
    class Box
    {
        public Box(Vector3 min, Vector3 max)
        {
            _aabb = new AABB(min, max, true);
            _triangleCount = 0;
        }

        public AABB _aabb;
        public uint _triangleCount;
    }

    Mesh _mesh;
    Vector3 _min;
    Vector3 _max;
    bool _firstFrame = true;
    public bool _mySolution = false;
    public bool _comparison = false;
    public int _randomSeed = 100;
    public int _boxCount = 1000;
    public bool _myRoutine = true;
    Box[] _boxes;

    void WriteResults(string filePath)
    {
        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(filePath, true);

        // Now output the results to terminal.
        for (int i = 0; i < _boxCount; ++i)
        {
            writer.WriteLine(i + "\t" + _boxes[i]._triangleCount);
        }

        writer.Close();
    }

    Vector3 RandomVector()
    {
        return new Vector3(Random.Range(_min.x, _max.x), Random.Range(_min.y, _max.y), Random.Range(_min.z, _max.z));
    }

    // Start is called before the first frame update
    void Start()
    {
        _mesh = GetComponent<MeshFilter>().mesh;
        NativeArray<Vector3> vertices;
        NativeArray<ushort> indices;

        using (Mesh.MeshDataArray dataArray = Mesh.AcquireReadOnlyMeshData(_mesh))
        {
            var data = dataArray[0];

            vertices = new NativeArray<Vector3>(_mesh.vertexCount, Allocator.Persistent);
            data.GetVertices(vertices);

            indices = new NativeArray<ushort>((int)_mesh.GetIndexCount(0), Allocator.Persistent);
            data.GetIndices(indices, 0);
        }

        _min = vertices[0];
        _max = vertices[0];
        _firstFrame = true;
        _boxes = new Box[_boxCount];

        foreach (var vert in vertices)
        {
            _min = Vector3.Min(vert, _min);
            _max = Vector3.Max(vert, _max);
        }

        // Create a series of random boxes.
        Random.InitState(_randomSeed);
        for (int i = 0; i < _boxCount; ++i)
        {
            var a = RandomVector();
            var b = RandomVector();

            var min = Vector3.Min(a, b);
            var max = Vector3.Max(a, b);

            // Pick one of the three axes to snap to the outer box.
            int clampAxis = Random.Range(0, 3);

            if (clampAxis == 0)
            {
                min.x = 0.0f;
                max.x = _max.x;
            }
            else if (clampAxis == 1)
            {
                min.y = 0.0f;
                max.y = _max.y;
            }
            else
            {
                min.z = 0.0f;
                max.z = _max.z;
            }

            _boxes[i] = new Box(min, max);
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
                    test1 = Intersect.Test2(tri, _boxes[i]._aabb);
                }
                else
                {
                    test1 = Intersect.Test(tri, _boxes[i]._aabb);

                    if (_comparison)
                    {
                        bool test2 = Intersect.Test2(tri, _boxes[i]._aabb);

                        if (test1 != test2)
                        {
                            Debug.Log("Mismatch on box " + i + ", triangle " + j);
                            Debug.Log("Base:" + test1 + " Mine:" + test2);

                            var c = _boxes[i]._aabb._centre;
                            var e = _boxes[i]._aabb._extents;
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
                    _boxes[i]._triangleCount += 1;
                }
            }

            totalIntersections += _boxes[i]._triangleCount;
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
        if (_firstFrame)
        {
            _firstFrame = false;
            if (_myRoutine)
            {
                WriteResults("C:\\Users\\joncr\\triangleAABB_Mine.txt");
            }
            else
            {
                WriteResults("C:\\Users\\joncr\\triangleAABB_Canonical.txt");
            }
        }
    }
}
