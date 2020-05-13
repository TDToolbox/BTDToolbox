using BTDToolbox.Classes.NewProjects;
using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using ZipFile = Ionic.Zip.ZipFile;

namespace BTDToolbox.Classes
{
    class NKHook
    {
        public static string nkhEXE = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\NKHook5\\NKHook5-Injector.exe";
        public static string pathTowerLoadPlugin = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\NKHook5\\Plugins\\NewTowerLoader.dll";
        public string versionsURL = "https://raw.githubusercontent.com/TDToolbox/BTDToolbox-2019_LiveFIles/master/Version";
        public static bool DoesNkhExist()
        {
            if (File.Exists(nkhEXE))
            {
                return true;
            }
            return false;
        }
        public static bool CanUseNKH()
        {
            if (Serializer.cfg.UseDeveloperMode)
                return true;

            if (!Main.canUseNKH)
                return false;

            if (CurrentProjectVariables.GameName != "BTD5")
                return false;

            if (!DoesNkhExist())
                return false;

            return true;
        }
        public static void LaunchNKH()
        {
            if(!DoesNkhExist())
            {
                ConsoleHandler.append("Unable to find NKHook5-Injector.exe. Failed to launch...");
                return;
            }
            ConsoleHandler.append("Launching NKHook");
            Process.Start(nkhEXE);
        }

        public static void OpenNkhGithub()
        {
            ConsoleHandler.append("Opening NKHook Github page...");
            Process.Start("https://github.com/DisabledMallis/NKHook5");
        }
        public static void OpenMainWebsite()
        {
            string url = "https://nkhook.pro/";
            ConsoleHandler.append("Opening NKHook's website...");
            Process.Start(url);
        }
        public string Update(string filename, string savePath, string url, string deleteText, int lineNumber, string currentVersion)
        {
            /*WebHandler web = new WebHandler();
            bool result = web.CheckForUpdate(url, deleteText, lineNumber, currentVersion);

            if(File.Exists(savePath) && !result)
                return web.LatestVersionNumber;

            web.DownloadFile(filename, url, savePath, deleteText, lineNumber);*/
            return "";//web.LatestVersionNumber;
        }
        public void DoUpdateNKH()
        {
            new Thread(() =>
            {
                WebHandler web = new WebHandler();
                string nkhfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\NKHook5";
                string savePath = nkhfolder + "\\NKHook5.zip";

                if(!web.CheckForUpdate(versionsURL, "NKHook5: ", 3, Serializer.cfg.NKHookVersion) && Directory.Exists(nkhfolder)
                && File.Exists(nkhfolder + "\\NKHook5.dll") && File.Exists(nkhfolder + "\\NKHook5-CLR.dll"))
                    return;

                if (File.Exists(savePath))
                    File.Delete(savePath);

                if (!Directory.Exists(nkhfolder))
                    Directory.CreateDirectory(nkhfolder);

                web.DownloadFile("NKHook5", versionsURL, savePath, "NKHook5: ", 3);


                string extractPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\NKHook5";
                ZipFile archive = new ZipFile(savePath);
                archive.ExtractAll(extractPath, ExtractExistingFileAction.OverwriteSilently);
                archive.Dispose();

                if (File.Exists(savePath))
                    File.Delete(savePath);

                File.Copy(nkhEXE, Environment.CurrentDirectory + "\\NKHook5-Injector.exe");
                Serializer.cfg.NKHookVersion = web.LatestVersionNumber;
                Serializer.SaveSettings();
            }).Start();
        }
        public void DoUpdateTowerPlugin()
        {
            new Thread(() =>
            {
                WebHandler web = new WebHandler();
                string pluginPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\NKHook5\\Plugins\\NewTowerLoader.dll";
                string nkhfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\NKHook5";

                if (!web.CheckForUpdate(versionsURL, "NKHookTowerLoader: ", 4, Serializer.cfg.TowerLoadNKPluginVersion) && File.Exists(pluginPath))
                    return;

                if (File.Exists(pluginPath))
                    File.Delete(pluginPath);

                if (!Directory.Exists(nkhfolder + "\\Plugins"))
                    Directory.CreateDirectory(nkhfolder + "\\Plugins");

                web.DownloadFile("NewTowerLoader.dll", versionsURL, pluginPath, "NKHookTowerLoader: ", 4);
                Serializer.cfg.TowerLoadNKPluginVersion = web.LatestVersionNumber;
                Serializer.SaveSettings();
            }).Start();
        }

        public static void DoesHaveTowerLoader()
        {
            if (File.Exists(pathTowerLoadPlugin))
                return;


            ConsoleHandler.append("NKHook NewTowerLoader plugin not found.");
            DownloadTowerLoader();
        }
        
        public static void DownloadTowerLoader()
        {
            /*ConsoleHandler.append("Downloading NKHook NewTowerLoader plugin...");

            new Thread(() =>
            {
                WebHandler webHandler = new WebHandler();
                webHandler.DownloadFile("NKHook", "",
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\NKHook5\\Plugins\\NewTowerLoader.dll", "NKHookTowerLoader: ");
                
                ConsoleHandler.append("Successfully downloaded TowerLoading Plugin");
            }).Start();*/
        }
    }
}
