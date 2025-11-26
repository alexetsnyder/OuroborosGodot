using Godot;
using System.Collections.Generic;
using System.Linq;

namespace AlienRun.scenes.math
{
    public partial class Math
    {
        public static Vector2 Mean(List<Vector2> points)
        {
            if (points.Count == 0)
            {
                return Vector2.Zero;
            }

            var xSum = points.Select(p => p.X).Sum();
            var ySum = points.Select(p => p.Y).Sum();

            return new Vector2(xSum / points.Count, ySum / points.Count);
        }

        public static Vector2 Midpoint(Vector2 p1,  Vector2 p2)
        {
            return (p1 + p2) / 2.0f;
        }
    }
}