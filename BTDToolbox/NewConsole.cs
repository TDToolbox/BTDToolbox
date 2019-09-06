using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BTDToolbox.ProjectConfigs;

namespace BTDToolbox
{
    public partial class NewConsole : Form
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

        //Config variables
        Window consoleForm;
        public static float consoleLogFont;
        string livePath = Environment.CurrentDirectory;
        string consoleFormOutput;


        public NewConsole()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;

            splitContainer1.Panel1.MouseMove += ToolbarDrag;
            splitContainer1.Panel2.MouseMove += ToolbarDrag;
            splitContainer1.MouseMove += ToolbarDrag;

            Sizer.MouseDown += SizerMouseDown;
            Sizer.MouseMove += SizerMouseMove;
            Sizer.MouseUp += SizerMouseUp;

            try
            {
                string json = File.ReadAllText(livePath + "\\config\\console_form.json");
                Window deserializedConsoleForm = JsonConvert.DeserializeObject<Window>(json);

                Size ConsoleFormSize = new Size(deserializedConsoleForm.SizeX, deserializedConsoleForm.SizeY);
                this.Size = ConsoleFormSize;

                this.StartPosition = FormStartPosition.Manual;
                this.Location = new Point(deserializedConsoleForm.PosX, deserializedConsoleForm.PosY);

                consoleLogFont = deserializedConsoleForm.FontSize;
                console_log.Font = new Font("Microsoft Sans Serif", consoleLogFont);
            }
            catch (System.IO.FileNotFoundException)
            {
                consoleForm = new Window("Console", this.Size.Width, this.Size.Height, this.Location.X, this.Location.Y, 10);
                consoleFormOutput = JsonConvert.SerializeObject(consoleForm);

                StreamWriter writeConsoleForm = new StreamWriter(livePath + "\\config\\console_form.json", false);
                writeConsoleForm.Write(consoleFormOutput);
                writeConsoleForm.Close();
            }
            catch (System.ArgumentException)
            {
                console_log.Font = new Font("Microsoft Sans Serif", 10);
            }

            close_button.Click += close_button_Click;
            this.FormClosed += exitHandling;
        }

        private void close_button_Click(object sender, EventArgs e)
        {
            serializeConfig();
            this.Hide();
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

        public void appendLog(String log)
        {
            console_log.Text += log + "\r\n";
        }

        private void serializeConfig()
        {
            consoleForm = new Window("Console", this.Size.Width, this.Size.Height, this.Location.X, this.Location.Y, consoleLogFont);
            consoleFormOutput = JsonConvert.SerializeObject(consoleForm);

            StreamWriter writeConsoleForm = new StreamWriter(livePath + "\\config\\console_form.json", false);
            writeConsoleForm.Write(consoleFormOutput);
            writeConsoleForm.Close();
        }
        private void exitHandling(object sender, EventArgs e)
        {
            serializeConfig();
        }
    }
}
