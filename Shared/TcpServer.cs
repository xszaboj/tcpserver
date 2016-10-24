using System;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Shared
{
    public class TcpServer
    {
        TcpListener _serverSocket;
        TcpClient _clientSocket;

        public void Start()
        {
            Task t = new Task(() =>
            {
                _serverSocket = new TcpListener(8889);
                _clientSocket = default(TcpClient);
                int counter = 0;

                _serverSocket.Start();

                counter = 0;
                while (true)
                {
                    counter += 1;
                    _clientSocket = _serverSocket.AcceptTcpClient();
                    ClientHandler client = new ClientHandler();
                    client.startClient(_clientSocket, Convert.ToString(counter));
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
