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

        public Get_BTDB_Password()
        {
            InitializeComponent();
            this.AcceptButton = CreateProject_Button;
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
                //this.Close();
            }
        }
    }
}
