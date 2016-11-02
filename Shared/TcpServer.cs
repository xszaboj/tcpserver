using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Shared.TCPWrappers;

namespace Shared
{
    public class TcpServer
    {
        // Thread signal.
        public static ManualResetEvent AllDone = new ManualResetEvent(false);
        private Socket _listener;
        private ClientHandler _clientHandler;

        public void Start()
        {
            _listener = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
            _clientHandler = new ClientHandler();
            try
            {
                _listener.Bind(GetIP());
                _listener.Listen(100);
                while (true)
                {
                    // Set the event to nonsignaled state.
                    AllDone.Reset();
                    _listener.BeginAccept(
                        new AsyncCallback(AcceptCallback),
                        _listener);

                    // Wait until a connection is made before continuing.
                    AllDone.WaitOne();
                }
            }
            catch (Exception e)
            {
                //TODO log error
            }
        }

        public void Stop()
        {
            _listener.Close();
        }

        private IPEndPoint GetIP()
        {
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            return new IPEndPoint(ipAddress, 8889);
        }

        private void AcceptCallback(IAsyncResult ar)
        {
            // Signal the main thread to continue.
            AllDone.Set();

            // Get the socket that handles the client request.
            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);

            // Create the state object.
            StateObject state = new StateObject();
            state.workSocket = handler;
            handler.BeginReceive(state.buffer, 0, 65537, 0,
                new AsyncCallback(_clientHandler.DoChat), state);
        }

        public class StateObject
        {
            // Client  socket.
            public Socket workSocket = null;
            // Size of receive buffer.
            public const int BufferSize = 65537;
            // Receive buffer.
            public byte[] buffer = new byte[BufferSize];

            public const char SplitCharacter = '$';
        }
    }
}
