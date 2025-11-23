using Godot;

namespace AlienRun.scenes.lane
{
    public partial class Bezier
    {
        public static Vector2 Quadratic(Vector2 p0, Vector2 p1, Vector2 p2, float t)
        {
            Vector2 q0 = p0.Lerp(p1, t);
            Vector2 q1 = p1.Lerp(p2, t);
            Vector2 r = q0.Lerp(q1, t);

            return r;
        }
    }
}