using System.Net.Sockets;
using TCPServer;

namespace Shared.Extensions
{
    public static class Extensions
    {
        public static bool IsConnected(this TcpClient client)
        {
            return client.Connected && client.Client.IsConnected();
        }
    }
}
