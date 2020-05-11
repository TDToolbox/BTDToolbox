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

namespace BTDToolbox.Extra_Forms
{
    public partial class First_Time_Use : ThemedForm
    {
        public First_Time_Use()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;

            int sizeX = 450;
            int sizeY = 550;
            this.Location = new Point(GeneralMethods.GetCenterScreen().X - sizeX/2 - 75, GeneralMethods.GetCenterScreen().Y - sizeY/ 2 - 145);
            
            this.Refresh();
            Serializer.cfg.ExistingUser = true;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void WatchTut_Button_Click(object sender, EventArgs e)
        {
            Browser browser = new Browser(Main.getInstance(), "https://youtu.be/nY9Cfe2O_XI?list=PLWFKnf1pcvUuGt2UQO7E5xNfVdn-IY_AQ&t=238");
            this.Close();
        }
    }
}
