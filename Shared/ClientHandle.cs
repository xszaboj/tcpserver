using System;
using System.Net.Sockets;
using System.Text;
using MouseProject;
using Shared.Commands;
using Shared.Commands.Data;
using Shared.Factories;

namespace Shared
{
    public class ClientHandler
    {
    
        private readonly MouseManager _manager;
        private readonly CommandFactory _commandFactory;
        private readonly CommandDataFactory _commandDataFactory;

        public ClientHandler()
        {
            _manager = new MouseManager();
            _commandFactory = new CommandFactory();
            _commandDataFactory = new CommandDataFactory();
        }

        public void DoChat(IAsyncResult res)
        {
            var data = ProcessIncomingData(res);
            foreach (var s in data)
            {
                ExecuteCommand(s); 
            }
        }

        private string[] ProcessIncomingData(IAsyncResult res)
        {
            // Retrieve the state object and the handler socket
            // from the asynchronous state object.
            TcpServer.StateObject state = (TcpServer.StateObject)res.AsyncState;
            Socket handler = state.workSocket;

            // Read data from the client socket. 
            int bytesRead = handler.EndReceive(res);
            if (bytesRead > 0)
            {
                return ConvertBytesToString(state.buffer, bytesRead, '$');
            }
            return new string[0];
        }

        private void ExecuteCommand(string data)
        {
            ReceivedData recdata = GetReceivedData(data);
            ICommandData commandData =
                _commandDataFactory.GetData(recdata);
            ICommand command = _commandFactory.GetCommand(recdata.DataType, _manager, commandData);
            command.Execute();
        }

        private ReceivedData GetReceivedData(string data)
        {
            ParsedData parsedData = ParsedData.ParseData(data);
            return new ReceivedData(parsedData.X, parsedData.Y, parsedData.Action);
        }

        private string[] ConvertBytesToString(byte[] bytes, int length, char split)
        {
            string dataFromClient = Encoding.ASCII.GetString(bytes, 0, length);
            return dataFromClient.Split(split);
        }
    }
}
