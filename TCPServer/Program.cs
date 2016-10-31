using System;
using Shared;

namespace TCPServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new TcpServer();
            server.Start();
            Console.ReadLine();
        }
    }
}
