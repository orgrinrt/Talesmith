using Godot;
using Talesmith.Core.Systems;
using Array = Godot.Collections.Array;

namespace Talesmith.Core.UI.TopBar
{
    public class ViewMenu : MenuButton
    {
        public override void _Ready()
        {
            GetPopup().AddCheckItem("Show Inspector");
            GetPopup().AddCheckItem("Show Binder");
            GetPopup().AddCheckItem("Big Icons");

            Array actionlist = InputMap.GetActionList("inspector_toggle");
            InputEventKey key = (InputEventKey)actionlist[0];
            GetPopup().SetItemAccelerator(0, key.GetScancodeWithModifiers());
            
            Array actionlist2 = InputMap.GetActionList("binder_toggle");
            InputEventKey key2 = (InputEventKey)actionlist2[0];
            GetPopup().SetItemAccelerator(1, key2.GetScancodeWithModifiers());

            GetPopup().Connect("index_pressed", this, nameof(OnItemPressed)); // returns the INDEX of the item pressed
        
            CallDeferred(nameof(SetValues));
        }

        private void OnItemPressed(int index)
        {
            if (GetPopup().IsItemCheckable(index))
            {
                bool isChecked = GetPopup().IsItemChecked(index);
            
                GetPopup().SetItemChecked(index, !isChecked);
            
                switch (index)
                {
                    case 0:
                        App.Self.Preferences.EmitSignal(nameof(Preferences.InspectorToggled), isChecked);
                        App.Self.Preferences.ViewPreferences.Set("show_inspector", isChecked);
                        break;
                    case 1:
                        App.Self.Preferences.EmitSignal(nameof(Preferences.BinderToggled), isChecked);
                        App.Self.Preferences.ViewPreferences.Set("show_binder", isChecked);
                        break;
                    case 2:
                        App.Self.Preferences.EmitSignal(nameof(Preferences.UseBigIconsToggled), isChecked);
                        App.Self.Preferences.ViewPreferences.Set("use_big_icons", isChecked);
                        break;
                }
            }
        }

        private void SetValues()
        {
            GetPopup().SetItemChecked(0, (bool) App.Self.Preferences.ViewPreferences.Get("show_inspector"));
            GetPopup().SetItemChecked(1, (bool) App.Self.Preferences.ViewPreferences.Get("show_binder"));
            GetPopup().SetItemChecked(2, (bool) App.Self.Preferences.ViewPreferences.Get("use_big_icons"));
        }
    }
}
