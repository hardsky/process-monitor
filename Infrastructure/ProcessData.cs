using System;

namespace Infrastructure
{
    /// <summary>
    /// In this moment it mostly map Diagnocts.Process properties
    /// ProcessMonitor pass this data to clients
    /// </summary>
    [Serializable]
    public class ProcessData
    {
        public int Id { get; set; } //Id
        public int Priority { get; set; } // BasePriority
        public long VirtMemory { get; set; } // VirtualMemorySize64 (bytes)
        public long PhysMemory { get; set; } // WorkingSet64 (bytes)
        public int TimeRunning { get; set; } // (Now - TotalProcessorTime).TotalSeconds
        public String Name { get; set; } // ProcessName
    }
}
