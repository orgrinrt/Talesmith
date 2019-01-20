using Godot;

namespace Talesmith.Core.UI.TopBar
{
    public class FileMenu : MenuButton
    {
        public override void _Ready()
        {
            GetPopup().AddItem("New");
            GetPopup().AddItem("Open");
            GetPopup().AddItem("Open Recent");
            GetPopup().AddSeparator();
            GetPopup().AddItem("Save");
            GetPopup().AddItem("Save As");
            GetPopup().AddSeparator();
            GetPopup().AddItem("Import");
            GetPopup().AddItem("Export");
            GetPopup().AddSeparator();
            GetPopup().AddItem("Exit");
        }
    }
}
