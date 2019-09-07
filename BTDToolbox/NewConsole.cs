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
    public partial class NewConsole : ThemedForm
    {
        //Config variables
        Window consoleForm;
        public static float consoleLogFont;
        string livePath = Environment.CurrentDirectory;
        string consoleFormOutput;


        public NewConsole() : base()
        {
            InitializeComponent();
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
        }

        public override void close_button_Click(object sender, EventArgs e)
        {
            serializeConfig();
            this.Hide();
        }

        public void appendLog(String log)
        {
            //console_log.Text += log + "\r\n";
            console_log.AppendText(log + "\r\n");
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

        private void NewConsole_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                if (JetProps.get().Count == 1)
                {
                    Launcher.launchGame(JetProps.getForm(0));
                }
                else if (JetProps.get().Count < 1)
                {
                    MessageBox.Show("You have no .jets or projects open, you need one to launch.");
                }
                else
                {
                    MessageBox.Show("You have multiple .jets or projects open, only one can be launched.");
                }
            }
        }

        private void Console_log_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
