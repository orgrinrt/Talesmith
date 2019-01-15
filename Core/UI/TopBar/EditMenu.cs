using Godot;
using System;

public class EditMenu : MenuButton
{
    public override void _Ready()
    {
        GetPopup().AddItem("Undo");
        GetPopup().AddItem("Redo");
        GetPopup().AddItem("History");
        GetPopup().AddSeparator();
        GetPopup().AddItem("Image Library");
        GetPopup().AddItem("Audio Library");
        GetPopup().AddItem("Backups");
        GetPopup().AddSeparator();
        GetPopup().AddItem("Preferences");
    }
}
