using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace TCPServerService
{
    public partial class MyTcpService : ServiceBase
    {
        private TcpServer _server;
        public MyTcpService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            DebugMode();
            _server = new TcpServer();
            _server.Start();
        }

        protected override void OnStop()
        {
            _server.Stop();
        }

        [Conditional("DEBUG_SERVICE")]
        private static void DebugMode()
        {
            Debugger.Break();
        }
    }
}
