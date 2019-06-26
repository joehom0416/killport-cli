using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KillPort.Models
{
   public class Port
    {

        public string ProcessName { get; set; }
        public int ProcessId { get; set; }
        public string Protocol { get; set; }
        public int PortNumber { get; set; }
        /// <summary>
        /// Internal constructor to initialize the mapping of process to port.
        /// </summary>
        /// <param name="ProcessName">Name of process </param>
        /// <param name="ProcessId">Id of Process</param>
        /// <param name="Protocol">Protocol</param>
        /// <param name="PortNumber">Port Number</param>
        internal Port(string processName, int processId, string protocol, int portNumber)
        {
            this.ProcessName = processName;
            this.ProcessId = processId;
            this.Protocol = protocol;
            this.PortNumber = portNumber;
        }
    }
}
