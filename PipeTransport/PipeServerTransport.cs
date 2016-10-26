using Infrastructure;
using System;
using System.Collections.Concurrent;
using System.IO.Pipes;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace PipeTransport
{
    /// <summary>
    /// 'Named Pipe' implementation for ITransport.
    /// Part that used on server side. (See ProcessMonitor project for example.)
    /// </summary>
    public class PipeServerTransport : ITransport
    {
        public class PipeClient : IClient
        {
            private PipeStream _stream;
            private Action<IClient> _onDisconnect;

            public PipeClient(PipeStream stream, Action<IClient> onDisconnect)
            {
                _stream = stream;
                _onDisconnect = onDisconnect;
            }

            public void Alert(MonitorData data)
            {
                try
                {
                    var bf = new BinaryFormatter();
                    bf.Serialize(_stream, new Message
                    {
                        Cmd = CommandType.ALERT,
                        Data = data
                    });
                }
                catch(Exception ex)
                {
                    _onDisconnect?.Invoke(this);
                    Console.WriteLine(ex);
                }
            }

            public void Update(MonitorData data)
            {
                try
                {
                    var bf = new BinaryFormatter();
                    bf.Serialize(_stream, new Message
                    {
                        Cmd = CommandType.UPDATE,
                        Data = data
                    });
                }
                catch(Exception ex)
                {
                    _onDisconnect?.Invoke(this);
                    Console.WriteLine(ex);
                }
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
                    var pipe = new NamedPipeServerStream("testpipe", PipeDirection.InOut, NamedPipeServerStream.MaxAllowedServerInstances);
                    pipe.WaitForConnection();

                    _pipes[pipe.GetHashCode()] = pipe;
                    _server.Subscribe(new PipeClient(pipe, x => _server.Unsubscribe(x)));
                }
            });
        }
    }
}
