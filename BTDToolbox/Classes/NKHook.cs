using BTDToolbox.Classes.NewProjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BTDToolbox.Classes
{
    class NKHook
    {
        public static string nkhEXE = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\NKHook5\\NKHook5-Injector.exe";
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
    }
}
