using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Godot;
using Directory = System.IO.Directory;
using Path = System.IO.Path;
using Timer = System.Timers.Timer;

namespace Taleteller.Core.Utils
{
    public class Log
    {
        private DirectoryInfo _logDir;
        
        private FileInfo _logPrint;
        private FileInfo _logError;
        private FileInfo _logCrash;
        private FileInfo _logNetwork;
        private FileInfo _logUi;
        private FileInfo _logDb;
        private FileInfo _logConsole;
        private FileInfo _logServer;
        
        
        private Queue<PrintJob> _printQueue = new Queue<PrintJob>();

        private Stopwatch _sw;
        private Timer _timer;

        public Log()
        {
            _logDir = Directory.CreateDirectory(Paths.LogPath);

            FileInfo[] files = _logDir.GetFiles();
            foreach (FileInfo fi in files)
            {
                if (      fi.Name == "___event.log"
                       || fi.Name == "__error.log"
                       || fi.Name == "_crash.log"
                       || fi.Name == "network.log"
                       || fi.Name == "ui.log"
                       || fi.Name == "database.log"
                       || fi.Name == "console.log"
                       || fi.Name == "server.log")
                {
                    fi.Delete();
                }
            }

            _logPrint = new FileInfo(Path.Combine(Paths.LogPath, "___event.log"));
            _logError = new FileInfo(Path.Combine(Paths.LogPath, "__error.log"));
            _logCrash = new FileInfo(Path.Combine(Paths.LogPath, "_crash.log"));
            _logNetwork = new FileInfo(Path.Combine(Paths.LogPath, "network.log"));
            _logUi = new FileInfo(Path.Combine(Paths.LogPath, "ui.log"));
            _logDb = new FileInfo(Path.Combine(Paths.LogPath, "database.log"));
            _logConsole = new FileInfo(Path.Combine(Paths.LogPath, "console.log"));
            _logServer = new FileInfo(Path.Combine(Paths.LogPath, "server.log"));

            _logPrint.Create().Dispose();
            _logError.Create().Dispose();
            _logCrash.Create().Dispose();
            _logNetwork.Create().Dispose();
            _logUi.Create().Dispose();
            _logDb.Create().Dispose();
            _logConsole.Create().Dispose();
            _logServer.Create().Dispose();

            _sw = new Stopwatch();
            _timer = new Timer();
            _sw.Start();

            // TODO: Consider creating a more sophisticated iterator for the Database methods
            _timer.Interval = 100;
            _timer.Elapsed += delegate { ProcessQueue(); };
            _timer.Enabled = true;
        }
        
        
        public void Print(string path, string value, bool gdPrint = false)
        {
            string print = _sw.ElapsedMilliseconds + " | " + path + " | " + value;
            
            _printQueue.Enqueue(new PrintJob(_logPrint.FullName, print));

            if (gdPrint)
            {
                GD.Print(print);
            }
        }
        
        public void Error(string path, string value, bool gdPrint = false)
        {
            string error = _sw.ElapsedMilliseconds + " | " + path + " | " + value;
            string print = _sw.ElapsedMilliseconds + " | " + path + " | " + "ERROR: " + value;
            
            _printQueue.Enqueue(new PrintJob(_logPrint.FullName, print));
            _printQueue.Enqueue(new PrintJob(_logError.FullName, error));
            
            if (gdPrint)
            {
                GD.Print(print);
            }
        }
        
        public void Crash(string path, string value, bool gdPrint = false)
        {
            string crash = _sw.ElapsedMilliseconds + " | " + path + " | " + value;
            string print = _sw.ElapsedMilliseconds + " | " + path + " | " + "CRASH: " + value;
            
            _printQueue.Enqueue(new PrintJob(_logPrint.FullName, print));
            _printQueue.Enqueue(new PrintJob(_logError.FullName, print));
            _printQueue.Enqueue(new PrintJob(_logCrash.FullName, crash));
            
            if (gdPrint)
            {
                GD.Print(print);
            }
        }
        
        public void Ui(string path, string value, bool gdPrint = false)
        {
            string ui = _sw.ElapsedMilliseconds + " | " + path + " | " + value;
            string print = _sw.ElapsedMilliseconds + " | " + path + " | " + "UI: " + value;
            
            _printQueue.Enqueue(new PrintJob(_logPrint.FullName, print));
            _printQueue.Enqueue(new PrintJob(_logUi.FullName, ui));
            
            if (gdPrint)
            {
                GD.Print(print);
            }
        }
        
        public void Database(string path, string value, bool gdPrint = false)
        {
            string db = _sw.ElapsedMilliseconds + " | " + path + " | " + value;
            string print = _sw.ElapsedMilliseconds + " | " + path + " | " + "DATABASE: " + value;
            
            _printQueue.Enqueue(new PrintJob(_logPrint.FullName, print));
            _printQueue.Enqueue(new PrintJob(_logDb.FullName, db));
            
            if (gdPrint)
            {
                GD.Print(print);
            }
        }
        
        public void Console(string path, string value, bool gdPrint = false)
        {
            string console = _sw.ElapsedMilliseconds + " | " + path + " | " + value;
            string print = _sw.ElapsedMilliseconds + " | " + path + " | " + "CONSOLE: " + value;
            
            _printQueue.Enqueue(new PrintJob(_logPrint.FullName, print));
            _printQueue.Enqueue(new PrintJob(_logConsole.FullName, console));
            
            if (gdPrint)
            {
                GD.Print(print);
            }
        }
        
        private void ProcessQueue()
        {
            while (_printQueue.Count > 0)
            {
                PrintJob job = _printQueue.Dequeue();
                using (StreamWriter writer = new StreamWriter(job.LogPath, true))
                {
                    writer.WriteLine(job.Print);
                    writer.NewLine = "";
                    writer.Dispose();
                }
            }
        }

        private struct PrintJob
        {
            public readonly string LogPath;
            public readonly string Print;
            public PrintJob(string logPath, string print)
            {
                LogPath = logPath;
                Print = print;
            }
        }
    }
}