using Godot;

public partial class ShapeGenerator
{
    private ShapeSettings _settings { get; set; }
    
    private NoiseFilter[] _noiseFilters { get; set; }

    public ShapeGenerator(ShapeSettings settings)
    {
        this._settings = settings;
        this._noiseFilters = new NoiseFilter[settings.NoiseLayers.Count];
        //this._noiseFilter = new NoiseFilter(settings.NoiseSettings);
        for (var i = 0; i < _noiseFilters.Length; i++)
        {
            _noiseFilters[i] = new NoiseFilter(settings.NoiseLayers[i].NoiseSettings);
        }
    }

    public Vector3 CalculatePointOnPlanet(Vector3 pointOneUnitSphere)
    {
        var firstLayerValue = 0.0f;
        var elevation = 0.0f;

        if (_noiseFilters.Length > 0)
        {
            firstLayerValue = _noiseFilters[0].Evaluate(pointOneUnitSphere);
            if (_settings.NoiseLayers[0].Enabled)
            {
                elevation = firstLayerValue;
            }
        }

        for (var i = 1; i < _noiseFilters.Length; i++)
        {
            if (_settings.NoiseLayers[i].Enabled)
            {
                float mask = _settings.NoiseLayers[i].UseFirstLayerAsMask ? firstLayerValue : 1.0f;
                elevation += _noiseFilters[i].Evaluate(pointOneUnitSphere) * mask;
            }
        }

        return pointOneUnitSphere * _settings.PlanetRadius * (1 + elevation);
    }
}
