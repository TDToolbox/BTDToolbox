using BTDToolbox.Classes;
using BTDToolbox.Classes.NewProjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BTDToolbox.GeneralMethods;

namespace BTDToolbox.Extra_Forms
{
    public partial class SelectGame : Form
    {
        #region Constructors
        public SelectGame()
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// Constructs the form with a jet file path to pass it to other forms easier. This would be used for example
        /// if the user browsed for a jet file, then the game name is asked for.
        /// </summary>
        /// <param name="jetPath">Path to the jet file that was selected</param>
        public SelectGame(string jetPath) : this()
        {
            this.JetPath = jetPath;
        }

        #endregion


        #region Properties
        public string JetPath { get; set; }
        public string GameName { get; set; }
        #endregion

        public void OnGameSelected()
        {
            if (!Guard.IsStringValid(JetPath))
                JetPath = Environment.CurrentDirectory + "\\Backups\\" + GameName + "_Original.jet";

            if (DoWork(GameName) == false)
            {
                this.Close();
                return;
            }

            ZipForm.existingJetFile = JetPath;

            Serializer.cfg.CurrentGame = GameName;
            Serializer.SaveSettings();

            ProjectHandler.CreateProject();
            CurrentProjectVariables.GameName = GameName;
            CurrentProjectVariables.GamePath = ReturnGamePath(GameName);

            var getName = new SetProjectName();
            getName.Show();

            this.Close();
        }

        public bool DoWork(string gameName)
        {
            if (isGamePathValid(gameName) == false)
            {
                string gameFolder = "";
                if (gameName == "BTD5")
                    gameFolder = "BloonsTD5";
                if (gameName == "BTDB")
                    gameFolder = "Bloons TD Battles";
                if (gameName == "BMC")
                    gameFolder = "Bloons Monkey City";

                bool failed = false;
                string tryFindGameDir = Main.TryFindSteamDir(gameFolder);

                if (tryFindGameDir == "")
                {
                    failed = true;
                    ConsoleHandler.append("Failed to automatically aquire game dir");
                }
                else
                {
                    ConsoleHandler.append("Game directory was automatically aquired...");
                    if (gameName == "BTD5")
                        Serializer.cfg.BTD5_Directory = tryFindGameDir;
                    else if (gameName == "BTDB")
                        Serializer.cfg.BTDB_Directory = tryFindGameDir;
                    else if (gameName == "BMC")
                        Serializer.cfg.BMC_Directory = tryFindGameDir;

                    Serializer.SaveSettings();

                    CurrentProjectVariables.GameName = tryFindGameDir;
                    ProjectHandler.SaveProject();
                }

                if (failed)
                {
                    DialogResult diag = MessageBox.Show("Unable to find the game directory. Do you want to create the project without" +
                        " the game? Choose no to browse for your game's EXE." +
                        " If you choose Yes, you wont be able to launch your mod until you select it.", "Browse for EXE", MessageBoxButtons.YesNoCancel); ;
                    
                    if (diag == DialogResult.Cancel)
                        return false;
                    else if (diag == DialogResult.Yes)
                    {
                        ConsoleHandler.append("Please browse for " + Get_EXE_Name(gameName));
                        browseForExe(gameName);
                        if (isGamePathValid(gameName) == false)
                        {
                            ConsoleHandler.append("Theres been an error identifying your game");
                            return false;
                        }
                    }
                }
            }

            if (!Validate_Backup(gameName))
                CreateBackup(gameName);

            if (!Validate_Backup(gameName))
            {
                ConsoleHandler.force_append_Notice("Failed to create a new project because the backup failed to be aquired...");
                return false;
            }
            return true;
        }


        #region UI Events
        private void BTD5_Button_Click(object sender, EventArgs e)
        {
            GameName = "BTD5";
            OnGameSelected();
        }

        private void BTDB_Button_Click(object sender, EventArgs e)
        {
            GameName = "BTDB";
            OnGameSelected();
        }

        private void BMC_Button_Click(object sender, EventArgs e)
        {
            GameName = "BMC";
            OnGameSelected();
        }
        #endregion
    }
}
