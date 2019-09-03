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
            this.invoke_textbox.Location = new System.Drawing.Point(12, 247);
            this.invoke_textbox.Name = "invoke_textbox";
            this.invoke_textbox.Size = new System.Drawing.Size(1434, 20);
            this.invoke_textbox.TabIndex = 0;
            // 
            // invoke_button
            // 
            this.invoke_button.Location = new System.Drawing.Point(1453, 244);
            this.invoke_button.Name = "invoke_button";
            this.invoke_button.Size = new System.Drawing.Size(75, 23);
            this.invoke_button.TabIndex = 1;
            this.invoke_button.Text = "Invoke";
            this.invoke_button.UseVisualStyleBackColor = true;
            // 
            // console_log
            // 
            this.console_log.FormattingEnabled = true;
            this.console_log.Location = new System.Drawing.Point(13, 13);
            this.console_log.Name = "console_log";
            this.console_log.Size = new System.Drawing.Size(1515, 225);
            this.console_log.TabIndex = 2;
            // 
            // Console
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1540, 279);
            this.Controls.Add(this.console_log);
            this.Controls.Add(this.invoke_button);
            this.Controls.Add(this.invoke_textbox);
            this.Name = "Console";
            this.Text = "Console";
            this.Load += new System.EventHandler(this.Console_Load);
            this.FormClosing += this.Console_Close;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox invoke_textbox;
        private System.Windows.Forms.Button invoke_button;
        private System.Windows.Forms.ListBox console_log;
    }
}