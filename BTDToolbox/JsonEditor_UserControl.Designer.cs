namespace BTDToolbox
{
    partial class JsonEditor_UserControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Editor_TextBox = new System.Windows.Forms.RichTextBox();
            this.tB_line = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // Editor_TextBox
            // 
            this.Editor_TextBox.AcceptsTab = true;
            this.Editor_TextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.Editor_TextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Editor_TextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Editor_TextBox.ForeColor = System.Drawing.Color.White;
            this.Editor_TextBox.Location = new System.Drawing.Point(32, 0);
            this.Editor_TextBox.Name = "Editor_TextBox";
            this.Editor_TextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.Editor_TextBox.Size = new System.Drawing.Size(719, 337);
            this.Editor_TextBox.TabIndex = 26;
            this.Editor_TextBox.Text = "";
            this.Editor_TextBox.SelectionChanged += new System.EventHandler(this.Editor_TextBox_SelectionChanged);
            this.Editor_TextBox.VScroll += new System.EventHandler(this.Editor_TextBox_VScroll);
            this.Editor_TextBox.FontChanged += new System.EventHandler(this.Editor_TextBox_FontChanged);
            this.Editor_TextBox.TextChanged += new System.EventHandler(this.Editor_TextBox_TextChanged);
            // 
            // tB_line
            // 
            this.tB_line.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tB_line.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.tB_line.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tB_line.Font = new System.Drawing.Font("Consolas", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tB_line.ForeColor = System.Drawing.Color.DarkGray;
            this.tB_line.Location = new System.Drawing.Point(0, 0);
            this.tB_line.Name = "tB_line";
            this.tB_line.ReadOnly = true;
            this.tB_line.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.tB_line.Size = new System.Drawing.Size(53, 337);
            this.tB_line.TabIndex = 27;
            this.tB_line.TabStop = false;
            this.tB_line.Text = "";
            this.tB_line.WordWrap = false;
            this.tB_line.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TB_line_MouseDown);
            // 
            // JsonEditor_UserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.Controls.Add(this.Editor_TextBox);
            this.Controls.Add(this.tB_line);
            this.Name = "JsonEditor_UserControl";
            this.Size = new System.Drawing.Size(754, 337);
            this.Resize += new System.EventHandler(this.JsonEditor_UserControl_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox Editor_TextBox;
        private System.Windows.Forms.RichTextBox tB_line;
    }
}
