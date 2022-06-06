using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace killport.Cli.tool.Helper
{
    public static class ProcessTaskKill
    {
        const string failedMessage = "taskkill command failed. This may require elevated permissions.";
        /// <summary>
        /// Kill the process by process Id
        /// </summary>
        /// <param name="processId">Process Id</param>
        public static string KillTask(int processId)
        {
            string result = "";
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
                result = (exitStatus != "0") ? failedMessage : output;
            }
            return result;
        }
    }
}
