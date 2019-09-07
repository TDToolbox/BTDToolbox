using Ionic.Zip;
using Ionic.Zlib;
using System;
using System.IO;
using System.Windows.Forms;

namespace BTDToolbox
{
    class JetCompiler
    {
        public static bool hasCustomProjectName = false;
        public static string customProjectName = "";
        public static string projectName;
        public static int filesInJet;
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
        public static DirectoryInfo decompile(string inputPath, DirectoryInfo targetFolder)
        {
            Random rand = new Random();
            ZipFile archive = new ZipFile(inputPath);
            archive.Password = "Q%_{6#Px]]";
            ConsoleHandler.appendLog("Creating project files...");
            if (MessageBox.Show("Click 'Ok' to create project files, this can take up to 2 minutes.", "", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            {
                ConsoleHandler.appendLog("Project files creation canceled");
                TD_Toolbox_Window.jetImportCancelled = true;
            }
            else
            {
                TD_Toolbox_Window.jetImportCancelled = false;
                string livePath = Environment.CurrentDirectory;
                if (hasCustomProjectName)
                {
                    projectName = (livePath + "\\proj_" + customProjectName);
                }
                else
                {
                    int randName = rand.Next(10000000, 99999999);
                    projectName = (livePath + "\\proj_" + randName);
                }

                //Extract and count progress
                filesInJet = archive.Count;
                //archive.ExtractProgress += ExtractingJet_Window.ZipExtractProgress;
                archive.ExtractAll(projectName);
                
                ConsoleHandler.appendLog("Project files created at: " + projectName);
                DirectoryInfo returner = new DirectoryInfo(projectName);
                return returner;
            }
            return null;

        }
    }
}
