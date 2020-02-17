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

namespace BTDToolbox
{
    public partial class SettingsWindow : ThemedForm
    {
        ConfigFile projectData;
        public SettingsWindow()
        {
            InitializeComponent();
            this.Size = new Size(350, 550);

            projectData = Serializer.Deserialize_Config();
            Setup();
        }
        private void Setup()
        {
            if (projectData.enableSplash == true)
                EnableSplash.CheckState = CheckState.Checked;
            if (projectData.useExternalEditor == true)
                useExternalEditor.CheckState = CheckState.Checked;
        }
        private void Save_Button_Click(object sender, EventArgs e)
        {
            if (useExternalEditor.Checked)
            {
                JetForm.useExternalEditor = true;
            }
            else
            {
                JetForm.useExternalEditor = false;
            }

            if (EnableSplash.Checked)
            {
                Program.enableSplash = true;
            }
            else
            {
                Program.enableSplash = false;
            }
            Serializer.SaveSmallSettings("splash", projectData);
            Serializer.SaveSmallSettings("external editor", projectData);


            ConsoleHandler.appendLog("Settings saved!!!");
        }
    }
}
