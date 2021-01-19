namespace vIDsafe
{
    partial class FormGeneratePassword
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
            this.panelTitle = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panelBack = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblGeneratedPassword = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnCopy = new System.Windows.Forms.Button();
            this.btnRegenerate = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lblLength = new System.Windows.Forms.Label();
            this.rbPassphrase = new System.Windows.Forms.RadioButton();
            this.rbPassword = new System.Windows.Forms.RadioButton();
            this.tbPasswordLength = new System.Windows.Forms.TrackBar();
            this.clbSettings = new System.Windows.Forms.CheckedListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.lvPasswordHistory = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnDateTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnPassword = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label4 = new System.Windows.Forms.Label();
            this.panelTitle.SuspendLayout();
            this.panelBack.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbPasswordLength)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 15.75F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(935, 557);
            this.label1.TabIndex = 0;
            this.label1.Text = "Overview";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelTitle
            // 
            this.panelTitle.AutoSize = true;
            this.panelTitle.Controls.Add(this.label2);
            this.panelTitle.Controls.Add(this.label3);
            this.panelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitle.Location = new System.Drawing.Point(25, 25);
            this.panelTitle.Name = "panelTitle";
            this.panelTitle.Padding = new System.Windows.Forms.Padding(0, 0, 0, 25);
            this.panelTitle.Size = new System.Drawing.Size(885, 67);
            this.panelTitle.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label2.ForeColor = System.Drawing.Color.DarkGray;
            this.label2.Location = new System.Drawing.Point(0, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(885, 21);
            this.label2.TabIndex = 4;
            this.label2.Text = "Create a password or a passphrase";
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.Gainsboro;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(885, 21);
            this.label3.TabIndex = 3;
            this.label3.Text = "Generate password";
            // 
            // panelBack
            // 
            this.panelBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(49)))), ((int)(((byte)(56)))));
            this.panelBack.Controls.Add(this.panel8);
            this.panelBack.Controls.Add(this.panel1);
            this.panelBack.Controls.Add(this.panelTitle);
            this.panelBack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBack.Location = new System.Drawing.Point(0, 0);
            this.panelBack.Name = "panelBack";
            this.panelBack.Padding = new System.Windows.Forms.Padding(25);
            this.panelBack.Size = new System.Drawing.Size(935, 557);
            this.panelBack.TabIndex = 3;
            // 
            // panel8
            // 
            this.panel8.AutoSize = true;
            this.panel8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(59)))), ((int)(((byte)(66)))));
            this.panel8.Controls.Add(this.panel2);
            this.panel8.Controls.Add(this.label6);
            this.panel8.Controls.Add(this.panel4);
            this.panel8.Controls.Add(this.panel5);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(25, 92);
            this.panel8.Name = "panel8";
            this.panel8.Padding = new System.Windows.Forms.Padding(25);
            this.panel8.Size = new System.Drawing.Size(594, 440);
            this.panel8.TabIndex = 11;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(59)))), ((int)(((byte)(66)))));
            this.panel2.Controls.Add(this.lblGeneratedPassword);
            this.panel2.Controls.Add(this.panel6);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(25, 47);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(0, 25, 0, 0);
            this.panel2.Size = new System.Drawing.Size(544, 168);
            this.panel2.TabIndex = 9;
            // 
            // lblGeneratedPassword
            // 
            this.lblGeneratedPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(32)))), ((int)(((byte)(36)))));
            this.lblGeneratedPassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblGeneratedPassword.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGeneratedPassword.ForeColor = System.Drawing.Color.White;
            this.lblGeneratedPassword.Location = new System.Drawing.Point(0, 25);
            this.lblGeneratedPassword.Name = "lblGeneratedPassword";
            this.lblGeneratedPassword.Size = new System.Drawing.Size(544, 128);
            this.lblGeneratedPassword.TabIndex = 8;
            this.lblGeneratedPassword.Text = "abcdef0123";
            this.lblGeneratedPassword.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.SpringGreen;
            this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel6.Location = new System.Drawing.Point(0, 153);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(544, 15);
            this.panel6.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(59)))), ((int)(((byte)(66)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label6.ForeColor = System.Drawing.Color.LightGray;
            this.label6.Location = new System.Drawing.Point(25, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(544, 22);
            this.label6.TabIndex = 7;
            this.label6.Text = "Generate a password";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label6.UseMnemonic = false;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(59)))), ((int)(((byte)(66)))));
            this.panel4.Controls.Add(this.btnCopy);
            this.panel4.Controls.Add(this.btnRegenerate);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(25, 215);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(544, 100);
            this.panel4.TabIndex = 10;
            // 
            // btnCopy
            // 
            this.btnCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btnCopy.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnCopy.FlatAppearance.BorderSize = 0;
            this.btnCopy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCopy.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCopy.ForeColor = System.Drawing.Color.White;
            this.btnCopy.Location = new System.Drawing.Point(297, 35);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(100, 30);
            this.btnCopy.TabIndex = 1;
            this.btnCopy.Text = "Copy";
            this.btnCopy.UseVisualStyleBackColor = false;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnRegenerate
            // 
            this.btnRegenerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btnRegenerate.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnRegenerate.FlatAppearance.BorderSize = 0;
            this.btnRegenerate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegenerate.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegenerate.ForeColor = System.Drawing.Color.White;
            this.btnRegenerate.Location = new System.Drawing.Point(148, 35);
            this.btnRegenerate.Name = "btnRegenerate";
            this.btnRegenerate.Size = new System.Drawing.Size(100, 30);
            this.btnRegenerate.TabIndex = 0;
            this.btnRegenerate.Text = "Regenerate";
            this.btnRegenerate.UseVisualStyleBackColor = false;
            this.btnRegenerate.Click += new System.EventHandler(this.btnRegenerate_Click);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(32)))), ((int)(((byte)(36)))));
            this.panel5.Controls.Add(this.lblLength);
            this.panel5.Controls.Add(this.rbPassphrase);
            this.panel5.Controls.Add(this.rbPassword);
            this.panel5.Controls.Add(this.tbPasswordLength);
            this.panel5.Controls.Add(this.clbSettings);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(25, 315);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(544, 100);
            this.panel5.TabIndex = 11;
            // 
            // lblLength
            // 
            this.lblLength.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.lblLength.AutoSize = true;
            this.lblLength.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblLength.Location = new System.Drawing.Point(266, 59);
            this.lblLength.Name = "lblLength";
            this.lblLength.Size = new System.Drawing.Size(13, 13);
            this.lblLength.TabIndex = 4;
            this.lblLength.Text = "5";
            this.lblLength.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rbPassphrase
            // 
            this.rbPassphrase.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.rbPassphrase.AutoSize = true;
            this.rbPassphrase.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbPassphrase.ForeColor = System.Drawing.Color.Gainsboro;
            this.rbPassphrase.Location = new System.Drawing.Point(34, 53);
            this.rbPassphrase.Name = "rbPassphrase";
            this.rbPassphrase.Size = new System.Drawing.Size(82, 17);
            this.rbPassphrase.TabIndex = 3;
            this.rbPassphrase.Text = "Passphrase";
            this.rbPassphrase.UseVisualStyleBackColor = true;
            this.rbPassphrase.CheckedChanged += new System.EventHandler(this.rbPassphrase_CheckedChanged);
            // 
            // rbPassword
            // 
            this.rbPassword.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.rbPassword.AutoSize = true;
            this.rbPassword.Checked = true;
            this.rbPassword.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbPassword.ForeColor = System.Drawing.Color.Gainsboro;
            this.rbPassword.Location = new System.Drawing.Point(34, 29);
            this.rbPassword.Name = "rbPassword";
            this.rbPassword.Size = new System.Drawing.Size(74, 17);
            this.rbPassword.TabIndex = 2;
            this.rbPassword.TabStop = true;
            this.rbPassword.Text = "Password";
            this.rbPassword.UseVisualStyleBackColor = true;
            this.rbPassword.CheckedChanged += new System.EventHandler(this.rbPassword_CheckedChanged);
            // 
            // tbPasswordLength
            // 
            this.tbPasswordLength.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.tbPasswordLength.AutoSize = false;
            this.tbPasswordLength.Location = new System.Drawing.Point(220, 41);
            this.tbPasswordLength.Maximum = 20;
            this.tbPasswordLength.Minimum = 5;
            this.tbPasswordLength.Name = "tbPasswordLength";
            this.tbPasswordLength.Size = new System.Drawing.Size(104, 18);
            this.tbPasswordLength.TabIndex = 1;
            this.tbPasswordLength.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbPasswordLength.Value = 5;
            this.tbPasswordLength.Scroll += new System.EventHandler(this.tbPasswordLength_Scroll);
            // 
            // clbSettings
            // 
            this.clbSettings.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.clbSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(32)))), ((int)(((byte)(36)))));
            this.clbSettings.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.clbSettings.CheckOnClick = true;
            this.clbSettings.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clbSettings.ForeColor = System.Drawing.Color.Gainsboro;
            this.clbSettings.FormattingEnabled = true;
            this.clbSettings.Items.AddRange(new object[] {
            "A-Z",
            "0-9",
            "a-z",
            "!$@^%#*"});
            this.clbSettings.Location = new System.Drawing.Point(439, 14);
            this.clbSettings.Name = "clbSettings";
            this.clbSettings.Size = new System.Drawing.Size(85, 68);
            this.clbSettings.TabIndex = 0;
            this.clbSettings.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbSettings_ItemCheck);
            this.clbSettings.SelectedIndexChanged += new System.EventHandler(this.clbSettings_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(32)))), ((int)(((byte)(36)))));
            this.panel1.Controls.Add(this.panel7);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(619, 92);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(25);
            this.panel1.Size = new System.Drawing.Size(291, 440);
            this.panel1.TabIndex = 10;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.lvPasswordHistory);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(25, 43);
            this.panel7.Name = "panel7";
            this.panel7.Padding = new System.Windows.Forms.Padding(0, 25, 0, 0);
            this.panel7.Size = new System.Drawing.Size(241, 372);
            this.panel7.TabIndex = 6;
            // 
            // lvPasswordHistory
            // 
            this.lvPasswordHistory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(59)))), ((int)(((byte)(66)))));
            this.lvPasswordHistory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvPasswordHistory.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnDateTime,
            this.columnPassword});
            this.lvPasswordHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvPasswordHistory.ForeColor = System.Drawing.Color.Gainsboro;
            this.lvPasswordHistory.FullRowSelect = true;
            this.lvPasswordHistory.HideSelection = false;
            this.lvPasswordHistory.Location = new System.Drawing.Point(0, 25);
            this.lvPasswordHistory.Name = "lvPasswordHistory";
            this.lvPasswordHistory.Size = new System.Drawing.Size(241, 347);
            this.lvPasswordHistory.Sorting = System.Windows.Forms.SortOrder.Descending;
            this.lvPasswordHistory.TabIndex = 3;
            this.lvPasswordHistory.UseCompatibleStateImageBehavior = false;
            this.lvPasswordHistory.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.DisplayIndex = 2;
            this.columnHeader1.Width = 0;
            // 
            // columnDateTime
            // 
            this.columnDateTime.DisplayIndex = 0;
            this.columnDateTime.Text = "Date/Time";
            this.columnDateTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnDateTime.Width = 121;
            // 
            // columnPassword
            // 
            this.columnPassword.DisplayIndex = 1;
            this.columnPassword.Text = "Password";
            this.columnPassword.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnPassword.Width = 121;
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label4.ForeColor = System.Drawing.Color.LightGray;
            this.label4.Location = new System.Drawing.Point(25, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(241, 18);
            this.label4.TabIndex = 5;
            this.label4.Text = "Previous passwords";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormGeneratePassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(935, 557);
            this.Controls.Add(this.panelBack);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.Name = "FormGeneratePassword";
            this.Text = "GeneratePassword";
            this.panelTitle.ResumeLayout(false);
            this.panelBack.ResumeLayout(false);
            this.panelBack.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbPasswordLength)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelTitle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panelBack;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblGeneratedPassword;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Button btnRegenerate;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.RadioButton rbPassphrase;
        private System.Windows.Forms.RadioButton rbPassword;
        private System.Windows.Forms.TrackBar tbPasswordLength;
        private System.Windows.Forms.CheckedListBox clbSettings;
        private System.Windows.Forms.Label lblLength;
        private System.Windows.Forms.ListView lvPasswordHistory;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnDateTime;
        private System.Windows.Forms.ColumnHeader columnPassword;
    }
}