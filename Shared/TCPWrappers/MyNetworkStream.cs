using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Shared.TCPWrappers
{
    public class MyNetworkStream : INetworkStream
    {
        private readonly NetworkStream _networkStream;

        public MyNetworkStream(NetworkStream networkStream)
        {
            _networkStream = networkStream;
        }

        public int Read(byte[] buffer, int offset, int size)
        {
            return _networkStream.Read(buffer, offset, size);
        }

        public bool DataAvailable
        {
            get { return _networkStream.DataAvailable; }
        }
    }
}
