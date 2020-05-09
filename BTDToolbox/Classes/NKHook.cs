using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return true;
        }
        public static void LaunchNKH()
        {
            if(DoesNkhExist())
                Process.Start(nkhEXE);
        }
    }
}
