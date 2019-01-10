using Godot;

namespace Taleteller.Core.Utils.Console
{
    public class ConsoleBridge : Control
    {
        private static Console _console = new Console();
        public static Console Console => _console;

        private BaseButton _sendCmd;
        private LineEdit _entryField;
        private BoxContainer _cmdHistoryList;

        public override void _Input(InputEvent inputEvent)
        {
            if (inputEvent.IsActionPressed("console_send"))
            {
                SendCmd();
            }
            else if (inputEvent.IsActionPressed("console_toggle"))
            {
                if (!Visible)
                {
                    GetTree().SetPause(true);
                    SetVisible(true);
                    _entryField.GrabFocus();
                }
                else
                {
                    SetVisible(false);
                    GetTree().SetPause(false);
                }
            }
            else if (inputEvent.IsActionPressed("ui_cancel") && Visible)
            {
                SetVisible(false);
                GetTree().SetPause(false);
            }
            UpdateGuiEntries();
            
        }

        public override void _Ready()
        {
            SetPauseMode(PauseModeEnum.Process);
            _sendCmd = (BaseButton) GetNode("./vbox/hbox/send");
            _entryField = (LineEdit) GetNode("./vbox/hbox/entry");
            _cmdHistoryList = (BoxContainer) GetNode("./vbox/scroll/vbox");

            ConsoleCommandBase cmdBase = (ConsoleCommandBase) GetNode("./commandBase");
            _console.Init(cmdBase);

            _sendCmd.Connect("pressed", this, "SendCmd");
        }

        public void SendCmd()
        {
            GD.Print("Entryfield: " + _entryField.Text);
            _console.ExecCurrEntry(_entryField.Text);
            UpdateGuiEntries();
            _entryField.Text = "";
        }

        public void UpdateGuiEntries()
        {
            foreach (Node child in _cmdHistoryList.GetChildren())
            {
                child.Free();
            }
            
            foreach (string str in _console.GetCmdHistory())
            {
                Label entry = new Label();
                entry.Text = str;
                _cmdHistoryList.AddChild(entry);
            }
        }
    }
}