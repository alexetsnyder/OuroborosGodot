using Godot;

public partial class DieController : Node
{
    public int Value
    {
        get => _currentFrame + 1;
    }

    private AnimatedSprite2D _die;

    private const int FrameCount = 5;

    private int _currentFrame = 0;

    public override void _Ready()
    {
        _die = GetNode<AnimatedSprite2D>("DieRoll");
    }

    public void Roll()
    {
        _die.Play();
    }

    public void Stop()
    {
        _die.Stop();
        _currentFrame = GD.RandRange(0, FrameCount);
        _die.Frame = _currentFrame;
    }
}
