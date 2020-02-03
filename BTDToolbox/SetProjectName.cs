using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BTDToolbox.ProjectConfig;

namespace BTDToolbox
{
    public partial class SetProjectName : Form
    {
        public static string projectName;
        public static string gameName;
        public bool hasClickedRandomName;
        ConfigFile programData;

        public SetProjectName()
        {
            InitializeComponent();
            programData = Serializer.Deserialize_Config();
            gameName = programData.CurrentGame;
            if (gameName == "BTDB")
            {
                CreateProject_Button.Text = "Continue";
            }
            else
                CreateProject_Button.Text = "Create Project";
                
            this.AcceptButton = CreateProject_Button;
            this.Activate();
        }
        private void CreateProject_Button_Click(object sender, EventArgs e)
        {
            if (CustomName_RadioButton.Checked)
            {
                if (ProjectName_TextBox.TextLength == 0)
                {
                    MessageBox.Show("Error! You didn't enter a project name!");
                }
                else
                {
                    SubmitModName();
                }
            }
            else
                SubmitModName();
        }
        private void SubmitModName()
        {
            ConsoleHandler.appendLog("You chose the project name: " + ProjectName_TextBox.Text);
            if (gameName == "BTDB")
            {
                var getPasss = new Get_BTDB_Password();
                getPasss.isExtracting = true;
                getPasss.projName = ProjectName_TextBox.Text;
                getPasss.Show();
                this.Close();
            }
            else
            {
                var extract = new ExtractingJet_Window();
                extract.projName = ProjectName_TextBox.Text;
                extract.Show();
                extract.Extract();
                this.Close();
            }
        }

        private void CustomName_RadioButton_MouseClick(object sender, MouseEventArgs e)
        {
            if (hasClickedRandomName)
            {
                CreateProject_Button.Location = new Point(CreateProject_Button.Location.X, CreateProject_Button.Location.Y + 40);
            }
            hasClickedRandomName = false;
            ProjectName_TextBox.Visible = true;
        }

        private void RandomName_RadioButton_MouseClick(object sender, MouseEventArgs e)
        {
            if (!hasClickedRandomName)
            {
                hasClickedRandomName = true;
                ProjectName_TextBox.Visible = false;
                CreateProject_Button.Location = new Point(CreateProject_Button.Location.X, CreateProject_Button.Location.Y - 40);
            }

        }
    }
}