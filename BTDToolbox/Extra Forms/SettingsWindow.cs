using BTDToolbox.Classes;
using BTDToolbox.Classes.NewProjects;
using BTDToolbox.Extra_Forms;
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

            
            Setup();
        }
        private void Setup()
        {
            this.Size = new Size(350, 500);
            projectData = Serializer.Deserialize_Config();

            this.canResize = false;
            this.moveCenterScreen = true;

            EnableSplash.Checked = projectData.enableSplash;
            useExternalEditor.Checked = projectData.useExternalEditor;
            DisableUpdates_CB.Checked = projectData.disableUpdates;
            AutoFormatJSON_CB.Checked = projectData.autoFormatJSON;

            if (NKHook.CanUseNKH())
                return;
            
            UseNKH_CB.Checked = CurrentProjectVariables.UseNKHook;
            UseNKH_CB.Visible = true;
            CurrentProjSettings_Label.Visible = true;
        }
        private void Save_Button_Click(object sender, EventArgs e)
        {
            JetForm.useExternalEditor = useExternalEditor.Checked;
            Program.enableSplash = EnableSplash.Checked;
            Main.disableUpdates = DisableUpdates_CB.Checked;
            Main.autoFormatJSON = AutoFormatJSON_CB.Checked;

            CurrentProjectVariables.UseNKHook = UseNKH_CB.Checked;

            Serializer.SaveSmallSettings("splash");
            Serializer.SaveSmallSettings("disableUpdates");
            Serializer.SaveSmallSettings("autoFormatJSON");
            Serializer.SaveSmallSettings("external editor");

            ProjectHandler.SaveProject();
            ConsoleHandler.append_CanRepeat("Settings saved!!!");
            this.Close();
        }

        private void UseNKH_CB_CheckedChanged(object sender, EventArgs e)
        {
            if (!NKHook.DoesNkhExist())
            {
                if(UseNKH_CB.Checked == true)
                {
                    UseNKH_CB.Checked = false;

                    ConsoleHandler.force_append_Notice("You need to have NKHook installed to do that!");
                    MessageBox.Show("You need to have NKHook installed to do that!");
                    NKHook_Message msg = new NKHook_Message();
                    msg.Show();
                }
            }
        }
    }
}
