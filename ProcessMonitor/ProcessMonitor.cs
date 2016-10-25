using Infrastructure;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ProcessMonitor
{
    class ProcessMonitor: IServer
    {
        private readonly ConcurrentDictionary<int, IClient> _clients = new ConcurrentDictionary<int, IClient>();
        private readonly Timer _timer = new Timer(3000);

        public ProcessMonitor()
        {
            _timer.Elapsed += async (sender, e) => await UpdateMonitorData();
        }

        public void Subscribe(IClient client)
        {
            _clients[client.GetHashCode()] = client;
        }

        public void Unsubscribe(IClient client)
        {
            ((IDictionary<int, IClient>)_clients).Remove(client.GetHashCode());
        }

        public void Start()
        {
            _timer.Start();
        }

        private Task UpdateMonitorData()
        {
            return Task.Factory.StartNew(() => {
                var data = new MonitorData();
                data.Processes = Process.GetProcesses().Select(p => new ProcessData
                {
                    Id = p.Id,
                    Priority = p.BasePriority,
                    VirtMemory = p.VirtualMemorySize64,
                    PhysMemory = p.WorkingSet64,
                    Name = p.ProcessName,
                }).ToList();

                foreach (var client in _clients.Values)
                {
                    client.Update(data);
                }
            });
        }
    }
}
