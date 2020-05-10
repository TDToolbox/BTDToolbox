using BTDToolbox.Classes;
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
using static BTDToolbox.ProjectConfig;

namespace BTDToolbox
{
    public partial class Console : ThemedForm
    {
        //Config variables
        ConfigFile programData;
        public static float consoleLogFont;
        public string lastMessage;
        string livePath = Environment.CurrentDirectory;
        float fontSize;
        public bool CanRepeat = false;
        int errorCount = 0;
        private static Console console;

        public Console() : base()
        {
            InitializeComponent();
            StartUp();

            console = this;
            this.FormClosed += exitHandling;
            tabControl1.TabPages[1].Text = "Errors (" + errorCount + ")";
        }

        private void StartUp()
        {
            Deserialize_Config();
            if (programData.ExistingUser == false)
            {
                this.StartPosition = FormStartPosition.Manual;
                Rectangle resolution = Screen.PrimaryScreen.Bounds;

                int x = resolution.Width;
                int y = resolution.Height;
                int sizeX = x / 2;
                int sizeY = y / 5;

                this.Size = new Size(sizeX, sizeY);
                this.Location = new Point(x - sizeX, y - this.Height - 90);
            }
            else
            {
                this.Size = new Size(programData.Console_SizeX, programData.Console_SizeY);
                this.Location = new Point(programData.Console_PosX, programData.Console_PosY);
            }
            fontSize = programData.Console_FontSize;
            this.Font = new Font("Consolas", fontSize);
        }

        public void GetAnnouncement()
        {
            WebHandler web = new WebHandler();
            string url = "https://raw.githubusercontent.com/TDToolbox/BTDToolbox-2019_LiveFIles/master/toolbox%20announcements";
            try
            {
                string answer = web.WaitOn_URL(url);
                output_log.SelectionColor = Color.OrangeRed;

                if (answer.Length > 0 && answer != null)
                    append("Announcement: " + answer);
                else
                    append("Failed to read announcement...");
            }
            catch
            {
                append_Notice("Something went wrong.. Failed to read announcements...");
            }
        }
        public void append_Notice(string notice)
        {
            Invoke((MethodInvoker)delegate {
                output_log.SelectionColor = Color.Yellow;
                append("Notice: " + notice);
            });   
        }
        public void force_append_Notice(string notice)
        {
                Invoke((MethodInvoker)delegate {
                    output_log.SelectionColor = Color.Yellow;
                    append_Force("Notice: " + notice);
                });               
        }
        public override void close_button_Click(object sender, EventArgs e)
        {
            Serializer.SaveConfig(this, "console");
            this.Hide();
        }


        public void append(String log)
        {
            
        }
        public void append(String log, bool canRepeat)
        {
            
        }
        public void append(String log, bool canRepeat, bool force) => append(log, canRepeat, force, false);
        

        public void append(String log, bool canRepeat, bool force, bool notice)
        {
            if (!canRepeat && log == lastMessage)
                return;

            DateTime now = DateTime.Now;
            string currentTime = now.Hour + ":" + now.Minute + ":" + now.Second;

            try
            {
                Invoke((MethodInvoker)delegate {
                    output_log.AppendText("" + currentTime + " - " + ">> " + log + "\r\n");
                    output_log.ScrollToCaret();
                });

                lastMessage = log;
            }
            catch (Exception)
            {

                Environment.Exit(0);
            }
        }

        public void append_Force(String log)
        {
            Invoke((MethodInvoker)delegate {
                if (this.Visible == false)
                    this.Visible = true;
                this.BringToFront();

                append(log);
            });
        }
        private void exitHandling(object sender, EventArgs e)
        {
            Serializer.SaveConfig(this, "console");
        }
        private void Deserialize_Config()
        {
            programData = Serializer.Deserialize_Config();
        }
        public static Console getInstance()
        {
            return console;
        }

        private void ExportLog_Button_Click(object sender, EventArgs e)
        {
            ConsoleHandler.append_CanRepeat("Copied console log to clipboard.");
            Clipboard.SetText(output_log.Text);
        }
    }
}
