using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCPClient
{
    class Program
    {
        private static bool on = true;
        static readonly TcpClient clientSocket = new TcpClient();
        static void Main(string[] args)
        {
            
            Console.WriteLine("Connecting.....");
            clientSocket.Connect("192.168.0.5", 8889);
            Console.WriteLine("Connected");
            NetworkStream serverStream = clientSocket.GetStream();
            while (on)
            {
                try
                {
                    Console.Write("Enter the string to be transmitted : ");
                    String str = Console.ReadLine();
                    sendMessage(str, serverStream);
                }

                catch (Exception e)
                {
                    Console.WriteLine("Error..... " + e.StackTrace);
                }
            }
        }

        private static void sendMessage(string str, NetworkStream serverStream)
        {
            if (str == "close")
            {
                clientSocket.Close();
                on = false;
            }
            byte[] outStream = GoData();
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();
            /*byte[] inStream = new byte[10025];
            serverStream.Read(inStream, 0, (int)clientSocket.ReceiveBufferSize);
            string returndata = Encoding.ASCII.GetString(inStream);
            Console.WriteLine("Data from Server : " + returndata);*/
        }


        static byte[] GoData()
        {

       

            List<Byte> res= new List<byte>();
            for(int i = 0; i < 1024 * 1024; i++)
                res.Add(255);

           return res.ToArray();
        }
    }
}
