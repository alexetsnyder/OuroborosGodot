using Godot;

[Tool]
[GlobalClass]
public partial class NoiseLayer : Resource
{
    private bool _enabled = true;

    [Export]
    public bool Enabled
    {
        get => _enabled;
        set
        {
            _enabled = value;
            EmitChanged();
        }
    }

    private bool _useFirstLayerAsMask = false;

    [Export]
    public bool UseFirstLayerAsMask
    {
        get => _useFirstLayerAsMask;
        set
        {
            _useFirstLayerAsMask = value;
            EmitChanged();
        }
    }

    private NoiseSettings _noiseSettings;

    [Export]
    public NoiseSettings NoiseSettings
    {
        get => _noiseSettings;
        set
        {
            if (_noiseSettings != null)
            {
                _noiseSettings.Changed -= OnNoiseSettingChanged;
            }
            _noiseSettings = value;
            if (_noiseSettings != null)
            {
                _noiseSettings.Changed += OnNoiseSettingChanged;
            }
        }
    }

    public NoiseLayer() 
    { 

    }

    private void OnNoiseSettingChanged()
    {
        EmitChanged();
    }
}
