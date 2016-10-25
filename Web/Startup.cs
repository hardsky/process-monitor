using Infrastructure;
using Microsoft.Owin;
using Owin;
using PipeTransport;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(Web.Startup))]
namespace Web
{
    public class Startup
    {
        private ITransport clientTransport;

        public void Configuration(IAppBuilder app)
        {
            // Any connection or hub wire up and configuration should go here
            app.MapSignalR();
            clientTransport = new PipeClientTransport(Client.Instance);
            clientTransport.Run();
        }
    }
}