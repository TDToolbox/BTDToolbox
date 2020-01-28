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
        public bool hasClickedRandomName;
        //public static bool doesProjectAlreadyExist;

        public SetProjectName()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Left = 100;
            this.Top = 100;
            this.AcceptButton = CreateProject_Button;
            this.Activate();
            ExtractingJet_Window.switchCase = "decompile";
        }
        private void SetProjectName_Load(object sender, EventArgs e)
        {
            
            this.Show();
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
                    ExtractingJet_Window.hasCustomProjectName = true;
                    ExtractingJet_Window.customName = ProjectName_TextBox.Text;
                    this.Close();
                    var extractT = new ExtractingJet_Window();

                }
            }
            else
            {
                this.Close();
                var extractT = new ExtractingJet_Window();   
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