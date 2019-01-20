using System;

namespace Talesmith.Core.UI.Workspaces.WorldConfig
{
    public class ConfigWorkspace : Workspace
    {
    
        public override void _Ready()
        {
            _workspaceEnum = WorkspaceEnum.WorldConfig;
        }
    
        public override ICycleableItem GetNextItem()
        {
            throw new NotImplementedException();
        }

        public override ICycleableItem GetPrevItem()
        {
            throw new NotImplementedException();
        }
    }
}
