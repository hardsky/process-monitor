using PipeTransport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace process_monitor
{
    class Program
    {
        static void Main(string[] args)
        {
            var monitor = new ProcessMonitor();
            monitor.Start();

            var transport = new PipeServerTransport(monitor);
            transport.Run();

            Console.ReadKey();
        }
    }
}
