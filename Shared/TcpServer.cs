using System;
using System.Diagnostics;
using System.IO;
using System.Net;
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
        public async void Start()
        {
            _serverSocket = new TcpListener(GetIP(), 8889);
            _serverSocket.Start();
            while (_running)
            {
                TcpClient tcpClient = await _serverSocket.AcceptTcpClientAsync();
                Task t = Process(tcpClient);
                await t;
            }
        }

        private async Task Process(TcpClient tcpClient)
        {
            try
            {
                NetworkStream networkStream = tcpClient.GetStream();
                StreamReader reader = new StreamReader(networkStream);
                while (true)
                {
                    string request = await reader.ReadLineAsync();
                    if (request != null)
                    {
                        Debug.WriteLine(request);
                    }
                    else
                        break; // client closede connection
                }
                tcpClient.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                if (tcpClient.Connected)
                    tcpClient.Close();
            }
        }

        public void Stop()
        {
            //TODO fix exception
            //http://stackoverflow.com/questions/7878019/how-do-i-stop-socketexception-a-blocking-operation-was-interrupted-by-a-call-t
            _running = false;
            _serverSocket.Stop();
        }

        private IPAddress GetIP()
        {
            string hostName = Dns.GetHostName();
            IPHostEntry ipHostInfo = Dns.GetHostEntry(hostName);
            foreach (IPAddress address in ipHostInfo.AddressList)
            {
                if (address.AddressFamily == AddressFamily.InterNetwork)
                {
                    return address;
                }
            }
            return null;
        }
    }
}
