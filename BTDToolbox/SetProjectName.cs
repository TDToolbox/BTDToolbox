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
using BTDToolbox.Classes.NewProjects;
using System.IO;

namespace BTDToolbox
{
    public partial class SetProjectName : Form
    {
        public static string projectName;
        public static string gameName;
        public bool hasClickedRandomName;
        public bool isRenaming = false;
        ConfigFile programData;
        public JetForm jetf;

        public SetProjectName()
        {
            InitializeComponent();
            programData = Serializer.Deserialize_Config();
            gameName = CurrentProjectVariables.GameName;

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
        public string ReturnName(string projName, string gameName)
        {
            Random rand = new Random();
            string projectName_Identifier = "\\proj_" + gameName + "_";

            if (projName == null || projName == "")
            {
                int randName = rand.Next(1, 99999999);
                projName = randName.ToString();
            }

            return projectName_Identifier + projName;
        }
        private void SubmitModName()
        {           
            if (CustomName_RadioButton.Checked)
                ConsoleHandler.appendLog("You chose the project name: " + ProjectName_TextBox.Text);
            else
                ConsoleHandler.appendLog("You chose a random project name");
            if (isRenaming == true)
            {
                string temp = gameName;
                jetf.RenameProject(ReturnName(ProjectName_TextBox.Text, gameName));
                gameName = temp;
                this.Close();
            }

            bool overwriteProj = true;
            string projName = ReturnName(ProjectName_TextBox.Text, gameName);
            string projdir = Environment.CurrentDirectory + "\\Projects\\" + projName;


            if (Directory.Exists(projdir))
            {
                var result = MessageBox.Show("A project with this name already exists, do you want to replace it with a new one?", "Replace Existing Project?", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                {
                    ConsoleHandler.appendLog("Deleting original project");
                    Directory.Delete(projdir, true);
                    overwriteProj = true;
                }
                else if (result == DialogResult.Cancel)
                {
                    overwriteProj = false;
                    this.Close();
                }
                else
                {
                    overwriteProj = false;
                    ProjectName_TextBox.Text = "";
                }
            }

            if (overwriteProj == true)
            {
                if (!Directory.Exists(projdir))
                    Directory.CreateDirectory(projdir);

                string backupPath = Environment.CurrentDirectory + "\\Backups\\" + CurrentProjectVariables.GameName + "_Original.jet";
                if (!GeneralMethods.Validate_Backup(gameName))
                    GeneralMethods.CreateBackup(gameName);

                if (File.Exists(backupPath))
                {
                    if (!Directory.Exists(projdir + "\\"))
                        Directory.CreateDirectory(projdir + "\\");
                    File.Copy(backupPath, projdir + "\\" + projName + ".jet");
                    CurrentProjectVariables.ProjectName = ReturnName(ProjectName_TextBox.Text, gameName).Replace("\\", "");
                    CurrentProjectVariables.PathToProjectClassFile = projdir;

                    ProjectHandler.SaveProject();

                    

                }
                else
                {
                    ConsoleHandler.force_appendLog("Unable to locate or create backup... Cancelling project creation...");
                    if (Directory.Exists(projdir))
                        Directory.Delete(projdir);
                }
                //this.Close();
            }
            //if (gameName == "BTDB")
            if (gameName != "BTDB")
            {
                CurrentProjectVariables.JetPassword = "Q%_{6#Px]]";
                ProjectHandler.SaveProject();

                DirectoryInfo dinfo = new DirectoryInfo(projdir);
                jetf = new JetForm(dinfo, Main.getInstance(), dinfo.Name);
                jetf.Show();
                jetf.PopulateTreeview();
            }
            else
            {
                var getPasss = new Get_BTDB_Password();
                getPasss.Show();
            }
            this.Close();
            /*else if (gameName == "BTDB")
            {
                var getPasss = new Get_BTDB_Password();
                getPasss.isExtracting = true;
                //getPasss.projName = ProjectName_TextBox.Text;

                string temp = gameName;
                getPasss.projName = ReturnName(ProjectName_TextBox.Text, gameName);
                gameName = temp;
                getPasss.Show();
                this.Close();
            }
            else if (gameName == "BMC")
            {
                
            }
            else
            {
                var extract = new ZipForm();
                //extract.projName = ProjectName_TextBox.Text;

                string temp = gameName;
                extract.projName = ReturnName(ProjectName_TextBox.Text, gameName);
                gameName = temp;
                
                extract.Show();
                extract.Extract();
                this.Close();
            }*/
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