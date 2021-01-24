namespace vIDsafe
{
    partial class FormVault
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
            this.panelBack = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.pnlVaultComponents = new System.Windows.Forms.Panel();
            this.lvCredentials = new System.Windows.Forms.ListView();
            this.columnID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnUserName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnURL = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.txtSearchCredential = new System.Windows.Forms.TextBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnDeleteAll = new System.Windows.Forms.Button();
            this.cmbIdentity = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnGeneratePassword = new System.Windows.Forms.Button();
            this.btnGenerateUsername = new System.Windows.Forms.Button();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.txtURL = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panelTitle = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnNewCredential = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.panelBack.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel10.SuspendLayout();
            this.pnlVaultComponents.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panelTitle.SuspendLayout();
            this.panel4.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelBack
            // 
            this.panelBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(49)))), ((int)(((byte)(56)))));
            this.panelBack.Controls.Add(this.panel3);
            this.panelBack.Controls.Add(this.panel1);
            this.panelBack.Controls.Add(this.panelTitle);
            this.panelBack.Controls.Add(this.panel4);
            this.panelBack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBack.Location = new System.Drawing.Point(0, 0);
            this.panelBack.Name = "panelBack";
            this.panelBack.Padding = new System.Windows.Forms.Padding(25);
            this.panelBack.Size = new System.Drawing.Size(823, 618);
            this.panelBack.TabIndex = 3;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(59)))), ((int)(((byte)(66)))));
            this.panel3.Controls.Add(this.panel10);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(25, 92);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(25);
            this.panel3.Size = new System.Drawing.Size(256, 461);
            this.panel3.TabIndex = 9;
            // 
            // panel10
            // 
            this.panel10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(59)))), ((int)(((byte)(66)))));
            this.panel10.Controls.Add(this.pnlVaultComponents);
            this.panel10.Controls.Add(this.cmbIdentity);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel10.Location = new System.Drawing.Point(25, 47);
            this.panel10.Name = "panel10";
            this.panel10.Padding = new System.Windows.Forms.Padding(0, 25, 0, 0);
            this.panel10.Size = new System.Drawing.Size(206, 389);
            this.panel10.TabIndex = 8;
            // 
            // pnlVaultComponents
            // 
            this.pnlVaultComponents.Controls.Add(this.lvCredentials);
            this.pnlVaultComponents.Controls.Add(this.txtSearchCredential);
            this.pnlVaultComponents.Controls.Add(this.btnRefresh);
            this.pnlVaultComponents.Controls.Add(this.btnDeleteAll);
            this.pnlVaultComponents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlVaultComponents.Location = new System.Drawing.Point(0, 48);
            this.pnlVaultComponents.Name = "pnlVaultComponents";
            this.pnlVaultComponents.Size = new System.Drawing.Size(206, 341);
            this.pnlVaultComponents.TabIndex = 1;
            // 
            // lvCredentials
            // 
            this.lvCredentials.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(32)))), ((int)(((byte)(36)))));
            this.lvCredentials.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvCredentials.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnID,
            this.columnUserName,
            this.columnURL,
            this.columnStatus});
            this.lvCredentials.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvCredentials.ForeColor = System.Drawing.Color.Gainsboro;
            this.lvCredentials.FullRowSelect = true;
            this.lvCredentials.HideSelection = false;
            this.lvCredentials.Location = new System.Drawing.Point(0, 23);
            this.lvCredentials.MultiSelect = false;
            this.lvCredentials.Name = "lvCredentials";
            this.lvCredentials.Size = new System.Drawing.Size(206, 238);
            this.lvCredentials.TabIndex = 30;
            this.lvCredentials.UseCompatibleStateImageBehavior = false;
            this.lvCredentials.View = System.Windows.Forms.View.Details;
            this.lvCredentials.SelectedIndexChanged += new System.EventHandler(this.lvCredentials_SelectedIndexChanged);
            this.lvCredentials.Resize += new System.EventHandler(this.lvCredentials_Resize);
            // 
            // columnID
            // 
            this.columnID.Text = "#";
            this.columnID.Width = 0;
            // 
            // columnUserName
            // 
            this.columnUserName.Text = "Username";
            this.columnUserName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnUserName.Width = 106;
            // 
            // columnURL
            // 
            this.columnURL.Text = "URL";
            this.columnURL.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnURL.Width = 106;
            // 
            // columnStatus
            // 
            this.columnStatus.Text = "Status";
            this.columnStatus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnStatus.Width = 106;
            // 
            // txtSearchCredential
            // 
            this.txtSearchCredential.BackColor = System.Drawing.Color.DimGray;
            this.txtSearchCredential.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearchCredential.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtSearchCredential.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchCredential.ForeColor = System.Drawing.Color.Gainsboro;
            this.txtSearchCredential.Location = new System.Drawing.Point(0, 0);
            this.txtSearchCredential.Name = "txtSearchCredential";
            this.txtSearchCredential.Size = new System.Drawing.Size(206, 23);
            this.txtSearchCredential.TabIndex = 29;
            this.txtSearchCredential.TextChanged += new System.EventHandler(this.txtSearchCredential_TextChanged);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.btnRefresh.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnRefresh.Location = new System.Drawing.Point(0, 261);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(0);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(206, 40);
            this.btnRefresh.TabIndex = 31;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnDeleteAll
            // 
            this.btnDeleteAll.BackColor = System.Drawing.Color.LightSlateGray;
            this.btnDeleteAll.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnDeleteAll.FlatAppearance.BorderSize = 0;
            this.btnDeleteAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteAll.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteAll.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnDeleteAll.Location = new System.Drawing.Point(0, 301);
            this.btnDeleteAll.Margin = new System.Windows.Forms.Padding(0);
            this.btnDeleteAll.Name = "btnDeleteAll";
            this.btnDeleteAll.Size = new System.Drawing.Size(206, 40);
            this.btnDeleteAll.TabIndex = 32;
            this.btnDeleteAll.Text = "Delete All";
            this.btnDeleteAll.UseVisualStyleBackColor = false;
            this.btnDeleteAll.Click += new System.EventHandler(this.btnDeleteAll_Click);
            // 
            // cmbIdentity
            // 
            this.cmbIdentity.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cmbIdentity.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbIdentity.BackColor = System.Drawing.Color.White;
            this.cmbIdentity.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmbIdentity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbIdentity.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmbIdentity.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbIdentity.FormattingEnabled = true;
            this.cmbIdentity.Location = new System.Drawing.Point(0, 25);
            this.cmbIdentity.Name = "cmbIdentity";
            this.cmbIdentity.Size = new System.Drawing.Size(206, 23);
            this.cmbIdentity.TabIndex = 0;
            this.cmbIdentity.SelectedIndexChanged += new System.EventHandler(this.cmbIdentity_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(59)))), ((int)(((byte)(66)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label4.ForeColor = System.Drawing.Color.LightGray;
            this.label4.Location = new System.Drawing.Point(25, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(206, 22);
            this.label4.TabIndex = 7;
            this.label4.Text = "Credentials";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label4.UseMnemonic = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(32)))), ((int)(((byte)(36)))));
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(281, 92);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(25);
            this.panel1.Size = new System.Drawing.Size(517, 461);
            this.panel1.TabIndex = 8;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(32)))), ((int)(((byte)(36)))));
            this.panel2.Controls.Add(this.btnGeneratePassword);
            this.panel2.Controls.Add(this.btnGenerateUsername);
            this.panel2.Controls.Add(this.txtNotes);
            this.panel2.Controls.Add(this.txtURL);
            this.panel2.Controls.Add(this.txtPassword);
            this.panel2.Controls.Add(this.txtUsername);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(25, 43);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(0, 25, 0, 0);
            this.panel2.Size = new System.Drawing.Size(467, 393);
            this.panel2.TabIndex = 6;
            // 
            // btnGeneratePassword
            // 
            this.btnGeneratePassword.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnGeneratePassword.BackColor = System.Drawing.Color.Khaki;
            this.btnGeneratePassword.FlatAppearance.BorderSize = 0;
            this.btnGeneratePassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGeneratePassword.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGeneratePassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.btnGeneratePassword.Location = new System.Drawing.Point(306, 88);
            this.btnGeneratePassword.Name = "btnGeneratePassword";
            this.btnGeneratePassword.Size = new System.Drawing.Size(109, 23);
            this.btnGeneratePassword.TabIndex = 15;
            this.btnGeneratePassword.Text = "Generate";
            this.btnGeneratePassword.UseVisualStyleBackColor = false;
            this.btnGeneratePassword.Click += new System.EventHandler(this.btnGeneratePassword_Click);
            // 
            // btnGenerateUsername
            // 
            this.btnGenerateUsername.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnGenerateUsername.BackColor = System.Drawing.Color.Khaki;
            this.btnGenerateUsername.FlatAppearance.BorderSize = 0;
            this.btnGenerateUsername.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerateUsername.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerateUsername.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.btnGenerateUsername.Location = new System.Drawing.Point(306, 59);
            this.btnGenerateUsername.Name = "btnGenerateUsername";
            this.btnGenerateUsername.Size = new System.Drawing.Size(109, 23);
            this.btnGenerateUsername.TabIndex = 14;
            this.btnGenerateUsername.Text = "Generate";
            this.btnGenerateUsername.UseVisualStyleBackColor = false;
            this.btnGenerateUsername.Click += new System.EventHandler(this.btnGenerateUsername_Click);
            // 
            // txtNotes
            // 
            this.txtNotes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.txtNotes.BackColor = System.Drawing.Color.DimGray;
            this.txtNotes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNotes.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNotes.ForeColor = System.Drawing.Color.Gainsboro;
            this.txtNotes.Location = new System.Drawing.Point(48, 117);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(367, 276);
            this.txtNotes.TabIndex = 13;
            // 
            // txtURL
            // 
            this.txtURL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.txtURL.BackColor = System.Drawing.Color.DimGray;
            this.txtURL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtURL.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtURL.ForeColor = System.Drawing.Color.Gainsboro;
            this.txtURL.Location = new System.Drawing.Point(48, 30);
            this.txtURL.Name = "txtURL";
            this.txtURL.Size = new System.Drawing.Size(367, 23);
            this.txtURL.TabIndex = 12;
            // 
            // txtPassword
            // 
            this.txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.txtPassword.BackColor = System.Drawing.Color.DimGray;
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.ForeColor = System.Drawing.Color.Gainsboro;
            this.txtPassword.Location = new System.Drawing.Point(48, 88);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(252, 23);
            this.txtPassword.TabIndex = 11;
            // 
            // txtUsername
            // 
            this.txtUsername.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.txtUsername.BackColor = System.Drawing.Color.DimGray;
            this.txtUsername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUsername.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsername.ForeColor = System.Drawing.Color.Gainsboro;
            this.txtUsername.Location = new System.Drawing.Point(48, 59);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(252, 23);
            this.txtUsername.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label3.ForeColor = System.Drawing.Color.LightGray;
            this.label3.Location = new System.Drawing.Point(25, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(467, 18);
            this.label3.TabIndex = 5;
            this.label3.Text = "Credential information";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelTitle
            // 
            this.panelTitle.AutoSize = true;
            this.panelTitle.Controls.Add(this.label2);
            this.panelTitle.Controls.Add(this.label1);
            this.panelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitle.Location = new System.Drawing.Point(25, 25);
            this.panelTitle.Name = "panelTitle";
            this.panelTitle.Padding = new System.Windows.Forms.Padding(0, 0, 0, 25);
            this.panelTitle.Size = new System.Drawing.Size(773, 67);
            this.panelTitle.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label2.ForeColor = System.Drawing.Color.DarkGray;
            this.label2.Location = new System.Drawing.Point(0, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(773, 21);
            this.label2.TabIndex = 4;
            this.label2.Text = "Create and view credentials";
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Gainsboro;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(773, 21);
            this.label1.TabIndex = 3;
            this.label1.Text = "Vault";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.tableLayoutPanel1);
            this.panel4.Controls.Add(this.tableLayoutPanel2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(25, 553);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(773, 40);
            this.panel4.TabIndex = 10;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.btnNewCredential, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(256, 40);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // btnNewCredential
            // 
            this.btnNewCredential.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnNewCredential.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNewCredential.FlatAppearance.BorderSize = 0;
            this.btnNewCredential.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewCredential.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewCredential.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnNewCredential.Location = new System.Drawing.Point(0, 0);
            this.btnNewCredential.Margin = new System.Windows.Forms.Padding(0);
            this.btnNewCredential.Name = "btnNewCredential";
            this.btnNewCredential.Size = new System.Drawing.Size(256, 40);
            this.btnNewCredential.TabIndex = 0;
            this.btnNewCredential.Text = "Add";
            this.btnNewCredential.UseVisualStyleBackColor = false;
            this.btnNewCredential.Click += new System.EventHandler(this.btnNewCredential_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.btnSave, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnDelete, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(256, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(517, 40);
            this.tableLayoutPanel2.TabIndex = 17;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.SeaGreen;
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnSave.Location = new System.Drawing.Point(0, 0);
            this.btnSave.Margin = new System.Windows.Forms.Padding(0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(258, 40);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.LightSlateGray;
            this.btnDelete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnDelete.Location = new System.Drawing.Point(258, 0);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(0);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(259, 40);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // FormVault
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(823, 618);
            this.Controls.Add(this.panelBack);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FormVault";
            this.Text = "Vault";
            this.panelBack.ResumeLayout(false);
            this.panelBack.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.pnlVaultComponents.ResumeLayout(false);
            this.pnlVaultComponents.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panelTitle.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelBack;
        private System.Windows.Forms.Panel panelTitle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ComboBox cmbIdentity;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.TextBox txtURL;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Button btnGenerateUsername;
        private System.Windows.Forms.Button btnGeneratePassword;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnNewCredential;
        private System.Windows.Forms.Panel pnlVaultComponents;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.ListView lvCredentials;
        private System.Windows.Forms.ColumnHeader columnID;
        private System.Windows.Forms.ColumnHeader columnUserName;
        private System.Windows.Forms.ColumnHeader columnURL;
        private System.Windows.Forms.ColumnHeader columnStatus;
        private System.Windows.Forms.TextBox txtSearchCredential;
        private System.Windows.Forms.Button btnDeleteAll;
    }
}