using System.Collections.Generic;
using Godot;
using Talesmith.Core.Systems;
using Talesmith.Core.UI.Workspaces;
using Talesmith.Core.UI.Workspaces.Atlas;
using Talesmith.Core.UI.Workspaces.WorldConfig;
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

        public void OpenPage(Workspace workspace, WorkspaceChangeType changeType)
        {
            HidePages();
            switch (workspace.GetType().ToString())
            {
                case nameof(AtlasWorkspace):
                    GetAtlasPage().Show();
                    break;
                case nameof(ConfigWorkspace):
                    // TODO
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

                WorkspaceController workspace = App.Self.GetWorkspaceController();
                GetTween().InterpolateProperty(
                    workspace,
                    "margin_right",
                    workspace.MarginRight,
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
                    new Vector2(GetTree().GetRoot().Size.x - RectSize.x, RectGlobalPosition.y),
                    _showingSpeed,
                    Tween.TransitionType.Cubic,
                    Tween.EaseType.Out);
                
                WorkspaceController workspace = App.Self.GetWorkspaceController();
                GetTween().InterpolateProperty(
                    workspace,
                    "margin_right",
                    workspace.MarginRight,
                    -RectSize.x,
                    _showingSpeed,
                    Tween.TransitionType.Cubic,
                    Tween.EaseType.Out);
                
                GetTween().Start();
                _showing = true;
            }
        }

        private void DeferredInit()
        {
            App.Self.Connect(nameof(App.WorkspaceAboutToChangeTo), this, nameof(OpenPage));
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

        private InspectorDragger GetDragger()
        {
            return GetNode<InspectorDragger>("./Dragger");
        }

        private Tween GetTween()
        {
            return GetNode<Tween>("./Tween");
        }
    }
}
