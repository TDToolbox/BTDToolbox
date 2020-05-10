using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTDToolbox.Classes.NewProjects;

namespace BTDToolbox.Extractor
{
    class Extractor
    {
        public static string sourcePath = "";
        public static void ExtractFile(string filepath)
        {
            string projName = CurrentProjectVariables.ProjectName;
            string destPath = Environment.CurrentDirectory + "\\" + projName + filepath;
            DirectoryInfo dinfo = new DirectoryInfo(destPath);
            if (!dinfo.Exists)
            {
                ConsoleHandler.append("Creating project files for: " + projName);

                ZipFile archive = new ZipFile(sourcePath);
                archive.Password = CurrentProjectVariables.JetPassword;
                archive.ExtractAll(destPath);
                archive.Dispose();
            }
        }
        
    }
}
