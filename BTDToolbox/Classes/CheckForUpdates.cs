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

namespace BTDToolbox
{
    class CheckForUpdates
    {
        WebClient client= new WebClient();
        public bool exitLoop { get; set; }
        public bool reinstall { get; set; }
        public void checkForUpdate()
        {

            if (reinstall)
                ConsoleHandler.appendLog("Reinstalling game...");
            else
                ConsoleHandler.appendLog("Checking for updates...");
            CheckURL();
        }
        private bool isNewUpdate(string url)
        {
            string versionText = url;

            if (versionText != null && versionText != "")
            {
                string[] split = versionText.Split('\n');
                string toolboxRelease = split[0].Replace("toolbox2019: ", "");
                string[] gitVersionNum = toolboxRelease.Split('/');
                string updateVersionNum = (gitVersionNum[gitVersionNum.Length - 2]).Replace(".", "");

                string currentVersion = "";
                int number;
                foreach (char c in Main.version)
                {
                    bool success = Int32.TryParse(c.ToString(), out number);
                    if (success == true)
                    {
                        currentVersion = currentVersion + c;
                    }
                }

                if (Int32.Parse(currentVersion) < Int32.Parse(updateVersionNum))
                {
                    return true;
                }
                return false;
            }
            else
            {
                ConsoleHandler.appendLog("ERROR! The program was unable to determine the latest version of BTD Toolbox. You can continue to use toolbox like normal, or try reopening the program to check again...");
                return false;
            }
        }
        public void downloadUpdater()
        {
            string versionText = client.DownloadString("https://raw.githubusercontent.com/TDToolbox/BTDToolbox-2019_LiveFIles/master/Version");
            string[] split = versionText.Split('\n');
            string updaterRelease = split[1].Replace("toolbox2019_updater: ", "");

            ConsoleHandler.appendLog("Downloading BTD Toolbox updater...");
            client.DownloadFile(updaterRelease, "Update"); //, Environment.CurrentDirectory);//
            ConsoleHandler.appendLog("Updater successfully downloaded!");
            File.Move("Update", "BTDToolbox_Updater.zip");

            string zipPath = Environment.CurrentDirectory + "\\BTDToolbox_Updater.zip";
            string extractedFilePath = Environment.CurrentDirectory;

            ConsoleHandler.appendLog("Extracting updater...");
            ZipFile archive = new ZipFile(zipPath);
            foreach (ZipEntry e in archive)
            {
                e.Extract(extractedFilePath, ExtractExistingFileAction.DoNotOverwrite);
            }
            archive.Dispose();
            ConsoleHandler.appendLog("BTD Toolbox updater has been successfully downloaded and extracted...");
        }
        public void UpdateToolbox()
        {
            ConsoleHandler.appendLog("Toolbox needs to close in order to update..");
            MessageBox.Show("Closing Toolbox to continue update...");
            Process.Start(Environment.CurrentDirectory + "\\BTDToolbox_Updater.exe");
        }
        public void CheckURL()
        {
            string url = "https://raw.githubusercontent.com/TDToolbox/BTDToolbox-2019_LiveFIles/master/Version";
            Thread thread = new Thread(() => GetURL(url));
            thread.Start();
        }
        private void GetURL(string url)
        {
            bool success = false;
            WebClient client = new WebClient();
            string downloadedString = "";
            try
            {
                downloadedString = client.DownloadString(url);
                success = true;
            }
            catch
            {
                if (success == false)
                {
                    for (int i = 0; i <= 100; i++)
                    {
                        Thread.Sleep(100);
                        try
                        {
                            if (!exitLoop)
                            {
                                downloadedString = client.DownloadString(url);
                                if (downloadedString != null && downloadedString != "")
                                {
                                    success = true;
                                    break;
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                        catch { }
                    }
                }
            }

            if(success == true)
            {
                if (reinstall)
                {
                    downloadUpdater();
                    UpdateToolbox();
                }
                else if (isNewUpdate(downloadedString))
                {
                    ConsoleHandler.appendLog("There is a new update availible for BTD Toolbox! Do you want to download it?");
                    DialogResult result = MessageBox.Show("There is a new update availible for BTD Toolbox! Do you want to download it?", "Update BTD Toolbox?", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        downloadUpdater();
                        UpdateToolbox();
                    }
                    else
                        ConsoleHandler.appendLog("Update cancelled...");
                }
                else
                    ConsoleHandler.appendLog("You are using the latest version of BTD Toolbox.");
            }
            else
            {
                ConsoleHandler.appendLog("ERROR! The program was unable to determine the latest version of BTD Toolbox. You can continue to use toolbox like normal, or try reopening the program to check again...");
            }
        }
    }
}
