using Godot;

namespace AlienRun.scenes.alien
{
    public partial class Alien : Node
    {
        [Export]
        public int Speed
        {
            get;
            set;
        } = 5;
    }
}