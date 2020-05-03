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
            ZipForm.existingJetFile = this.JetPath;

            Main.gameName = GameName;
            Serializer.SaveConfig(this, "game");

            ProjectHandler.CreateProject();
            CurrentProjectVariables.GameName = GameName;
            CurrentProjectVariables.GamePath = GeneralMethods.ReturnGamePath(GameName);
            ProjectHandler.SaveProject();


            var getName = new SetProjectName();
            getName.Show();

            this.Close();
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
