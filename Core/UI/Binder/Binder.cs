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

namespace Talesmith.Core.UI.Binder
{
    public class Binder : Control
    {
        private bool _showing = true;
        private float _showingSpeed;
        
        public override void _Ready()
        {
            CallDeferred(nameof(DeferredInit));
        }

        private void OpenBinderContentTab(Workspace workspace, WorkspaceChangeType changeType)
        {
            HideBinderContentTabs();

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
                    break;
                case AetasWorkspace _:
                    GetAetasContent().Show();
                    break;
                case ConfigWorkspace _:
                    GetConfigContent().Show();
                    break;
            }
        }
        
        private void HideBinderContentTabs()
        {
            foreach (Node node in GetContentContainer().GetChildren())
            {
                if (node is BinderContentPage page)
                {
                    page.Hide();
                }
            }
        }

        private void ToggleBinder(bool visible)
        {
            int margin = (int) App.Self.Preferences.AppearancePreferences.Get("ui_base_margin");
            
            if (_showing)
            {
                GetTween().InterpolateProperty(
                    this,
                    "rect_position",
                    RectGlobalPosition,
                    new Vector2(-RectSize.x - 2, RectGlobalPosition.y),
                    _showingSpeed,
                    Tween.TransitionType.Cubic,
                    Tween.EaseType.Out);

                WorkspaceController workspace = App.Self.GetWorkspaceController();
                GetTween().InterpolateProperty(
                    workspace,
                    "margin_left",
                    workspace.MarginLeft,
                    0,
                    _showingSpeed,
                    Tween.TransitionType.Cubic,
                    Tween.EaseType.Out);
                
                Dock.Dock dock = App.Self.Dock;
                GetTween().InterpolateProperty(
                    dock,
                    "margin_left",
                    dock.MarginLeft,
                    margin,
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
                    new Vector2(0, RectGlobalPosition.y),
                    _showingSpeed,
                    Tween.TransitionType.Cubic,
                    Tween.EaseType.Out);
                
                WorkspaceController workspace = App.Self.GetWorkspaceController();
                GetTween().InterpolateProperty(
                    workspace,
                    "margin_left",
                    workspace.MarginLeft,
                    RectSize.x,
                    _showingSpeed,
                    Tween.TransitionType.Cubic,
                    Tween.EaseType.Out);
                
                Dock.Dock dock = App.Self.Dock;
                GetTween().InterpolateProperty(
                    dock,
                    "margin_left",
                    dock.MarginLeft,
                    RectSize.x + margin,
                    _showingSpeed,
                    Tween.TransitionType.Cubic,
                    Tween.EaseType.Out);
                
                GetTween().Start();
                _showing = true;
            }
        }
    
        private void DeferredInit()
        {
            App.Self.Connect(nameof(App.WorkspaceChangeInitiated), this, nameof(OpenBinderContentTab));
            App.Self.Preferences.Connect(nameof(Preferences.BinderToggled), this, nameof(ToggleBinder));
            _showingSpeed = (float) App.Self.Preferences.AppearancePreferences.Get("ui_animation_speed");
            _showing = !(bool) App.Self.Preferences.ViewPreferences.Get("show_binder");
            App.Self.Preferences.Connect(nameof(Preferences.AnimationSpeedChanged), this, nameof(OnAnimationSpeedChanged));
            
            ToggleBinder(_showing);
        }
        
        private void OnAnimationSpeedChanged(float newSpeed)
        {
            _showingSpeed = newSpeed;
        }

        private Control GetContentContainer()
        {
            return GetNode<Control>("./Content");
        }

        private Tween GetTween()
        {
            return GetNode<Tween>("./Tween");
        }
        
        private Home GetHomeContent()
        {
            return GetContentContainer().GetNode<Home>("./Home");
        }
        
        private Studia GetStudiaContent()
        {
            return GetContentContainer().GetNode<Studia>("./Studia");
        }
        
        private Atlas GetAtlasContent()
        {
            return GetContentContainer().GetNode<Atlas>("./Atlas");
        }

        private Aetas GetAetasContent()
        {
            return GetContentContainer().GetNode<Aetas>("./Aetas");
        }
        
        private Config GetConfigContent()
        {
            return GetContentContainer().GetNode<Config>("./Config");
        }
    }
}