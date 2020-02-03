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
    public partial class NewConsole : ThemedForm
    {
        //Config variables
        ConfigFile programData;
        public static float consoleLogFont;
        public string lastMessage;
        string livePath = Environment.CurrentDirectory;
        float fontSize;

        private static NewConsole console;

        public NewConsole() : base()
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
                    console_log.Invoke(new Action(() => console_log.AppendText(">> " + log + "\r\n")));
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
        public static NewConsole getInstance()
        {
            return console;
        }
    }
}
