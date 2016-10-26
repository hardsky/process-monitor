using System;

namespace Infrastructure
{
    /// <summary>
    /// Transport between Server & Clients
    /// </summary>
    public interface ITransport:IDisposable
    {
        /// <summary>
        /// Start communication. Should be called on server and client side.
        /// </summary>
        void Run();
    }
}
