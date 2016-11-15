using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Shared.Events;
using Shared.TCPWrappers;

namespace Shared
{
    public class TcpServer
    {
        private bool _running = true;
        private int _clientNumber = 1;

        TcpListener _serverSocket;
        private ClientHandler _handler;

        public event ClientConnectedHandler ClientConnected;
        public event ClientConnectedHandler ClientDisconnected;
        public delegate void ClientConnectedHandler(TcpServer s, ClientEventArg e);

        public async Task Start()
        {
            _running = true;
            _handler = new ClientHandler();
            _serverSocket = new TcpListener(GetIP(), 8889);
            _serverSocket.Start();
            while (_running)
            {
                try
                {
                    TcpClient tcpClient = await _serverSocket.AcceptTcpClientAsync();
                    ClientEventArg clientData = new ClientEventArg() {Name = $"{_clientNumber++}"};
                    Task t = Process(tcpClient, clientData);
                    ClientConnected?.Invoke(this, clientData);
                    await t;
                }
                catch (Exception)
                {
                    //Just suppress exception
                }
            }
        }

        private async Task Process(TcpClient tcpClient, ClientEventArg clientData)
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
                        _handler.ExecuteCommand(request);
                    }
                    else
                        break; // client closede connection
                }
                tcpClient.Close();
                ClientDisconnected?.Invoke(this, clientData);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                if (tcpClient.Connected)
                {
                    tcpClient.Close();
                    ClientDisconnected?.Invoke(this, clientData);
                }
            }
        }

        public void Stop()
        {
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
