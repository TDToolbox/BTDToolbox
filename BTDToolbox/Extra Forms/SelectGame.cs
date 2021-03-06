﻿using BTDToolbox.Classes;
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
            if(isGamePathValid(gameName))
            {
                if (!Validate_Backup(gameName))
                    CreateBackup(gameName);
                return true;
            }

            string gameD = GetGameDir(gameName);
            if(Guard.IsStringValid(gameD))
            {
                SaveGamePath(gameName, gameD);
                if (!Validate_Backup(gameName))
                    CreateBackup(gameName);
                return true;
            }

            DialogResult diag = MessageBox.Show("Unable to find the game directory. Do you want to create the project without" +
                    " the game? If you choose Yes you wont be able to launch your mod from toolbox until you " +
                    "save the game path.", "Create Project Without the Game?", MessageBoxButtons.YesNo); ;

            if (diag == DialogResult.Yes)
                return true;
            
            return false;
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
