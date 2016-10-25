using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Infrastructure;
using Microsoft.AspNet.SignalR.Hubs;

namespace Web
{
    [HubName("processHub")]
    public class ProcessHub : Hub
    {
        private Microsoft.AspNet.SignalR.Hubs.IHubConnectionContext<dynamic> clients;
        private readonly Client _client;

        public ProcessHub() : this(Client.Instance) { }

        public ProcessHub(Microsoft.AspNet.SignalR.Hubs.IHubConnectionContext<dynamic> clients)
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