using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace BTDToolbox
{
    class Launcher
    {
        public static void launchGame(JetForm form)
        {/*
            while (true)
            {
                ConsoleHandler.appendLog("Launching game...");
                try
                {
                    string gameJetPath = Settings.readGamePath() + "\\..\\Assets\\BTD5.jet";
                    if (!File.Exists(Environment.CurrentDirectory + "\\Backups\\Original.jet"))
                    {
                        ConsoleHandler.appendLog("Jet backup not found, creating one...");
                        Directory.CreateDirectory(Environment.CurrentDirectory + "\\Backups");
                        File.Copy(gameJetPath, Environment.CurrentDirectory + "\\Backups\\Original.jet");
                        ConsoleHandler.appendLog("Backup done");
                    }
                    DirectoryInfo projDir = new DirectoryInfo(Environment.CurrentDirectory + "\\" + form.projName);
                    ConsoleHandler.appendLog("Compiling jet...");
                    ExtractingJet_Window.isDecompiling = false;
                    ExtractingJet_Window.isCompiling = true;
                    ExtractingJet_Window.launchProgram = true;
                    ExtractingJet_Window ejw = new ExtractingJet_Window();
                    ejw.compile(projDir, gameJetPath);
                    ConsoleHandler.appendLog("Jet compiled");
                    Process.Start(Settings.readGamePath());
                    ConsoleHandler.appendLog("Steam is taking over for the rest of the launch.");
                    break;
                }
                catch (Exception exc)
                {
                    ConsoleHandler.appendLog(exc.ToString());
                    ConsoleHandler.appendLog("No launch dir defined or is wrong.");
                    OpenFileDialog fileDiag = new OpenFileDialog();
                    fileDiag.Title = "Open game exe";
                    fileDiag.DefaultExt = "exe";
                    fileDiag.Filter = "Exe files (*.exe)|*.exe|All files (*.*)|*.*";
                    fileDiag.Multiselect = false;
                    if (fileDiag.ShowDialog() == DialogResult.OK)
                    {
                        string file = fileDiag.FileName;
                        Settings.setGamePath(file);
                        string jet = file + "\\..\\Assets\\BTD5.jet";
                        if(!File.Exists(Environment.CurrentDirectory + "\\Backups\\Original.jet"))
                        {
                            ConsoleHandler.appendLog("Jet backup not found, creating one...");
                            Directory.CreateDirectory(Environment.CurrentDirectory + "\\Backups");
                            File.Copy(jet, Environment.CurrentDirectory + "\\Backups\\Original.jet");
                            ConsoleHandler.appendLog("Backup done");
                        }
                        ConsoleHandler.appendLog("Launch settings saved in launchSettings.txt");
                    }
                    else
                    {
                        ConsoleHandler.appendLog("Launch cancelled");
                        break;
                    }
                }
            }
        }

        public static void restoreGame()
        {
            if (!File.Exists(Environment.CurrentDirectory + "\\Backups\\Original.jet"))
            {
                MessageBox.Show("No backup found that can be restored! Use steam to re-download the original .jet");
                return;
            }
            ConsoleHandler.appendLog("Restoring backup .jet");
            string gameJetPath = Settings.readGamePath() + "\\..\\Assets\\BTD5.jet";
            File.Delete(gameJetPath);
            File.Copy(Environment.CurrentDirectory + "\\Backups\\Original.jet", gameJetPath);
            ConsoleHandler.appendLog("Backup restored");
            MessageBox.Show("Backup .jet restored!");
        }*/
        }
    }
}
