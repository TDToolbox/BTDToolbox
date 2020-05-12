using BTDToolbox.Classes;
using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BTDToolbox.GeneralMethods;


namespace BTDToolbox
{
    class UpdateHandler
    {
        WebClient client= new WebClient();
        WebHandler reader;

        public bool reinstall { get; set; }
        string toolbox_updater_zipName = "BTDToolbox_Updater.zip";
        string gitURL = "https://raw.githubusercontent.com/TDToolbox/BTDToolbox-2019_LiveFIles/master/Version";

        public void HandleUpdates()
        {
            if(!reinstall)
            {
                if(CheckForUpdates())
                {
                    if(AskToUpdate())
                    {
                        DownloadUpdate();
                        ExtractUpdate();
                        LaunchUpdate();
                    }
                    else
                    { ConsoleHandler.append("Update cancelled..."); }
                }
                else
                { ConsoleHandler.append("Toolbox is up to date!"); }
            }
            else
            {
                DownloadUpdate();
                ExtractUpdate();
                LaunchUpdate();
            }
            
        }
        private bool CheckForUpdates()
        {
            reader = new WebHandler();
            try
            {
                return reader.CheckForUpdate(gitURL, "toolbox2019: ", 0, Main.version);
            }
            catch
            {
                ConsoleHandler.append_Notice("Something went wrong when checking for updates.. Failed to check for updates");
                return false;
            }
            
        }
        private void DownloadUpdate()
        {
            reader = new WebHandler();
            string git_Text = reader.WaitOn_URL(gitURL);
            string updaterURL = reader.processGit_Text(git_Text, "toolbox2019_updater: ", 1);

            ConsoleHandler.append("Downloading BTD Toolbox updater...");
            client.DownloadFile(updaterURL, "Update");

            if (File.Exists(toolbox_updater_zipName))
                File.Delete(toolbox_updater_zipName);
            File.Move("Update", toolbox_updater_zipName);
            ConsoleHandler.append("Updater successfully downloaded!");
        }
        private void ExtractUpdate()
        {
            string zipPath = Environment.CurrentDirectory + "\\" + toolbox_updater_zipName;
            string extractedFilePath = Environment.CurrentDirectory;
            ConsoleHandler.append("Extracting updater...");
            ZipFile archive = new ZipFile(zipPath);
            foreach (ZipEntry e in archive)
            {
                e.Extract(extractedFilePath, ExtractExistingFileAction.DoNotOverwrite);
            }
            archive.Dispose();
            ConsoleHandler.append("BTD Toolbox updater has been successfully downloaded and extracted...");
        }
        private bool AskToUpdate()
        {
            ConsoleHandler.append("There is a new update availible for BTD Toolbox! Do you want to download it?");
            DialogResult result = MessageBox.Show("There is a new update availible for BTD Toolbox! Do you want to download it?", "Update BTD Toolbox?", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                return true;
            }
            else
                return false;
        }
        private void LaunchUpdate()
        {
            ConsoleHandler.append("Toolbox needs to close in order to update..");
            MessageBox.Show("Closing Toolbox to continue update...");

            //save config real quick
            Serializer.cfg.recentUpdate = true;
            Serializer.SaveSettings();

            Process p = new Process();
            p.StartInfo.Arguments = "-lineNumber:0 -url:https://raw.githubusercontent.com/TDToolbox/BTDToolbox-2019_LiveFIles/master/Updater_launch%20parameters";
            //p.StartInfo.Arguments = "-fileName:BTD_Toolbox -processName:BTDToolbox -exeName:BTDToolbox.exe -updateZip_Name:BTDToolbox_Updater.zip -ignoreFiles:BTDToolbox_Updater,Backups,DotNetZip,.json -deleteFiles:BTDToolbox_Updater.zip,Update -url:https://raw.githubusercontent.com/TDToolbox/BTDToolbox-2019_LiveFIles/master/Version -replaceText:toolbox2019: -lineNumber:0";
            p.StartInfo.FileName = Environment.CurrentDirectory + "\\BTDToolbox_Updater.exe";
            p.Start();

            
        }
    }
}
