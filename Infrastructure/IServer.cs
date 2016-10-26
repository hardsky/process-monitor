namespace Infrastructure
{
    /// <summary>
    /// In this moment, it's only ProcessMonitor, that implements this interface.
    /// </summary>
    public interface IServer
    {
        /// <summary>
        /// Subscribe client for updates from server
        /// </summary>
        /// <param name="client"></param>
        void Subscribe(IClient client);

        /// <summary>
        /// Unsubscribe, when client does not need updates any more.
        /// </summary>
        /// <param name="client"></param>
        void Unsubscribe(IClient client);
    }
}
