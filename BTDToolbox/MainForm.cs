using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static System.Environment;

namespace BTDToolbox
{
    public partial class TD_Toolbox_Window : Form {

        private string file;

        public TD_Toolbox_Window()
        {
            InitializeComponent();
            Controls.OfType<MdiClient>().FirstOrDefault().BackColor = Color.FromArgb(15,15,15);
        }

        private void TD_Toolbox_Window_Load(object sender, EventArgs e)
        {
            ConsoleHandler.console = new NewConsole();
            ConsoleHandler.console.MdiParent = this;
            ConsoleHandler.console.Show();
            ConsoleHandler.appendLog("Program loaded!");
            
            ConsoleHandler.appendLog("Searching for existing projects...");
            DirectoryInfo dirInfo = new DirectoryInfo(Environment.CurrentDirectory);
            foreach(DirectoryInfo subdir in dirInfo.GetDirectories())
            {
                if(subdir.Name.StartsWith("proj_"))
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
            if(JetProps.get().Count <= 1)
            {
                Launcher.launchGame(JetProps.getForm(0));
            }
            else
            {
                MessageBox.Show("You have multiple .jets open, only one can be launched.");
            }
        }

        private void consoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConsoleHandler.console.Show();
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

        private void themedFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ThemedFormTemplate tft = new ThemedFormTemplate();
            tft.MdiParent = this;
            tft.Show();
        }
    }
}
