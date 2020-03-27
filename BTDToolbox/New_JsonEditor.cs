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
        JsonEditor_UserControl uc;
        TabPage tp;

        bool loaded = false;
        public static int JsonEditor_Width = 0;
        public static int JsonEditor_Height = 0;
        public static JsonEditor_UserControl[] userControls;
        public New_JsonEditor()
        {
            InitializeComponent();
            this.MdiParent = Main.getInstance();

            JsonEditor_Width = tabControl1.Width;
            JsonEditor_Height = tabControl1.Height;

            userControls = new JsonEditor_UserControl[0];

            for (int x = 0; x < 3; x++)
            {
                uc = new JsonEditor_UserControl();
                tp = new TabPage();
                Array.Resize(ref userControls, userControls.Length + 1);
                userControls[userControls.Length - 1] = uc;

                //uc.Size = new Size(tp.Width, tp.Height);
                tp.Controls.Add(uc);
                this.tabControl1.TabPages.Add(tp);
                //tabControl1.Resize += uc.ResizeEvent;
            }
        }
        private void New_JsonEditor_KeyDown(object sender, KeyEventArgs e)
        {
            /*if (e.Control && e.KeyCode == Keys.F)
            {
                FindReplace_Panel.Visible = !FindReplace_Panel.Visible;
                FindReplace_Panel.BringToFront();
            }*/
        }
        private void TabControl1_SizeChanged(object sender, EventArgs e)
        {
            //ConsoleHandler.appendLog_CanRepeat("sizechanged");
            if (userControls != null)
            {
                foreach (var x in userControls)
                {
                    x.Size = new Size(tabControl1.SelectedTab.Width, tabControl1.SelectedTab.Height);
                }
            }
        }
    }
}
