using Godot;
using System;
using Talesmith.Core;

public class ViewMenu : MenuButton
{
    public override void _Ready()
    {
        GetPopup().AddCheckItem("Inspector");
        GetPopup().AddCheckItem("Big Icons");

        GetPopup().Connect("index_pressed", this, nameof(OnItemPressed)); // returns the INDEX of the item pressed
        
        CallDeferred(nameof(SetValues));
    }

    private void OnItemPressed(int index)
    {
        if (GetPopup().IsItemCheckable(index))
        {
            GetPopup().SetItemChecked(index, !GetPopup().IsItemChecked(index));
        }
        
        switch (index)
        {
            case 0:
                App.Self.Preferences.EmitSignal(nameof(Preferences.InspectorToggled), GetPopup().IsItemChecked(index));
                break;
            case 1:
                GD.Print("USE BIG ICONS CHANGED");
                break;
        }
    }

    private void SetValues()
    {
        GetPopup().SetItemChecked(0, (bool) App.Self.Preferences.ViewPreferences.Get("show_inspector"));
        GetPopup().SetItemChecked(1, (bool) App.Self.Preferences.ViewPreferences.Get("use_big_icons"));
    }
}
