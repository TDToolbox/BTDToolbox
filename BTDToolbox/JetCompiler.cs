using Ionic.Zip;
using Ionic.Zlib;
using System;
using System.IO;
using System.Windows.Forms;

namespace BTDToolbox
{
    class JetCompiler
    {
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
            if (MessageBox.Show("Click 'Ok' to create project files, this can take up to 2 minutes.", "", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                string livePath = Environment.CurrentDirectory;
                string tempName = (livePath + "\\proj_" + rand.Next());
                archive.ExtractAll(tempName);
                ConsoleHandler.appendLog("Project files created at: " + tempName);
                DirectoryInfo returner = new DirectoryInfo(tempName);
                return returner;
            }
            ConsoleHandler.appendLog("Project files creation canceled");
            return null;
        }
    }
}
