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

        private static Console console;

        public Console() : base()
        {
            InitializeComponent();
            StartUp();

            console = this;
            this.FormClosed += exitHandling;
        }

        private void StartUp()
        {
            Deserialize_Config();

            this.Size = new Size(programData.Console_SizeX, programData.Console_SizeY);
            this.Location = new Point(programData.Console_PosX, programData.Console_PosY);
            fontSize = programData.Console_FontSize;
            this.Font = new Font("Consolas", fontSize);
        }

        public void GetAnnouncement()
        {
            WebHandler web = new WebHandler();
            string url = "https://raw.githubusercontent.com/TDToolbox/BTDToolbox-2019_LiveFIles/master/toolbox%20announcements";
            string answer = web.WaitOn_URL(url);
            output_log.SelectionColor = Color.OrangeRed;

            appendLog("Announcement: " + answer);
        }

        public override void close_button_Click(object sender, EventArgs e)
        {
            Serializer.SaveConfig(this, "console", programData);
            this.Hide();
        }

        public void appendLog(String log)
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
                catch(Exception)
                {
                   Environment.Exit(0);
                }
                
            }

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
