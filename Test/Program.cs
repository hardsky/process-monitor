using Infrastructure;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Client
    {
        public void Run()
        {
            while (true)
            {
                try
                {
                    using (var _pipe = new NamedPipeClientStream(".", "testpipe", PipeDirection.InOut))
                    {
                        _pipe.Connect();
                        while (true)
                        {
                            if (!_pipe.IsConnected)
                                break;

                            if (!_pipe.CanRead)
                                continue;

                            var bf = new BinaryFormatter();
                            var msg = bf.Deserialize(_pipe) as Message;
                            if (msg == null)
                                continue;

                            Console.WriteLine($"Client: message = {msg}");
                        }
                    }
                }
                catch
                {
                    continue;
                }
            }
        }
    }

    class Server
    {
        public void Run()
        {
            while (true)
            {
                using (var _pipe = new NamedPipeServerStream("testpipe", PipeDirection.InOut, NamedPipeServerStream.MaxAllowedServerInstances))
                {
                    _pipe.WaitForConnection();
                    var bf = new BinaryFormatter();
                    bf.Serialize(_pipe, new Message
                    {
                        Cmd = CommandType.UPDATE,
                        Data = new MonitorData
                        {
                            Processes = new List<ProcessData>()
                        }
                    });
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var server = new Server();
            Task.Factory.StartNew(server.Run);

            var client = new Client();
            Task.Factory.StartNew(client.Run);

            Console.ReadKey();
        }
    }
}
