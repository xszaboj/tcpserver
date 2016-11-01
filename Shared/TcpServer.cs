using System;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Shared.TCPWrappers;

namespace Shared
{
    public class TcpServer
    {
        private bool _running = true;
        TcpListener _serverSocket;
        ITcpClient _clientSocket;
        //TODO write as asynchronous
        //https://msdn.microsoft.com/en-us/library/fx6588te(v=vs.110).aspx
        public void Start()
        {
            Task t = new Task(() =>
            {
                _serverSocket = new TcpListener(8889);
                _serverSocket.Start();
                while (_running)
                {
                    _clientSocket = new MyTcpClient(_serverSocket.AcceptTcpClient());
                    ClientHandler client = new ClientHandler();
                    client.StartClient(_clientSocket);
                }
            });
            t.Start();
        }

        public void Stop()
        {
            //TODO fix exception
            //http://stackoverflow.com/questions/7878019/how-do-i-stop-socketexception-a-blocking-operation-was-interrupted-by-a-call-t
            _running = false;
            _serverSocket.Stop();
        }
    }
}
