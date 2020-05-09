using BTDToolbox.Classes;
using BTDToolbox.Classes.NewProjects;
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
        bool saveNKH = false;
        public SettingsWindow()
        {
            InitializeComponent();
            this.Size = new Size(350, 500);

            projectData = Serializer.Deserialize_Config();
            Setup();
        }
        private void Setup()
        {
            EnableSplash.Checked = projectData.enableSplash;
            useExternalEditor.Checked = projectData.useExternalEditor;
            DisableUpdates_CB.Checked = projectData.disableUpdates;
            AutoFormatJSON_CB.Checked = projectData.autoFormatJSON;

            if(CurrentProjectVariables.GameName == "BTD5")
            {
                if(NKHook.DoesNkhExist())
                {
                    UseNKH_CB.Checked = CurrentProjectVariables.UseNKHook;
                    
                    saveNKH = true;
                    UseNKH_CB.Visible = true;
                    CurrentProjSettings_Label.Visible = true;
                }
            }
        }
        private void Save_Button_Click(object sender, EventArgs e)
        {
            JetForm.useExternalEditor = useExternalEditor.Checked;
            Program.enableSplash = EnableSplash.Checked;
            Main.disableUpdates = DisableUpdates_CB.Checked;
            Main.autoFormatJSON = AutoFormatJSON_CB.Checked;

            if(saveNKH == true) CurrentProjectVariables.UseNKHook = UseNKH_CB.Checked;

            Serializer.SaveSmallSettings("splash");
            Serializer.SaveSmallSettings("disableUpdates");
            Serializer.SaveSmallSettings("autoFormatJSON");
            Serializer.SaveSmallSettings("external editor");

            ProjectHandler.SaveProject();
            ConsoleHandler.appendLog_CanRepeat("Settings saved!!!");
            this.Close();
        }
    }
}
