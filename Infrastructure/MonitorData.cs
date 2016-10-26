using System;
using System.Collections.Generic;

namespace Infrastructure
{
    /// <summary>
    /// Data from ProcessMonitor
    /// </summary>
    [Serializable]
    public class MonitorData
    {
        /// <summary>
        /// Used with IClient.Alert, can be null or empty for IClient.Update
        /// </summary>
        public List<string> Alerts { get; set; }

        /// <summary>
        /// Used with IClient.Update, can be null or empty for IClient.Alert
        /// </summary>
        public List<ProcessData> Processes { get; set; }
    }
}
