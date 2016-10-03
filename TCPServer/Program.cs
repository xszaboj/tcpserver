using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MouseProject;
using TouchApp.MySpace;

namespace TCPServer
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpListener serverSocket = new TcpListener(8889);
            TcpClient clientSocket = default(TcpClient);
            int counter = 0;

            serverSocket.Start();
            Console.WriteLine(" >> " + "Server Started");

            counter = 0;
            while (true)
            {
                counter += 1;
                clientSocket = serverSocket.AcceptTcpClient();
                Console.WriteLine(" >> " + "Client No:" + Convert.ToString(counter) + " started!");
                handleClinet client = new handleClinet();
                client.startClient(clientSocket, Convert.ToString(counter));
            }

            clientSocket.Close();
            serverSocket.Stop();
            Console.WriteLine(" >> " + "exit");
            Console.ReadLine();
        }

        //Class to handle each client request separatly
        public class handleClinet
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

                while ((true))
                {
                    try
                    {
                        if (clientSocket.Connected && clientSocket.Client.IsConnected())
                        {
                            requestCount = requestCount + 1;
                            NetworkStream networkStream = clientSocket.GetStream();
                            if (networkStream.DataAvailable)
                            {
                                int bytesRead = networkStream.Read(bytesFrom, 0, (int) clientSocket.ReceiveBufferSize);
                                dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom, 0, bytesRead);
                                var data = dataFromClient.Split('$');
                                for (int index = 0; index < data.Length-1; index++)
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
                                            case TouchEnum.Single:
                                            _manager.MoveCursor(originalX + x, originalY + y);
                                            break;
                                            case TouchEnum.Multi:
                                            _manager.Scroll(y);
                                            break;
                                            case TouchEnum.SingleClick:
                                            _manager.Click();
                                            break;
                                            case TouchEnum.RightClick:
                                            _manager.RightClick();
                                            break;
                                            case TouchEnum.DoubleClick:
                                            _manager.DoubleClick();
                                            break;
                                    }
                                    
                                    Console.WriteLine(" >> " + "From client-{0}-{1}", clNo, s);
                                }
                            }
                            /*rCount = Convert.ToString(requestCount);
                            serverResponse = "Server to clinet(" + clNo + ") " + rCount;
                            sendBytes = Encoding.ASCII.GetBytes(serverResponse);
                            networkStream.Write(sendBytes, 0, sendBytes.Length);
                            networkStream.Flush();
                            Console.WriteLine(" >> " + serverResponse);*/
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(" >> " + ex.ToString());
                    }
                }
            }
        } 
    }
}
