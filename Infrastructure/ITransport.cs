using System;

namespace Infrastructure
{
    public interface ITransport:IDisposable
    {
        /// <summary>
        /// Start communication. Should be called on server and client side.
        /// </summary>
        void Run();
    }
}
