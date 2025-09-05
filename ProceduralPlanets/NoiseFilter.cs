using Godot;


public partial class NoiseFilter
{
    private Noise _noise = new Noise();
    private NoiseSettings _noiseSettings;

    public NoiseFilter(NoiseSettings settings)
    {
        this._noiseSettings = settings;
    }

    public float Evaluate(Vector3 point)
    {
        var noiseValue = 0.0f;
        var frequency = _noiseSettings.BaseRoughness;
        var amplitude = 1.0f;

        for (var i = 0; i < _noiseSettings.NumLayers; i++)
        {
            float v = _noise.Evaluate(point * frequency + _noiseSettings.Center);
            noiseValue += (v + 1.0f) * 0.5f * amplitude;
            frequency *= _noiseSettings.Roughness;
            amplitude *= _noiseSettings.Persistence;
        }

        noiseValue = Mathf.Max(0.0f, noiseValue - _noiseSettings.MinValue);
        return noiseValue * _noiseSettings.Strength;
    }
}

