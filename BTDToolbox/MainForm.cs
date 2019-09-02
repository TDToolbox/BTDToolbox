using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTDToolbox
{
    public partial class TD_Toolbox_Window : Form
    {
        public TD_Toolbox_Window()
        {
            InitializeComponent();
        }

        private void TD_Toolbox_Window_Load(object sender, EventArgs e)
        {

        }

        private void newJetWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            JetForm jf = new JetForm();
            jf.MdiParent = this;
            jf.Show();
        }

        private void jetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDiag = new OpenFileDialog();
            fileDiag.Title = "Open .jet";
            fileDiag.DefaultExt = "jet";
            fileDiag.Filter = "Jet files (*.jet)|*.jet|All files (*.*)|*.*";
            fileDiag.FileOk += jetOpen;
            fileDiag.ShowDialog();
        }

        private void jetOpen(object sender, CancelEventArgs e)
        {
            JetForm jf = new JetForm();
            jf.MdiParent = this;
            jf.Show();
        }
    }
}
