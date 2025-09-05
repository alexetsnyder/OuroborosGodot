using Godot;

[Tool]
public partial class Planet : Node3D
{
    [Export(PropertyHint.Range, "2, 256")]
    public int Resolution
    {
        get => _resolution;
        set
        {
            _resolution = value;
            GeneratePlanet();
        }

    }

    [Export]
    public ShapeSettings ShapeSettings 
    {
        get => _shapeSettings;
        set
        {
            if (_shapeSettings != null)
            {
                _shapeSettings.Changed -= OnShapeSettingsChanged;
            }
            _shapeSettings = value;
            if (_shapeSettings != null)
            {
                _shapeSettings.Changed += OnShapeSettingsChanged;
            }
        }
    }

    [Export]
    public ColorSettings ColorSettings 
    {
        get => _colorSettings;
        set
        {
            if (_colorSettings != null)
            {
                _colorSettings.Changed -= OnColorSettingsChanged;
            }
            _colorSettings = value;
            if (_colorSettings != null)
            {
                _colorSettings.Changed += OnColorSettingsChanged;
            }
        }
    }

    private ShapeSettings _shapeSettings;

    private ColorSettings _colorSettings;

    private int _resolution = 10;

    private MeshInstance3D _meshInstance3d;

    private TerrainFace[] _terrainFaces;

    private ShapeGenerator _shapeGenerator;

    private const int _faceCount = 6;

    private void Initialize()
    {
        _shapeGenerator = new ShapeGenerator(ShapeSettings);

        if (_meshInstance3d == null)
        {
            _meshInstance3d = GetNode<MeshInstance3D>("MeshInstance3D");
        }

        if (_meshInstance3d.Mesh is ArrayMesh arrayMesh)
        {
            arrayMesh.ClearSurfaces();
        }

        _terrainFaces = new TerrainFace[_faceCount];

        Vector3[] directions = { Vector3.Up, Vector3.Down, Vector3.Left, Vector3.Right, Vector3.Forward, Vector3.Back };

        for (int i = 0; i < _faceCount; i++)
        {
            _terrainFaces[i] = new TerrainFace(_shapeGenerator, Resolution, directions[i]);
            
        }
    }

    public void GeneratePlanet()
    {
        Initialize();
        GenerateMeshs();
        GenerateColors();
    }

    private void GenerateMeshs()
    {
        foreach (var face in _terrainFaces)
        {
            face.GenerateMesh(_meshInstance3d);
        }
    }

    private void GenerateColors()
    {
        var material = new StandardMaterial3D
        {
            AlbedoColor = ColorSettings.PlanetColor
        };
        _meshInstance3d.MaterialOverride = material;
    }

    private void OnShapeSettingsChanged()
    {
        Initialize();
        GenerateMeshs();
    }

    private void OnColorSettingsChanged()
    {
        GenerateColors();
    }
}
