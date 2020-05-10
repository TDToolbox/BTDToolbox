using System;
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
    public partial class AlwaysUseNKH : Form
    {
        bool useNKHDefault = false;
        public bool Launch { get; set; }
        public AlwaysUseNKH()
        {
            InitializeComponent();               
        }
        public AlwaysUseNKH(bool launch) : this()
        {
            Launch = launch;
        }

        private void Submit()
        {
            if(Launch == true)
            {
                if(useNKHDefault)
                    GeneralMethods.CompileJet("launch nkh");
                else
                    GeneralMethods.CompileJet("launch");
            }
            this.Close();
        }
        private void No_Button_Click(object sender, EventArgs e)
        {
            CurrentProjectVariables.DontAskAboutNKH = Dont_Ask_Again_Checkbox.Checked;
            ProjectHandler.SaveProject();
            useNKHDefault = false;

            ConsoleHandler.append("You selected No. If you checked \"Dont Ask Me Again\" then BTD Toolbox won't ask you" +
                " for the rest of this session. You can always change this by going to Settings and checking Use NKHook");
            Submit();
        }

        private void Yes_Button_Click(object sender, EventArgs e)
        {
            CurrentProjectVariables.UseNKHook = true;
            ProjectHandler.SaveProject();
            useNKHDefault = true;

            ConsoleHandler.append("You selected Yes. From now on when you quick launch this project BTD Toolbox " +
                "will use NKHook");
            Submit();
        }
    }
}
