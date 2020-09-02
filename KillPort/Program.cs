using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using KillPort.Models;
using System.Security.Principal;
namespace KillPort
{
    class Program
    {

        static void Main(string[] args)
        {

            if (!IsAdminMode())
            {
                Console.WriteLine("KillPort is required administrator permission to kill the port.");
            }
            else
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
                                    Port p = ProcessPorts.GetPorts().FirstOrDefault(x => x.PortNumber == port);
                                    if (p == null)
                                    {
                                        Console.WriteLine($"Port number {port} is not occupied by any other proccess.");
                                    }
                                    else
                                    {
                                        Console.Write($"'{p.ProcessName}' (PID {p.ProcessId}) is using port number {p.PortNumber}. Confirm kill it (Y/N) ?");
                                        string input = Console.ReadLine();
                                        if (input.Equals("y", StringComparison.OrdinalIgnoreCase))
                                        {
                                            Console.WriteLine($"Port {p.PortNumber} has been killed.");
                                        }
                                        Console.WriteLine(ProcessTaskKill.KillTask(p.ProcessId));
                                    }



                                    i += 1;
                                }
                                else
                                {
                                    Console.WriteLine($"'{args[i + 1]}' is not a valid port number.");
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
            }




#if DEBUG
            Console.ReadKey();
#endif

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

        private static bool IsAdminMode()
        {
            bool isElevated;
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                isElevated = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            return isElevated;
        }

    }
}
