using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using KillPort.Models;
namespace KillPort
{
    class Program
    {

        static void Main(string[] args)
        {

            int i;
            for (i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {

                    case "-p":
                    case "--port":
                     
                        if (args.Length >= (i + 1))
                        {
                            int port;
                            if (int.TryParse(args[i + 1], out port))
                            {
                                foreach (Port p in ProcessPorts.GetPorts().FindAll(x => x.PortNumber == port))
                                {
                                   
                                    ProcessTaskKill.KillTask(p.ProcessId);
                                    
                                }
                                i += 1;
                            }
                        }
                        break;
                    case "-v":
                    case "--version":
                        ShowVersion();
                        break;
                    case "-h":
                    case "--help":
                    default:
                        ShowHelp();
                        break;

                }
            }

            Console.ReadKey();
        }

        private static void ShowVersion()
        {

            Console.WriteLine(@"
 _    _ _ _                  _   
| | _(_) | |_ __   ___  _ __| |_ 
| |/ / | | | '_ \ / _ \| '__| __|
|   <| | | | |_) | (_) | |  | |_ 
|_|\_\_|_|_| .__/ \___/|_|   \__|
           |_|                   
 "
+ Assembly.GetExecutingAssembly().GetName().Version);
        }

        private static void ShowHelp()
        {
            Console.WriteLine(@"
Usage:

  killport [options] (<name>|<path>|<list>)
  killport [-h|--help]


Options:
  -p, --port <port number>       Kill the process running on the particular port number    
  -h, --help                 Display help.
  -v, --version              Display version.

");
        }
    }
}
