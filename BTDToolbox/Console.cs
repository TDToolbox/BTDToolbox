using System;
using System.Windows.Forms;

namespace BTDToolbox
{
    public partial class Console : Form
    {
        public Console()
        {
            InitializeComponent();
        }

        private void Console_Load(object sender, EventArgs e)
        {
        }
        private void Console_Close(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        public void appendLog(String log)
        {
            console_log.Items.Add(log);
        }
    }
}
