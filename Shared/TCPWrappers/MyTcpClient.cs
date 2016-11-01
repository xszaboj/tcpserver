using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Shared.Extensions;

namespace Shared.TCPWrappers
{
    public class MyTcpClient :ITcpClient
    {
        private readonly TcpClient _client;
        private INetworkStream _networkStream;
        public MyTcpClient(TcpClient client)
        {
            _client = client;
        }

        public bool IsConnected()
        {
            return _client.IsConnected();
        }

        public INetworkStream GetStream()
        {
            if (_networkStream != null) return _networkStream;
            _networkStream = new MyNetworkStream(_client.GetStream());
            return _networkStream;
        }

        public int ReceiveBufferSize
        {
            get { return _client.ReceiveBufferSize; }
        }

        public void Close()
        {
            _client.Close();
        }
    }
}
