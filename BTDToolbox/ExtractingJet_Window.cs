﻿using Ionic.Zip;
using Ionic.Zlib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        public static bool isDecompiling;

        //Project name variables
        public static bool hasCustomProjectName;
        public static string projectName;
        public static int filesInJet;

        //Extraction variables
        public static string file;
        public static int totalFiles;
        public static int filesExtracted;
        public static bool isProjectCreated;


        public ExtractingJet_Window()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Left = 120;
            this.Top = 120;
            if (isCompiling)
            {
                this.Text = "Compiling....";
            }
            if (isDecompiling)
            {
                this.Text = "Extracting....";
                this.Show();
                DirectoryInfo dir = new DirectoryInfo(Environment.CurrentDirectory);
                DirectoryInfo extract = this.decompile(file, dir);
            }
        }
        private void ExtractJet_Window_Load(object sender, EventArgs e)
        {

        }


        public static void compile(DirectoryInfo target, string outputPath)
        {
            ZipFile toExport = new ZipFile();
            toExport.Password = "Q%_{6#Px]]";
            toExport.AddDirectory(target.FullName);
            toExport.Encryption = EncryptionAlgorithm.PkzipWeak;
            toExport.Name = outputPath;
            toExport.CompressionLevel = CompressionLevel.Level6;
            toExport.Save();
        }

        public DirectoryInfo decompile(string inputPath, DirectoryInfo targetFolder)
        {
            
            Random rand = new Random();
            ZipFile archive = new ZipFile(inputPath);
            archive.Password = "Q%_{6#Px]]";
            ConsoleHandler.appendLog("Creating project files...");

            string livePath = Environment.CurrentDirectory;
            if (hasCustomProjectName)
            {
                projectName = (livePath + "\\proj_" + projectName);
            }
            else
            {
                int randName = rand.Next(10000000, 99999999);
                projectName = (livePath + "\\proj_" + randName);
            }
            
            //Extract and count progress
            
            try
            {
                using (ZipFile zip = ZipFile.Read(inputPath))
                {
                    totalFiles = archive.Count();
                    archive.ExtractProgress += ZipExtractProgress;
                    archive.ExtractAll(projectName);
                }
                ConsoleHandler.appendLog("Project files created at: " + projectName);
                this.Close();
            }
            catch(Ionic.Zip.ZipException)
            {
                MessageBox.Show("A project with this name already exists. Do you want to replace it, or choose a different project name?");
                var reopenSetProjectName = new SetProjectName();
                reopenSetProjectName.Show();
                SetProjectName.doesProjectAlreadyExist = true;
                this.Close();
            }
            return null;
            
        }
        
        private void ZipExtractProgress(object sender, ExtractProgressEventArgs e)
        {
            if (e.TotalBytesToTransfer > 0)
            {   
                progressBar.Value = Convert.ToInt32(100 * e.BytesTransferred / e.TotalBytesToTransfer);
                e.BytesTransferred++;
            }
        }
    }
}