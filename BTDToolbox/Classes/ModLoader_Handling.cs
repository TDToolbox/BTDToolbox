using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BTDToolbox.Classes
{
    class ModLoader_Handling
    {
        string modloader_zipName = "BTDMod Loader.zip";
        string modLoader_Path = "";
        public bool ValidateManager()
        {
            bool loaderExists = false;
            string[] files = Directory.GetFiles(Environment.CurrentDirectory, "*Mod*Loader*", SearchOption.AllDirectories);
            foreach (string file in files)
            {
                if (File.Exists(file))
                {
                    modLoader_Path = file;
                    return true;                    
                }
            }

            if (loaderExists == false)
            {
                return false;
                
            }
            return false;
        }

        public void DeleteManager()
        {

        }
        public void Launch()
        {

        }
        public void Handle_ModLoader()
        {
            if(ValidateManager())
            {
                ConsoleHandler.appendLog("Launching mod loader...");
                Process.Start(modLoader_Path);
            }
            else
            {
                ConsoleHandler.appendLog("Mod loader not found! Downloading mod loader...");
                Download_ModLoader();
                Extract_ModLoader();
                if(ValidateManager())
                {
                    ConsoleHandler.appendLog("Launching mod loader...");
                    Process.Start(modLoader_Path);
                }
            }
        }
        private void Download_ModLoader()
        {
            WebHandler web = new WebHandler();
            WebClient client = new WebClient();
            string url = "https://raw.githubusercontent.com/TDToolbox/BTDToolbox-2019_LiveFIles/master/Version";
            
            
            string gitText = web.WaitOn_URL(url);
            string downloadURL = web.processGit_Text(gitText, "modloader: ", 2);
            client.DownloadFile(downloadURL, "Update");

            if (File.Exists(modloader_zipName))
                File.Delete(modloader_zipName);
            File.Move("Update", modloader_zipName);
            ConsoleHandler.appendLog("Mod Loader successfully downloaded!");
        }
        private void Extract_ModLoader()
        {
            string zipPath = Environment.CurrentDirectory + "\\" + modloader_zipName;
            string extractedFilePath = Environment.CurrentDirectory;

            ConsoleHandler.appendLog("Extracting Mod Loader...");
            ZipFile archive = new ZipFile(zipPath);
            foreach (ZipEntry e in archive)
            {
                e.Extract(extractedFilePath, ExtractExistingFileAction.DoNotOverwrite);
            }
            archive.Dispose();
            ConsoleHandler.appendLog("BTD Mod Loader updater has been successfully downloaded and extracted...");

            File.Delete(modloader_zipName);
            if (File.Exists(modloader_zipName))
            {
                File.Delete(modloader_zipName);
            }
        }
    }
}
