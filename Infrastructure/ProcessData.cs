using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    [Serializable]
    public class ProcessData
    {
        public int Id { get; set; } //Id
        public int Priority { get; set; } // BasePriority
        public long VirtMemory { get; set; } // VirtualMemorySize64 (bytes)
        public long PhysMemory { get; set; } // WorkingSet64 (bytes)
        public String Name { get; set; } // ProcessName
    }
}
