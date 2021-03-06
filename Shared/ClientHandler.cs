﻿using System;
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
    class ClientHandler
    {
    
        private MouseManager _manager;
        private CommandFactory _commandFactory;
        private CommandDataFactory _commandDataFactory;
        private ITcpClient _clientSocket;
        private  static readonly byte[] BytesFrom = new byte[65537];
        private const char SplitCharacter = '$';

        public void StartClient(ITcpClient inClientSocket)
        {

            _manager = new MouseManager();
            _commandFactory = new CommandFactory();
            _commandDataFactory = new CommandDataFactory();
            _clientSocket = inClientSocket;
            Thread ctThread = new Thread(DoChat);
            ctThread.Start();
        }

        private void DoChat()
        {
            bool running = true;
            while (running)
            {
                if (_clientSocket.IsConnected())
                {
                    ProcessIncomingData();
                }
                else
                {
                    running = false;
                }
            }
        }

        private void ProcessIncomingData()
        {
            INetworkStream networkStream = _clientSocket.GetStream();
            if (networkStream.DataAvailable)
            {
                string[] data = ConvertBytesToString(networkStream);
                for (int index = 0; index < data.Length - 1; index++)
                {
                    ExecuteCommand(data[index]);
                }
            }
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
            var originalX = _manager.GetX();
            var originalY = _manager.GetY();
            return new ReceivedData(parsedData.X, parsedData.Y, parsedData.Action);
        }

        private string[] ConvertBytesToString(INetworkStream networkStream)
        {
            int bytesRead = networkStream.Read(BytesFrom, 0, _clientSocket.ReceiveBufferSize);
            string dataFromClient = Encoding.ASCII.GetString(BytesFrom, 0, bytesRead);
            return dataFromClient.Split(SplitCharacter);
        }
    }
}
