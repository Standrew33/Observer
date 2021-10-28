using System;
using System.Diagnostics;
using System.IO;

namespace Observer
{
    class Logger
    {
        public void WriteLog(Process process)
        {
            FileStream fileStream = new FileStream("Log.txt", FileMode.Append, FileAccess.Write);
            StreamWriter streamWriter = new StreamWriter(fileStream);

            streamWriter.WriteLine("ID {0}  Name {1}  Running Time {2} Start running {3}", process.Id, process.ProcessName, DateTime.Now - process.StartTime, process.StartTime);

            streamWriter.Close();
            fileStream.Close();
        }
    }
}
