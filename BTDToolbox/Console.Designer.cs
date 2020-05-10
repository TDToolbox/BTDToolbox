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
            this.invoke_button = new System.Windows.Forms.Button();
            this.invoke_textbox = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.consoe_tab = new System.Windows.Forms.TabPage();
            this.output_log = new System.Windows.Forms.RichTextBox();
            this.error_tab = new System.Windows.Forms.TabPage();
            this.ErrorLog = new System.Windows.Forms.RichTextBox();
            this.ExportLog_Button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.titleSeperator)).BeginInit();
            this.titleSeperator.Panel1.SuspendLayout();
            this.titleSeperator.Panel2.SuspendLayout();
            this.titleSeperator.SuspendLayout();
            this.contentPanel.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.consoe_tab.SuspendLayout();
            this.error_tab.SuspendLayout();
            this.SuspendLayout();
            // 
            // titleSeperator
            // 
            // 
            // titleSeperator.Panel1
            // 
            this.titleSeperator.Panel1.Controls.Add(this.ExportLog_Button);
            // 
            // TitleLabel
            // 
            this.TitleLabel.Location = new System.Drawing.Point(12, 5);
            this.TitleLabel.Size = new System.Drawing.Size(58, 16);
            this.TitleLabel.Text = "Console";
            this.TitleLabel.Visible = false;
            // 
            // contentPanel
            // 
            this.contentPanel.BackColor = System.Drawing.Color.Black;
            this.contentPanel.Controls.Add(this.tabControl1);
            this.contentPanel.Controls.Add(this.invoke_button);
            this.contentPanel.Controls.Add(this.invoke_textbox);
            this.contentPanel.Location = new System.Drawing.Point(3, -29);
            this.contentPanel.Size = new System.Drawing.Size(794, 438);
            // 
            // close_button
            // 
            this.close_button.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.close_button.FlatAppearance.BorderSize = 0;
            this.close_button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Indigo;
            this.close_button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Purple;
            this.close_button.Location = new System.Drawing.Point(748, 0);
            // 
            // invoke_button
            // 
            this.invoke_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.invoke_button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.invoke_button.ForeColor = System.Drawing.Color.White;
            this.invoke_button.Location = new System.Drawing.Point(588, 411);
            this.invoke_button.Name = "invoke_button";
            this.invoke_button.Size = new System.Drawing.Size(203, 27);
            this.invoke_button.TabIndex = 4;
            this.invoke_button.Text = "Invoke";
            this.invoke_button.UseVisualStyleBackColor = false;
            // 
            // invoke_textbox
            // 
            this.invoke_textbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.invoke_textbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.invoke_textbox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.invoke_textbox.ForeColor = System.Drawing.Color.White;
            this.invoke_textbox.Location = new System.Drawing.Point(4, 413);
            this.invoke_textbox.Name = "invoke_textbox";
            this.invoke_textbox.Size = new System.Drawing.Size(578, 23);
            this.invoke_textbox.TabIndex = 3;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.consoe_tab);
            this.tabControl1.Controls.Add(this.error_tab);
            this.tabControl1.Location = new System.Drawing.Point(9, 30);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(772, 375);
            this.tabControl1.TabIndex = 7;
            // 
            // consoe_tab
            // 
            this.consoe_tab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.consoe_tab.Controls.Add(this.output_log);
            this.consoe_tab.Location = new System.Drawing.Point(4, 22);
            this.consoe_tab.Name = "consoe_tab";
            this.consoe_tab.Padding = new System.Windows.Forms.Padding(3);
            this.consoe_tab.Size = new System.Drawing.Size(764, 349);
            this.consoe_tab.TabIndex = 0;
            this.consoe_tab.Text = "Console";
            // 
            // output_log
            // 
            this.output_log.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.output_log.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.output_log.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.output_log.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.output_log.ForeColor = System.Drawing.Color.White;
            this.output_log.Location = new System.Drawing.Point(1, 0);
            this.output_log.Name = "output_log";
            this.output_log.ReadOnly = true;
            this.output_log.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.output_log.Size = new System.Drawing.Size(763, 349);
            this.output_log.TabIndex = 7;
            this.output_log.Text = "";
            // 
            // error_tab
            // 
            this.error_tab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.error_tab.Controls.Add(this.ErrorLog);
            this.error_tab.Location = new System.Drawing.Point(4, 22);
            this.error_tab.Name = "error_tab";
            this.error_tab.Padding = new System.Windows.Forms.Padding(3);
            this.error_tab.Size = new System.Drawing.Size(764, 349);
            this.error_tab.TabIndex = 1;
            this.error_tab.Text = "Error log  (0)";
            // 
            // ErrorLog
            // 
            this.ErrorLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ErrorLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.ErrorLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ErrorLog.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ErrorLog.ForeColor = System.Drawing.Color.White;
            this.ErrorLog.Location = new System.Drawing.Point(0, 0);
            this.ErrorLog.Name = "ErrorLog";
            this.ErrorLog.ReadOnly = true;
            this.ErrorLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.ErrorLog.Size = new System.Drawing.Size(770, 371);
            this.ErrorLog.TabIndex = 7;
            this.ErrorLog.Text = "";
            // 
            // ExportLog_Button
            // 
            this.ExportLog_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ExportLog_Button.BackColor = System.Drawing.Color.Olive;
            this.ExportLog_Button.FlatAppearance.BorderSize = 0;
            this.ExportLog_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ExportLog_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExportLog_Button.ForeColor = System.Drawing.Color.White;
            this.ExportLog_Button.Location = new System.Drawing.Point(601, -1);
            this.ExportLog_Button.Name = "ExportLog_Button";
            this.ExportLog_Button.Size = new System.Drawing.Size(141, 25);
            this.ExportLog_Button.TabIndex = 3;
            this.ExportLog_Button.Text = "Export Console Log";
            this.ExportLog_Button.UseVisualStyleBackColor = false;
            this.ExportLog_Button.Click += new System.EventHandler(this.ExportLog_Button_Click);
            // 
            // Console
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "Console";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "NewConsole";
            this.titleSeperator.Panel1.ResumeLayout(false);
            this.titleSeperator.Panel1.PerformLayout();
            this.titleSeperator.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.titleSeperator)).EndInit();
            this.titleSeperator.ResumeLayout(false);
            this.contentPanel.ResumeLayout(false);
            this.contentPanel.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.consoe_tab.ResumeLayout(false);
            this.error_tab.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button invoke_button;
        private System.Windows.Forms.TextBox invoke_textbox;
        public System.Windows.Forms.TextBox console_log;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage consoe_tab;
        public System.Windows.Forms.RichTextBox output_log;
        private System.Windows.Forms.TabPage error_tab;
        public System.Windows.Forms.RichTextBox ErrorLog;
        private System.Windows.Forms.Button ExportLog_Button;
    }
}