using GDMechanic.Wiring;
using Godot;
using Talesmith.Core.Systems;
using Talesmith.Core.UI.Workspaces;

namespace Talesmith.Core.UI.Dock
{
    public class Dock : Control
    {
        private bool _showing = true;
        private float _showingSpeed = 0.5f;
        
        public override void _Ready()
        {
            this.Wire();

            CallDeferred(nameof(DeferredInit));
        }
        
        private void ToggleDock(bool visible)
        {
            int margin = (int) App.Self.Preferences.AppearancePreferences.Get("ui_base_margin");
            
            if (_showing)
            {
                GetTween().InterpolateProperty(
                    this,
                    "rect_position",
                    RectGlobalPosition,
                    new Vector2(RectGlobalPosition.x, GetTree().GetRoot().GetViewport().Size.y + 2),
                    _showingSpeed,
                    Tween.TransitionType.Cubic,
                    Tween.EaseType.Out);

                WorkspaceController workspace = App.Self.GetWorkspaceController();
                GetTween().InterpolateProperty(
                    workspace,
                    "margin_bottom",
                    workspace.MarginBottom,
                    0,
                    _showingSpeed,
                    Tween.TransitionType.Cubic,
                    Tween.EaseType.Out);
                
                GetTween().Start();
                _showing = false;
            }
            else
            {
                GetTween().InterpolateProperty(
                    this,
                    "rect_position",
                    RectGlobalPosition,
                    new Vector2(RectGlobalPosition.x, GetTree().GetRoot().GetViewport().Size.y - RectSize.y),
                    _showingSpeed,
                    Tween.TransitionType.Cubic,
                    Tween.EaseType.Out);
                
                WorkspaceController workspace = App.Self.GetWorkspaceController();
                GetTween().InterpolateProperty(
                    workspace,
                    "margin_bottom",
                    workspace.MarginBottom,
                    -RectSize.y,
                    _showingSpeed,
                    Tween.TransitionType.Cubic,
                    Tween.EaseType.Out);
                
                GetTween().Start();
                _showing = true;
            }
        }

        private void DeferredInit()
        {
            int margin = (int) App.Self.Preferences.AppearancePreferences.Get("ui_base_margin");
            MarginLeft = -App.Self.GetInspector().RectSize.x - margin;
            MarginRight = App.Self.GetBinder().RectSize.x + margin;
            _showingSpeed = (float) App.Self.Preferences.AppearancePreferences.Get("ui_animation_speed");
            _showing = !(bool) App.Self.Preferences.ViewPreferences.Get("show_dock");
            
            App.Self.Preferences.Connect(nameof(Preferences.DockToggled), this, nameof(ToggleDock));
            App.Self.Preferences.Connect(nameof(Preferences.AnimationSpeedChanged), this, nameof(OnAnimationSpeedChanged));
            
            ToggleDock(_showing);
        }
        
        private void OnAnimationSpeedChanged(float newSpeed)
        {
            _showingSpeed = newSpeed;
        }

        private Tween GetTween()
        {
            return GetNode<Tween>("./Tween");
        }
    }
}