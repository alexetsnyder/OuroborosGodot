using Godot;
using System.Linq;

public partial class Main : Node2D
{
    private const int DieCount = 5;

    private DieController[] _dice;

    public override void _Ready()
    {
        _dice = new DieController[DieCount];
        for (int i = 0; i < DieCount; i++)
        {
            _dice[i] = GetNode<DieController>($"Dice/Die{i + 1}");
        }
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseButton inputEventMouseButton && 
            inputEventMouseButton.ButtonIndex == MouseButton.Left)
        {
            if (@event.IsPressed())
            {
                var collider = RaycastFromMousePosition();
                if (collider != null)
                {
                    collider.ShowOutline();
                }
                else
                {
                    foreach (var die in _dice)
                    {
                        die.HideOutline();
                    }
                }
            }
        }
    }

    private DieController RaycastFromMousePosition()
    {
        var spaceState = GetWorld2D().DirectSpaceState;
        var query = new PhysicsPointQueryParameters2D();
        query.Position = GetGlobalMousePosition();
        query.CollideWithAreas = true;
        query.CollisionMask = 1;
        var results = spaceState.IntersectPoint(query);

        foreach (var result in results)
        {
            DieController collider = result["collider"].As<DieController>();
            if (collider != null)
            {
                GD.Print($"Collided with: {collider.Name}");
                return collider;

            }
        }

        return null;
    }

    public void OnRollPressed()
    {
        for (int i = 0; i < DieCount; i++)
        {
            _dice[i].Roll(i + 1);
        }
    }

    public void OnDieRolled()
    {
        bool allDiceRolled =_dice.All(d => !d.IsRolling);

        if (allDiceRolled)
        {
            GD.Print($"All Dice Rolled. Value: {_dice.Sum(d => d.Value)}");
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
