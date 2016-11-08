using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MouseProject;
using Shared.Commands;
using Shared.Commands.Data;
using Shared.Extensions;
using Shared.Factories;
using Shared.TCPWrappers;
using TCPServer;

namespace Shared
{
    public class ClientHandler
    {
    
        private readonly MouseManager _manager;
        private readonly CommandFactory _commandFactory;
        private readonly CommandDataFactory _commandDataFactory;

        public ClientHandler()
        {
            _commandDataFactory = new CommandDataFactory();
            _commandFactory = new CommandFactory();
            _manager = new MouseManager();
        }


        public void ExecuteCommand(string data)
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
    }
}
