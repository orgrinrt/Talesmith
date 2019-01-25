using System;
using GDMechanic.Wiring;

namespace Talesmith.Core.UI.Workspaces.Aetas
{
    public class AetasWorkspace : Workspace
    {
        public override void _Ready()
        {
            base._Ready();
            this.Wire();
        }

        public override ICycleableItem GetNextItem()
        {
            return App.Self.Workspaces.Config;
        }

        public override ICycleableItem GetPrevItem()
        {
            return App.Self.Workspaces.Atlas;
        }
    }
}