using BTDToolbox.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTDToolbox.Extra_Forms
{
    public partial class NKHook_Message : ThemedForm
    {
        public NKHook_Message()
        {
            InitializeComponent();
            this.MdiParent = Main.getInstance();

            canResize = false;
            this.Size = new Size(800, 550);

            Serializer.nkhookMsgShown = true;
            Serializer.SaveSmallSettings("nkhookMSG");
        }
        private void GetNKHook_Button_Click(object sender, EventArgs e)
        {
            NKHook.OpenMainWebsite();
            this.Close();
        }

        private void NKHook_Message_Shown(object sender, EventArgs e)
        {
            var center = GeneralMethods.GetCenterScreen();
            this.Location = new Point(center.X - (this.Size.Width / 2), center.Y - (this.Size.Height / 2) - 100);
        }
    }
}
