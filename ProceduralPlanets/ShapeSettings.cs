using Godot;
using Godot.Collections;

[Tool]
[GlobalClass]
public partial class ShapeSettings : Resource
{
    private float _planetRadius = 1.0f;

    [Export]
    public float PlanetRadius 
    {
        get => _planetRadius;
        set
        {
            _planetRadius = value;
            EmitChanged();
        }
    }

    private Array<NoiseLayer> _noiseLayers;

    [Export]
    public Array<NoiseLayer> NoiseLayers
    {
        get => _noiseLayers;
        set
        {
            _noiseLayers = value;
            //if (_noiseLayers != null)
            //{
            //    foreach (var noiseLayer  in _noiseLayers)
            //    {
            //        noiseLayer.Changed -= OnNoiseLayerChanged;
            //    }
            //}
            //_noiseLayers = value;
            //if (_noiseLayers != null)
            //{
            //    foreach (var noiseLayer in _noiseLayers)
            //    {
            //        noiseLayer.Changed += OnNoiseLayerChanged;
            //    }
            //}
        }
    }

    //private NoiseSettings _noiseSettings;

    //[Export]
    //public NoiseSettings NoiseSettings 
    //{
    //    get => _noiseSettings; 
    //    set
    //    {
    //        if (_noiseSettings != null)
    //        {
    //            _noiseSettings.Changed -= OnNoiseSettingsChanged;
    //        }
    //        _noiseSettings = value;
    //        if (_noiseSettings != null)
    //        {
    //            _noiseSettings.Changed += OnNoiseSettingsChanged;
    //        }
    //    }
    //}


    public ShapeSettings() : this(1.0f) { }

    public ShapeSettings(float planetRadius)
    {
        PlanetRadius = planetRadius;
    }

    //private void OnNoiseSettingsChanged()
    //{
    //    EmitChanged();
    //}

    private void OnNoiseLayerChanged()
    {
        EmitChanged();
    }
}
