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

        private Sprite2D _alienSprite;

        public override void _Ready()
        {
            _alienSprite = GetNode<Sprite2D>("AlienSprite");
        }

        public void Show()
        {
            _alienSprite.Show();
        }

        public void Hide()
        {
            _alienSprite.Hide();
        }
    }
}