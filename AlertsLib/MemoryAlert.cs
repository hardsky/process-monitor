using Infrastructure;
using System;
using System.Globalization;

namespace AlertsLib
{
    public class MemoryAlert : IAlertRule
    {
        private const int MEMORY_UPPER_BOUND = 200000000; // bytes

        public string Check(ProcessData p)
        {
            if(p.PhysMemory > MEMORY_UPPER_BOUND)
            {
                return $"Process '{p.Name}' exceeded memory upper bound. Current physycal memory = {String.Format(CultureInfo.InvariantCulture, "{0:0.00}", (p.PhysMemory / 1000.0 / 1000))} MiB";
            }

            return null;
        }
    }
}
