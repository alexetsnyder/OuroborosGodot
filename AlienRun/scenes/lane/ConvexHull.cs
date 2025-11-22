using Godot;
using System.Collections.Generic;

namespace AlienRun.scenes.lane
{
    public partial class ConvexHull
    {
        public static int Orientation(Vector2 p, Vector2 q, Vector2 r)
        {
            return (int)((q.Y - p.Y) * (r.X - q.X) - (q.X - p.X) * (r.Y - q.Y));
        }

        public static List<Vector2> Jarvis(List<Vector2> points)
        {
            if (points.Count < 3)
            {
                return [];
            }

            List<Vector2> convexHull = [];

            int leftmostIndex = 0;
            for (int i = 0; i < points.Count; i++)
            {
                if (points[i].X < points[leftmostIndex].X)
                {
                    leftmostIndex = i;
                }
            }

            int currentPointIndex = leftmostIndex, q;

            do
            {
                convexHull.Add(points[currentPointIndex]);

                q = (currentPointIndex + 1) % points.Count;

                for (int i = 0; i < points.Count; i++)
                {
                    if (Orientation(points[currentPointIndex], points[i], points[q]) < 0)
                    {
                        q = i;
                    }
                }

                currentPointIndex = q;
            } while (currentPointIndex != leftmostIndex);

            return convexHull;
        }
    }
}