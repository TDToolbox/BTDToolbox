using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTDToolbox
{
    public partial class ThemedForm : Form
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

        public ThemedForm()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            //this.Dock = DockStyle.Fill;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            //this.FormBorderStyle = FormBorderStyle.None;

            

            TitleLabel.MouseMove += ToolbarDrag;
            titleSeperator.Panel1.MouseMove += ToolbarDrag;
            titleSeperator.Panel2.MouseMove += ToolbarDrag;
            titleSeperator.MouseMove += ToolbarDrag;

            Sizer.MouseDown += SizerMouseDown;
            Sizer.MouseMove += SizerMouseMove;
            Sizer.MouseUp += SizerMouseUp;

            close_button.Click += close_button_Click;
            this.Load += onThemedLoad;
        }
        public void onThemedLoad(object sender, EventArgs e)
        {
            titleSeperator.SplitterDistance = 25;
        }

        public virtual void close_button_Click(object sender, EventArgs e)
        {
            if (JsonEditor.jsonError != true)
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
            if ((MousePosition.X - Mx + Sw) >= minWidth && (MousePosition.Y - My + Sh) >= minHeight)
            {
                if (mov == true)
                {
                    titleSeperator.SplitterDistance = 25;
                    //splitContainer1.Anchor = (AnchorStyles.Top|AnchorStyles.Left|AnchorStyles.Bottom|AnchorStyles.Right);
                    //titleSeperator.Dock = DockStyle.Fill;
                    Width = MousePosition.X - Mx + Sw;
                    Height = MousePosition.Y - My + Sh;
                }
            }
            else if ((MousePosition.X - Mx + Sw) >= minWidth && (MousePosition.Y - My + Sh) < minHeight)
            {
                if (mov == true)
                {
                    titleSeperator.SplitterDistance = 25;
                    Width = MousePosition.X - Mx + Sw;
                    Height = minHeight;
                }
            }
            else if ((MousePosition.X - Mx + Sw) < minWidth && (MousePosition.Y - My + Sh) >= minHeight)
            {
                if (mov == true)
                {
                    titleSeperator.SplitterDistance = 25;
                    Height = MousePosition.Y - My + Sh;
                    Width = minWidth;
                }
            }
            else
            {
                titleSeperator.SplitterDistance = 25;
                Height = minHeight;
                Width = minWidth;
            }
        }
        private bool CheckInBounds()
        {
            
            return true;
        }
        private void SizerMouseUp(object sender, MouseEventArgs e)
        {
            mov = false;
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

        private void ThemedForm_Resize(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
                Invoke((MethodInvoker)delegate { Main.getInstance().Refresh(); });
            else
                Main.getInstance().Refresh();

        }
    }
}
