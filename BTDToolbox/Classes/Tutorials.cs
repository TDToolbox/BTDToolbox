using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Windows.Forms;

namespace BTDToolbox.Classes
{
    class Tutorials
    {
        private static Tutorials tut;
        string tutorialFilePath = Environment.CurrentDirectory + "\\TutorialInfo.txt";
        public static Dictionary<string, string> tutorials = new Dictionary<string, string>();
        
        public static bool HasFreshTutList()
        {
            if (tut == null)
                return false;
            else
                return true;
        }
        
        public static void GetTutorialsList()
        {
            if (tut == null)
                tut = new Tutorials();

            string url = "https://raw.githubusercontent.com/TDToolbox/BTDToolbox-2019_LiveFIles/master/btd%20modding%20tutorials";
            WebHandler web = new WebHandler();
            new Thread(() =>
            {
                string returnText = web.WaitOn_URL(url);
                if (!Guard.IsStringValid(returnText))
                {
                    if (!File.Exists(tut.tutorialFilePath))
                    {
                        ConsoleHandler.append("Failed to get fresh tutorials list and unable to find the stored list. " +
                            "If this problem continues please restart BTD Toolbox.");
                        tut = null;
                        return;
                    }
                    ConsoleHandler.append("Failed to get tutorials list. Toolbox will use the stored list instead, please note it may " +
                        "not be updated. Restart BTD Toolbox if you want to try getting the list again.");
                }
                else
                {
                    ConsoleHandler.append("Aquired tutorials list");
                    tut.CreateTutorialsFile();
                    tut.WriteToTutFile(returnText);
                    ConsoleHandler.append("Finished writing tutorials list to file");
                }
                tut.CreateTutorialsDictionary(tutorials);
                tut.PopulateTutorialsList();
            }).Start();
        }

        private void PopulateTutorialsList()
        {
            if (tutorials.Count == 0)
                return;

            Main.getInstance().Invoke((MethodInvoker)delegate
            {
                Main.getInstance().Tutorials_Button.DropDownItems.Clear();

                foreach (var tutorial in tutorials)
                {
                    Main.getInstance().Tutorials_Button.DropDownItems.Add(tutorial.Key);
                }

                Main.getInstance().Tutorials_Button.ShowDropDown();
            });

        }

        public static void OpenTutorial(string tutorialName)
        {
            if(tutorials == null)
            {
                ConsoleHandler.append("Tutorial list is null. Unable to open tutorial");
                return;
            }
            ConsoleHandler.append("Opening tutorial:  " + tutorialName);
            Browser browser = new Browser(Main.getInstance(), tutorials[tutorialName]);
        }
        private void CreateTutorialsFile()
        {
            ConsoleHandler.append("Creating new tutorials list");
            if (File.Exists(tutorialFilePath))
                File.Delete(tutorialFilePath);

            File.Create(tutorialFilePath).Close();
        }

        private Dictionary<string, string> CreateTutorialsDictionary(Dictionary<string, string> tutorials)
        {
            string[] lines = File.ReadAllLines(tutorialFilePath);
            if (lines.Length == 0)
                return tutorials;

            foreach(string line in lines)
            {
                string[] split = line.Split('|');
                tutorials.Add(split[0].Replace("|", "").Trim(), split[1].Replace("|", "").Trim());
            }
            return tutorials;
        }

        private void WriteToTutFile(string text)
        {
            File.WriteAllText(tutorialFilePath, text);
        }
    }
}
