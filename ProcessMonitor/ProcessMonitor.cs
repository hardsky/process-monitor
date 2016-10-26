using AlertsLib;
using Infrastructure;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
                var alertGen = new AlertMonitor();
                alertGen.AddAlertRule(new MemoryAlert());

                var data = new MonitorData();
                data.Processes = Process.GetProcesses().Select(p => 
                {
                    var processData = new ProcessData
                    {
                        Id = p.Id,
                        Priority = p.BasePriority,
                        VirtMemory = p.VirtualMemorySize64,
                        PhysMemory = p.WorkingSet64,
                        //TimeRunning = (int)p.TotalProcessorTime.TotalSeconds, //TODO: permission issue
                        Name = p.ProcessName,
                    };

                    alertGen.Check(processData);

                    return processData;

                 }).OrderByDescending(p=>p.PhysMemory).ToList();

                if (alertGen.IsAlert)
                {
                    var alertData = new MonitorData
                    {
                        Alerts = alertGen.Alerts
                    };

                    foreach (var client in _clients.Values)
                    {
                        client.Update(data);
                        client.Alert(alertData);
                    }
                }
                else
                {
                    foreach (var client in _clients.Values)
                    {
                        client.Update(data);
                    }
                }
            });
        }
    }
}
