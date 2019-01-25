using System;
using GDMechanic.Wiring;

namespace Talesmith.Core.UI.Workspaces.Home
{
    public class HomeWorkspace : Workspace
    {
        public override void _Ready()
        {
            base._Ready();
            this.Wire();
        }
    
        public override ICycleableItem GetNextItem()
        {
            return App.Self.Workspaces.Studia;
        }

        public override ICycleableItem GetPrevItem()
        {
            return App.Self.Workspaces.Config;
        }
    }
}