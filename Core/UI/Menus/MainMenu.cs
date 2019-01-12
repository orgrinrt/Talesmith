using Godot;

namespace Talesmith.Core.UI.Menus
{
    public class MainMenu : Container
    {
        private bool _showing = false;
        private Vector2 _showingPos = new Vector2(0, 40);
        private float _showingSpeed = 0.8f;
    
        public override void _Ready()
        {
            GetAppIcon().Connect("button_up", this, nameof(OnAppIconPressed));
        }

        private void OnAppIconPressed()
        {
            if (_showing)
            {
                GetTween().InterpolateProperty(
                    this,
                    "rect_position",
                    RectGlobalPosition,
                    _showingPos - new Vector2(0, RectSize.y),
                    _showingSpeed,
                    Tween.TransitionType.Cubic,
                    Tween.EaseType.Out
                );
                GetTween().Start();
                _showing = false;
            }
            else
            {
                GetTween().InterpolateProperty(
                    this,
                    "rect_position",
                    RectGlobalPosition,
                    _showingPos,
                    _showingSpeed,
                    Tween.TransitionType.Cubic,
                    Tween.EaseType.Out
                );
                GetTween().Start();
                _showing = true;
            }
        }

        private TextureButton GetAppIcon()
        {
            return GetNode<TextureButton>("../AppIcon");
        }

        private Tween GetTween()
        {
            return GetNode<Tween>("./Tween");
        }
    }
}
