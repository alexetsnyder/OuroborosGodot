using Godot;
using System;

namespace AlienRun.scenes.math
{
    public partial class DrawablePoint
    {
        public Vector2 Point
        {
            get;
            set;
        }

        public Color Color
        {
            get;
            set;
        }

        public DrawablePoint(Vector2 point, Color color)
        {
            Point = point;
            Color = color;
        }
    }
}