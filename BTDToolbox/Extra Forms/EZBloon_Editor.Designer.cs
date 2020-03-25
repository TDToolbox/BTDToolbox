namespace BTDToolbox.Extra_Forms
{
    partial class EZBloon_Editor
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
            this.BloonName_TextBox = new System.Windows.Forms.RichTextBox();
            this.BloonType_Label = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.InitialHealth_TextBox = new System.Windows.Forms.RichTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.BaseSpeed_TextBox = new System.Windows.Forms.RichTextBox();
            this.CanGoUnderground_CheckBox = new System.Windows.Forms.CheckBox();
            this.RotateToPathDirection_Checkbox = new System.Windows.Forms.CheckBox();
            this.label16 = new System.Windows.Forms.Label();
            this.SpriteFile_TextBox = new System.Windows.Forms.RichTextBox();
            this.Save_Button = new System.Windows.Forms.Button();
            this.BloonFiles_ComboBox = new System.Windows.Forms.ComboBox();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.label18 = new System.Windows.Forms.Label();
            this.ChildBloons_CheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.HitAddon_TextBox = new System.Windows.Forms.RichTextBox();
            this.HitAddon_Label = new System.Windows.Forms.Label();
            this.DrawLayer_TextBox = new System.Windows.Forms.RichTextBox();
            this.DrawLayer_Label = new System.Windows.Forms.Label();
            this.ChildEffectScale_TextBox = new System.Windows.Forms.RichTextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.Radius_TextBox = new System.Windows.Forms.RichTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.Scale_TextBox = new System.Windows.Forms.RichTextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.RBE_TextBox = new System.Windows.Forms.RichTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.SpeedMultiplier_TextBox = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.ApplyStatus_CheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.label10 = new System.Windows.Forms.Label();
            this.BloonAbility_CheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.label9 = new System.Windows.Forms.Label();
            this.StatusImmunity_CheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.label8 = new System.Windows.Forms.Label();
            this.DamageImmunity_CheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.SwitchPanel = new System.Windows.Forms.Button();
            this.AdvancedView_Checkbox = new System.Windows.Forms.CheckBox();
            this.Panel1.SuspendLayout();
            this.Panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // BloonName_TextBox
            // 
            this.BloonName_TextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.BloonName_TextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.BloonName_TextBox.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BloonName_TextBox.ForeColor = System.Drawing.Color.White;
            this.BloonName_TextBox.Location = new System.Drawing.Point(56, 73);
            this.BloonName_TextBox.Name = "BloonName_TextBox";
            this.BloonName_TextBox.ReadOnly = true;
            this.BloonName_TextBox.Size = new System.Drawing.Size(250, 24);
            this.BloonName_TextBox.TabIndex = 1;
            this.BloonName_TextBox.Text = "";
            this.BloonName_TextBox.TextChanged += new System.EventHandler(this.TowerName_TextBox_TextChanged);
            // 
            // BloonType_Label
            // 
            this.BloonType_Label.AutoSize = true;
            this.BloonType_Label.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.BloonType_Label.Font = new System.Drawing.Font("Microsoft YaHei UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BloonType_Label.ForeColor = System.Drawing.Color.White;
            this.BloonType_Label.Location = new System.Drawing.Point(14, 18);
            this.BloonType_Label.Name = "BloonType_Label";
            this.BloonType_Label.Size = new System.Drawing.Size(159, 35);
            this.BloonType_Label.TabIndex = 3;
            this.BloonType_Label.Text = "Bloon Type";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(56, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(174, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Bloon type  (cant edit):";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(338, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(145, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Damage immunity:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(56, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 20);
            this.label4.TabIndex = 8;
            this.label4.Text = "Initial health:";
            // 
            // InitialHealth_TextBox
            // 
            this.InitialHealth_TextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.InitialHealth_TextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.InitialHealth_TextBox.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InitialHealth_TextBox.ForeColor = System.Drawing.Color.White;
            this.InitialHealth_TextBox.Location = new System.Drawing.Point(56, 123);
            this.InitialHealth_TextBox.Name = "InitialHealth_TextBox";
            this.InitialHealth_TextBox.Size = new System.Drawing.Size(250, 24);
            this.InitialHealth_TextBox.TabIndex = 7;
            this.InitialHealth_TextBox.Text = "";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(56, 149);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 20);
            this.label5.TabIndex = 10;
            this.label5.Text = "Base speed:";
            // 
            // BaseSpeed_TextBox
            // 
            this.BaseSpeed_TextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.BaseSpeed_TextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.BaseSpeed_TextBox.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BaseSpeed_TextBox.ForeColor = System.Drawing.Color.White;
            this.BaseSpeed_TextBox.Location = new System.Drawing.Point(56, 172);
            this.BaseSpeed_TextBox.Name = "BaseSpeed_TextBox";
            this.BaseSpeed_TextBox.Size = new System.Drawing.Size(250, 24);
            this.BaseSpeed_TextBox.TabIndex = 9;
            this.BaseSpeed_TextBox.Text = "";
            // 
            // CanGoUnderground_CheckBox
            // 
            this.CanGoUnderground_CheckBox.AutoSize = true;
            this.CanGoUnderground_CheckBox.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F);
            this.CanGoUnderground_CheckBox.ForeColor = System.Drawing.Color.White;
            this.CanGoUnderground_CheckBox.Location = new System.Drawing.Point(392, 279);
            this.CanGoUnderground_CheckBox.Name = "CanGoUnderground_CheckBox";
            this.CanGoUnderground_CheckBox.Size = new System.Drawing.Size(180, 24);
            this.CanGoUnderground_CheckBox.TabIndex = 12;
            this.CanGoUnderground_CheckBox.Text = "Can go underground";
            this.CanGoUnderground_CheckBox.UseVisualStyleBackColor = true;
            // 
            // RotateToPathDirection_Checkbox
            // 
            this.RotateToPathDirection_Checkbox.AutoSize = true;
            this.RotateToPathDirection_Checkbox.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F);
            this.RotateToPathDirection_Checkbox.ForeColor = System.Drawing.Color.White;
            this.RotateToPathDirection_Checkbox.Location = new System.Drawing.Point(392, 299);
            this.RotateToPathDirection_Checkbox.Name = "RotateToPathDirection_Checkbox";
            this.RotateToPathDirection_Checkbox.Size = new System.Drawing.Size(204, 24);
            this.RotateToPathDirection_Checkbox.TabIndex = 13;
            this.RotateToPathDirection_Checkbox.Text = "Rotate to path direction";
            this.RotateToPathDirection_Checkbox.UseVisualStyleBackColor = true;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.White;
            this.label16.Location = new System.Drawing.Point(56, 269);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(83, 20);
            this.label16.TabIndex = 40;
            this.label16.Text = "Sprite file:";
            // 
            // SpriteFile_TextBox
            // 
            this.SpriteFile_TextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.SpriteFile_TextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SpriteFile_TextBox.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SpriteFile_TextBox.ForeColor = System.Drawing.Color.White;
            this.SpriteFile_TextBox.Location = new System.Drawing.Point(56, 290);
            this.SpriteFile_TextBox.Name = "SpriteFile_TextBox";
            this.SpriteFile_TextBox.Size = new System.Drawing.Size(317, 24);
            this.SpriteFile_TextBox.TabIndex = 39;
            this.SpriteFile_TextBox.Text = "";
            // 
            // Save_Button
            // 
            this.Save_Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.Save_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Save_Button.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F);
            this.Save_Button.ForeColor = System.Drawing.Color.White;
            this.Save_Button.Location = new System.Drawing.Point(890, 484);
            this.Save_Button.Name = "Save_Button";
            this.Save_Button.Size = new System.Drawing.Size(164, 58);
            this.Save_Button.TabIndex = 47;
            this.Save_Button.Text = "Save";
            this.Save_Button.UseVisualStyleBackColor = false;
            this.Save_Button.Click += new System.EventHandler(this.Save_Button_Click);
            // 
            // BloonFiles_ComboBox
            // 
            this.BloonFiles_ComboBox.DropDownHeight = 500;
            this.BloonFiles_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.BloonFiles_ComboBox.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BloonFiles_ComboBox.FormattingEnabled = true;
            this.BloonFiles_ComboBox.IntegralHeight = false;
            this.BloonFiles_ComboBox.ItemHeight = 19;
            this.BloonFiles_ComboBox.Location = new System.Drawing.Point(24, 56);
            this.BloonFiles_ComboBox.Name = "BloonFiles_ComboBox";
            this.BloonFiles_ComboBox.Size = new System.Drawing.Size(398, 27);
            this.BloonFiles_ComboBox.TabIndex = 48;
            this.BloonFiles_ComboBox.SelectedValueChanged += new System.EventHandler(this.AllTowerFiles_ComboBox_SelectedValueChanged);
            // 
            // Panel1
            // 
            this.Panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel1.Controls.Add(this.label18);
            this.Panel1.Controls.Add(this.ChildBloons_CheckedListBox);
            this.Panel1.Controls.Add(this.HitAddon_TextBox);
            this.Panel1.Controls.Add(this.HitAddon_Label);
            this.Panel1.Controls.Add(this.DrawLayer_TextBox);
            this.Panel1.Controls.Add(this.DrawLayer_Label);
            this.Panel1.Controls.Add(this.ChildEffectScale_TextBox);
            this.Panel1.Controls.Add(this.label21);
            this.Panel1.Controls.Add(this.Radius_TextBox);
            this.Panel1.Controls.Add(this.label6);
            this.Panel1.Controls.Add(this.SpriteFile_TextBox);
            this.Panel1.Controls.Add(this.label16);
            this.Panel1.Controls.Add(this.Scale_TextBox);
            this.Panel1.Controls.Add(this.label20);
            this.Panel1.Controls.Add(this.RBE_TextBox);
            this.Panel1.Controls.Add(this.label7);
            this.Panel1.Controls.Add(this.SpeedMultiplier_TextBox);
            this.Panel1.Controls.Add(this.label1);
            this.Panel1.Controls.Add(this.label2);
            this.Panel1.Controls.Add(this.BloonName_TextBox);
            this.Panel1.Controls.Add(this.InitialHealth_TextBox);
            this.Panel1.Controls.Add(this.label4);
            this.Panel1.Controls.Add(this.BaseSpeed_TextBox);
            this.Panel1.Controls.Add(this.label5);
            this.Panel1.Controls.Add(this.CanGoUnderground_CheckBox);
            this.Panel1.Controls.Add(this.RotateToPathDirection_Checkbox);
            this.Panel1.Location = new System.Drawing.Point(2, 50);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(1071, 425);
            this.Panel1.TabIndex = 49;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.White;
            this.label18.Location = new System.Drawing.Point(683, 6);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(104, 20);
            this.label18.TabIndex = 63;
            this.label18.Text = "Child bloons:";
            // 
            // ChildBloons_CheckedListBox
            // 
            this.ChildBloons_CheckedListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.ChildBloons_CheckedListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ChildBloons_CheckedListBox.CheckOnClick = true;
            this.ChildBloons_CheckedListBox.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F);
            this.ChildBloons_CheckedListBox.ForeColor = System.Drawing.Color.White;
            this.ChildBloons_CheckedListBox.FormattingEnabled = true;
            this.ChildBloons_CheckedListBox.Items.AddRange(new object[] {
            "Red",
            "Blue",
            "Green",
            "Yellow",
            "Pink",
            "Black",
            "White",
            "Lead",
            "Zebra",
            "Rainbow",
            "Ceramic",
            "MOAB",
            "BFB",
            "ZOMG"});
            this.ChildBloons_CheckedListBox.Location = new System.Drawing.Point(678, 39);
            this.ChildBloons_CheckedListBox.Name = "ChildBloons_CheckedListBox";
            this.ChildBloons_CheckedListBox.Size = new System.Drawing.Size(321, 374);
            this.ChildBloons_CheckedListBox.TabIndex = 62;
            this.ChildBloons_CheckedListBox.SelectedIndexChanged += new System.EventHandler(this.ChildBloons_CheckedListBox_SelectedIndexChanged);
            this.ChildBloons_CheckedListBox.SelectedValueChanged += new System.EventHandler(this.ChildBloons_CheckedListBox_SelectedValueChanged);
            // 
            // HitAddon_TextBox
            // 
            this.HitAddon_TextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.HitAddon_TextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.HitAddon_TextBox.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HitAddon_TextBox.ForeColor = System.Drawing.Color.White;
            this.HitAddon_TextBox.Location = new System.Drawing.Point(333, 359);
            this.HitAddon_TextBox.Name = "HitAddon_TextBox";
            this.HitAddon_TextBox.Size = new System.Drawing.Size(250, 24);
            this.HitAddon_TextBox.TabIndex = 60;
            this.HitAddon_TextBox.Text = "";
            // 
            // HitAddon_Label
            // 
            this.HitAddon_Label.AutoSize = true;
            this.HitAddon_Label.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HitAddon_Label.ForeColor = System.Drawing.Color.White;
            this.HitAddon_Label.Location = new System.Drawing.Point(333, 336);
            this.HitAddon_Label.Name = "HitAddon_Label";
            this.HitAddon_Label.Size = new System.Drawing.Size(86, 20);
            this.HitAddon_Label.TabIndex = 61;
            this.HitAddon_Label.Text = "Hit addon:";
            // 
            // DrawLayer_TextBox
            // 
            this.DrawLayer_TextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.DrawLayer_TextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DrawLayer_TextBox.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DrawLayer_TextBox.ForeColor = System.Drawing.Color.White;
            this.DrawLayer_TextBox.Location = new System.Drawing.Point(56, 359);
            this.DrawLayer_TextBox.Name = "DrawLayer_TextBox";
            this.DrawLayer_TextBox.Size = new System.Drawing.Size(250, 24);
            this.DrawLayer_TextBox.TabIndex = 58;
            this.DrawLayer_TextBox.Text = "";
            // 
            // DrawLayer_Label
            // 
            this.DrawLayer_Label.AutoSize = true;
            this.DrawLayer_Label.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DrawLayer_Label.ForeColor = System.Drawing.Color.White;
            this.DrawLayer_Label.Location = new System.Drawing.Point(56, 336);
            this.DrawLayer_Label.Name = "DrawLayer_Label";
            this.DrawLayer_Label.Size = new System.Drawing.Size(89, 20);
            this.DrawLayer_Label.TabIndex = 59;
            this.DrawLayer_Label.Text = "Draw layer:";
            // 
            // ChildEffectScale_TextBox
            // 
            this.ChildEffectScale_TextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.ChildEffectScale_TextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ChildEffectScale_TextBox.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChildEffectScale_TextBox.ForeColor = System.Drawing.Color.White;
            this.ChildEffectScale_TextBox.Location = new System.Drawing.Point(333, 224);
            this.ChildEffectScale_TextBox.Name = "ChildEffectScale_TextBox";
            this.ChildEffectScale_TextBox.Size = new System.Drawing.Size(250, 24);
            this.ChildEffectScale_TextBox.TabIndex = 56;
            this.ChildEffectScale_TextBox.Text = "";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.White;
            this.label21.Location = new System.Drawing.Point(333, 201);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(136, 20);
            this.label21.TabIndex = 57;
            this.label21.Text = "Child effect scale:";
            // 
            // Radius_TextBox
            // 
            this.Radius_TextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.Radius_TextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Radius_TextBox.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Radius_TextBox.ForeColor = System.Drawing.Color.White;
            this.Radius_TextBox.Location = new System.Drawing.Point(333, 126);
            this.Radius_TextBox.Name = "Radius_TextBox";
            this.Radius_TextBox.Size = new System.Drawing.Size(250, 24);
            this.Radius_TextBox.TabIndex = 54;
            this.Radius_TextBox.Text = "";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(333, 103);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 20);
            this.label6.TabIndex = 55;
            this.label6.Text = "Radius:";
            // 
            // Scale_TextBox
            // 
            this.Scale_TextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.Scale_TextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Scale_TextBox.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Scale_TextBox.ForeColor = System.Drawing.Color.White;
            this.Scale_TextBox.Location = new System.Drawing.Point(333, 172);
            this.Scale_TextBox.Name = "Scale_TextBox";
            this.Scale_TextBox.Size = new System.Drawing.Size(250, 24);
            this.Scale_TextBox.TabIndex = 52;
            this.Scale_TextBox.Text = "";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.White;
            this.label20.Location = new System.Drawing.Point(333, 149);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(51, 20);
            this.label20.TabIndex = 53;
            this.label20.Text = "Scale:";
            // 
            // RBE_TextBox
            // 
            this.RBE_TextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.RBE_TextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RBE_TextBox.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RBE_TextBox.ForeColor = System.Drawing.Color.White;
            this.RBE_TextBox.Location = new System.Drawing.Point(333, 73);
            this.RBE_TextBox.Name = "RBE_TextBox";
            this.RBE_TextBox.Size = new System.Drawing.Size(250, 24);
            this.RBE_TextBox.TabIndex = 50;
            this.RBE_TextBox.Text = "";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(333, 50);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(40, 20);
            this.label7.TabIndex = 51;
            this.label7.Text = "RBE:";
            // 
            // SpeedMultiplier_TextBox
            // 
            this.SpeedMultiplier_TextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.SpeedMultiplier_TextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SpeedMultiplier_TextBox.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SpeedMultiplier_TextBox.ForeColor = System.Drawing.Color.White;
            this.SpeedMultiplier_TextBox.Location = new System.Drawing.Point(56, 224);
            this.SpeedMultiplier_TextBox.Name = "SpeedMultiplier_TextBox";
            this.SpeedMultiplier_TextBox.Size = new System.Drawing.Size(250, 24);
            this.SpeedMultiplier_TextBox.TabIndex = 48;
            this.SpeedMultiplier_TextBox.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(56, 201);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 20);
            this.label1.TabIndex = 49;
            this.label1.Text = "Speed multiplier:";
            // 
            // Panel2
            // 
            this.Panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel2.Controls.Add(this.ApplyStatus_CheckedListBox);
            this.Panel2.Controls.Add(this.label10);
            this.Panel2.Controls.Add(this.BloonAbility_CheckedListBox);
            this.Panel2.Controls.Add(this.label9);
            this.Panel2.Controls.Add(this.StatusImmunity_CheckedListBox);
            this.Panel2.Controls.Add(this.label8);
            this.Panel2.Controls.Add(this.DamageImmunity_CheckedListBox);
            this.Panel2.Controls.Add(this.label3);
            this.Panel2.Location = new System.Drawing.Point(5, 89);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(1071, 389);
            this.Panel2.TabIndex = 50;
            this.Panel2.Visible = false;
            // 
            // ApplyStatus_CheckedListBox
            // 
            this.ApplyStatus_CheckedListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.ApplyStatus_CheckedListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ApplyStatus_CheckedListBox.CheckOnClick = true;
            this.ApplyStatus_CheckedListBox.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F);
            this.ApplyStatus_CheckedListBox.ForeColor = System.Drawing.Color.White;
            this.ApplyStatus_CheckedListBox.FormattingEnabled = true;
            this.ApplyStatus_CheckedListBox.Items.AddRange(new object[] {
            "Camo",
            "Regen"});
            this.ApplyStatus_CheckedListBox.Location = new System.Drawing.Point(813, 31);
            this.ApplyStatus_CheckedListBox.Name = "ApplyStatus_CheckedListBox";
            this.ApplyStatus_CheckedListBox.Size = new System.Drawing.Size(183, 352);
            this.ApplyStatus_CheckedListBox.TabIndex = 69;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label10.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(831, 8);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(103, 20);
            this.label10.TabIndex = 68;
            this.label10.Text = "Apply status:";
            // 
            // BloonAbility_CheckedListBox
            // 
            this.BloonAbility_CheckedListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.BloonAbility_CheckedListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.BloonAbility_CheckedListBox.CheckOnClick = true;
            this.BloonAbility_CheckedListBox.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F);
            this.BloonAbility_CheckedListBox.ForeColor = System.Drawing.Color.White;
            this.BloonAbility_CheckedListBox.FormattingEnabled = true;
            this.BloonAbility_CheckedListBox.Items.AddRange(new object[] {
            "StunTowersAbility",
            "BloonariusAbility",
            "ShieldAbility",
            "SlowTowersAbility"});
            this.BloonAbility_CheckedListBox.Location = new System.Drawing.Point(565, 31);
            this.BloonAbility_CheckedListBox.Name = "BloonAbility_CheckedListBox";
            this.BloonAbility_CheckedListBox.Size = new System.Drawing.Size(183, 352);
            this.BloonAbility_CheckedListBox.TabIndex = 67;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label9.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(583, 8);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(99, 20);
            this.label9.TabIndex = 66;
            this.label9.Text = "Bloon ability";
            // 
            // StatusImmunity_CheckedListBox
            // 
            this.StatusImmunity_CheckedListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.StatusImmunity_CheckedListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.StatusImmunity_CheckedListBox.CheckOnClick = true;
            this.StatusImmunity_CheckedListBox.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F);
            this.StatusImmunity_CheckedListBox.ForeColor = System.Drawing.Color.White;
            this.StatusImmunity_CheckedListBox.FormattingEnabled = true;
            this.StatusImmunity_CheckedListBox.Items.AddRange(new object[] {
            "Ice",
            "Glue",
            "Slow",
            "Stun",
            "Camo",
            "Regen",
            "Napalm",
            "BeeSting",
            "IceShards",
            "VacStatus",
            "ViralFrost",
            "Permafrost",
            "MoveToPath",
            "AbsoluteZero",
            "CrippleMOAB",
            "DazeEffect",
            "MoveOnCurve",
            "BloonChipperSuck"});
            this.StatusImmunity_CheckedListBox.Location = new System.Drawing.Point(53, 31);
            this.StatusImmunity_CheckedListBox.Name = "StatusImmunity_CheckedListBox";
            this.StatusImmunity_CheckedListBox.Size = new System.Drawing.Size(197, 352);
            this.StatusImmunity_CheckedListBox.TabIndex = 65;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label8.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(71, 8);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(126, 20);
            this.label8.TabIndex = 64;
            this.label8.Text = "Status immunity";
            // 
            // DamageImmunity_CheckedListBox
            // 
            this.DamageImmunity_CheckedListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.DamageImmunity_CheckedListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DamageImmunity_CheckedListBox.CheckOnClick = true;
            this.DamageImmunity_CheckedListBox.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F);
            this.DamageImmunity_CheckedListBox.ForeColor = System.Drawing.Color.White;
            this.DamageImmunity_CheckedListBox.FormattingEnabled = true;
            this.DamageImmunity_CheckedListBox.Items.AddRange(new object[] {
            "Ice",
            "Fire",
            "Plasma",
            "Piercing",
            "Explosive",
            "Foam",
            "Juggernaut",
            "ShredBloon",
            "MOABMauler",
            "DamageOverTime"});
            this.DamageImmunity_CheckedListBox.Location = new System.Drawing.Point(320, 31);
            this.DamageImmunity_CheckedListBox.Name = "DamageImmunity_CheckedListBox";
            this.DamageImmunity_CheckedListBox.Size = new System.Drawing.Size(183, 352);
            this.DamageImmunity_CheckedListBox.TabIndex = 63;
            // 
            // SwitchPanel
            // 
            this.SwitchPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.SwitchPanel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SwitchPanel.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F);
            this.SwitchPanel.ForeColor = System.Drawing.Color.White;
            this.SwitchPanel.Location = new System.Drawing.Point(711, 484);
            this.SwitchPanel.Name = "SwitchPanel";
            this.SwitchPanel.Size = new System.Drawing.Size(164, 58);
            this.SwitchPanel.TabIndex = 51;
            this.SwitchPanel.Text = "Page 2";
            this.SwitchPanel.UseVisualStyleBackColor = false;
            this.SwitchPanel.Click += new System.EventHandler(this.SwitchPanel_Click);
            // 
            // AdvancedView_Checkbox
            // 
            this.AdvancedView_Checkbox.AutoSize = true;
            this.AdvancedView_Checkbox.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F);
            this.AdvancedView_Checkbox.ForeColor = System.Drawing.Color.White;
            this.AdvancedView_Checkbox.Location = new System.Drawing.Point(911, 20);
            this.AdvancedView_Checkbox.Name = "AdvancedView_Checkbox";
            this.AdvancedView_Checkbox.Size = new System.Drawing.Size(143, 24);
            this.AdvancedView_Checkbox.TabIndex = 60;
            this.AdvancedView_Checkbox.Text = "Advanced users";
            this.AdvancedView_Checkbox.UseVisualStyleBackColor = true;
            this.AdvancedView_Checkbox.CheckedChanged += new System.EventHandler(this.AdvancedView_Checkbox_CheckedChanged);
            // 
            // EZBloon_Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.ClientSize = new System.Drawing.Size(1066, 554);
            this.Controls.Add(this.AdvancedView_Checkbox);
            this.Controls.Add(this.SwitchPanel);
            this.Controls.Add(this.BloonFiles_ComboBox);
            this.Controls.Add(this.Save_Button);
            this.Controls.Add(this.BloonType_Label);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.Panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "EZBloon_Editor";
            this.Text = "EZBloon_Editor";
            this.Shown += new System.EventHandler(this.EasyTowerEditor_Shown);
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.Panel2.ResumeLayout(false);
            this.Panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RichTextBox BloonName_TextBox;
        private System.Windows.Forms.Label BloonType_Label;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox InitialHealth_TextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RichTextBox BaseSpeed_TextBox;
        private System.Windows.Forms.CheckBox CanGoUnderground_CheckBox;
        private System.Windows.Forms.CheckBox RotateToPathDirection_Checkbox;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.RichTextBox SpriteFile_TextBox;
        private System.Windows.Forms.Button Save_Button;
        private System.Windows.Forms.ComboBox BloonFiles_ComboBox;
        private System.Windows.Forms.Panel Panel1;
        private System.Windows.Forms.Panel Panel2;
        private System.Windows.Forms.RichTextBox SpeedMultiplier_TextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox Radius_TextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RichTextBox Scale_TextBox;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.RichTextBox RBE_TextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button SwitchPanel;
        private System.Windows.Forms.RichTextBox ChildEffectScale_TextBox;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.RichTextBox DrawLayer_TextBox;
        private System.Windows.Forms.Label DrawLayer_Label;
        private System.Windows.Forms.CheckBox AdvancedView_Checkbox;
        private System.Windows.Forms.RichTextBox HitAddon_TextBox;
        private System.Windows.Forms.Label HitAddon_Label;
        private System.Windows.Forms.CheckedListBox ChildBloons_CheckedListBox;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.CheckedListBox DamageImmunity_CheckedListBox;
        private System.Windows.Forms.CheckedListBox StatusImmunity_CheckedListBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckedListBox ApplyStatus_CheckedListBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckedListBox BloonAbility_CheckedListBox;
        private System.Windows.Forms.Label label9;
    }
}