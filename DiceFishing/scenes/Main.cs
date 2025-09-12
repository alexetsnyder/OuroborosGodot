using Godot;
using System.Linq;

public partial class Main : Node2D
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

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseButton inputEventMouseButton && 
            inputEventMouseButton.ButtonIndex == MouseButton.Left)
        {
            if (@event.IsPressed())
            {
                RaycastFromMousePosition();
            }
            else
            {
                GD.Print("Left Click Released!");
            }
        }
    }

    private void RaycastFromMousePosition()
    {
        var spaceState = GetWorld2D().DirectSpaceState;
        var query = new PhysicsPointQueryParameters2D();
        query.Position = GetGlobalMousePosition();
        query.CollideWithAreas = true;
        query.CollisionMask = 1;
        var result = spaceState.IntersectPoint(query);
        if (result.Count > 0)
        {
            GD.Print($"Clicked on: {result[0]["collider"]}");
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

    public void OnDieHoverOn(Area2D emitter)
    {
        emitter.Scale = new Vector2(1.05f, 1.05f);
    }

    public void OnDieHoverOff(Area2D emitter)
    {
        emitter.Scale = new Vector2(1f, 1f);
    }
}
