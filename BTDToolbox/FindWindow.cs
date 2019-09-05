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
    public partial class FindWindow : Form
    {
        public bool find;
        public bool replace;
        public string windowName;
        public FindWindow()
        {

            InitializeComponent();
            if (find == true)
            {
                this.Text = toolStripButton1.Text;
            }

        }
        public event EventHandler SearchJsonEditor;
        private void ToolStripButton1_Click(object sender, EventArgs e)
        {
            find = true;
            replace = false;
            this.Text = toolStripButton1.Text;
        }

        private void ToolStripButton2_Click(object sender, EventArgs e)
        {
            find = false;
            replace = true;
            this.Text = toolStripButton2.Text;
        }
        
        void Button1_Click(object sender, EventArgs e)
        {
            if (SearchJsonEditor != null)
            {
                SearchJsonEditor(sender, e);
            }
        }
    }
}
