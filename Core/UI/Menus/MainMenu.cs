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

            GetAtlasButton().Connect("button_up", this, nameof(OnAtlasPressed));
            GetConfigButton().Connect("button_up", this, nameof(OnConfigPressed));
            
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
            UnpressAllMenuItems();
            GetAtlasButton().Pressed = false;
            App.Self.GetPageController().ChangePage(PageEnum.Atlas);
        }

        private void OnConfigPressed()
        {
            UnpressAllMenuItems();
            GetConfigButton().Pressed = false;
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
                    _showingPos - new Vector2(0, RectSize.y + 5),
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

        private void UnpressAllMenuItems()
        {
            foreach (Node node in GetNode("./Menu").GetChildren())
            {
                if (node is Button button)
                {
                    button.Pressed = false;
                }
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

        private Button GetAtlasButton()
        {
            return GetNode<Button>("./Menu/Atlas");
        }

        private Button GetConfigButton()
        {
            return GetNode<Button>("./Menu/Config");
        }
    }
}
