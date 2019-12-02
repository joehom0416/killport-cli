using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace KillPort
{
    public static class ProcessTaskKill
    {
        /// <summary>
        /// Kill the process by process Id
        /// </summary>
        /// <param name="processId">Process Id</param>
        public static void KillTask(int processId)
        {
            using (Process proc = new Process())
            {
                proc.StartInfo.FileName = "taskkill.exe";
                proc.StartInfo.Arguments = "/F /PID " + processId;
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardInput = true;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.RedirectStandardError = true;
                proc.Start();
                //Get output and Error
                string output = proc.StandardOutput.ReadToEnd() + proc.StandardError.ReadToEnd();
                string exitStatus = proc.ExitCode.ToString();
                if (exitStatus != "0")
                {
                    Console.WriteLine("taskkill command failed. This may require elevated permissions.");
                }
                else
                {
                    Console.WriteLine(output);
                }
            }
        }
    }
}
