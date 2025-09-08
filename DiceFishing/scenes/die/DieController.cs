using Godot;

public partial class DieController : Node
{
    [Signal]
    public delegate void DieRolledEventHandler();

    private const int FrameCount = 5;

    public int Value
    {
        get => _currentFrame + 1;
    }

    public bool IsRolling { get; set; } = false;

    private AnimatedSprite2D _dieRoll;

    private Timer _rollTimer;

    private int _currentFrame = 0;

    public override void _Ready()
    {
        _dieRoll = GetNode<AnimatedSprite2D>("DieRoll");
        _rollTimer = GetNode<Timer>("RollTimer");
    }

    /// <summary>
    /// Starts the roll dice animation and a timer that runs for time seconds. 
    /// </summary>
    /// <param name="time">Time in seconds for how long the dice roll</param>
    public void Roll(int time)
    {
        IsRolling = true;
        _rollTimer.Start(time);
        _dieRoll.Play();
    }

    private void Stop()
    {
        IsRolling = false;
        _dieRoll.Stop();
        _currentFrame = GD.RandRange(0, FrameCount);
        _dieRoll.Frame = _currentFrame;
    }

    public void OnRollTimerTimeout()
    {
        Stop();
        EmitSignal(SignalName.DieRolled);
    }
}
