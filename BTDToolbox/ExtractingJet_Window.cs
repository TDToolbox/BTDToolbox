using Ionic.Zip;
using Ionic.Zlib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTDToolbox
{
    public partial class ExtractingJet_Window : Form
    {
        //Project Variables
        public static bool isCompiling;
        public static bool isOutput;
        public static bool isDecompiling;
        public string livePath = Environment.CurrentDirectory;

        //Project name variables
        public static bool hasCustomProjectName;
        public static string projectName;
        public static string customName;
        public static int filesInJet;

        //Extraction variables
        public static string file;
        public static int totalFiles;
        public static int filesExtracted;
        public static bool isProjectCreated;
        public static int filesCompiled;

        //Compile variables
        public static bool launchProgram;
        public static string currentProject;
        public DirectoryInfo dir = new DirectoryInfo(Environment.CurrentDirectory);

        public ExtractingJet_Window()
        {
            InitializeComponent();
            
            this.StartPosition = FormStartPosition.Manual;
            this.Left = 120;
            this.Top = 120;
            if(isOutput)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = "Export .jet";
                sfd.DefaultExt = "jet";
                sfd.Filter = "Jet files (*.jet)|*.jet|All files (*.*)|*.*";
                if(sfd.ShowDialog() == DialogResult.OK)
                {
                    this.Show();
                    DirectoryInfo projDir = new DirectoryInfo(Environment.CurrentDirectory + "\\" + currentProject);
                    ConsoleHandler.appendLog("Compiling jet...");
                    this.compile(projDir, sfd.FileName);
                    ConsoleHandler.appendLog("Jet compiled");
                }
                isOutput = false;
                return;
            }

            this.Show();
            if (isCompiling)
            {
                this.Text = "Compiling....";
                isDecompiling = false;

                //Just do a quick check to see if launchsettings.txt exists
                try
                {
                    string path = System.IO.File.ReadAllText(livePath + "\\launchsettings.txt");
                }
                catch (FileNotFoundException)
                {
                    MessageBox.Show("No launch dir defined or is wrong. Please select BTD5.exe so the program can export your project...");
                    ConsoleHandler.appendLog("No launch dir defined or is wrong.");
                    OpenFileDialog fileDiag = new OpenFileDialog();
                    fileDiag.Title = "Open game exe";
                    fileDiag.DefaultExt = "exe";
                    fileDiag.Filter = "Exe files (*.exe)|*.exe|All files (*.*)|*.*";
                    fileDiag.Multiselect = false;
                    if (fileDiag.ShowDialog() == DialogResult.OK)
                    {
                        string file = fileDiag.FileName;
                        Settings.setGamePath(file);
                        string jet = file + "\\..\\Assets\\BTD5.jet";
                        ConsoleHandler.appendLog("Launch settings saved in launchSettings.txt");
                    }
                }
                finally
                {
                    string path = System.IO.File.ReadAllText(livePath + "\\launchsettings.txt");
                    string gameJetPath = path + "\\..\\Assets\\BTD5.jet";
                    DirectoryInfo projDir = new DirectoryInfo(Environment.CurrentDirectory + "\\" + currentProject);

                    if (!File.Exists(Environment.CurrentDirectory + "\\Backups\\Original.jet"))
                    {
                        ConsoleHandler.appendLog("Jet backup not found, creating one...");
                        Directory.CreateDirectory(Environment.CurrentDirectory + "\\Backups");
                        File.Copy(gameJetPath, Environment.CurrentDirectory + "\\Backups\\Original.jet");
                        ConsoleHandler.appendLog("Backup done");
                    }
                    ConsoleHandler.appendLog("Compiling jet...");
                    this.compile(projDir, gameJetPath);
                    ConsoleHandler.appendLog("Jet compiled");
                }
                if (launchProgram)
                {
                    Process.Start(Settings.readGamePath());
                    ConsoleHandler.appendLog("Steam is taking over for the rest of the launch.");
                }
                isCompiling = false;
            }
            if (isDecompiling)
            {
                this.Text = "Extracting....";
                try
                {
                    DirectoryInfo extract = this.decompile(file, dir);
                }
                catch
                {
                    DialogResult varr = MessageBox.Show("A project with this name already exists. Do you want to replace it, or choose a different project name?", "", MessageBoxButtons.OKCancel);
                    if (varr == DialogResult.OK)
                    {
                        Directory.Delete(projectName, true);
                        DirectoryInfo extract = this.decompile(file, dir);
                    }
                    if (varr == DialogResult.Cancel)
                    {
                        var reopenSetProjectName = new SetProjectName();
                        reopenSetProjectName.Show();
                        this.Close();
                    }
                }
                isDecompiling = false;
            }
        }
        private void ExtractJet_Window_Load(object sender, EventArgs e)
        {
            if (isCompiling)
            {
                CurrentFileProgress_Label.Hide();
                CurrentFileProgress.Hide();

                TotalProgress_ProgressBar.Location = new Point(TotalProgress_ProgressBar.Location.X, TotalProgress_ProgressBar.Location.Y - 50);
                TotalProgress_Label.Location = new Point(TotalProgress_Label.Location.X, TotalProgress_Label.Location.Y - 50);
                richTextBox1.Location = new Point(richTextBox1.Location.X, richTextBox1.Location.Y - 50);
                label1.Location = new Point(label1.Location.X, label1.Location.Y - 50);
                this.Size = new Size(this.Size.Width, this.Size.Height - 50);
                pictureBox1.Location = new Point(pictureBox1.Location.X, pictureBox1.Location.Y - 50);
            }
            this.Show();
        }

        public void compile(DirectoryInfo target, string outputPath)
        {
            DirectoryInfo projDir = new DirectoryInfo(Environment.CurrentDirectory + "\\" + currentProject);
            int numFiles = Directory.GetFiles((Environment.CurrentDirectory + "\\" + currentProject), "*", SearchOption.AllDirectories).Length;
            int numFolders = Directory.GetDirectories(Environment.CurrentDirectory + "\\" + currentProject, "*", SearchOption.AllDirectories).Count();
            totalFiles = numFiles + numFolders;

            filesCompiled = 0;
            ZipFile toExport = new ZipFile();
            toExport.Password = "Q%_{6#Px]]";
            toExport.AddProgress += ZipCompileProgress;
            toExport.AddDirectory(target.FullName);
            toExport.Encryption = EncryptionAlgorithm.PkzipWeak;
            toExport.Name = outputPath;
            toExport.CompressionLevel = CompressionLevel.Level6;
            try
            {
                toExport.Save();
            }
            catch
            {
                MessageBox.Show("ERROR! The game is currently running. Please close the game and try again...");
            }
            this.Hide();
        }

        public DirectoryInfo decompile(string inputPath, DirectoryInfo targetFolder)
        {

            Random rand = new Random();
            ZipFile archive = new ZipFile(inputPath);
            archive.Password = "Q%_{6#Px]]";
            ConsoleHandler.appendLog("Creating project files...");

            if (hasCustomProjectName)
            {
                projectName = (livePath + "\\proj_" + customName);
            }
            else
            {
                int randName = rand.Next(10000000, 99999999);
                projectName = (livePath + "\\proj_" + randName);
            }
            //Extract and count progress
            
            using (ZipFile zip = ZipFile.Read(inputPath))
            {
                totalFiles = archive.Count();
                filesExtracted = 0;
                archive.ExtractProgress += ZipExtractProgress;
                archive.ExtractAll(projectName);
                DirectoryInfo dinfo = new DirectoryInfo(projectName);
                JetForm jf = new JetForm(dinfo, TD_Toolbox_Window.getInstance(), dinfo.Name);
                jf.MdiParent = TD_Toolbox_Window.getInstance();
                jf.Show();
            }
            ConsoleHandler.appendLog("Project files created at: " + projectName);
            this.Close();

            return null;
        }

        private void ZipExtractProgress(object sender, ExtractProgressEventArgs e)
        {
            if (e.TotalBytesToTransfer > 0)
            {
                label1.Refresh();
                CurrentFileProgress_Label.Refresh();
                richTextBox1.Text = e.CurrentEntry.FileName;
                richTextBox1.Refresh();
                CurrentFileProgress.Value = Convert.ToInt32(100 * e.BytesTransferred / e.TotalBytesToTransfer);
                e.BytesTransferred++;
            }
            if (e.EventType != ZipProgressEventType.Extracting_BeforeExtractEntry)
                return;
            filesExtracted++;
            TotalProgress_ProgressBar.Value = 100 * filesExtracted / totalFiles;
            this.Refresh();
        }
        private void ZipCompileProgress(object sender, AddProgressEventArgs e)
        {
            if (e.EventType != ZipProgressEventType.Adding_AfterAddEntry)
                return;
            if (TotalProgress_ProgressBar.Value == 100)
            {
                this.Close();
            }
            richTextBox1.Text = e.CurrentEntry.FileName;
            filesCompiled++;
            TotalProgress_ProgressBar.Value = 100 * filesCompiled / totalFiles;
            this.Refresh();
        }

        //Restore backup .jet
        public static void restoreGame()
        {
            if (!File.Exists(Environment.CurrentDirectory + "\\Backups\\Original.jet"))
            {
                MessageBox.Show("No backup found that can be restored! Use steam to re-download the original .jet");
                return;
            }
            ConsoleHandler.appendLog("Restoring backup .jet");
            string gameJetPath = Settings.readGamePath() + "\\..\\Assets\\BTD5.jet";
            File.Delete(gameJetPath);
            File.Copy(Environment.CurrentDirectory + "\\Backups\\Original.jet", gameJetPath);
            ConsoleHandler.appendLog("Backup restored");
            MessageBox.Show("Backup .jet restored!");
        }
    }
}