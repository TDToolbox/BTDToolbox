using BTDToolbox.Classes;
using Microsoft.VisualBasic.Logging;
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

namespace BTDToolbox
{
    public partial class Console : ThemedForm
    {
        int errorCount = 0;

        private string lastMessage;
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
            if (Serializer.cfg.ExistingUser == false)
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
                this.Size = new Size(Serializer.cfg.Console_SizeX, Serializer.cfg.Console_SizeY);
                this.Location = new Point(Serializer.cfg.Console_PosX, Serializer.cfg.Console_PosY);
            }
            this.Font = new Font("Consolas", Serializer.cfg.Console_FontSize);
        }
        public static Console getInstance()
        {
            return console;
        }


        public void append(String log, bool canRepeat, bool force, bool notice)
        {
            if (!canRepeat && log == lastMessage) return;

            DateTime now = DateTime.Now;

            string secondSeperator = ":";
            if (now.Second < 10)
                secondSeperator = ":0";
            
            string currentTime = now.Hour + ":" + now.Minute + secondSeperator + now.Second;
            
            try
            {
                WriteToLogFile("" + currentTime + " - " + ">> " + log + "\r\n");
                Invoke((MethodInvoker)delegate
                {
                    if (force) { Visible = true; BringToFront(); }
                    if (notice) output_log.SelectionColor = Color.Yellow;

                    output_log.AppendText("" + currentTime + " - " + ">> " + log + "\r\n");
                    output_log.ScrollToCaret();
                });

                lastMessage = log;
            }
            catch (Exception e) { try { WriteToLogFile("CRASH DETECTED!   " + currentTime + " - " + ">> " + log + "\r\nException Message:   >>" + e.Message); Environment.Exit(0); } catch { } }
        }
        
        
        public void GetAnnouncement()
        {
            WebHandler web = new WebHandler();
            string url = "https://raw.githubusercontent.com/TDToolbox/BTDToolbox-2019_LiveFIles/master/toolbox%20announcements";

            string answer = web.WaitOn_URL(url);

            Invoke((MethodInvoker)delegate
            {
                output_log.SelectionColor = Color.LawnGreen;
            });


            if (!Guard.IsStringValid(answer))
                ConsoleHandler.append("Failed to read announcement...");

            ConsoleHandler.append("Announcement: " + answer);
        }


        private void ExportLog_Button_Click(object sender, EventArgs e)
        {
            ConsoleHandler.append_CanRepeat("Copied console log to clipboard.");
            Clipboard.SetText(output_log.Text);
        }


        private void exitHandling(object sender, EventArgs e)
        {
            Serializer.SaveSettings();
        }

        public override void close_button_Click(object sender, EventArgs e)
        {
            ConsoleHandler.append("Hiding console.");
            Serializer.SaveSettings();
            this.Hide();
        }

        private bool DoesLogFileExist()
        {
            if (File.Exists(Environment.CurrentDirectory + "\\ConsoleLog.txt"))
                return true;
            return false;
        }
        public void CreateLogFile()
        {
            if (DoesLogFileExist())
                File.Delete(Environment.CurrentDirectory + "\\ConsoleLog.txt");

            File.Create(Environment.CurrentDirectory + "\\ConsoleLog.txt").Close();
            if (console != null)
                ConsoleHandler.append("New log file created.");
        }
        public void WriteToLogFile(string text)
        {
            try
            {
                string logFile = Environment.CurrentDirectory + "\\ConsoleLog.txt";
                string logText = File.ReadAllText(logFile);

                if (logText.Length > 0)
                    logText += "\n";
                logText += text;

                try
                {
                    StreamWriter stream = new StreamWriter(logFile);
                    stream.Write(logText);
                    stream.Close();
                }
                catch { };
            }catch(IOException)
            {
                ConsoleHandler.append("couldn't save the console log");
            }
            
        }

        private void Sizer_MouseUp(object sender, MouseEventArgs e)
        {
            output_log.ScrollToCaret();
        }
    }
}
