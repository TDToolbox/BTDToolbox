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
    public partial class SetProjectName : Form
    {
        public static string projectName;
        public static string gameName;
        public bool hasClickedRandomName;

        public SetProjectName()
        {
            InitializeComponent();
            
            if (gameName == "BTDB")
            {
                CreateProject_Button.Text = "Continue";
            }
            else
                CreateProject_Button.Text = "Create Project";

            this.AcceptButton = CreateProject_Button;
            this.Activate();
            ExtractingJet_Window.switchCase = "decompile";
        }
        private void CreateProject_Button_Click(object sender, EventArgs e)
        {
            if (!hasClickedRandomName)
            {
                if (ProjectName_TextBox.TextLength == 0)
                {
                    MessageBox.Show("Error! You didn't enter a project name!");
                }
                else
                {
                    if (gameName == "BTDB")
                    {
                        var getPass = new Get_BTDB_Password();
                        Get_BTDB_Password.projectName = ProjectName_TextBox.Text;
                        getPass.Show();
                        this.Close();
                    }
                    else
                    {
                        ExtractingJet_Window.hasCustomProjectName = true;
                        ExtractingJet_Window.customName = ProjectName_TextBox.Text;
                        this.Close();
                        var extractT = new ExtractingJet_Window();
                    }
                    

                }
            }
            else
            {
                if (gameName == "BTDB")
                {
                    var getPass = new Get_BTDB_Password();
                    getPass.Show();
                    this.Close();
                }
                else
                {
                    this.Close();
                    var extractT = new ExtractingJet_Window();
                }   
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