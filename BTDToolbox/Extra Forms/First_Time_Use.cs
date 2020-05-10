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
    public partial class First_Time_Use : ThemedForm
    {
        public First_Time_Use() : base()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;

            int sizeX = 450;
            int sizeY = 550;
            this.Location = new Point(GeneralMethods.GetCenterScreen().X - sizeX/2 - 75, GeneralMethods.GetCenterScreen().Y - sizeY/ 2 - 145);
            
            this.Refresh();
            //this.Size = new Size(sizeX, sizeY);
            
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
