using Godot;
using Godot.Collections;

public class TerrainFace
{
    private int _resolution { get; set; }

    private Vector3 _localUp {  get; set; }

    private Vector3 _axisA { get; set; }

    private Vector3 _axisB { get; set; }

    private ShapeGenerator _shapeGenerator;

    public TerrainFace(ShapeGenerator shapeGenerator, int resolution, Vector3 localUp)
    {
        _resolution = resolution;
        _localUp = localUp;
        _shapeGenerator = shapeGenerator;

        _axisA = new Vector3(localUp.Y, localUp.Z, localUp.X);
        _axisB = _axisA.Cross(_localUp); 
    }

    public void GenerateMesh(MeshInstance3D meshInstance3D)
    {
        Array surfaceArray = [];
        surfaceArray.Resize((int)Mesh.ArrayType.Max);

        int size = _resolution * _resolution;
        Vector3[] verticies = new Vector3[size];
        Vector2[] uvs = new Vector2[size];
        Vector3[] normals = new Vector3[size];
        int[] indicies = new int[(_resolution - 1) * (_resolution - 1) * 6];

        int triangleIndex = 0;
        for (int y = 0; y < _resolution; y++)
        {
            for (int x = 0; x < _resolution; x++)
            {
                int index = x + y * _resolution;
                Vector2 percent = new Vector2(x, y) / (_resolution - 1);
                Vector3 pointOnAUnitCube = _localUp + (percent.X - 0.5f) * 2 * _axisA + (percent.Y - 0.5f) * 2 * _axisB;
                Vector3 pointOnAUnitSphere = pointOnAUnitCube.Normalized();
                verticies[index] = _shapeGenerator.CalculatePointOnPlanet(pointOnAUnitSphere);
                //verticies[index] = pointOnAUnitCube;
                uvs[index] = percent;
                normals[index] = pointOnAUnitSphere;

                if (x != _resolution - 1 && y != _resolution - 1)
                {
                    indicies[triangleIndex] = index;
                    indicies[triangleIndex + 1] = index + _resolution + 1;
                    indicies[triangleIndex + 2] = index + _resolution;

                    indicies[triangleIndex + 3] = index;
                    indicies[triangleIndex + 4] = index + 1;
                    indicies[triangleIndex + 5] = index + _resolution + 1;

                    triangleIndex += 6;
                }
            }
        }

        surfaceArray[(int)Mesh.ArrayType.Vertex] = verticies;
        surfaceArray[(int)Mesh.ArrayType.TexUV] = uvs;
        surfaceArray[(int)Mesh.ArrayType.Normal] = normals;
        surfaceArray[(int)Mesh.ArrayType.Index] = indicies;

        if (meshInstance3D != null && meshInstance3D.Mesh is ArrayMesh arrayMesh)
        {
            arrayMesh.AddSurfaceFromArrays(Mesh.PrimitiveType.Triangles, surfaceArray);
        }
    }

}
