using System;

namespace Infrastructure
{
    [Serializable]
    public enum CommandType
    {
        UPDATE = 1,
        ALERT = 2
    }

    [Serializable]
    public class Message
    {
        public CommandType Cmd { get; set; }
        public MonitorData Data { get; set; }
    }
}
