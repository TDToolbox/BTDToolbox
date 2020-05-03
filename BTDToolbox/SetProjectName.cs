﻿using System;
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
            CreateProject();
        }
        private void CreateProject()
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
            //MessageBox.Show(projectName_Identifier + projName);
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

            bool writeProj = true;
            string projName = ReturnName(ProjectName_TextBox.Text, gameName);//.Replace("\\", "");
            string projdir = Environment.CurrentDirectory + "\\Projects" + projName;

/*            
            MessageBox.Show(projName);
            MessageBox.Show(projdir);*/

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
                        ConsoleHandler.appendLog("Deleting original project");
                        try
                        {
                            Directory.Delete(projdir, true);
                        }
                        catch { ConsoleHandler.appendLog("Directory is currently open in windows file explorer..."); }
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

                    if(!File.Exists(projdir + projName + ".jet"))
                    {
                        //File.Copy(backupPath, projdir + "\\" + projName + ".jet");
                        CurrentProjectVariables.ProjectName = projName;
                        CurrentProjectVariables.PathToProjectClassFile = projdir;
                        CurrentProjectVariables.PathToProjectFiles = projdir + projName;

                        ProjectHandler.SaveProject();
                    }
                    else
                    {
                        ConsoleHandler.force_appendLog("It appears the project already exists OR it is currently opened " +
                            "somewhere... Unable to continue, please try again..." +
                            "\nIf this error persists, please contact Toolbox devs");
                        this.Close();
                    }

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
                    ConsoleHandler.force_appendLog("Unable to locate or create backup... Cancelling project creation...");
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
    }
}