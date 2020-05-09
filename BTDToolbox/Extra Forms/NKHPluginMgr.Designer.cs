namespace BTDToolbox.Extra_Forms
{
    partial class NKHPluginMgr
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.UnloadedPlugin_LB = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.LoadedPlugin_LB = new System.Windows.Forms.ListBox();
            this.LoadPlugin_Button = new System.Windows.Forms.Button();
            this.UnloadPlugin_Button = new System.Windows.Forms.Button();
            this.Done_Button = new System.Windows.Forms.Button();
            this.BrowseForPlugin_Button = new System.Windows.Forms.Button();
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
            this.TitleLabel.Size = new System.Drawing.Size(156, 16);
            this.TitleLabel.Text = "NKHook Plugin Manager";
            // 
            // contentPanel
            // 
            this.contentPanel.Controls.Add(this.BrowseForPlugin_Button);
            this.contentPanel.Controls.Add(this.Done_Button);
            this.contentPanel.Controls.Add(this.UnloadPlugin_Button);
            this.contentPanel.Controls.Add(this.LoadPlugin_Button);
            this.contentPanel.Controls.Add(this.LoadedPlugin_LB);
            this.contentPanel.Controls.Add(this.label4);
            this.contentPanel.Controls.Add(this.label3);
            this.contentPanel.Controls.Add(this.UnloadedPlugin_LB);
            this.contentPanel.Controls.Add(this.label2);
            this.contentPanel.Controls.Add(this.label1);
            // 
            // close_button
            // 
            this.close_button.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.close_button.FlatAppearance.BorderSize = 0;
            this.close_button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Indigo;
            this.close_button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Purple;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(180, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(364, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Please use this Plugin Manager only for testing.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(120, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Loaded Plugins";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(504, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(139, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Unloaded Plugins";
            // 
            // UnloadedPlugin_LB
            // 
            this.UnloadedPlugin_LB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.UnloadedPlugin_LB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UnloadedPlugin_LB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.UnloadedPlugin_LB.ForeColor = System.Drawing.Color.White;
            this.UnloadedPlugin_LB.FormattingEnabled = true;
            this.UnloadedPlugin_LB.ItemHeight = 20;
            this.UnloadedPlugin_LB.Location = new System.Drawing.Point(425, 101);
            this.UnloadedPlugin_LB.Name = "UnloadedPlugin_LB";
            this.UnloadedPlugin_LB.Size = new System.Drawing.Size(306, 242);
            this.UnloadedPlugin_LB.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(120, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(481, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "Use our mod loader, TD Loader, if you want to play with plugins";
            // 
            // LoadedPlugin_LB
            // 
            this.LoadedPlugin_LB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.LoadedPlugin_LB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LoadedPlugin_LB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.LoadedPlugin_LB.ForeColor = System.Drawing.Color.White;
            this.LoadedPlugin_LB.FormattingEnabled = true;
            this.LoadedPlugin_LB.ItemHeight = 20;
            this.LoadedPlugin_LB.Location = new System.Drawing.Point(46, 101);
            this.LoadedPlugin_LB.Name = "LoadedPlugin_LB";
            this.LoadedPlugin_LB.Size = new System.Drawing.Size(306, 242);
            this.LoadedPlugin_LB.TabIndex = 7;
            // 
            // LoadPlugin_Button
            // 
            this.LoadPlugin_Button.Location = new System.Drawing.Point(358, 178);
            this.LoadPlugin_Button.Name = "LoadPlugin_Button";
            this.LoadPlugin_Button.Size = new System.Drawing.Size(61, 23);
            this.LoadPlugin_Button.TabIndex = 8;
            this.LoadPlugin_Button.Text = "<<";
            this.LoadPlugin_Button.UseVisualStyleBackColor = true;
            this.LoadPlugin_Button.Click += new System.EventHandler(this.LoadPlugin_Button_Click);
            // 
            // UnloadPlugin_Button
            // 
            this.UnloadPlugin_Button.Location = new System.Drawing.Point(358, 216);
            this.UnloadPlugin_Button.Name = "UnloadPlugin_Button";
            this.UnloadPlugin_Button.Size = new System.Drawing.Size(61, 23);
            this.UnloadPlugin_Button.TabIndex = 9;
            this.UnloadPlugin_Button.Text = ">>";
            this.UnloadPlugin_Button.UseVisualStyleBackColor = true;
            this.UnloadPlugin_Button.Click += new System.EventHandler(this.UnloadPlugin_Button_Click);
            // 
            // Done_Button
            // 
            this.Done_Button.Location = new System.Drawing.Point(628, 355);
            this.Done_Button.Name = "Done_Button";
            this.Done_Button.Size = new System.Drawing.Size(103, 37);
            this.Done_Button.TabIndex = 10;
            this.Done_Button.Text = "Done";
            this.Done_Button.UseVisualStyleBackColor = true;
            this.Done_Button.Click += new System.EventHandler(this.Done_Button_Click);
            // 
            // BrowseForPlugin_Button
            // 
            this.BrowseForPlugin_Button.Location = new System.Drawing.Point(326, 357);
            this.BrowseForPlugin_Button.Name = "BrowseForPlugin_Button";
            this.BrowseForPlugin_Button.Size = new System.Drawing.Size(137, 33);
            this.BrowseForPlugin_Button.TabIndex = 11;
            this.BrowseForPlugin_Button.Text = "Browse for plugins";
            this.BrowseForPlugin_Button.UseVisualStyleBackColor = true;
            this.BrowseForPlugin_Button.Click += new System.EventHandler(this.BrowseForPlugin_Button_Click);
            // 
            // NKHPluginMgr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "NKHPluginMgr";
            this.Text = "NKHPluginMgr";
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

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox UnloadedPlugin_LB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button UnloadPlugin_Button;
        private System.Windows.Forms.Button LoadPlugin_Button;
        private System.Windows.Forms.ListBox LoadedPlugin_LB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button Done_Button;
        private System.Windows.Forms.Button BrowseForPlugin_Button;
    }
}