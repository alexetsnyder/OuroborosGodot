using Godot;

public partial class Main : Node
{
    private DieController _dieController;

    private Timer _rollTimer;

    public override void _Ready()
    {
        _dieController = GetNode<DieController>("Die");
        _rollTimer = GetNode<Timer>("RollTimer");
    }

    public void OnRollPressed()
    {
        _dieController.Roll();
        _rollTimer.Start();
    }

    public void OnRollTimerTimeout()
    {
        _dieController.Stop();
    }
}
