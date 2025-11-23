using AlienRun.scenes.lane;
using Godot;
using System.Collections.Generic;

public partial class Lane : Node2D
{
	private RandomNumberGenerator _randomNumberGenerator = new RandomNumberGenerator();

	private Line2D _lane;

    private List<Vector2> _randomPoints;

    private List<Vector2> _convexHull;

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

		AddCustomPoints();
	}

    public override void _Draw()
    {
        base._Draw();
		foreach (var point in _randomPoints)
		{
			DrawCircle(point, 6.0f, new Color(0.0f, 0.0f, 0.0f));
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

	private void AddCustomPoints()
	{
		List<Vector2> innerPoints = [];
		List<Vector2> outerPoints = [];
		for (int i = 0; i < _convexHull.Count; i++)
		{
			var randPerc = _randomNumberGenerator.RandfRange(0.1f, 0.4f); // 0.1f + _randomNumberGenerator.Randf() * (0.4f - 0.1f);
			GD.Print($"Inner Perc: {randPerc}");
			var innerPoint = CalculateCustomPoint(_convexHull[i], _convexHull[(i + 1) % _convexHull.Count], randPerc);

			randPerc = _randomNumberGenerator.RandfRange(0.6f, 0.9f); // 0.6f + _randomNumberGenerator. .Randf() * (0.9f - 0.6f);
            GD.Print($"Outer Perc: {randPerc}");
            var outerPoint = CalculateCustomPoint(_convexHull[i], _convexHull[(i + 1) % _convexHull.Count], randPerc);

			innerPoints.Add(innerPoint);
			outerPoints.Add(outerPoint);
        }

		_randomPoints.AddRange(innerPoints);
		_randomPoints.AddRange(outerPoints);
	}

	private Vector2 CalculateCustomPoint(Vector2 point1, Vector2 point2, float percent)
	{
		var x = (1.0f - percent) * point1.X + percent * point2.X;
		var y = (1.0f - percent) * point1.Y + percent * point2.Y;
		return new Vector2(x, y);
	}
}
