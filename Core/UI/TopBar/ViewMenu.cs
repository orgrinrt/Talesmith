using Godot;
using System;
using Talesmith.Core;
using Talesmith.Core.Utils;
using Array = Godot.Collections.Array;

public class ViewMenu : MenuButton
{
    public override void _Ready()
    {
        GetPopup().AddCheckItem("Inspector");
        GetPopup().AddCheckItem("Big Icons");

        Array actionlist = InputMap.GetActionList("inspector_toggle");
        InputEventKey key = (InputEventKey)actionlist[0];
        GetPopup().SetItemAccelerator(0, key.GetScancodeWithModifiers());

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
                    App.Self.Preferences.EmitSignal(nameof(Preferences.UseBigIconsToggled), isChecked);
                    App.Self.Preferences.ViewPreferences.Set("use_big_icons", isChecked);
                    break;
            }
        }
    }

    private void SetValues()
    {
        GetPopup().SetItemChecked(0, (bool) App.Self.Preferences.ViewPreferences.Get("show_inspector"));
        GetPopup().SetItemChecked(1, (bool) App.Self.Preferences.ViewPreferences.Get("use_big_icons"));
    }
}
