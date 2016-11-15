using System;
using Shared;
using Shared.Events;

namespace TCPServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new TcpServer();
            server.Start();
            server.ClientConnected += (tcpServer, arg) =>
            {
                Console.WriteLine("Client number {0} just connected.", arg.Name);
            };
            server.ClientDisconnected += (tcpServer, arg) =>
            {
                Console.WriteLine("Client number {0} just disconnected.", arg.Name);
            };
            Console.ReadLine();
        }
    }
}
