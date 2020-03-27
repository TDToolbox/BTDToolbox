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
    public partial class New_JsonEditor : ThemedForm
    {
        public New_JsonEditor()
        {
            InitializeComponent();
            this.MdiParent = Main.getInstance();
        }

        private void New_JsonEditor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.F)
            {
                FindReplace_Panel.Visible = !FindReplace_Panel.Visible;
            }
        }
    }
}
