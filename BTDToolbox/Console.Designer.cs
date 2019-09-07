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
        private void InitializeComponent()
        {
            this.invoke_textbox = new System.Windows.Forms.TextBox();
            this.invoke_button = new System.Windows.Forms.Button();
            this.console_log = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // invoke_textbox
            // 
            this.invoke_textbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.invoke_textbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.invoke_textbox.Location = new System.Drawing.Point(14, 286);
            this.invoke_textbox.Name = "invoke_textbox";
            this.invoke_textbox.Size = new System.Drawing.Size(793, 21);
            this.invoke_textbox.TabIndex = 0;
            // 
            // invoke_button
            // 
            this.invoke_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.invoke_button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.invoke_button.ForeColor = System.Drawing.Color.White;
            this.invoke_button.Location = new System.Drawing.Point(814, 285);
            this.invoke_button.Name = "invoke_button";
            this.invoke_button.Size = new System.Drawing.Size(237, 27);
            this.invoke_button.TabIndex = 1;
            this.invoke_button.Text = "Invoke";
            this.invoke_button.UseVisualStyleBackColor = false;
            // 
            // console_log
            // 
            this.console_log.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.console_log.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.console_log.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.console_log.ForeColor = System.Drawing.Color.White;
            this.console_log.FormattingEnabled = true;
            this.console_log.ItemHeight = 16;
            this.console_log.Location = new System.Drawing.Point(15, 15);
            this.console_log.Name = "console_log";
            this.console_log.Size = new System.Drawing.Size(1038, 244);
            this.console_log.TabIndex = 2;
            // 
            // Console
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.ClientSize = new System.Drawing.Size(1067, 322);
            this.Controls.Add(this.console_log);
            this.Controls.Add(this.invoke_button);
            this.Controls.Add(this.invoke_textbox);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "Console";
            this.Text = "Console";
            this.Load += new System.EventHandler(this.Console_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox invoke_textbox;
        private System.Windows.Forms.Button invoke_button;
        private System.Windows.Forms.ListBox console_log;
    }
}