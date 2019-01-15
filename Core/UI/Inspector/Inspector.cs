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
            CallDeferred(nameof(InitConnections));
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
                    ToggleInspector(false); // note that the bool here is just to accomodate the InspectorToggled signal from ViewPrefs (we dont use it here)
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

        private void InitConnections()
        {
            App.Self.Connect(nameof(App.MainPageChanged), this, nameof(OpenPage));
            App.Self.Preferences.Connect(nameof(Preferences.InspectorToggled), this, nameof(ToggleInspector));
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
