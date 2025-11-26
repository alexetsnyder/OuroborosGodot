using AlienRun.scenes.math;
using Godot;
using System.Collections.Generic;
using System.Linq;

namespace AlienRun.scenes.lane
{
    public partial class Lane : Node2D
    {
        [Export]
        public float PerturbScale
        {
            get;
            set;
        } = 1.0f;

        private RandomNumberGenerator _randomNumberGenerator = new RandomNumberGenerator();

        private Line2D _lane;

        private List<DrawablePoint> _randomPoints;

        private List<Vector2> _convexHull;

        private List<Vector2> _innerPoints = [];

        private List<Vector2> _outerPoints = [];

        private List<Vector2> _curves = [];

        public override void _Ready()
        {
            _randomPoints = [.. GetRandomPoints(60, 1092, 60, 598, 10).Select(p => new DrawablePoint(p, new Color(0, 0, 0)))];

            _convexHull = ConvexHull.Jarvis([.. _randomPoints.Select(d => d.Point)]);

            AddMidpoint();

            CalculateCurves();

            _lane = GetNode<Line2D>("LaneLine");
            _lane.DefaultColor = new Color(1, 1, 1, 1);
            foreach (var point in _curves)
            {
                _lane.AddPoint(point);
            }
        }

        public override void _Draw()
        {
            base._Draw();
            foreach (var point in _randomPoints)
            {
                DrawCircle(point.Point, 6.0f, point.Color);
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

        private void AddMidpoint()
        {
            var center = Math.Mean(_convexHull);

            var index = _randomNumberGenerator.RandiRange(0, _convexHull.Count - 2);
            var midPoint = Math.Midpoint(_convexHull[index], _convexHull[index + 1]);

            midPoint = center + PerturbScale * (midPoint - center);

            _convexHull.Insert(index + 1, midPoint);
            _randomPoints.Add(new DrawablePoint(midPoint, new Color(1, 0, 0)));
        }

        private void CalculateCurves()
        {
            AddCustomPoints();

            var tValues = new List<float> { 0.0f, 0.1f, 0.2f, 0.3f, 0.4f, 0.5f, 0.6f, 0.7f, 0.8f, 0.9f, 1.0f };

            for (int i = 0; i < _convexHull.Count; i++)
            {
                if (i == 0)
                {
                    var last = _outerPoints.Count;
                    for (int j = 0; j < tValues.Count; j++)
                    {
                        var v = Bezier.Quadratic(_outerPoints[last - 1], _convexHull[i], _innerPoints[i], tValues[j]);
                        _curves.Add(v);
                    }
                }
                else
                {
                    for (int j = 0; j < tValues.Count; j++)
                    {
                        var v = Bezier.Quadratic(_outerPoints[i - 1], _convexHull[i], _innerPoints[i], tValues[j]);
                        _curves.Add(v);
                    }
                }
            }

            _curves.Add(_outerPoints[^1]);
        }

        private void AddCustomPoints()
        {
            for (int i = 0; i < _convexHull.Count; i++)
            {
                var randPerc = _randomNumberGenerator.RandfRange(0.1f, 0.4f);
                var innerPoint = CalculateCustomPoint(_convexHull[i], _convexHull[(i + 1) % _convexHull.Count], randPerc);

                randPerc = _randomNumberGenerator.RandfRange(0.6f, 0.9f);
                var outerPoint = CalculateCustomPoint(_convexHull[i], _convexHull[(i + 1) % _convexHull.Count], randPerc);

                _innerPoints.Add(innerPoint);
                _outerPoints.Add(outerPoint);
            }

            _randomPoints.AddRange([.. _innerPoints.Select(p => new DrawablePoint(p, new Color(0, 1, 0)))]);
            _randomPoints.AddRange([.. _outerPoints.Select(p => new DrawablePoint(p, new Color(0, 1, 0)))]);
        }

        private Vector2 CalculateCustomPoint(Vector2 point1, Vector2 point2, float percent)
        {
            var x = (1.0f - percent) * point1.X + percent * point2.X;
            var y = (1.0f - percent) * point1.Y + percent * point2.Y;
            return new Vector2(x, y);
        }
    }

}

