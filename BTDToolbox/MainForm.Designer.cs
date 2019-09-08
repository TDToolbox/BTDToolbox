namespace BTDToolbox
{
    partial class TD_Toolbox_Window
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TD_Toolbox_Window));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.File_ToolStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.New_ToolStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.ImportNewJet = new System.Windows.Forms.ToolStripMenuItem();
            this.btdpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenExistingProject = new System.Windows.Forms.ToolStripMenuItem();
            this.openRecentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Find_Button = new System.Windows.Forms.ToolStripMenuItem();
            this.Replace_Button = new System.Windows.Forms.ToolStripMenuItem();
            this.Launch_Program_ToolStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ToggleConsole = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenJetExplorer = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RestoreBackup = new System.Windows.Forms.ToolStripMenuItem();
            this.Credits = new System.Windows.Forms.ToolStripMenuItem();
            this.debugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.themedFormToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nullJetFormToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.versionTag = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.File_ToolStrip,
            this.viewToolStripMenuItem,
            this.Launch_Program_ToolStrip,
            this.viewToolStripMenuItem1,
            this.helpToolStripMenuItem,
            this.debugToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1483, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "File";
            // 
            // File_ToolStrip
            // 
            this.File_ToolStrip.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.New_ToolStrip,
            this.openToolStripMenuItem1,
            this.openRecentToolStripMenuItem,
            this.settingsToolStripMenuItem});
            this.File_ToolStrip.ForeColor = System.Drawing.Color.White;
            this.File_ToolStrip.Name = "File_ToolStrip";
            this.File_ToolStrip.Size = new System.Drawing.Size(37, 20);
            this.File_ToolStrip.Text = "File";
            // 
            // New_ToolStrip
            // 
            this.New_ToolStrip.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ImportNewJet,
            this.btdpToolStripMenuItem});
            this.New_ToolStrip.Name = "New_ToolStrip";
            this.New_ToolStrip.Size = new System.Drawing.Size(142, 22);
            this.New_ToolStrip.Text = "New";
            // 
            // ImportNewJet
            // 
            this.ImportNewJet.Name = "ImportNewJet";
            this.ImportNewJet.Size = new System.Drawing.Size(171, 22);
            this.ImportNewJet.Text = "Project from .jet";
            this.ImportNewJet.Click += new System.EventHandler(this.ImportNewJet_Click);
            // 
            // btdpToolStripMenuItem
            // 
            this.btdpToolStripMenuItem.Name = "btdpToolStripMenuItem";
            this.btdpToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.btdpToolStripMenuItem.Text = "Project from .btdp";
            // 
            // openToolStripMenuItem1
            // 
            this.openToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenExistingProject});
            this.openToolStripMenuItem1.Name = "openToolStripMenuItem1";
            this.openToolStripMenuItem1.Size = new System.Drawing.Size(142, 22);
            this.openToolStripMenuItem1.Text = "Open";
            // 
            // OpenExistingProject
            // 
            this.OpenExistingProject.Name = "OpenExistingProject";
            this.OpenExistingProject.Size = new System.Drawing.Size(154, 22);
            this.OpenExistingProject.Text = "Existing project";
            this.OpenExistingProject.Click += new System.EventHandler(this.OpenExistingProject_Click);
            // 
            // openRecentToolStripMenuItem
            // 
            this.openRecentToolStripMenuItem.Name = "openRecentToolStripMenuItem";
            this.openRecentToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.openRecentToolStripMenuItem.Text = "Open Recent";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Find_Button,
            this.Replace_Button});
            this.viewToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.viewToolStripMenuItem.Text = "Edit";
            // 
            // Find_Button
            // 
            this.Find_Button.Name = "Find_Button";
            this.Find_Button.Size = new System.Drawing.Size(115, 22);
            this.Find_Button.Text = "Find";
            this.Find_Button.Click += new System.EventHandler(this.Find_Button_Click);
            // 
            // Replace_Button
            // 
            this.Replace_Button.Name = "Replace_Button";
            this.Replace_Button.Size = new System.Drawing.Size(115, 22);
            this.Replace_Button.Text = "Replace";
            this.Replace_Button.Click += new System.EventHandler(this.Replace_Button_Click);
            // 
            // Launch_Program_ToolStrip
            // 
            this.Launch_Program_ToolStrip.ForeColor = System.Drawing.Color.White;
            this.Launch_Program_ToolStrip.Name = "Launch_Program_ToolStrip";
            this.Launch_Program_ToolStrip.Size = new System.Drawing.Size(73, 20);
            this.Launch_Program_ToolStrip.Text = "Launch 🚀";
            this.Launch_Program_ToolStrip.Click += new System.EventHandler(this.LaunchProgram_Click);
            // 
            // viewToolStripMenuItem1
            // 
            this.viewToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToggleConsole,
            this.OpenJetExplorer});
            this.viewToolStripMenuItem1.ForeColor = System.Drawing.Color.White;
            this.viewToolStripMenuItem1.Name = "viewToolStripMenuItem1";
            this.viewToolStripMenuItem1.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem1.Text = "View";
            // 
            // ToggleConsole
            // 
            this.ToggleConsole.Name = "ToggleConsole";
            this.ToggleConsole.Size = new System.Drawing.Size(180, 22);
            this.ToggleConsole.Text = "Console";
            this.ToggleConsole.Click += new System.EventHandler(this.ToggleConsole_Click);
            // 
            // OpenJetExplorer
            // 
            this.OpenJetExplorer.Name = "OpenJetExplorer";
            this.OpenJetExplorer.Size = new System.Drawing.Size(180, 22);
            this.OpenJetExplorer.Text = "Jet Explorer";
            this.OpenJetExplorer.Click += new System.EventHandler(this.OpenJetExplorer_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RestoreBackup,
            this.Credits});
            this.helpToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // RestoreBackup
            // 
            this.RestoreBackup.Name = "RestoreBackup";
            this.RestoreBackup.Size = new System.Drawing.Size(174, 22);
            this.RestoreBackup.Text = "Restore backup .jet";
            this.RestoreBackup.Click += new System.EventHandler(this.RestoreBackup_Click);
            // 
            // Credits
            // 
            this.Credits.Name = "Credits";
            this.Credits.Size = new System.Drawing.Size(174, 22);
            this.Credits.Text = "Credits";
            this.Credits.Click += new System.EventHandler(this.OpenCredits_Click);
            // 
            // debugToolStripMenuItem
            // 
            this.debugToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.themedFormToolStripMenuItem,
            this.nullJetFormToolStripMenuItem});
            this.debugToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.debugToolStripMenuItem.Name = "debugToolStripMenuItem";
            this.debugToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.debugToolStripMenuItem.Text = "Debug";
            // 
            // themedFormToolStripMenuItem
            // 
            this.themedFormToolStripMenuItem.Name = "themedFormToolStripMenuItem";
            this.themedFormToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.themedFormToolStripMenuItem.Text = "Themed Form";
            this.themedFormToolStripMenuItem.Click += new System.EventHandler(this.Debug_ThemedForm_Click);
            // 
            // nullJetFormToolStripMenuItem
            // 
            this.nullJetFormToolStripMenuItem.Name = "nullJetFormToolStripMenuItem";
            this.nullJetFormToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            // 
            // versionTag
            // 
            this.versionTag.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.versionTag.AutoSize = true;
            this.versionTag.BackColor = System.Drawing.Color.Transparent;
            this.versionTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.versionTag.ForeColor = System.Drawing.Color.White;
            this.versionTag.Location = new System.Drawing.Point(12, 677);
            this.versionTag.Name = "versionTag";
            this.versionTag.Size = new System.Drawing.Size(98, 55);
            this.versionTag.TabIndex = 3;
            this.versionTag.Text = "null";
            // 
            // TD_Toolbox_Window
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1483, 741);
            this.Controls.Add(this.versionTag);
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "TD_Toolbox_Window";
            this.Text = "Toolbox - Made by DisabledMallis and Gurrenm3";
            this.Load += new System.EventHandler(this.TD_Toolbox_Window_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TD_Toolbox_Window_KeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem File_ToolStrip;
        private System.Windows.Forms.ToolStripMenuItem New_ToolStrip;
        private System.Windows.Forms.ToolStripMenuItem ImportNewJet;
        private System.Windows.Forms.ToolStripMenuItem btdpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Launch_Program_ToolStrip;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem ToggleConsole;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RestoreBackup;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem OpenExistingProject;
        private System.Windows.Forms.ToolStripMenuItem debugToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem themedFormToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Find_Button;
        private System.Windows.Forms.ToolStripMenuItem Replace_Button;
        private System.Windows.Forms.ToolStripMenuItem Credits;
        private System.Windows.Forms.ToolStripMenuItem openRecentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenJetExplorer;
        private System.Windows.Forms.ToolStripMenuItem nullJetFormToolStripMenuItem;
        private System.Windows.Forms.Label versionTag;
    }
}

