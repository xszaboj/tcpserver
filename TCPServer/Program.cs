using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MouseProject;
using Shared;
using TouchApp.MySpace;

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
