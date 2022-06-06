using killport.Cli.tool.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace killport.Cli.tool.Helper
{
    public static class ProcessPorts
    {
        /// <summary>
        /// Get Process Name
        /// </summary>
        /// <param name="processId">Id of process</param>
        /// <returns>Return nama of the process if the process is exist, otherwise return empty string.</returns>
        public static string GetProcessName(int processId)
        {
            string result = string.Empty;
            try
            {
                result = Process.GetProcessById(processId).ProcessName;
            }
            catch
            {
                //do nothing
            }

            return result;
        }
        /// <summary>
        /// Get list of Port
        /// </summary>
        /// <returns>Return as list of port</returns>
        public static List<Port> GetPorts()
        {
            List<Port> result = new List<Port>();
            try
            {
                using (Process proc = new Process())
                {
                    proc.StartInfo.FileName = "netstat.exe";
                    proc.StartInfo.Arguments = "-ano";
                    proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
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
                        Console.WriteLine("NetStat command failed.   This may require elevated permissions.");
                    }
                    //get result, the result might be more than one row, split the rows

                    string[] rows = Regex.Split(output, "\r\n");
                    foreach (string row in rows)
                    {
                        ///parttern:  UDP    10.0.75.1:1900         *:*                                    2932
                        ///1- protocol
                        ///2- ip address
                        ///3- :
                        ///4: process id
                        string[] tokens = Regex.Split(row, "\\s+");
                        if (tokens.Length > 4 && (tokens[1].Equals("UDP") || tokens[1].Equals("TCP")))
                        {
                            string ipAddress = Regex.Replace(tokens[2], @"\[(.*?)\]", "1.1.1.1");
                            try
                            {
                                result.Add(new Port(
                                tokens[1] == "UDP" ? GetProcessName(Convert.ToInt16(tokens[4])) : GetProcessName(Convert.ToInt16(tokens[5])),
                                    tokens[1] == "UDP" ? Convert.ToInt16(tokens[4]) : Convert.ToInt16(tokens[5]),
                                    ipAddress.Contains("1.1.1.1") ? String.Format("{0}v6", tokens[1]) : String.Format("{0}v4", tokens[1]),
                                    Convert.ToInt32(ipAddress.Split(':')[1])
                                ));
                            }
                            catch
                            {
                                Console.WriteLine("Could not convert the following NetStat row to a Process to Port mapping.");
                                Console.WriteLine(row);
                            }

                        }
                        else
                        {
                            if (!row.Trim().StartsWith("Proto") && !row.Trim().StartsWith("Active") && !String.IsNullOrWhiteSpace(row))
                            {
                                Console.WriteLine("Unrecognized NetStat row to a Process to Port mapping.");
                                Console.WriteLine(rows);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }

    }
}
