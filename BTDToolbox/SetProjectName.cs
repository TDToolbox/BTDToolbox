using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BTDToolbox.Classes.NewProjects;
using System.IO;
using BTDToolbox.Classes;

namespace BTDToolbox
{
    public partial class SetProjectName : Form
    {
        public static string projectName;
        public static string gameName;
        public bool hasClickedRandomName;
        public bool isRenaming;
        public JetForm jetf;
        string customFolder;

        public SetProjectName()
        {
            InitializeComponent();

            gameName = CurrentProjectVariables.GameName;

            if (!NKHook.CanUseNKH())
            {
                label2.Location = new Point(12, 120);
                UseNKH_CB.Visible = false;
            }

            this.AcceptButton = CreateProject_Button;
            this.Activate();
            this.Show();
            this.Shown += SetProjectName_Shown;
        }

        private void SetProjectName_Shown(object sender, EventArgs e)
        {
            if (isRenaming == true)
                CreateProject_Button.Text = "Rename";
            else if (gameName == "BTDB")
                CreateProject_Button.Text = "Continue";
            else
                CreateProject_Button.Text = "Create Project";
        }

        private void CreateProject_Button_Click(object sender, EventArgs e)
        {
            CreateProject();
        }

        private void CreateProject()
        {
            if (CustomName_RadioButton.Checked && ProjectName_TextBox.TextLength == 0)
                MessageBox.Show("Error! You didn't enter a project name!");

            SubmitModName();
        }

        public string ReturnName(string projName, string gameName)
        {
            Random rand = new Random();
            string projectName_Identifier = "proj_" + gameName + "_";

            if (projName == null || projName == "")
            {
                int randName = rand.Next(1, 99999999);
                projName = randName.ToString();
            }
            return projectName_Identifier + projName;
        }

        private bool HasCustomDestination()
        {
            if (Guard.IsStringValid(customFolder))
                return true;
            else
                return false;
        }

        private void RenameProject()
        {
            string newProjName = ReturnName(ProjectName_TextBox.Text, gameName);
            string renamePath = "";

            if (HasCustomDestination())
                renamePath = customFolder + "\\" + newProjName;
            else
                renamePath = Environment.CurrentDirectory + "\\Projects\\" + newProjName;

            ConsoleHandler.append("Renaming project to  " + newProjName);
            jetf.RenameProject(newProjName, renamePath);
        }
        private void SubmitModName()
        {
            
            if (CustomName_RadioButton.Checked)
                ConsoleHandler.append("You chose the project name: " + ProjectName_TextBox.Text);
            else
                ConsoleHandler.append("You chose a random project name");
            if (isRenaming == true)
            {
                this.Hide();
                RenameProject();
                this.Close();
                return;
            }

            bool writeProj = true;
            string projName = ReturnName(ProjectName_TextBox.Text, gameName);
            string projdir = "";

            if(Guard.IsStringValid(customFolder))
                projdir = customFolder + "\\" + projName;
            else
                projdir = Environment.CurrentDirectory + "\\Projects\\" + projName;

            if (Directory.Exists(projdir))
            {
                var result = MessageBox.Show("A project with this name already exists, do you want to replace it with a new one?", "Replace Existing Project?", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Cancel)
                {
                    writeProj = false;
                    this.Close();
                }
                else
                {
                    if (result == DialogResult.Yes)
                    {
                        ConsoleHandler.append("Deleting original project");
                        try
                        {
                            Directory.Delete(projdir, true);
                        }
                        catch { ConsoleHandler.append("Directory is currently open in windows file explorer..."); }
                        writeProj = true;
                    }
                    else
                    {
                        writeProj = false;
                        ProjectName_TextBox.Text = "";
                    }
                }
            }

            if (writeProj == true)
            {
                if (!Directory.Exists(projdir))
                    Directory.CreateDirectory(projdir);

                string backupPath = Environment.CurrentDirectory + "\\Backups\\" + CurrentProjectVariables.GameName + "_Original.jet";
                if (!GeneralMethods.Validate_Backup(gameName))
                    GeneralMethods.CreateBackup(gameName);

                if (File.Exists(backupPath))
                {
                    if (!Directory.Exists(projdir))
                        Directory.CreateDirectory(projdir);

                    CurrentProjectVariables.ProjectName = projName;
                    CurrentProjectVariables.PathToProjectClassFile = projdir;
                    CurrentProjectVariables.PathToProjectFiles = projdir + "\\" + projName;
                    CurrentProjectVariables.UseNKHook = UseNKH_CB.Checked;
                    ProjectHandler.SaveProject();

                    if (gameName == "BTDB")
                    {
                        var getPasss = new Get_BTDB_Password();
                        getPasss.Show();
                        getPasss.isExtracting = true;
                        this.Close();
                    }
                    else
                    {
                        var zip = new ZipForm();
                        zip.Show();
                        zip.Extract();
                        this.Close();
                    }
                }
                else
                {
                    ConsoleHandler.append_Force("Unable to locate or create backup... Cancelling project creation...");
                    if (Directory.Exists(projdir))
                        Directory.Delete(projdir);
                    this.Close();
                }
            }
            
            //This stuff is for zip projects
            /*if (gameName != "BTDB")
            {
                CurrentProjectVariables.JetPassword = "Q%_{6#Px]]";
                ProjectHandler.SaveProject();

                DirectoryInfo dinfo = new DirectoryInfo(projdir);
                jetf = new JetForm(dinfo, Main.getInstance(), dinfo.Name);
                jetf.MdiParent = Main.getInstance();
                jetf.Show();
                jetf.PopulateTreeview();
            }
            else
            {
                var getPasss = new Get_BTDB_Password();
                getPasss.Show();
            }
            this.Close();*/
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

        private void SetProjectName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CreateProject();
            }
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void CustomLocation_Button_Click(object sender, EventArgs e)
        {
            string path = GeneralMethods.BrowseForDirectory("Please choose where to save the project", Environment.CurrentDirectory);
            this.Focus();

            if (!Guard.IsStringValid(path))
            {
                ConsoleHandler.append("You didn't select a valid folder. Please try again, or use let toolbox use the default folder");
                MessageBox.Show("You didn't select a valid folder. Please try again, or use let toolbox use the default folder");
                return;
            }
            
            customFolder = path;
        }

        private void UseNKH_CB_CheckedChanged(object sender, EventArgs e)
        {
            if(UseNKH_CB.Checked)
                ConsoleHandler.force_append_Notice("Checking this will make toolbox use NKH with your project by default. You can always change this later in Settings if you want by clicking \"File\" at the top, then \"Settings\"");
            this.Focus();
        }
    }
}