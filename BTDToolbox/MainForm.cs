using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static BTDToolbox.ProjectConfigs;
using static System.Environment;

namespace BTDToolbox
{
    public partial class TD_Toolbox_Window : Form {

        private string file;
        public int mainFormFontSize;
        public bool enableConsole;
        MainWindow mainForm;
        string mainFormOutput;
        string livePath = Environment.CurrentDirectory;

        public TD_Toolbox_Window()
        {
            InitializeComponent(); 
            try
            {
                string json = File.ReadAllText(livePath + "\\config\\main_form.json");
                MainWindow deserializedMainForm = JsonConvert.DeserializeObject<MainWindow>(json);

                Size MainFormSize = new Size(deserializedMainForm.SizeX, deserializedMainForm.SizeY);
                this.Size = MainFormSize;

                this.StartPosition = FormStartPosition.Manual;
                this.Location = new Point(deserializedMainForm.PosX, deserializedMainForm.PosY);

                Font mainFormFontSize = new Font("Microsoft Sans Serif", deserializedMainForm.FontSize);
                this.Font = mainFormFontSize;

                enableConsole = deserializedMainForm.EnableConsole;
            }
            catch (FileNotFoundException)
            {
                mainForm = new MainWindow("Main Form", this.Size.Width, this.Size.Height, this.Location.X, this.Location.Y, 10, true);
                mainFormOutput = JsonConvert.SerializeObject(mainForm);

                string livePath = Environment.CurrentDirectory;
                StreamWriter writeMainForm = new StreamWriter(livePath + "\\config\\main_form.json", false);
                writeMainForm.Write(mainFormOutput);
                writeMainForm.Close();
            }
            catch (DirectoryNotFoundException)
            {
                Directory.CreateDirectory(livePath + "\\config");
            }
            catch (System.ArgumentException)
            {
                mainFormFontSize = 10;
            }


            Controls.OfType<MdiClient>().FirstOrDefault().BackColor = Color.Black;

            this.FormClosed += exitHandling;
            Controls.OfType<MdiClient>().FirstOrDefault().BackColor = Color.FromArgb(15,15,15);
        }

        private void TD_Toolbox_Window_Load(object sender, EventArgs e)
        {
            ConsoleHandler.console = new NewConsole();
            ConsoleHandler.console.MdiParent = this;
            if (enableConsole == true)
            {
                ConsoleHandler.console.Show();
            }
            else
            {
                ConsoleHandler.console.Hide();
            }
            ConsoleHandler.appendLog("Program loaded!");

            ConsoleHandler.appendLog("Searching for existing projects...");
            DirectoryInfo dirInfo = new DirectoryInfo(Environment.CurrentDirectory);
            foreach (DirectoryInfo subdir in dirInfo.GetDirectories())
            {
                if (subdir.Name.StartsWith("proj_"))
                {
                    ConsoleHandler.appendLog("Loading project " + subdir.Name);
                    JetForm jf = new JetForm(subdir, this, subdir.Name);
                    jf.MdiParent = this;
                    jf.Show();
                    ConsoleHandler.appendLog("Loaded project " + subdir.Name);
                }
            }
            
        }

        private void jetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDiag = new OpenFileDialog();
            fileDiag.Title = "Open .jet";
            fileDiag.DefaultExt = "jet";
            fileDiag.Filter = "Jet files (*.jet)|*.jet|All files (*.*)|*.*";
            fileDiag.Multiselect = false;
            if(fileDiag.ShowDialog() == DialogResult.OK)
            {
                file = fileDiag.FileName;
                JetForm jf = new JetForm(file, this, fileDiag.SafeFileName);
                jf.MdiParent = this;
                jf.Show();
            }
        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(JetProps.get().Count == 1)
            {
                Launcher.launchGame(JetProps.getForm(0));
            }
            else if(JetProps.get().Count < 1)
            {
                MessageBox.Show("You have no .jets or projects open, you need one to launch.");
            }
            else
            {
                MessageBox.Show("You have multiple .jets or projects open, only one can be launched.");
            }
        }

        private void consoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ConsoleHandler.console.Visible)
            {
                ConsoleHandler.console.Hide();
                enableConsole = false;
            }
            else
            {
                enableConsole = true;
                ConsoleHandler.console.Show();
            }
        }

        private void restoreBackupjetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Launcher.restoreGame();
        }

        private void existingProjectToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Select project folder";
            fbd.ShowNewFolderButton = false;
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                string selected = fbd.SelectedPath;
                DirectoryInfo dirInfo = new DirectoryInfo(selected);
                string[] split = fbd.SelectedPath.Split('\\');
                string name = split[split.Length - 1];
                JetForm jf = new JetForm(dirInfo, this, name);
                jf.MdiParent = this;
                jf.Show();
            }
        }
      
        private void exitHandling(object sender, EventArgs e)
        {
            if(ConsoleHandler.console.Visible)
            {
                enableConsole = true;
            }
            else
            {
                enableConsole = false;
            }
            mainForm = new MainWindow("Main Form", this.Size.Width, this.Size.Height, this.Location.X, this.Location.Y, this.Font.Size, enableConsole);
            mainFormOutput = JsonConvert.SerializeObject(mainForm);

            string livePath = Environment.CurrentDirectory;
            StreamWriter writeMainForm = new StreamWriter(livePath + "\\config\\main_form.json", false);
            writeMainForm.Write(mainFormOutput);
            writeMainForm.Close();
        }
        private void themedFormToolStripMenuItem_Click(object sender, EventArgs e)
        { 
            ThemedFormTemplate tft = new ThemedFormTemplate();
            tft.MdiParent = this;
            tft.Show();
        }

        private void FindToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FindWindow findForm = new FindWindow();
            findForm.Show();
            findForm.Text = "Find";
            findForm.replace = false;
            findForm.find = true;
        }

        private void ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FindWindow findForm = new FindWindow();
            findForm.Show();
            findForm.Text = "Replace";
            findForm.replace = true;
            findForm.find = false;
            
        }
    }
}
