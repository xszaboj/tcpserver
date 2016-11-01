using System;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Shared.TCPWrappers;

namespace Shared
{
    public class TcpServer
    {
        TcpListener _serverSocket;
        ITcpClient _clientSocket;

        public void Start()
        {
            Task t = new Task(() =>
            {
                _serverSocket = new TcpListener(8889);
                _serverSocket.Start();
                while (true)
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
            _clientSocket.Close();
            _serverSocket.Stop();
        }
    }
}
