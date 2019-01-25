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
            GetPopup().AddCheckItem("Show Dock");
            GetPopup().AddCheckItem("Big Icons");

            Array inspectorActionList = InputMap.GetActionList("inspector_toggle");
            InputEventKey inspectorKey = (InputEventKey)inspectorActionList[0];
            GetPopup().SetItemAccelerator(0, inspectorKey.GetScancodeWithModifiers());
            
            Array binderActionList = InputMap.GetActionList("binder_toggle");
            InputEventKey binderKey = (InputEventKey)binderActionList[0];
            GetPopup().SetItemAccelerator(1, binderKey.GetScancodeWithModifiers());
            
            Array dockActionList = InputMap.GetActionList("dock_toggle");
            InputEventKey dockKey = (InputEventKey)dockActionList[0];
            GetPopup().SetItemAccelerator(2, dockKey.GetScancodeWithModifiers());

            GetPopup().Connect("index_pressed", this, nameof(OnItemPressed));
        
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
                        App.Self.Preferences.ViewPreferences.Set("show_inspector", !isChecked);
                        App.Self.Preferences.EmitSignal(nameof(Preferences.InspectorToggled), !isChecked);
                        break;
                    case 1:
                        App.Self.Preferences.ViewPreferences.Set("show_binder", !isChecked);
                        App.Self.Preferences.EmitSignal(nameof(Preferences.BinderToggled), !isChecked);
                        break;
                    case 2:
                        App.Self.Preferences.ViewPreferences.Set("show_dock", !isChecked);
                        App.Self.Preferences.EmitSignal(nameof(Preferences.DockToggled), !isChecked);
                        break;
                    case 3:
                        App.Self.Preferences.ViewPreferences.Set("use_big_icons", !isChecked);
                        App.Self.Preferences.EmitSignal(nameof(Preferences.UseBigIconsToggled), !isChecked);
                        break;
                }
            }
        }

        private void SetValues()
        {
            GetPopup().SetItemChecked(0, (bool) App.Self.Preferences.ViewPreferences.Get("show_inspector"));
            GetPopup().SetItemChecked(1, (bool) App.Self.Preferences.ViewPreferences.Get("show_binder"));
            GetPopup().SetItemChecked(2, (bool) App.Self.Preferences.ViewPreferences.Get("show_dock"));
            GetPopup().SetItemChecked(3, (bool) App.Self.Preferences.ViewPreferences.Get("use_big_icons"));
        }
    }
}
