using System.Collections.Generic;
using Godot;
using Talesmith.Core.UI.Pages;
using Environment = System.Environment;
using Path = System.IO.Path;

namespace Talesmith.Core.UI.Inspector
{
    public class Inspector : Control
    {
        private bool _showing = true;
        private float _showingSpeed = 0.5f;
        
        public override void _Ready()
        {
            CallDeferred(nameof(DeferredInit));
        }

        public void OpenPage(Page page)
        {
            switch (page.PageEnum)
            {
                case PageEnum.Atlas:
                    HidePages();
                    GetAtlasPage().Show();
                    break;
                case PageEnum.WorldConfig:
                    
                    break;
            }
        }

        private void HidePages()
        {
            foreach (Node node in GetChildren())
            {
                if (node is InspectorPage page)
                {
                    page.Hide();
                }
            }
        }

        public void ToggleInspector(bool visible)
        {
            if (_showing)
            {
                GetTween().InterpolateProperty(
                    this,
                    "rect_position",
                    RectGlobalPosition,
                    new Vector2(GetTree().GetRoot().Size.x + 1, RectGlobalPosition.y),
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
                    new Vector2(GetTree().GetRoot().Size.x - RectSize.x, RectGlobalPosition.y),
                    _showingSpeed,
                    Tween.TransitionType.Cubic,
                    Tween.EaseType.Out);
                GetTween().Start();
                _showing = true;
            }
        }

        private void DeferredInit()
        {
            App.Self.Connect(nameof(App.MainPageChanged), this, nameof(OpenPage));
            App.Self.Preferences.Connect(nameof(Preferences.InspectorToggled), this, nameof(ToggleInspector));
            _showingSpeed = (float) App.Self.Preferences.AppearancePreferences.Get("ui_animation_speed");
            App.Self.Preferences.Connect(nameof(Preferences.AnimationSpeedChanged), this, nameof(OnAnimationSpeedChanged));
        }

        private void OnAnimationSpeedChanged(float newSpeed)
        {
            _showingSpeed = newSpeed;
        }

        private InspectorPage GetAtlasPage()
        {
            return GetNode<InspectorPage>("./Atlas");
        }

        private Dragger GetDragger()
        {
            return GetNode<Dragger>("./Dragger");
        }

        private Tween GetTween()
        {
            return GetNode<Tween>("./Tween");
        }
    }
}
