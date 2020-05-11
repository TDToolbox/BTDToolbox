namespace BTDToolbox
{
    partial class SettingsWindow : ThemedForm
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
        private new void InitializeComponent()
        {
            this.EnableSplash = new System.Windows.Forms.CheckBox();
            this.Save_Button = new System.Windows.Forms.Button();
            this.useExternalEditor = new System.Windows.Forms.CheckBox();
            this.DisableUpdates_CB = new System.Windows.Forms.CheckBox();
            this.AutoFormatJSON_CB = new System.Windows.Forms.CheckBox();
            this.Settings_Label = new System.Windows.Forms.Label();
            this.UseNKH_CB = new System.Windows.Forms.CheckBox();
            this.CurrentProjSettings_Label = new System.Windows.Forms.Label();
            this.UseDeveloperMode = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.titleSeperator)).BeginInit();
            this.titleSeperator.Panel1.SuspendLayout();
            this.titleSeperator.Panel2.SuspendLayout();
            this.titleSeperator.SuspendLayout();
            this.contentPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TitleBar_RightCorner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TitleBar_LeftCorner)).BeginInit();
            this.SuspendLayout();
            // 
            // titleSeperator
            // 
            // 
            // TitleLabel
            // 
            this.TitleLabel.Size = new System.Drawing.Size(56, 16);
            this.TitleLabel.Text = "Settings";
            // 
            // contentPanel
            // 
            this.contentPanel.Controls.Add(this.UseDeveloperMode);
            this.contentPanel.Controls.Add(this.CurrentProjSettings_Label);
            this.contentPanel.Controls.Add(this.UseNKH_CB);
            this.contentPanel.Controls.Add(this.Settings_Label);
            this.contentPanel.Controls.Add(this.AutoFormatJSON_CB);
            this.contentPanel.Controls.Add(this.DisableUpdates_CB);
            this.contentPanel.Controls.Add(this.useExternalEditor);
            this.contentPanel.Controls.Add(this.Save_Button);
            this.contentPanel.Controls.Add(this.EnableSplash);
            // 
            // close_button
            // 
            this.close_button.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.close_button.FlatAppearance.BorderSize = 0;
            this.close_button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.close_button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            // 
            // TitleBar_RightCorner
            // 
            this.TitleBar_RightCorner.Size = new System.Drawing.Size(50, 59);
            // 
            // EnableSplash
            // 
            this.EnableSplash.AutoSize = true;
            this.EnableSplash.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EnableSplash.ForeColor = System.Drawing.Color.White;
            this.EnableSplash.Location = new System.Drawing.Point(28, 57);
            this.EnableSplash.Name = "EnableSplash";
            this.EnableSplash.Size = new System.Drawing.Size(181, 24);
            this.EnableSplash.TabIndex = 1;
            this.EnableSplash.Text = "Enable Splash Screen";
            this.EnableSplash.UseVisualStyleBackColor = true;
            // 
            // Save_Button
            // 
            this.Save_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Save_Button.Location = new System.Drawing.Point(644, 362);
            this.Save_Button.Name = "Save_Button";
            this.Save_Button.Size = new System.Drawing.Size(118, 32);
            this.Save_Button.TabIndex = 2;
            this.Save_Button.Text = "Save";
            this.Save_Button.UseVisualStyleBackColor = true;
            this.Save_Button.Click += new System.EventHandler(this.Save_Button_Click);
            // 
            // useExternalEditor
            // 
            this.useExternalEditor.AutoSize = true;
            this.useExternalEditor.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.useExternalEditor.ForeColor = System.Drawing.Color.White;
            this.useExternalEditor.Location = new System.Drawing.Point(28, 87);
            this.useExternalEditor.Name = "useExternalEditor";
            this.useExternalEditor.Size = new System.Drawing.Size(201, 24);
            this.useExternalEditor.TabIndex = 3;
            this.useExternalEditor.Text = "Use External Text Editor";
            this.useExternalEditor.UseVisualStyleBackColor = true;
            // 
            // DisableUpdates_CB
            // 
            this.DisableUpdates_CB.AutoSize = true;
            this.DisableUpdates_CB.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DisableUpdates_CB.ForeColor = System.Drawing.Color.White;
            this.DisableUpdates_CB.Location = new System.Drawing.Point(28, 147);
            this.DisableUpdates_CB.Name = "DisableUpdates_CB";
            this.DisableUpdates_CB.Size = new System.Drawing.Size(315, 24);
            this.DisableUpdates_CB.TabIndex = 4;
            this.DisableUpdates_CB.Text = "Disable updates (NOT RECOMMENDED)";
            this.DisableUpdates_CB.UseVisualStyleBackColor = true;
            // 
            // AutoFormatJSON_CB
            // 
            this.AutoFormatJSON_CB.AutoSize = true;
            this.AutoFormatJSON_CB.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AutoFormatJSON_CB.ForeColor = System.Drawing.Color.White;
            this.AutoFormatJSON_CB.Location = new System.Drawing.Point(28, 117);
            this.AutoFormatJSON_CB.Name = "AutoFormatJSON_CB";
            this.AutoFormatJSON_CB.Size = new System.Drawing.Size(231, 24);
            this.AutoFormatJSON_CB.TabIndex = 5;
            this.AutoFormatJSON_CB.Text = "Automatically format JSON?";
            this.AutoFormatJSON_CB.UseVisualStyleBackColor = true;
            // 
            // Settings_Label
            // 
            this.Settings_Label.AutoSize = true;
            this.Settings_Label.Font = new System.Drawing.Font("Oetztype", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Settings_Label.ForeColor = System.Drawing.Color.White;
            this.Settings_Label.Location = new System.Drawing.Point(11, 10);
            this.Settings_Label.Name = "Settings_Label";
            this.Settings_Label.Size = new System.Drawing.Size(330, 35);
            this.Settings_Label.TabIndex = 6;
            this.Settings_Label.Text = "BTD Toolbox Settings";
            // 
            // UseNKH_CB
            // 
            this.UseNKH_CB.AutoSize = true;
            this.UseNKH_CB.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UseNKH_CB.ForeColor = System.Drawing.Color.White;
            this.UseNKH_CB.Location = new System.Drawing.Point(17, 272);
            this.UseNKH_CB.Name = "UseNKH_CB";
            this.UseNKH_CB.Size = new System.Drawing.Size(121, 24);
            this.UseNKH_CB.TabIndex = 7;
            this.UseNKH_CB.Text = "Use NKHook";
            this.UseNKH_CB.UseVisualStyleBackColor = true;
            this.UseNKH_CB.Visible = false;
            this.UseNKH_CB.CheckedChanged += new System.EventHandler(this.UseNKH_CB_CheckedChanged);
            // 
            // CurrentProjSettings_Label
            // 
            this.CurrentProjSettings_Label.AutoSize = true;
            this.CurrentProjSettings_Label.Font = new System.Drawing.Font("Oetztype", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrentProjSettings_Label.ForeColor = System.Drawing.Color.White;
            this.CurrentProjSettings_Label.Location = new System.Drawing.Point(11, 225);
            this.CurrentProjSettings_Label.Name = "CurrentProjSettings_Label";
            this.CurrentProjSettings_Label.Size = new System.Drawing.Size(372, 35);
            this.CurrentProjSettings_Label.TabIndex = 8;
            this.CurrentProjSettings_Label.Text = "Current Project Settings";
            this.CurrentProjSettings_Label.Visible = false;
            // 
            // UseDeveloperMode
            // 
            this.UseDeveloperMode.AutoSize = true;
            this.UseDeveloperMode.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UseDeveloperMode.ForeColor = System.Drawing.Color.White;
            this.UseDeveloperMode.Location = new System.Drawing.Point(28, 177);
            this.UseDeveloperMode.Name = "UseDeveloperMode";
            this.UseDeveloperMode.Size = new System.Drawing.Size(183, 24);
            this.UseDeveloperMode.TabIndex = 9;
            this.UseDeveloperMode.Text = "Use Developer Mode";
            this.UseDeveloperMode.UseVisualStyleBackColor = true;
            // 
            // SettingsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "SettingsWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "SettingsWindow";
            this.titleSeperator.Panel1.ResumeLayout(false);
            this.titleSeperator.Panel1.PerformLayout();
            this.titleSeperator.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.titleSeperator)).EndInit();
            this.titleSeperator.ResumeLayout(false);
            this.contentPanel.ResumeLayout(false);
            this.contentPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TitleBar_RightCorner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TitleBar_LeftCorner)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox EnableSplash;
        private System.Windows.Forms.Button Save_Button;
        private System.Windows.Forms.CheckBox useExternalEditor;
        private System.Windows.Forms.CheckBox DisableUpdates_CB;
        private System.Windows.Forms.Label Settings_Label;
        private System.Windows.Forms.CheckBox AutoFormatJSON_CB;
        private System.Windows.Forms.Label CurrentProjSettings_Label;
        private System.Windows.Forms.CheckBox UseNKH_CB;
        private System.Windows.Forms.CheckBox UseDeveloperMode;
    }
}