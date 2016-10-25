using Infrastructure;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace PipeTransport
{
    public class PipeServerTransport : ITransport
    {
        public class PipeClient : IClient
        {
            private PipeStream _stream;

            public PipeClient(PipeStream stream)
            {
                _stream = stream;
            }

            public void Alert(MonitorData data)
            {
                var bf = new BinaryFormatter();
                bf.Serialize(_stream, data);
            }

            public void Update(MonitorData data)
            {
                var bf = new BinaryFormatter();
                bf.Serialize(_stream, new Message
                {
                    Cmd = CommandType.UPDATE,
                    Data = data
                });
            }
        }

        private readonly IServer _server;
        private ConcurrentDictionary<int, NamedPipeServerStream> _pipes = new ConcurrentDictionary<int, NamedPipeServerStream>();

        public PipeServerTransport(IServer server)
        {
            _server = server;
        }


        public void Dispose()
        {
        }

        public void Run()
        {
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    var pipe = new NamedPipeServerStream("testpipe", PipeDirection.InOut);
                    pipe.WaitForConnection();

                    _pipes[pipe.GetHashCode()] = pipe;
                    _server.Subscribe(new PipeClient(pipe));
                }
            });
        }
    }
}
