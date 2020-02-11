using IWshRuntimeLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTDToolbox.Classes
{
    class CreateDesktopShortcut
    {
        /*public static void CreateShortcut(string shortcutName)//, string shortcutPath)//, string targetFileLocation)
        {
            string deskDir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            string shortcutLocation = Path.Combine(deskDir, shortcutName + ".lnk");
            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutLocation);

            shortcut.Description = "BTDToolbox shortcut";   // The description of the shortcut
            shortcut.IconLocation = Properties.Resources.PossibleBTD5MODIcon.ToString();           // The icon of the shortcut
            shortcut.TargetPath = Environment.CurrentDirectory + "BTDToolbox.exe";                 // The path of the file that will launch when the shortcut is run
            shortcut.Save();                                    // Save the shortcut
        }*/
        public static void CreateShortcut()
        {
            object shDesktop = (object)"Desktop";
            WshShell shell = new WshShell();
            string shortcutAddress = (string)shell.SpecialFolders.Item(ref shDesktop) + @"\BTDToolbox.lnk";
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutAddress);
            shortcut.Description = "New shortcut for a Notepad";
            shortcut.Hotkey = "Ctrl+Shift+N";
            shortcut.TargetPath = Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\\BTDToolbox.exe";
            shortcut.Save();
        }
    }
}
