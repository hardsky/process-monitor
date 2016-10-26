using PipeTransport;
using System;

namespace ProcessMonitor
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
