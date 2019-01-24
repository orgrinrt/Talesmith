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
        
        public override void _Ready()
        {
            MarginRight = -App.Self.Inspector.RectSize.x;
            MarginBottom = App.Self.Dock.MarginTop;;
            MarginLeft = App.Self.Binder.RectSize.x;
            MarginTop = 40;
            
            App.Self.Connect(nameof(App.WorkspaceChangeInitiated), this, nameof(OnWorkspaceAboutToChange));

            CallDeferred(nameof(DeferredInit));
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

        private void DeferredInit()
        {
            CallDeferred(nameof(DeferredDeferredInit)); // :-D
        }

        private void DeferredDeferredInit() // :-D
        {
            App.Self.EmitSignal(nameof(App.WorkspaceChangeInitiated), GetHomeWorkspace(), WorkspaceChangeType.Cycle);
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
