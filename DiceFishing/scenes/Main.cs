using Godot;
using System.Linq;

public partial class Main : Node
{
    private const int DieCount = 5;

    private DieController[] _dieControllers;

    public override void _Ready()
    {
        _dieControllers = new DieController[DieCount];
        for (int i = 0; i < DieCount; i++)
        {
            _dieControllers[i] = GetNode<DieController>($"Dice/Die{i + 1}");
        }
    }

    public void OnRollPressed()
    {
        for (int i = 0; i < DieCount; i++)
        {
            _dieControllers[i].Roll(i + 1);
        }
    }

    public void OnDieRolled()
    {
        bool allDiceRolled =_dieControllers.All(d => !d.IsRolling);

        if (allDiceRolled)
        {
            GD.Print($"All Dice Rolled. Value: {_dieControllers.Sum(d => d.Value)}");
        }
    }
}
