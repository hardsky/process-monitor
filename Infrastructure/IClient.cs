namespace Infrastructure
{
    public interface IClient
    {
        /// <summary>
        /// Server sends process info updates to Client
        /// </summary>
        /// <param name="data">Info about processes on machine.</param>
        void Update(MonitorData data);

        /// <summary>
        /// Server sends alert to client about some processes
        /// </summary>
        /// <param name="data">Info about processes with extrem values on machine.</param>
        void Alert(MonitorData data);
    }
}
