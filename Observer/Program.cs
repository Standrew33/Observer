using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Observer
{
    public class ConsoleSpiner
    {
        int counter;
        public ConsoleSpiner()
        {
            counter = 0;
        }
        public void Turn()
        {
            counter++;
            switch (counter % 4)
            {
                case 0: Console.Write("/"); break;
                case 1: Console.Write("-"); break;
                case 2: Console.Write("\\"); break;
                case 3: Console.Write("|"); break;
            }
            Console.SetCursorPosition(Console.CursorLeft == 0 ? Console.CursorLeft : Console.CursorLeft - 1, Console.CursorTop);
        }
    }

    class Monitor
    {

        private static string proccessName;

        private static int lifeTime;

        private static int frequencyCheck;

        public static Thread threadKillProcess;

        public static Thread threadExitProgram;

        static void Main(string[] args)
        {
            ProcessKiller processKiller = new ProcessKiller();
            if (CheckParameters(args)) 
            {
                threadKillProcess = new Thread(() => 
                { 
                    processKiller.KillProcess();
                    threadExitProgram.Interrupt();
                });
                threadExitProgram = new Thread(ExitProgram);
                threadKillProcess.Start();
                threadExitProgram.Start();
            }
        }

        public static void ExitProgram()
        {
            Console.WriteLine("");
            Console.WriteLine("Press any key to exit the monitoring process...\n");

            ConsoleSpiner spin = new ConsoleSpiner();
            Console.Write("Working... ");

            while (threadKillProcess.IsAlive && !Console.KeyAvailable) {
                spin.Turn();
            }

            Environment.Exit(0);
        }

        public static bool CheckParameters(string[] args)
        {

            if (args.Length < 3 || !int.TryParse(args[1], out lifeTime) || !int.TryParse(args[2], out frequencyCheck))
            {
                Console.WriteLine("Incorrect arguments");
                return false;
            }
            else
            {
                SetNameProcess(args[0]);
                SetLifeTime(int.Parse(args[1]));
                SetFrequency(int.Parse(args[2]));
                return true;
            }
        }

        public static void SetNameProcess(string name)
        {
            proccessName = name;
        }

        public string GetNameProcess()
        {
            return proccessName;
        }


        public static void SetLifeTime(int time)
        {
            lifeTime = time;
        }

        public int GetLifeTime()
        {
            return lifeTime;
        }

        public static void SetFrequency(decimal frequency)
        {
            frequencyCheck = (int)Math.Truncate(60000 / frequency);
        }

        public int GetFrequency()
        {
            return frequencyCheck;
        }
    }
}
