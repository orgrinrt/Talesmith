using System;
using GDMechanic.Wiring;

namespace Talesmith.Core.UI.Workspaces.Studia
{
    public class StudiaWorkspace : Workspace
    {
        public override void _Ready()
        {
            base._Ready();
            this.Wire();
        }
    
        public override ICycleableItem GetNextItem()
        {
            return App.Self.Workspaces.Atlas;
        }

        public override ICycleableItem GetPrevItem()
        {
            return App.Self.Workspaces.Home;
        }
    }
}