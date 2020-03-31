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

            int i = 0;
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
                    appendLog("Announcement: " + answer);
                else
                    appendLog("Failed to read announcement...");
            }
            catch
            {
                appendNotice("Something went wrong.. Failed to read announcements...");
            }
        }
        public void appendNotice(string notice)
        {
            Invoke((MethodInvoker)delegate {
                output_log.SelectionColor = Color.Yellow;
                appendLog("Notice: " + notice);
            });   
        }
        public void force_appendNotice(string notice)
        {
                Invoke((MethodInvoker)delegate {
                    output_log.SelectionColor = Color.Yellow;
                    force_appendLog("Notice: " + notice);
                });               
        }
        public override void close_button_Click(object sender, EventArgs e)
        {
            Serializer.SaveConfig(this, "console", programData);
            this.Hide();
        }

        public void appendLog(String log)
        {
            if (!CanRepeat)
            {
                if (log != lastMessage)
                {
                    try
                    {
                        Invoke((MethodInvoker)delegate {
                            output_log.AppendText(">> " + log + "\r\n");
                            output_log.ScrollToCaret();
                        });

                        lastMessage = log;
                    }
                    catch (Exception)
                    {
                        Environment.Exit(0);
                    }
                }
            }
            else
            {
                try
                {
                    Invoke((MethodInvoker)delegate {
                        output_log.AppendText(">> " + log + "\r\n");
                        output_log.ScrollToCaret();
                    });

                    lastMessage = log;
                }
                catch (Exception)
                {
                    Environment.Exit(0);
                }
            }
        }

        public void force_appendLog(String log)
        {
            Invoke((MethodInvoker)delegate {
                if (this.Visible == false)
                    this.Visible = true;
                this.BringToFront();

                appendLog(log);
            });
        }
        private void exitHandling(object sender, EventArgs e)
        {
            Serializer.SaveConfig(this, "console", programData);
        }
        private void Deserialize_Config()
        {
            programData = Serializer.Deserialize_Config();
        }
        public static Console getInstance()
        {
            return console;
        }
    }
}
