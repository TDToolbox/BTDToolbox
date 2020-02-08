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

namespace BTDToolbox
{
    public partial class SplashScreen : CSWinFormLayeredWindow.PerPixelAlphaForm
    {
        public int topLeft_corner_X { get; set; }
        public int topLeft_corner_Y { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        Timer tmr;

        public SplashScreen()
        {
            InitializeComponent();
            
            var splashImg = Properties.Resources.Splash1;
            this.SelectBitmap(splashImg);

            int splash_centerX = splashImg.Width/2;
            int splash_centerY = splashImg.Height/2;

            

            this.Top = (GeneralMethods.GetCenterScreen().Y) - splash_centerY - 55;
            this.Left = (GeneralMethods.GetCenterScreen().X) - splash_centerX;


        }
        private void SplashScreen_Shown(object sender, EventArgs e)
        {
            tmr = new Timer();

            tmr.Interval = 3000;
            tmr.Start();
            tmr.Tick += tmr_Tick;
        }
        void tmr_Tick(object sender, EventArgs e)

        {
            tmr.Stop();

            Main mf = new Main();
            mf.Show();
            this.Hide();

        }
        protected override void WndProc(ref Message m)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_MOVE = 0xF010;

            switch (m.Msg)
            {
                case WM_SYSCOMMAND:
                    int command = m.WParam.ToInt32() & 0xfff0;
                    if (command == SC_MOVE)
                        return;
                    break;
            }
            base.WndProc(ref m);
        }
    }
}
