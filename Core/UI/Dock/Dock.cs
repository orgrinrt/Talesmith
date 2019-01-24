using GDMechanic.Wiring;
using Godot;

namespace Talesmith.Core.UI.Dock
{
    public class Dock : Control
    {
        public override void _Ready()
        {
            this.Wire();

            CallDeferred(nameof(DeferredInit));
        }

        private void DeferredInit()
        {
            int margin = (int) App.Self.Preferences.AppearancePreferences.Get("ui_base_margin");
            MarginLeft = -App.Self.GetInspector().RectSize.x - margin;
            MarginRight = App.Self.GetBinder().RectSize.x + margin;
        }
    }
}