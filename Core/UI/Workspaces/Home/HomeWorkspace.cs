using System;
using GDMechanic.Wiring;

namespace Talesmith.Core.UI.Workspaces.Home
{
    public class HomeWorkspace : Workspace
    {
        public override void _Ready()
        {
            this.Wire();
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