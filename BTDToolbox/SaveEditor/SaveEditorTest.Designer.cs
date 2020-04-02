namespace BTDToolbox.SaveEditor
{
    partial class SaveEditorTest
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
            this.pathBox = new System.Windows.Forms.RichTextBox();
            this.Decrypt_Button = new System.Windows.Forms.Button();
            this.Encrypt_Button = new System.Windows.Forms.Button();
            this.Browse_Button = new System.Windows.Forms.Button();
            this.flag_bypass = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // pathBox
            // 
            this.pathBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pathBox.Location = new System.Drawing.Point(21, 55);
            this.pathBox.Name = "pathBox";
            this.pathBox.Size = new System.Drawing.Size(447, 34);
            this.pathBox.TabIndex = 0;
            this.pathBox.Text = "";
            // 
            // Decrypt_Button
            // 
            this.Decrypt_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Decrypt_Button.Location = new System.Drawing.Point(143, 147);
            this.Decrypt_Button.Name = "Decrypt_Button";
            this.Decrypt_Button.Size = new System.Drawing.Size(96, 34);
            this.Decrypt_Button.TabIndex = 1;
            this.Decrypt_Button.Text = "Decrypt";
            this.Decrypt_Button.UseVisualStyleBackColor = true;
            this.Decrypt_Button.Click += new System.EventHandler(this.Decrypt_Button_Click);
            // 
            // Encrypt_Button
            // 
            this.Encrypt_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Encrypt_Button.Location = new System.Drawing.Point(258, 148);
            this.Encrypt_Button.Name = "Encrypt_Button";
            this.Encrypt_Button.Size = new System.Drawing.Size(94, 33);
            this.Encrypt_Button.TabIndex = 2;
            this.Encrypt_Button.Text = "Encrypt";
            this.Encrypt_Button.UseVisualStyleBackColor = true;
            this.Encrypt_Button.Click += new System.EventHandler(this.Encrypt_Button_Click);
            // 
            // Browse_Button
            // 
            this.Browse_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Browse_Button.Location = new System.Drawing.Point(442, 175);
            this.Browse_Button.Name = "Browse_Button";
            this.Browse_Button.Size = new System.Drawing.Size(78, 34);
            this.Browse_Button.TabIndex = 3;
            this.Browse_Button.Text = "Browse";
            this.Browse_Button.UseVisualStyleBackColor = true;
            this.Browse_Button.Click += new System.EventHandler(this.Browse_Button_Click);
            // 
            // flag_bypass
            // 
            this.flag_bypass.AutoSize = true;
            this.flag_bypass.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flag_bypass.Location = new System.Drawing.Point(21, 95);
            this.flag_bypass.Name = "flag_bypass";
            this.flag_bypass.Size = new System.Drawing.Size(132, 24);
            this.flag_bypass.TabIndex = 4;
            this.flag_bypass.Text = "Check for btdb";
            this.flag_bypass.UseVisualStyleBackColor = true;
            // 
            // SaveEditorTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(591, 221);
            this.Controls.Add(this.flag_bypass);
            this.Controls.Add(this.Browse_Button);
            this.Controls.Add(this.Encrypt_Button);
            this.Controls.Add(this.Decrypt_Button);
            this.Controls.Add(this.pathBox);
            this.Name = "SaveEditorTest";
            this.Text = "SaveEditorTest";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox pathBox;
        private System.Windows.Forms.Button Decrypt_Button;
        private System.Windows.Forms.Button Encrypt_Button;
        private System.Windows.Forms.Button Browse_Button;
        private System.Windows.Forms.CheckBox flag_bypass;
    }
}