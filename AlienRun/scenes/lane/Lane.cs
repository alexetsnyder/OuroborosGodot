using AlienRun.scenes.lane;
using Godot;
using System.Collections.Generic;

public partial class Lane : Node2D
{
	private RandomNumberGenerator _randomNumberGenerator = new RandomNumberGenerator();

	private Line2D _lane;

    private List<Vector2> _randomPoints;

    private List<Vector2> _convexHull;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_randomPoints = GetRandomPoints(60, 1092, 60, 598, 10);

        _convexHull = ConvexHull.Jarvis(_randomPoints);

		_lane = GetNode<Line2D>("LaneLine");
		_lane.DefaultColor = new Color(0, 1, 0, 1);
		foreach (var point in _convexHull)
		{
			_lane.AddPoint(point);
		}

		if (_convexHull.Count > 0)
		{
			_lane.AddPoint(_convexHull[0]);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public override void _Draw()
    {
        base._Draw();
		foreach (var point in _randomPoints)
		{
			DrawCircle(point, 10.0f, new Color(0.0f, 0.0f, 0.0f));
		}
    }

	private List<Vector2> GetRandomPoints(int minWidth, int maxWidth, int minHeight, int maxHeight, int n)
	{
		List<Vector2> points = new List<Vector2>();

		for (int i = 0; i < n; i++)
		{
			var x = _randomNumberGenerator.RandfRange(minWidth, maxWidth);
			var y = _randomNumberGenerator.RandfRange(minHeight, maxHeight);

			points.Add(new Vector2(x, y));
		}

		return points;
	}
}
