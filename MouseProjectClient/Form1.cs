using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MouseProject;

namespace MouseProjectClient
{
    public partial class Form1 : Form
    {
        private MouseManager _mouseManager;
        Timer t1 = new Timer();
        public Form1()
        {
            InitializeComponent();
            _mouseManager = new MouseManager();
            t1.Interval = 50;
            t1.Tick += new EventHandler(timer1_Tick);
            t1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int x = Int32.Parse(this.xcord.Text);
            int y = Int32.Parse(this.ycord.Text);
            _mouseManager.MoveCursor(x, y);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            xcordr.Text = Cursor.Position.X.ToString();
            ycordr.Text = Cursor.Position.Y.ToString();
        }
    }
}
