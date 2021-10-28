using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;

namespace Observer
{
    class ProcessKiller
    {
        Monitor monitor = new Monitor();
        public void KillProcess()
        {
            string nameProcess = monitor.GetNameProcess();
            Process[] processes = Process.GetProcessesByName(nameProcess);

            if (processes.Length == 0)
                Environment.Exit(0);

            Logger logger = new Logger();

            do
            {
                foreach (Process process in processes)
                {
                    if (!CheckTimeLife(process))
                    {
                        Console.WriteLine("\nProcces ID{0}  {1}  {2} killed\n", process.Id, process.ProcessName, DateTime.Now - process.StartTime);
                        Console.WriteLine("More info in Log file\n");
                        logger.WriteLog(process);
                    }
                }
                Thread.Sleep(500);
                processes = Process.GetProcessesByName(nameProcess);
                if (processes.Length != 0)
                {
                    Thread.Sleep(monitor.GetFrequency());
                }
            }
            while (processes.Length != 0);
            
        }

        public bool CheckTimeLife(Process process)
        {
            try
            {
                if (DateTime.Now.Subtract(process.StartTime) > TimeSpan.FromMinutes(monitor.GetLifeTime()))
                {
                    process.Kill();
                    return false;
                }
                else
                {
                    return true;
                }

            }
            catch (Win32Exception ex)
            {
                if (ex.NativeErrorCode == 5)
                    throw;
                return false;
            }
        }
    }
}
