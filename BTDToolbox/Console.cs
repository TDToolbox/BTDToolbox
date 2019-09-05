using Newtonsoft.Json;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using static BTDToolbox.ProjectConfigs;

namespace BTDToolbox
{
    public partial class Console : Form
    {
        public static float consoleLogFont;
        string livePath = Environment.CurrentDirectory;

        public Console()
        {
            InitializeComponent();
            /*
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
            this.FormClosed += exitHandling;
            */
        }

        private void Console_Load(object sender, EventArgs e)
        {
        }
        private void Console_Close(object sender, FormClosingEventArgs e)
        {
            if (ConsoleHandler.console.Visible)
            {
                ConsoleHandler.console.Hide();
                e.Cancel = true;
            }
            else
            {
                ConsoleHandler.console.Show();
            }
        }

        public void appendLog(String log)
        {
            console_log.Items.Add(log);
        }
        /*
        private void exitHandling(object sender, EventArgs e)
        {
            ConsoleHandler.console.Visible = false;

            consoleForm = new Window("Console", this.Size.Width, this.Size.Height, this.Location.X, this.Location.Y, consoleLogFont);
            consoleFormOutput = JsonConvert.SerializeObject(consoleForm);

            StreamWriter writeConsoleForm = new StreamWriter(livePath + "\\config\\console_form.json", false);
            writeConsoleForm.Write(consoleFormOutput);
            writeConsoleForm.Close();
        }*/
    }
}
