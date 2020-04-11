﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BTDToolbox.Classes.NewProjects;

namespace BTDToolbox
{
    public partial class Get_BTDB_Password : Form
    {
        //new refactoring variables
        public static bool rememberPass;
        public string projName { get; set; }
        public string destPath { get; set; }
        public bool isExtracting { get; set; }
        public bool launch { get; set; }
        string savedProjPath = CurrentProjectVariables.JetPassword;
        string savedSettingsPass = Serializer.Deserialize_Config().battlesPass;
        public Get_BTDB_Password()
        {
            InitializeComponent();
            this.AcceptButton = CreateProject_Button;

            if (savedSettingsPass == "" || savedSettingsPass == null)
                UseLastPass_CB.Visible = false;
            else
                UseLastPass_CB.Visible = true;

            if (rememberPass == true)
                Dont_Ask_Again_Checkbox.Checked = true;
            else
                Dont_Ask_Again_Checkbox.Checked = false;
        }
        public void GetPass()
        {
            string password = Password_TextBox.Text.ToString();
            if (password.Length < 3)
            {
                ConsoleHandler.appendLog("The password you entered was too short...");
                MessageBox.Show("The password you entered was too short...");
            }
            else
            {
                ConsoleHandler.appendLog("You entered the password:  " + password);
                var zip = new ZipForm();
                zip.jetFile_Game = "BTDB";
                zip.password = password;
                zip.projName = projName;                
                zip.Show();

                if (Dont_Ask_Again_Checkbox.Checked)
                {
                    rememberPass = true;
                    ConsoleHandler.appendLog("Program will remember your password for the rest of this session.");
                    ZipForm.rememberedPassword = Password_TextBox.Text;
                }
                else
                {
                    rememberPass = false;
                    ZipForm.rememberedPassword = "";
                }
                if (isExtracting == true)
                {
                    zip.Extract();
                }
                else
                {
                    zip.destPath = destPath;
                    zip.launch = launch;
                    zip.Compile();
                }
                this.Close();
            }
        }

        private void CreateProject_Button_Click(object sender, EventArgs e)
        {
            string projectJet = CurrentProjectVariables.PathToProjectClassFile + "\\" + CurrentProjectVariables.ProjectName + ".jet";

            if (File.Exists(projectJet))
            {
                bool result = GeneralMethods.Bad_JetPass(projectJet, Password_TextBox.Text);
                if (!result)
                {
                    CurrentProjectVariables.JetPassword = Password_TextBox.Text;
                    ProjectHandler.SaveProject();
                    this.Close();
                }
                else
                {
                    ConsoleHandler.force_appendLog("You entered a bad password. Please check your password and try again");
                }
            }
            else
            {
                ConsoleHandler.force_appendLog("Your project file was not detected...");
            }

            //GetPass();
        }

        private void UseLastPass_CB_CheckedChanged(object sender, EventArgs e)
        {
            if (UseLastPass_CB.Checked)
            {
                if (savedProjPath.Length > 0)
                    Password_TextBox.Text = savedProjPath;
                else if(savedSettingsPass.Length > 0)
                    Password_TextBox.Text = savedSettingsPass;
                Password_TextBox.ReadOnly = true;
            }
            else
            {
                Password_TextBox.Text = "";
                Password_TextBox.ReadOnly = false;
            }
        }
    }
}
