using System.Collections.Generic;
using Godot;
using Talesmith.Core.UI.Pages.Atlas;

namespace Talesmith.Core.UI.Pages
{
    public class WorkspaceController : Control
    {
        private Workspace _currWorkspace;
        public Workspace CurrWorkspace => _currWorkspace;
        
        public override void _Ready()
        {
            MarginRight = -App.Self.GetInspector().RectSize.x;
        }

        public void ChangeWorkspace(WorkspaceEnum workspace)
        {
            HideWorkspaces();

            Workspace workspaceToOpen = new Workspace();
            switch (workspace)
            {
                case WorkspaceEnum.Atlas:
                    workspaceToOpen = GetAtlasPage();
                    break;
                case WorkspaceEnum.WorldConfig:
                    workspaceToOpen = GetConfigPage();
                    break;
            }

            workspaceToOpen.Show();
            App.Self.EmitSignal(nameof(App.MainPageChanged), workspaceToOpen);
            _currWorkspace = workspaceToOpen;
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

        private AtlasWorkspace GetAtlasPage()
        {
            return GetNode<AtlasWorkspace>("./AtlasPage");
        }

        private ConfigWorkspace GetConfigPage()
        {
            return GetNode<ConfigWorkspace>("./ConfigPage");
        }
    }
}
