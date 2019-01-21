using System.Collections.Generic;
using Godot;
using Talesmith.Core.Systems;
using Talesmith.Core.UI.Binder.BinderContentPages;
using Talesmith.Core.UI.Workspaces;
using Talesmith.Core.UI.Workspaces.Aetas;
using Talesmith.Core.UI.Workspaces.Atlas;
using Talesmith.Core.UI.Workspaces.Home;
using Talesmith.Core.UI.Workspaces.Studia;
using Talesmith.Core.UI.Workspaces.WorldConfig;
using Talesmith.Core.Utils;
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

        public void OpenInspectorTabContent(Workspace workspace, WorkspaceChangeType changeType)
        {
            HideInspectorTabContents();

            switch (workspace)
            {
                case HomeWorkspace _:
                    GetHomeContent().Show();
                    break;
                case StudiaWorkspace _:
                    GetStudiaContent().Show();
                    break;
                case AtlasWorkspace _:
                    GetAtlasContent().Show();
                    foreach (Node node in GetDisabledTabs().GetChildren())
                    {
                        if (node.Name == "Layers")
                        {
                            node.ReParent(GetTabContainer());
                        }
                    }
                    break;
                case AetasWorkspace _:
                    GetAetasContent().Show();
                    break;
                case ConfigWorkspace _:
                    GetConfigContent().Show();
                    break;
            }
        }

        private void HideInspectorTabContents()
        {
            foreach (Node node in GetInspectorContent().GetChildren())
            {
                if (node is InspectorContentPage page)
                {
                    page.Hide();
                }
            }

            foreach (Node node in GetTabContainer().GetChildren())
            {
                if (node.Name != "Inspector")
                {
                    node.ReParent(GetDisabledTabs());
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
            App.Self.Connect(nameof(App.WorkspaceChangeInitiated), this, nameof(OpenInspectorTabContent));
            App.Self.Preferences.Connect(nameof(Preferences.InspectorToggled), this, nameof(ToggleInspector));
            _showingSpeed = (float) App.Self.Preferences.AppearancePreferences.Get("ui_animation_speed");
            App.Self.Preferences.Connect(nameof(Preferences.AnimationSpeedChanged), this, nameof(OnAnimationSpeedChanged));
        }

        private void OnAnimationSpeedChanged(float newSpeed)
        {
            _showingSpeed = newSpeed;
        }
        
        private InspectorContentPages.Home GetHomeContent()
        {
            return GetNode<InspectorContentPages.Home>("./Tabs/Inspector/Home");
        }
        
        private InspectorContentPages.Studia GetStudiaContent()
        {
            return GetNode<InspectorContentPages.Studia>("./Tabs/Inspector/Studia");
        }

        private InspectorContentPages.Atlas GetAtlasContent()
        {
            return GetNode<InspectorContentPages.Atlas>("./Tabs/Inspector/Atlas");
        }
        
        private InspectorContentPages.Aetas GetAetasContent()
        {
            return GetNode<InspectorContentPages.Aetas>("./Tabs/Inspector/Aetas");
        }
        
        private InspectorContentPages.Config GetConfigContent()
        {
            return GetNode<InspectorContentPages.Config>("./Tabs/Inspector/Config");
        }

        private InspectorDragger GetDragger()
        {
            return GetNode<InspectorDragger>("./Dragger");
        }

        private TabContainer GetTabContainer()
        {
            return GetNode<TabContainer>("./Tabs");
        }

        private ScrollContainer GetInspectorContent()
        {
            return GetTabContainer().GetNode<ScrollContainer>("./Inspector");
        }

        private Control GetDisabledTabs()
        {
            return GetNode<Control>("./DisabledTabs");
        }

        private Tween GetTween()
        {
            return GetNode<Tween>("./Tween");
        }
    }
}
