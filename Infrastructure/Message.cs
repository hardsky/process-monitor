using System;

namespace Infrastructure
{
    [Serializable]
    public enum CommandType
    {
        UPDATE = 1,
        ALERT = 2
    }

    /// <summary>
    /// Can be used to communicate through transport.
    /// Pass messages between server and clients.
    /// </summary>
    [Serializable]
    public class Message
    {
        public CommandType Cmd { get; set; }
        public MonitorData Data { get; set; }
    }
}
