using Godot;
using System.Collections.Generic;
using System.Linq;


public partial class Main : Node2D
{
    [Export]
    public int RollTimeInSeconds { get; set; } = 2;

    private const int DieCount = 5;

    private Die[] _dice;

    private List<Dice.DiceType> _diceData;

    private FishingRegress _fishingRegress;

    private int _regressNumber = 0;

    public override void _Ready()
    {
        _fishingRegress = GetNode<FishingRegress>("FishingUI/FishingRegress");

        _dice = new Die[DieCount];
        for (int i = 0; i < DieCount; i++)
        {
            _dice[i] = GetNode<Die>($"FishingUI/Dice/Die{i + 1}");
        }

        _diceData = new List<Dice.DiceType>();
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
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
                    collider.IsSelected = true;
                    collider.ShowOutline();
                    _diceData.Add((Dice.DiceType)collider.Value);
                }
            }
        }
    }

    private Die RaycastFromMousePosition()
    {
        var spaceState = GetWorld2D().DirectSpaceState;
        var query = new PhysicsPointQueryParameters2D();
        query.Position = GetGlobalMousePosition();
        query.CollideWithAreas = true;
        query.CollisionMask = 1;
        var results = spaceState.IntersectPoint(query);

        foreach (var result in results)
        {
            Die collider = result["collider"].As<Die>();
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
        if (_fishingRegress.Value > 0 && _dice.All(d => !d.IsRolling))
        {
            for (int i = 0; i < DieCount; i++)
            {
                var die = _dice[i];
                if (!die.IsRolling && !die.IsSelected)
                {
                    _regressNumber++;
                    _dice[i].Roll(RollTimeInSeconds);
                }

            }

            //var tween = GetTree().CreateTween();
            //tween.TweenProperty(_fishingRegress, "value", _fishingRegress.Value - _regressNumber, 0.8);
            _fishingRegress.Regress(_regressNumber);
            _regressNumber = 0;
        }
    }

    public void OnClearPressed()
    {
        _diceData.Clear();
        foreach (var die in _dice)
        {
            die.HideOutline();
            die.IsSelected = false;
        }
    }

    public void OnEvaluatePressed()
    {
        GD.Print(Dice.Evaluate(_diceData));
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
