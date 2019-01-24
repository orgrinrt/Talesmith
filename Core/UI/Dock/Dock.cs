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
            MarginLeft = -App.Self.GetInspector().RectSize.x - (int)App.Self.Preferences.AppearancePreferences.Get("ui_base_margin");
            MarginRight = App.Self.GetBinder().RectSize.x + (int)App.Self.Preferences.AppearancePreferences.Get("ui_base_margin");
        }
    }
}