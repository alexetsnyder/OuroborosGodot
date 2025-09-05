using Godot;

[Tool]
[GlobalClass]
public partial class ColorSettings : Resource
{
    [Export]
    public Color PlanetColor 
    {
        get => _planetColor; 
        set
        {
            _planetColor = value;
            EmitChanged();
        }
    }

    private Color _planetColor;

    public ColorSettings() : this(Colors.Black) { }

    public ColorSettings(Color planetColor)
    {
        PlanetColor = planetColor;
    }

}
