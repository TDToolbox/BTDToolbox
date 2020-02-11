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
            this.output_log = new System.Windows.Forms.RichTextBox();
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
            this.contentPanel.Controls.Add(this.output_log);
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
            // invoke_button
            // 
            this.invoke_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.invoke_button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.invoke_button.ForeColor = System.Drawing.Color.White;
            this.invoke_button.Location = new System.Drawing.Point(570, 379);
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
            this.invoke_textbox.Location = new System.Drawing.Point(4, 381);
            this.invoke_textbox.Name = "invoke_textbox";
            this.invoke_textbox.Size = new System.Drawing.Size(560, 23);
            this.invoke_textbox.TabIndex = 3;
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
            this.output_log.Location = new System.Drawing.Point(3, 3);
            this.output_log.Name = "output_log";
            this.output_log.ReadOnly = true;
            this.output_log.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.output_log.Size = new System.Drawing.Size(770, 371);
            this.output_log.TabIndex = 6;
            this.output_log.Text = "";
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button invoke_button;
        private System.Windows.Forms.TextBox invoke_textbox;
        public System.Windows.Forms.TextBox console_log;
        public System.Windows.Forms.RichTextBox output_log;
    }
}