using System.Collections.Generic;

namespace Taleteller.Core.Utils.Console
{
    public class Console
    {
        private List<string> _cmdHistory = new List<string>();
        private string _currCmdEntryField = "";
        
        private Parser _parser = new Parser();

        public void Init(ConsoleCommandBase cmdBase)
        {
            _parser.SetConsole(this);
            _parser.SetCommandBase(cmdBase);
        }

        public void UpdateCurrEntryField(string entry)
        {
            _currCmdEntryField = entry;
        }

        public void ExecCurrEntry(string entry)
        {
            UpdateCurrEntryField(entry);
            _parser.Parse(entry);
        }

        public void AddToCmdHistory(string entry)
        {
            _cmdHistory.Add(entry);
        }

        public List<string> GetCmdHistory()
        {
            return _cmdHistory;
        }

        public void Echo(string entry)
        {
            _cmdHistory.Add(entry);
        }
    }
}