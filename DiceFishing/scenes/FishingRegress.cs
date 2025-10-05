using Godot;

public partial class FishingRegress : TextureProgressBar
{
    public void Reset()
    {
        var tween = CreateTween();
        tween.TweenProperty(this, "value", MaxValue, 0.8);
    }

	public void Regress(int regressNumber)
	{
        var tween = CreateTween();
        tween.TweenProperty(this, "value", Value - regressNumber, 0.8);
    }
}
