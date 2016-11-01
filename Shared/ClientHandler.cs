using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MouseProject;
using TCPServer;

namespace Shared
{
    class ClientHandler
    {
    
        private MouseManager _manager;
        TcpClient clientSocket;
        string clNo;
        public void startClient(TcpClient inClientSocket, string clineNo)
        {

            _manager = new MouseManager();
            this.clientSocket = inClientSocket;
            this.clNo = clineNo;
            Thread ctThread = new Thread(doChat);
            ctThread.Start();
        }

        private void doChat()
        {
            int requestCount = 0;
            byte[] bytesFrom = new byte[65537];
            string dataFromClient = null;
            Byte[] sendBytes = null;
            string serverResponse = null;
            string rCount = null;
            requestCount = 0;
            bool running = true;

            while (running)
            {
                try
                {
                    //TODO separate into testable class
                    if (clientSocket.Connected && clientSocket.Client.IsConnected())
                    {
                        requestCount = requestCount + 1;
                        NetworkStream networkStream = clientSocket.GetStream();
                        if (networkStream.DataAvailable)
                        {
                            int bytesRead = networkStream.Read(bytesFrom, 0, (int)clientSocket.ReceiveBufferSize);
                            dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom, 0, bytesRead);
                            var data = dataFromClient.Split('$');
                            for (int index = 0; index < data.Length - 1; index++)
                            {
                                var s = data[index];
                                string[] coords = s.Split('|');
                                int x = Int32.Parse(coords[0]);
                                int y = Int32.Parse(coords[1]);
                                TouchEnum action = (TouchEnum)Enum.Parse(typeof(TouchEnum), coords[2]);
                                var originalX = _manager.GetX();
                                var originalY = _manager.GetY();
                                switch (action)
                                {
                                    case TouchEnum.Move:
                                        _manager.MoveCursor(originalX + x, originalY + y);
                                        break;
                                    case TouchEnum.Scroll:
                                        _manager.Scroll(y);
                                        break;
                                    case TouchEnum.SingleClick:
                                        _manager.Click();
                                        break;
                                    case TouchEnum.SingleClickDown:
                                        _manager.ClickDown();
                                        break;
                                    case TouchEnum.SingleClickUp:
                                        _manager.ClickUp();
                                        break;
                                    case TouchEnum.RightClick:
                                        _manager.RightClick();
                                        break;
                                    case TouchEnum.DoubleClick:
                                        _manager.DoubleClick();
                                        break;
                                }
                                //TODO log info
                            }
                        }
                    }
                    else
                    {
                        running = false;
                    }
                }
                catch (Exception ex)
                {
                    //TODO log exception
                }
            }
        }
    }
}
