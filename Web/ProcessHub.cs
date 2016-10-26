using Microsoft.AspNet.SignalR;
using Infrastructure;
using Microsoft.AspNet.SignalR.Hubs;

namespace Web
{
    [HubName("processHub")]
    public class ProcessHub : Hub
    {
        private IHubConnectionContext<dynamic> clients;
        private readonly Client _client;

        public ProcessHub() : this(Client.Instance) { }

        public ProcessHub(IHubConnectionContext<dynamic> clients)
        {
            this.clients = clients;
        }

        public ProcessHub(Client client)
        {
            _client = client;
        }

        public MonitorData GetMonitorData()
        {
            return _client.GetCachedData();
        }
    }
}