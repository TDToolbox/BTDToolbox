namespace BTDToolbox
{
    partial class Console
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
            this.console_log = new System.Windows.Forms.TextBox();
            this.invoke_button = new System.Windows.Forms.Button();
            this.invoke_textbox = new System.Windows.Forms.TextBox();
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
            this.TitleLabel.Size = new System.Drawing.Size(58, 16);
            this.TitleLabel.Text = "Console";
            // 
            // contentPanel
            // 
            this.contentPanel.Controls.Add(this.console_log);
            this.contentPanel.Controls.Add(this.invoke_button);
            this.contentPanel.Controls.Add(this.invoke_textbox);
            // 
            // close_button
            // 
            this.close_button.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.close_button.FlatAppearance.BorderSize = 0;
            this.close_button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Indigo;
            this.close_button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Purple;
            // 
            // console_log
            // 
            this.console_log.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.console_log.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.console_log.ForeColor = System.Drawing.Color.White;
            this.console_log.Location = new System.Drawing.Point(3, 3);
            this.console_log.Multiline = true;
            this.console_log.Name = "console_log";
            this.console_log.ReadOnly = true;
            this.console_log.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.console_log.Size = new System.Drawing.Size(769, 371);
            this.console_log.TabIndex = 5;
            // 
            // invoke_button
            // 
            this.invoke_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.invoke_button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.invoke_button.ForeColor = System.Drawing.Color.White;
            this.invoke_button.Location = new System.Drawing.Point(570, 379);
            this.invoke_button.Name = "invoke_button";
            this.invoke_button.Size = new System.Drawing.Size(203, 23);
            this.invoke_button.TabIndex = 4;
            this.invoke_button.Text = "Invoke";
            this.invoke_button.UseVisualStyleBackColor = false;
            // 
            // invoke_textbox
            // 
            this.invoke_textbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.invoke_textbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.invoke_textbox.Location = new System.Drawing.Point(4, 380);
            this.invoke_textbox.Name = "invoke_textbox";
            this.invoke_textbox.Size = new System.Drawing.Size(560, 20);
            this.invoke_textbox.TabIndex = 3;
            // 
            // NewConsole
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "NewConsole";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "NewConsole";
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

        private System.Windows.Forms.Button invoke_button;
        private System.Windows.Forms.TextBox invoke_textbox;
        private System.Windows.Forms.TextBox console_log;
    }
}