using Godot;

public partial class Die : Area2D
{
    [Signal]
    public delegate void DieRolledEventHandler();

    [Signal]
    public delegate void OnHoverOnEventHandler(Area2D emitter);

    [Signal]
    public delegate void OnHoverOffEventHandler(Area2D emitter);

    public bool _isSelected = false;

    public bool IsSelected 
    { 
        get => _isSelected; 
        set
        {
            _isSelected = value;
            if (_isSelected)
            {
                ShowOutline();
            }
            else
            {
                HideOutline();
            }
        }
    }

    private const int FrameCount = 5;

    public int Value
    {
        get => _currentFrame + 1;
    }

    public bool IsRolling { get; set; } = false;

    private AnimatedSprite2D _dieRoll;

    private Timer _rollTimer;

    private ColorRect _colorRect;

    private int _currentFrame = 0;

    public override void _Ready()
    {
        _dieRoll = GetNode<AnimatedSprite2D>("DieRoll");
        _rollTimer = GetNode<Timer>("RollTimer");
        _colorRect = GetNode<ColorRect>("DieOutline");
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

    private void ShowOutline()
    {
        _colorRect.Visible = true;
    }

    private void HideOutline()
    {
        _colorRect.Visible = false;
    }

    private void OnRollTimerTimeout()
    {
        Stop();
        EmitSignal(SignalName.DieRolled);
    }

    private void OnMouseEntered()
    {
        EmitSignal(SignalName.OnHoverOn, this);
    }

    private void OnMouseExited()
    {
        EmitSignal(SignalName.OnHoverOff, this);
    }
}
