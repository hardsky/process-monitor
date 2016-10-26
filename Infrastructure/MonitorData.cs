using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    [Serializable]
    public class MonitorData
    {
        public List<string> Alerts { get; set; }
        public List<ProcessData> Processes { get; set; }
    }
}
