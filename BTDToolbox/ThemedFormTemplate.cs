using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTDToolbox
{
    public partial class ThemedFormTemplate : Form
    {
        //Low level for toolbar dragging
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;
        [DllImport("User32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        //for resizing
        int Mx;
        int My;
        int Sw;
        int Sh;
        bool mov;

        //Resize defaults
        int minWidth = 200;
        int minHeight = 100;

        public ThemedFormTemplate()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;

            splitContainer1.Panel1.MouseMove += ToolbarDrag;
            splitContainer1.Panel2.MouseMove += ToolbarDrag;
            splitContainer1.MouseMove += ToolbarDrag;

            Sizer.MouseDown += SizerMouseDown;
            Sizer.MouseMove += SizerMouseMove;
            Sizer.MouseUp += SizerMouseUp;

            close_button.Click += close_button_Click;
        }

        private void close_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //resizing event methods
        private void SizerMouseDown(object sender, MouseEventArgs e)
        {
            mov = true;
            My = MousePosition.Y;
            Mx = MousePosition.X;
            Sw = Width;
            Sh = Height;
        }
        private void SizerMouseMove(object sender, MouseEventArgs e)
        {
            if (mov == true)
            {
                splitContainer1.SplitterDistance = 25;
                //splitContainer1.Anchor = (AnchorStyles.Top|AnchorStyles.Left|AnchorStyles.Bottom|AnchorStyles.Right);
                splitContainer1.Dock = DockStyle.Fill;
                Width = MousePosition.X - Mx + Sw;
                Height = MousePosition.Y - My + Sh;
            }
        }
        private void SizerMouseUp(object sender, MouseEventArgs e)
        {
            mov = false;
            if (Width < minWidth)
            {
                Width = minWidth;
            }
            if (Height < minHeight)
            {
                Height = minHeight;
            }
        }

        //toolbar drag method
        private void ToolbarDrag(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }
    }
}
