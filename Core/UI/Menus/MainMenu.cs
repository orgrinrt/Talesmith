using Godot;
using Talesmith.Core.UI.Pages;

namespace Talesmith.Core.UI.Menus
{
    public class MainMenu : Container
    {
        private bool _showing = false;
        private Vector2 _showingPos = new Vector2(0, 40);
        private float _showingSpeed = 0.5f;
    
        public override void _Ready()
        {
            GetAppIcon().Connect("button_up", this, nameof(OnAppIconPressed));

            GetNode("./Menu/Atlas").Connect("button_up", this, nameof(OnAtlasPressed));
            GetNode("./Menu/Config").Connect("button_up", this, nameof(OnConfigPressed));
            
            CallDeferred(nameof(DeferredInit));
        }

        public override void _Input(InputEvent @event)
        {
            base._Input(@event);

            if (@event.IsActionPressed("main_menu_toggle") || @event.IsActionPressed("ui_home"))
            {
                OnAppIconPressed();
            }
        }

        private void OnAtlasPressed()
        {
            App.Self.GetPageController().ChangePage(PageEnum.Atlas);
        }

        private void OnConfigPressed()
        {
            App.Self.GetPageController().ChangePage(PageEnum.WorldConfig);
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

        private void DeferredInit()
        {
            _showingSpeed = (float) App.Self.Preferences.AppearancePreferences.Get("ui_animation_speed");
            App.Self.Preferences.Connect(nameof(Preferences.AnimationSpeedChanged), this, nameof(OnAnimationSpeedChanged));
        }

        private void OnAnimationSpeedChanged(float newSpeed)
        {
            _showingSpeed = newSpeed;
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
