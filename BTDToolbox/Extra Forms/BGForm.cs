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
    public partial class BGForm : Form
    {
        private Bitmap bgImg;
        private Size bgSize;
        public BGForm()
        {
            InitializeComponent();

            Random rand = new Random();
            switch (rand.Next(0, 2))
            {
                case 0:
                    bgImg = Properties.Resources.Logo1;
                    break;
                case 1:
                    bgImg = Properties.Resources.Logo2;
                    break;
            }

            bgSize = bgImg.Size;
            this.Size = bgSize;
            this.BackgroundImage = bgImg;

            int splash_centerX = Size.Width / 2;
            int splash_centerY = Size.Height / 2;



            this.Top = (GeneralMethods.GetCenterScreen().Y) - splash_centerY-100;
            this.Left = (GeneralMethods.GetCenterScreen().X) - splash_centerX-100;
            /*Properties.Resources.
            
            Point center = GeneralMethods.GetCenterScreen();*/

        }

        private void BGForm_MouseClick(object sender, MouseEventArgs e)
        {
            this.SendToBack();
        }

        private void BGForm_MouseDown(object sender, MouseEventArgs e)
        {
            this.SendToBack();
        }
    }
}
