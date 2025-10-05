using Godot;

public partial class FishingRegress : TextureProgressBar
{
	public void Regress(int regressNumber)
	{
        var tween = CreateTween();
        tween.TweenProperty(this, "value", Value - regressNumber, 0.8);
    }
}
