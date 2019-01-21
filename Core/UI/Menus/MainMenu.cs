using Godot;
using Talesmith.Core.Systems;
using Talesmith.Core.UI.Workspaces;
using Talesmith.Core.UI.Workspaces.Aetas;
using Talesmith.Core.UI.Workspaces.Atlas;
using Talesmith.Core.UI.Workspaces.Home;
using Talesmith.Core.UI.Workspaces.Studia;
using Talesmith.Core.UI.Workspaces.WorldConfig;

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

            GetHomeButton().Connect("button_up", this, nameof(OnHomePressed));
            GetStudiaButton().Connect("button_up", this, nameof(OnStudiaPressed));
            GetAtlasButton().Connect("button_up", this, nameof(OnAtlasPressed));
            GetAetasButton().Connect("button_up", this, nameof(OnAetasPressed));
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

        private void UpdatePressedButton(Workspace workspace, WorkspaceChangeType changeType)
        {
            UnpressAllMenuItems();

            switch (workspace)
            {
                case HomeWorkspace _:
                    GetHomeButton().Pressed = changeType != WorkspaceChangeType.Click;
                    break;
                case StudiaWorkspace _:
                    GetStudiaButton().Pressed = changeType != WorkspaceChangeType.Click;
                    break;
                case AtlasWorkspace _:
                    GetAtlasButton().Pressed = changeType != WorkspaceChangeType.Click;
                    break;
                case AetasWorkspace _:
                    GetAetasButton().Pressed = changeType != WorkspaceChangeType.Click;
                    break;
                case ConfigWorkspace _:
                    GetConfigButton().Pressed = changeType != WorkspaceChangeType.Click;
                    break;
            }
        }
        
        
        private void OnHomePressed()
        {
            App.Self.EmitSignal(nameof(App.WorkspaceChangeInitiated), App.Self.WorkspaceController.Home, WorkspaceChangeType.Click);
        }
        
        private void OnStudiaPressed()
        {
            App.Self.EmitSignal(nameof(App.WorkspaceChangeInitiated), App.Self.WorkspaceController.Studia, WorkspaceChangeType.Click);
        }

        private void OnAtlasPressed()
        {
            App.Self.EmitSignal(nameof(App.WorkspaceChangeInitiated), App.Self.WorkspaceController.Atlas, WorkspaceChangeType.Click);
        }
        
        private void OnAetasPressed()
        {
            App.Self.EmitSignal(nameof(App.WorkspaceChangeInitiated), App.Self.WorkspaceController.Aetas, WorkspaceChangeType.Click);
        }

        private void OnConfigPressed()
        {
            App.Self.EmitSignal(nameof(App.WorkspaceChangeInitiated), App.Self.WorkspaceController.Config, WorkspaceChangeType.Click);
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
            App.Self.Connect(nameof(App.WorkspaceChangeInitiated), this, nameof(UpdatePressedButton));
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
        
        private Button GetHomeButton()
        {
            return GetNode<Button>("./Menu/Home");
        }
        
        private Button GetStudiaButton()
        {
            return GetNode<Button>("./Menu/Studia");
        }

        private Button GetAtlasButton()
        {
            return GetNode<Button>("./Menu/Atlas");
        }
        
        private Button GetAetasButton()
        {
            return GetNode<Button>("./Menu/Aetas");
        }

        private Button GetConfigButton()
        {
            return GetNode<Button>("./Menu/Config");
        }
    }
}
