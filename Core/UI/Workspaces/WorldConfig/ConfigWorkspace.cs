using System;

namespace Talesmith.Core.UI.Workspaces.WorldConfig
{
    public class ConfigWorkspace : Workspace
    {
    
        public override void _Ready()
        {
            base._Ready();
        }
    
        public override ICycleableItem GetNextItem()
        {
            return App.Self.Workspaces.Home;
        }

        public override ICycleableItem GetPrevItem()
        {
            return App.Self.Workspaces.Aetas;
        }
    }
}
