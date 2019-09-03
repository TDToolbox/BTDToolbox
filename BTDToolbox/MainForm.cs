using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace BTDToolbox
{
    public partial class TD_Toolbox_Window : Form {

        private string file;

        public TD_Toolbox_Window()
        {
            InitializeComponent();
        }

        private void TD_Toolbox_Window_Load(object sender, EventArgs e)
        {
            ConsoleHandler.console = new Console();
            ConsoleHandler.console.MdiParent = this;
            ConsoleHandler.console.Show();
            ConsoleHandler.appendLog("Program loaded!");
        }

        private void newJetWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            JetForm jf = new JetForm(file);
            jf.MdiParent = this;
            jf.Show();
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
                jetOpen(file);
            }
        }

        private void jetOpen(String file)
        {
            JetForm jf = new JetForm(file);
            jf.MdiParent = this;
            jf.Show();
        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConsoleHandler.appendLog("Launching game...");
            try
            {
                Process.Start(Settings.readGamePath());
                ConsoleHandler.appendLog("Steam is taking over for the rest of the launch.");
            } catch (Exception ex)
            {
                ConsoleHandler.appendLog("No launch dir defined or is wrong.");
                OpenFileDialog fileDiag = new OpenFileDialog();
                fileDiag.Title = "Open game exe";
                fileDiag.DefaultExt = "exe";
                fileDiag.Filter = "Exe files (*.exe)|*.exe|All files (*.*)|*.*";
                fileDiag.Multiselect = false;
                if (fileDiag.ShowDialog() == DialogResult.OK)
                {
                    file = fileDiag.FileName;
                    Settings.setGamePath(file);
                }
                ConsoleHandler.appendLog("Launch dir saved in launchSettings.txt");
            }
        }

        private void consoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConsoleHandler.console.Show();
        }

        private void existingProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void projectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }
    }
}
