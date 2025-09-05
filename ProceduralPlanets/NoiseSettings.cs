using Godot;


[Tool]
[GlobalClass]
public partial class NoiseSettings : Resource
{
    private int _numLayers = 1;

    [Export(PropertyHint.Range, "1, 8")]
    public int NumLayers
    {
        get => _numLayers;
        set
        {
            _numLayers = value;
            EmitChanged();
        }
    }

    private float _strength = 1.0f;

    [Export]
    public float Strength 
    { 
        get => _strength;
        set
        {
            _strength = value;
            EmitChanged();
        }
    }

    private float _baseRoughness = 1.0f;

    [Export]
    public float BaseRoughness
    {
        get => _baseRoughness;
        set
        {
            _baseRoughness = value;
            EmitChanged();
        }
    }

    private float _roughness = 2.0f;

    [Export]
    public float Roughness 
    { 
        get => _roughness; 
        set
        {
            _roughness = value;
            EmitChanged();
        }
    }

    private float _persistence = 1.0f;

    [Export]
    public float Persistence
    {
        get => _persistence;
        set
        {
            _persistence = value;
            EmitChanged();
        }
    }

    private Vector3 _center;

    [Export]
    public Vector3 Center 
    { 
        get => _center; 
        set
        {
            _center = value;
            EmitChanged();
        }
    }

    private float _minValue = 1.0f;

    [Export]
    public float MinValue
    {
        get => _minValue;
        set
        {
            _minValue = value;
            EmitChanged();
        }
    }

    public NoiseSettings() : this(Vector3.Zero) { }

    public NoiseSettings(Vector3 center)
    {
        this.Center = center;
    }

}
