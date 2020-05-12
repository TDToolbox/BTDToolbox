using BTDToolbox.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Animation;

namespace BTDToolbox.Extra_Forms
{
    public partial class TDLoader_Message : ThemedForm
    {
        public TDLoader_Message()
        {
            InitializeComponent();
            canResize = false;
            moveCenterScreen = true;

            this.MdiParent = Main.getInstance();
            this.Size = new Size(800, 550);
            this.Show();
            this.Location = new Point(this.Location.X + 90, this.Location.Y);
        }

        private void GetNKHook_Button_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.nexusmods.com/bloonstd5/mods/4");
            this.Close();
        }
    }
}
