using Infrastructure;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Web;

namespace Web
{
    public class Client : IClient
    {
        private readonly static Lazy<Client> _instance = new Lazy<Client>(() => new Client(GlobalHost.ConnectionManager.GetHubContext<ProcessHub>().Clients));
        public static Client Instance
        {
            get
            {
                return _instance.Value;
            }
        }


        private MonitorData _data;
        private IHubConnectionContext<dynamic> _clients;

        public Client(IHubConnectionContext<dynamic> clients)
        {
            _clients = clients;
        }

        public void Alert(MonitorData data)
        {
            _data = data;
            _clients.All.alert(data);
        }

        public void Update(MonitorData data)
        {
            _data = data;
            _clients.All.update(data);
        }

        public MonitorData GetCachedData()
        {
            return _data;
        }
    }
}