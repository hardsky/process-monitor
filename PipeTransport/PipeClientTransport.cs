using Infrastructure;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace PipeTransport
{
    public class PipeClientTransport : ITransport
    {
        private readonly IClient _client;

        public PipeClientTransport(IClient client)
        {
            _client = client;
        }

        public void Dispose()
        {
        }

        public void Run()
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    using (var _pipe = new NamedPipeClientStream(".", "testpipe", PipeDirection.InOut))
                    {
                        _pipe.Connect();

                        var cmds = new Dictionary<CommandType, Action<MonitorData>>
                            {
                                { CommandType.UPDATE, _client.Update},
                                { CommandType.ALERT, _client.Alert},
                            };

                        while (true)
                        {
                            var bf = new BinaryFormatter();
                            var msg = (Message)bf.Deserialize(_pipe);
                            cmds[msg.Cmd](msg.Data);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    Run();
                }
            });
        }
    }
}
