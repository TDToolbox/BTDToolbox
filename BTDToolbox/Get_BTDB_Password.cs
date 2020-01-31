using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTDToolbox
{
    public partial class Get_BTDB_Password : Form
    {
        public static string projectName;
        public static bool setPassword;
        public static string compileOperation;

        public Get_BTDB_Password()
        {
            InitializeComponent();
            this.AcceptButton = CreateProject_Button;
            
            if (setPassword == true)
            {
                if (ExtractingJet_Window.jetPassword != null)
                {
                    this.Close();
                }
                else
                {
                    ExportingJet();
                }
                
            }
        }
        private void ExportingJet()
        {
            CreateProject_Button.Text = "Submit Password";
            this.Text = "Enter password to compile...";
        }

        private void CreateProject_Button_Click(object sender, EventArgs e)
        {
            string password = Password_TextBox.Text.ToString();
            if(password.Length < 3)
            {
                MessageBox.Show("The password you entered was too short...");
            }
            else
            {
                if (setPassword == true)
                {
                    if (compileOperation.Contains("output"))
                        ExtractingJet_Window.switchCase = "output";
                    else if (compileOperation.Contains("compile"))
                        ExtractingJet_Window.switchCase = "compile";
                }
                if (projectName != null || projectName != "")
                {
                    ExtractingJet_Window.hasCustomProjectName = true;
                    ExtractingJet_Window.customName = projectName;
                }
                else
                {
                    ExtractingJet_Window.hasCustomProjectName = false;
                }
                ExtractingJet_Window.jetPassword = password;
                var extract = new ExtractingJet_Window();

                if (setPassword == true)
                    this.Close();
            }
        }
    }
}
