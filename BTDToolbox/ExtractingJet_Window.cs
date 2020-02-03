using Ionic.Zip;
using Ionic.Zlib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BTDToolbox.ProjectConfig;
using static BTDToolbox.GeneralMethods;

namespace BTDToolbox
{
    public partial class ExtractingJet_Window : Form
    {
        //Project Variables
        JetForm jf;

        //zip variables
        public static int totalFiles = 0;        
        public int filesTransfered = 0;

        //Config variables
        ConfigFile programData;
        public string gameDir;
        public string gameName;
        public string steamJetPath;
        public static string BTD5_Dir;
        public static string BTDB_Dir;
        

        //other variables
        public string projectName_Identifier = "";
        public string fullProjName = "";
        public string jetFile_Game { get; set; }
        public string sourcePath { get; set; }
        public string destPath { get; set; }
        public string projName { get; set; }
        public string password { get; set; }
        public bool launch { get; set; }

        //Threads
        Thread backgroundThread;

        public ExtractingJet_Window()
        {
            InitializeComponent();
            StartUp();
            this.StartPosition = FormStartPosition.Manual;
            this.Left = 120;
            this.Top = 120;
        }
        private void StartUp()
        {
            programData = DeserializeConfig();
            string std = DeserializeConfig().CurrentGame;
            string jetName = "";

            if (std == "BTDB")
            {
                gameDir = DeserializeConfig().BTDB_Directory;
                jetName = "data.jet";
                
            }
            else
            {
                gameDir = DeserializeConfig().BTD5_Directory;
                jetName = "BTD5.jet";
                password = "Q%_{6#Px]]";
            }
            gameName = std;
            steamJetPath = gameDir + "\\Assets\\" + jetName;
            projectName_Identifier = "\\proj_" + gameName + "_";
            ValidateEXE(gameName);
        }
        public void Extract()
        {
            if (sourcePath == null || sourcePath == "")
                sourcePath = Environment.CurrentDirectory + "\\Backups\\" + gameName + "_Original.jet";
            if (File.Exists(sourcePath))
            {
                Random rand = new Random();

                if (projName == null || projName == "")
                {
                    int randName = rand.Next(1, 99999999);
                    projName = randName.ToString();
                }
                fullProjName = projectName_Identifier + projName;

                //check password
                if (File.Exists(sourcePath) && gameName == "BTDB")
                {
                    this.Hide();
                    bool passRes = Bad_JetPass(sourcePath, password);
                    if (passRes == true)
                    {
                        DialogResult res = MessageBox.Show("You entered the wrong password. Would you like to try again?", "Wrong Password!", MessageBoxButtons.OKCancel);
                        if (res == DialogResult.OK)
                        {
                            var getpas = new Get_BTDB_Password();
                            getpas.projName = projName;
                            getpas.isExtracting = true;
                            getpas.Show();
                        }
                        else
                            this.Close();
                    }
                    else
                    {
                        this.Show();
                        backgroundThread = new Thread(Extract_OnThread);
                        backgroundThread.Start();
                    }
                }
                else
                {
                    backgroundThread = new Thread(Extract_OnThread);
                    backgroundThread.Start();
                }
            }
            else
                ConsoleHandler.appendLog("Failed to find file to extract");
        }
        private void Extract_OnThread()
        {
            destPath = Environment.CurrentDirectory + "\\" + fullProjName;
            DirectoryInfo dinfo = new DirectoryInfo(destPath);
            if (!dinfo.Exists)
            {
                ConsoleHandler.appendLog("Creating project files for: " + fullProjName);

                ZipFile archive = new ZipFile(sourcePath);
                archive.Password = password;
                totalFiles = archive.Count();
                filesTransfered = 0;
                archive.ExtractProgress += ZipExtractProgress;
                archive.ExtractAll(destPath);
                archive.Dispose();

                ConsoleHandler.appendLog("Project files created at: " + fullProjName);
                Invoke((MethodInvoker)delegate {
                    jf = new JetForm(dinfo, Main.getInstance(), dinfo.Name);
                    jf.MdiParent = Main.getInstance();
                    jf.Show();
                });
            }
            else
            {
                 DialogResult varr = MessageBox.Show("A project with this name already exists. Do you want to replace it?", "", MessageBoxButtons.OKCancel);
                if (varr == DialogResult.OK)
                {
                    MessageBox.Show("Overwriting existing project... Please close the current Jet Explorer window.");
                    ConsoleHandler.appendLog("Deleting existing project....");
                    DeleteDirectory(dinfo.ToString());
                    ConsoleHandler.appendLog("Project Deleted. Creating new project...");
                    Extract_OnThread();
                }
                if (varr == DialogResult.Cancel)
                {
                    try
                    {
                        this.Invoke(new Action(() => this.Close()));
                    }
                    catch (Exception ex)
                    {
                        PrintError(ex.Message);
                    }
                    backgroundThread.Abort();
                }
            }
            try
            {
                this.Invoke(new Action(() => this.Close()));
            }
            catch (Exception ex)
            {
                PrintError(ex.Message);
            }
            backgroundThread.Abort();
        }
        public void Compile()
        {
            if(gameName == "BTDB")
            {
                if(password == null || password.Length <= 0)
                {
                    var getpas = new Get_BTDB_Password();
                    getpas.launch = launch;
                    getpas.projName = projName;
                    getpas.destPath = destPath;
                    getpas.Show();
                    this.Close();
                }
                else
                {
                    backgroundThread = new Thread(Compile_OnThread);
                    backgroundThread.Start();
                }                
            }
            else
            {
                backgroundThread = new Thread(Compile_OnThread);
                backgroundThread.Start();
            }
        }
        private void Compile_OnThread()
        {
            string dir = "";
            if (destPath == null || destPath == "")
                dir = steamJetPath;
            else
                dir = destPath;

            if (DeserializeConfig().LastProject == null)
            {
                Serializer.SaveConfig(jf, "jet explorer", programData);
            }
            DirectoryInfo projDir = new DirectoryInfo(DeserializeConfig().LastProject);
            if (Directory.Exists(projDir.ToString()))
            {
                ConsoleHandler.appendLog("Compiling jet...");
                int numFiles = Directory.GetFiles((projDir.ToString()), "*", SearchOption.AllDirectories).Length;
                int numFolders = Directory.GetDirectories(projDir.ToString(), "*", SearchOption.AllDirectories).Count();
                totalFiles = numFiles + numFolders;
                filesTransfered = 0;

                ZipFile toExport = new ZipFile();

                toExport.Password = password;
                toExport.AddProgress += ZipCompileProgress;
                toExport.AddDirectory(projDir.FullName);
                toExport.Encryption = EncryptionAlgorithm.PkzipWeak;
                toExport.Name = dir;
                toExport.CompressionLevel = CompressionLevel.Level6;
                toExport.Save();
                toExport.Dispose();
                ConsoleHandler.appendLog("Jet was successfully exported to: " + projDir.FullName);

                if (launch == true)
                {
                    LaunchGame(gameName);
                }
                try
                {
                    this.Invoke(new Action(() => this.Close()));
                }
                catch (Exception ex)
                {
                    PrintError(ex.Message);
                }
                backgroundThread.Abort();
            }
        }
        private void ExtractJet_Window_Load(object sender, EventArgs e)
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
        private void ZipExtractProgress(object sender, ExtractProgressEventArgs e)
        {
            if (e.EventType != ZipProgressEventType.Extracting_BeforeExtractEntry)
                return;
            if (filesTransfered >= (totalFiles / 100))
            {
                try
                {
                    TotalProgress_ProgressBar.Invoke(new Action(() => TotalProgress_ProgressBar.Value = 100 * filesTransfered / totalFiles));
                }
                catch (Exception ex)
                {
                    PrintError(ex.Message);
                }
            }

            filesTransfered++;
        }
        private void ZipCompileProgress(object sender, AddProgressEventArgs e)
        {
            
            if (e.EventType != ZipProgressEventType.Adding_AfterAddEntry)
                return;
            if (filesTransfered >= (totalFiles/100))
            {
                try
                {
                    TotalProgress_ProgressBar.Invoke(new Action(() => TotalProgress_ProgressBar.Value = 100 * filesTransfered / totalFiles));
                }
                catch(Exception ex)
                {
                    PrintError(ex.Message);
                }
            }
            filesTransfered++;
        }
        private void PrintError(string exception)
        {
            ConsoleHandler.appendLog("An error occured that may prevent the program from running properly.\r\nThe error is below: \r\n\r\n" + exception + "\r\n");
            this.Close();
        }
    }
}