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
namespace BTDToolbox
{
    public partial class SettingsWindow : ThemedForm
    {
        public SettingsWindow()
        {
            InitializeComponent();
            Setup();
        }
        private void Setup()
        {
            this.Size = new Size(350, 500);

            this.canResize = false;
            this.moveCenterScreen = true;

            EnableSplash.Checked = Serializer.cfg.enableSplash;
            useExternalEditor.Checked = Serializer.cfg.useExternalEditor;
            DisableUpdates_CB.Checked = Serializer.cfg.disableUpdates;
            AutoFormatJSON_CB.Checked = Serializer.cfg.autoFormatJSON;
            UseDeveloperMode.Checked = Serializer.cfg.UseDeveloperMode;

            if (!NKHook.CanUseNKH())
                return;
            
            UseNKH_CB.Checked = CurrentProjectVariables.UseNKHook;
            UseNKH_CB.Visible = true;
            CurrentProjSettings_Label.Visible = true;
        }
        private void Save_Button_Click(object sender, EventArgs e)
        {
            Serializer.cfg.useExternalEditor = useExternalEditor.Checked;
            Serializer.cfg.enableSplash = EnableSplash.Checked;
            Serializer.cfg.disableUpdates = DisableUpdates_CB.Checked;
            Serializer.cfg.autoFormatJSON = AutoFormatJSON_CB.Checked;
            Serializer.cfg.UseDeveloperMode = UseDeveloperMode.Checked;

            CurrentProjectVariables.UseNKHook = UseNKH_CB.Checked;

            Serializer.SaveSettings();
            ProjectHandler.SaveProject();
            ConsoleHandler.append_CanRepeat("Settings saved!!!");
            DeveloperMode.ControlDeveloperMode();
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
