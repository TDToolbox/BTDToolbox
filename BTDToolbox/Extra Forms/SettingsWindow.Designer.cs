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
            ((System.ComponentModel.ISupportInitialize)(this.titleSeperator)).BeginInit();
            this.titleSeperator.Panel1.SuspendLayout();
            this.titleSeperator.Panel2.SuspendLayout();
            this.titleSeperator.SuspendLayout();
            this.contentPanel.SuspendLayout();
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
            this.contentPanel.Controls.Add(this.DisableUpdates_CB);
            this.contentPanel.Controls.Add(this.useExternalEditor);
            this.contentPanel.Controls.Add(this.Save_Button);
            this.contentPanel.Controls.Add(this.EnableSplash);
            // 
            // close_button
            // 
            this.close_button.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.close_button.FlatAppearance.BorderSize = 0;
            this.close_button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Indigo;
            this.close_button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Purple;
            // 
            // EnableSplash
            // 
            this.EnableSplash.AutoSize = true;
            this.EnableSplash.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EnableSplash.ForeColor = System.Drawing.Color.White;
            this.EnableSplash.Location = new System.Drawing.Point(15, 22);
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
            this.useExternalEditor.Location = new System.Drawing.Point(15, 52);
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
            this.DisableUpdates_CB.Location = new System.Drawing.Point(15, 82);
            this.DisableUpdates_CB.Name = "DisableUpdates_CB";
            this.DisableUpdates_CB.Size = new System.Drawing.Size(315, 24);
            this.DisableUpdates_CB.TabIndex = 4;
            this.DisableUpdates_CB.Text = "Disable updates (NOT RECOMMENDED)";
            this.DisableUpdates_CB.UseVisualStyleBackColor = true;
            // 
            // SettingsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "SettingsWindow";
            this.Text = "SettingsWindow";
            this.titleSeperator.Panel1.ResumeLayout(false);
            this.titleSeperator.Panel1.PerformLayout();
            this.titleSeperator.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.titleSeperator)).EndInit();
            this.titleSeperator.ResumeLayout(false);
            this.contentPanel.ResumeLayout(false);
            this.contentPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox EnableSplash;
        private System.Windows.Forms.Button Save_Button;
        private System.Windows.Forms.CheckBox useExternalEditor;
        private System.Windows.Forms.CheckBox DisableUpdates_CB;
    }
}