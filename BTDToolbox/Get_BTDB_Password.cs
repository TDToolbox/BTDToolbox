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


        //new refactoring variables
        public string projName { get; set; }
        public bool isExtracting { get; set; }

        public Get_BTDB_Password()
        {
            InitializeComponent();
            this.AcceptButton = CreateProject_Button;
            
        }
        public void GetPass()
        {
            string password = Password_TextBox.Text.ToString();
            if (password.Length < 3)
                MessageBox.Show("The password you entered was too short...");
            else
            {
                var zip = new ExtractingJet_Window();
                zip.jetFile_Game = "BTDB";
                zip.password = password;
                zip.projName = projName;                
                zip.Show();
                if (isExtracting == true)
                {
                    zip.Extract();
                }
                else
                {

                }
                this.Close();
            }
        }
        private void ExportingJetSetup()
        {
            CreateProject_Button.Text = "Submit Password";
            this.Text = "Enter password to compile...";
        }

        private void CreateProject_Button_Click(object sender, EventArgs e)
        {
            GetPass();
            /*string password = Password_TextBox.Text.ToString();
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
                if (projectName != null && projectName != "")
                {
                    ExtractingJet_Window.hasCustomProjectName = true;
                    ExtractingJet_Window.customName = projectName;
                }
                else
                {
                    ExtractingJet_Window.hasCustomProjectName = false;
                }
                ExtractingJet_Window.customName = projectName;
                ExtractingJet_Window.jetPassword = password;
                var extract = new ExtractingJet_Window();

                this.Close();
            }*/
        }
    }
}
