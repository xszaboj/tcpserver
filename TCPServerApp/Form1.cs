using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Shared;
using Shared.Events;
using TCPServerApp.Properties;

namespace TCPServerApp
{
    public partial class Form1 : Form
    {
        private readonly TcpServer _server;
        public Form1()
        {
            InitializeComponent();
            _server = new TcpServer();
            _server.Start();
            statusValue.Text = Resources.Started;
            Hide();
            WindowState = FormWindowState.Minimized;
            RegisterEvents(_server);
        }

        private void RegisterEvents(TcpServer server)
        {
            server.ClientConnected += ServerOnClientConnected;
            server.ClientDisconnected += ServerOnClientDisconnected;
        }

        private void ServerOnClientDisconnected(TcpServer tcpServer, ClientEventArg clientData)
        {
            dataLog.AppendText(AddLine($"Client number {clientData.Name} just disconnected."));
        }

        private void ServerOnClientConnected(TcpServer tcpServer, ClientEventArg clientData)
        {
            dataLog.AppendText(AddLine($"Client number {clientData.Name} just connected."));
        }

        private string AddLine(string message)
        {
            return $"{Environment.NewLine} {DateTime.Now} - {message}";
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
                Hide();
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //Start
            _server.Start();
            statusValue.Text = Resources.Started;
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Stop
            _server.Stop();
            statusValue.Text = Resources.Stoped;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _server.Stop();
            Close();
        }
    }
}
