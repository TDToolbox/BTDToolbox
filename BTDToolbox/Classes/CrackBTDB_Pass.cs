using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BTDToolbox.GeneralMethods;

namespace BTDToolbox.Classes
{
    class CrackBTDB_Pass
    {
        public static string PasswordToolsDir = Environment.CurrentDirectory + "\\BTDB Password tools";
        public static string DirPrompts_Dir = Environment.CurrentDirectory + "\\BTDB Password tools\\Directory Prompts";
        public static string Strings2Dir = Environment.CurrentDirectory + "\\BTDB Password tools\\Strings2";
        public static string JohntrDir = Environment.CurrentDirectory + "\\BTDB Password tools\\Johntr";

        public void Get_BTDB_Password()
        {
            if (isGamePathValid("BTDB") == true)
            {
                MessageBox.Show("Currently this only works for the steam version");
                Thread bgThread = new Thread(HandleTools);
                bgThread.Start();
            }
            else
            {
                ConsoleHandler.appendLog("Your game directory has not been set. Please select browse for Battles-Win.exe");
                browseForExe("BTDB");
                Get_BTDB_Password();
            }
        }
        public void HandleTools()
        {
            CheckForDependencies();

            DialogResult reuslt = MessageBox.Show("It will take up to 5 minutes to download the necessary tools. This is a One Time thing.\n\nDo you wish to continue?", "Do you wish to continue?", MessageBoxButtons.YesNo);
            if (reuslt == DialogResult.Yes)
            {
                DownloadTools();

                ConsoleHandler.appendLog("Extracting strings2.zip");
                ExtractTools(Strings2Dir + "\\strings2.zip", Strings2Dir);
                ConsoleHandler.appendLog("Extracting johntr.zip");
                ExtractTools(JohntrDir + "\\johntr.zip", JohntrDir);

                File.Delete(Strings2Dir + "\\strings2.zip");
                File.Delete(JohntrDir + "\\johntr.zip");
            }
            else
            {
                ConsoleHandler.appendLog("Download cancelled...");
            }

        }
        public bool CheckForDependencies()
        {
            DirectoryInfo dinfo = new DirectoryInfo(PasswordToolsDir);
            if (!dinfo.Exists)
            {
                return false;
            }
            else
            {

            }
            return true;
        }

        //
        //Johntr download is commented out
        //
        public void DownloadTools()
        {
            WebClient Client = new WebClient();
            string strings2_url = "";
            string johntr_url = "";
            string DirPropmts_url = "https://www.dropbox.com/s/9xumg3cvo3gbo5x/Directory_Prompts.reg?dl=0";

            
            if (GetOS_Type() == "64")    //Get 64bit version of tools, otherwise get 32bit version
            {
                strings2_url = "http://split-code.com/files/strings2_x64_v1-2.zip";
                johntr_url = "https://www.dropbox.com/s/6hiifxlffqrizeu/john-1.9.0-jumbo-1-win64.zip?dl=1";
            }
            else
            {
                strings2_url = "http://split-code.com/files/strings2_x86_v1-2.zip";
                johntr_url = "https://www.dropbox.com/s/t3pd2ypnhqo9n0f/john-1.9.0-jumbo-1-win32.zip?dl=1";
            }

            //Create directories if they're not there
            if (!Directory.Exists(DirPrompts_Dir))
                Directory.CreateDirectory(DirPrompts_Dir);
            if (!Directory.Exists(Strings2Dir))
                Directory.CreateDirectory(Strings2Dir);
            if (!Directory.Exists(JohntrDir))
                Directory.CreateDirectory(JohntrDir);
                

            //Delete previous files if they ARE there
            if (File.Exists(DirPrompts_Dir + "\\Directory_Prompts.reg"))
                File.Delete(DirPrompts_Dir + "\\Directory_Prompts.reg");

            if (File.Exists(Strings2Dir + "\\strings2.zip"))
                File.Delete(Strings2Dir + "\\strings2.zip");

            if (File.Exists(JohntrDir + "\\johntr.zip"))
                File.Delete(JohntrDir + "\\johntr.zip");


            //Download files
            ConsoleHandler.appendLog("Downloading Directory Prompts...");
            Client.DownloadFile(DirPropmts_url, DirPrompts_Dir + "\\Directory_Prompts.reg");
            ConsoleHandler.appendLog("Finished downloading Directory Prompts.");

            ConsoleHandler.appendLog("Downloading strings2...");
            Client.DownloadFile(strings2_url, Strings2Dir + "\\strings2");
            File.Move(Strings2Dir + "\\strings2", Strings2Dir + "\\strings2.zip");
            ConsoleHandler.appendLog("Finished downloading strings2.");

            ConsoleHandler.appendLog("Downloading johntr. This will take up to 5 minutes...\n>> If the program freezes, just leave it.");
            Client.DownloadFile(johntr_url, JohntrDir + "\\johntr.zip");
            ConsoleHandler.appendLog("Finished downloading johntr");
            ConsoleHandler.appendLog("Thank you for being patient. The tools have finished downloading.");
        }
        private void ExtractTools(string zipPath, string outputPath)
        {
            ConsoleHandler.appendLog("Beginning extraction...");
            ZipFile archive = new ZipFile(zipPath);
            foreach (ZipEntry e in archive)
            {
                e.Extract(outputPath, ExtractExistingFileAction.DoNotOverwrite);
            }
            archive.Dispose();
            ConsoleHandler.appendLog("Tool has been successfully downloaded and extracted...");
        }
    }
}
