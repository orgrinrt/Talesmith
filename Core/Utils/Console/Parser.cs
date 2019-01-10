using System.Collections.Generic;
using Godot;

namespace Taleteller.Core.Utils.Console
{
    public class Parser
    {
        private Console _console;
        private ConsoleCommandBase _cmdBase;

        public void Parse(string cmd)
        {
            string cmdId = "there was an: error";
            Exec(FirstPass(cmd));
        }
        
        private string[] FirstPass(string cmd)
        {
            List<string> output = new List<string>();
            string[] splitUpStr = cmd.Split(' ');
            string[] trimmed = new string[splitUpStr.Length];
            for (int i = 0; i < trimmed.Length; i++)
            {
                trimmed[i] = splitUpStr[i].Trim();
                GD.Print("Trimmed[i] = " + trimmed[i]);
            }
            GD.Print("survived the first pass");
            output.Add(trimmed[0]);

            if (splitUpStr.Length > 1)
            {
                string[] splitUpStr2 = splitUpStr[1].Split(',');
                string[] trimmed2 = new string[splitUpStr2.Length];
                for (int i = 0; i < trimmed2.Length; i++)
                {
                    trimmed2[i] = splitUpStr2[i].Trim();
                    output.Add(trimmed2[i]);
                    GD.Print("Trimmed2[i] = " + trimmed[i]);
                }

                return output.ToArray();
            }
            else
            {
                GD.Print("splitUpStr.Length wasnt bigger than 1");
                return output.ToArray();
            }
        }

        private void Exec(string[] values)
        {
            GD.Print("CmdId: " + values[0]);
            switch (values[0])
            {
                case "echo":
                    for (int i = 1; i < values.Length; i++)
                    {
                        _console.AddToCmdHistory(values[i]);
                    }
                    break;
                    
                case "ui_quickbar_add_placeholder_ability":
                    if (values.Length <= 1)
                    {
                        _console.Echo("ERR: ui_quickbar_add_ability - No value given, please set a value with your statement");
                    }

                    switch (values[1])
                    {
                        // TODO: Create some default abilities
                    }

                    _console.AddToCmdHistory(values[0] + " set to " + values[1]);
                    break;
            }
        }

        public void SetConsole(Console c)
        {
            _console = c;
        }

        public void SetCommandBase(ConsoleCommandBase cmdBase)
        {
            _cmdBase = cmdBase;
        }
    }
}