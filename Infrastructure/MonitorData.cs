using System;
using System.Collections.Generic;

namespace Infrastructure
{
    [Serializable]
    public class MonitorData
    {
        public List<string> Alerts { get; set; }
        public List<ProcessData> Processes { get; set; }
    }
}
