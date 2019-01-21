using System.Collections.Generic;
using Godot;
using Talesmith.Core.UI.Workspaces.Aetas;
using Talesmith.Core.UI.Workspaces.Atlas;
using Talesmith.Core.UI.Workspaces.Home;
using Talesmith.Core.UI.Workspaces.Studia;
using Talesmith.Core.UI.Workspaces.WorldConfig;

namespace Talesmith.Core.UI.Workspaces
{
    public class WorkspaceController : Control
    {
        private Workspace _currWorkspace;
        public Workspace CurrWorkspace => _currWorkspace;

        public HomeWorkspace Home => GetHomeWorkspace();
        public StudiaWorkspace Studia => GetStudiaWorkspace();
        public AtlasWorkspace Atlas => GetAtlasWorkspace();
        public AetasWorkspace Aetas => GetAetasWorkspace();
        public ConfigWorkspace Config => GetConfigWorkspace();

        public int ExtraMarginLeft = 25;
        public int ExtraMarginRight = -25;
        public int ExtraMarginBottom = -25;
        public int ExtraMarginTop = 25;
        
        public override void _Ready()
        {
            MarginRight = -App.Self.Inspector.RectSize.x;
            MarginBottom = -App.Self.Dock.RectSize.y;
            MarginLeft = App.Self.Binder.RectSize.x;
            MarginTop = 40;
            
            App.Self.Connect(nameof(App.WorkspaceChangeInitiated), this, nameof(OnWorkspaceAboutToChange));
            
            ChangeWorkspace(Home);
        }
        
        public override void _Input(InputEvent @event)
        {
            base._Input(@event);

            if (@event.IsActionPressed("cycle_workspaces_prev"))
            {
                App.Self.EmitSignal(nameof(App.WorkspaceChangeInitiated), CurrWorkspace.PrevItem, WorkspaceChangeType.Cycle);
            }
            else if (@event.IsActionPressed("cycle_workspaces_next"))
            {
                App.Self.EmitSignal(nameof(App.WorkspaceChangeInitiated), CurrWorkspace.NextItem, WorkspaceChangeType.Cycle);
            }
        }
        
        public void OnWorkspaceAboutToChange(Workspace workspace, WorkspaceChangeType changeType)
        {
            ChangeWorkspace(workspace);
        }

        public void ChangeWorkspace(Workspace workspace)
        {
            HideWorkspaces();
            workspace.Show();
            App.Self.EmitSignal(nameof(App.WorkspaceChanged), workspace);
            _currWorkspace = workspace;
        }

        private void HideWorkspaces()
        {
            foreach (Node node in GetChildren())
            {
                if (node is Workspace page)
                {
                    page.Hide();
                }
            }
        }

        private Workspace[] GetWorkspaces()
        {
            List<Workspace> result = new List<Workspace>();
            
            foreach (Node node in GetChildren())
            {
                if (node is Workspace page)
                {
                    result.Add(page);
                }
            }

            return result.ToArray();
        }
        
        private HomeWorkspace GetHomeWorkspace()
        {
            return GetNode<HomeWorkspace>("./Home");
        }
        
        private StudiaWorkspace GetStudiaWorkspace()
        {
            return GetNode<StudiaWorkspace>("./Studia");
        }

        private AtlasWorkspace GetAtlasWorkspace()
        {
            return GetNode<AtlasWorkspace>("./Atlas");
        }

        private AetasWorkspace GetAetasWorkspace()
        {
            return GetNode<AetasWorkspace>("./Aetas");
        }

        private ConfigWorkspace GetConfigWorkspace()
        {
            return GetNode<ConfigWorkspace>("./Config");
        }
    }
}
