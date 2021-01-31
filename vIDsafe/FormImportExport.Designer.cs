namespace vIDsafe
{
    partial class FormImportExport
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
            this.panelTitle = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panelBack = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.lvLogs = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnDateTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnLog = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.cmbIdentity = new System.Windows.Forms.ComboBox();
            this.cmbExportFormat = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel11 = new System.Windows.Forms.Panel();
            this.rdbAllData = new System.Windows.Forms.RadioButton();
            this.rdbIdentity = new System.Windows.Forms.RadioButton();
            this.panel12 = new System.Windows.Forms.Panel();
            this.btnExport = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.cmbImportFormat = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.panel13 = new System.Windows.Forms.Panel();
            this.rdbReplace = new System.Windows.Forms.RadioButton();
            this.rdbDoNotReplace = new System.Windows.Forms.RadioButton();
            this.panel9 = new System.Windows.Forms.Panel();
            this.btnImport = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.panelTitle.SuspendLayout();
            this.panelBack.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel10.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel11.SuspendLayout();
            this.panel12.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel13.SuspendLayout();
            this.panel9.SuspendLayout();
            this.SuspendLayout();
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
            this.panelTitle.Tag = "TitlePanel";
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
            this.label2.Tag = "SubTitleLabel";
            this.label2.Text = "Make your data portable";
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
            this.label1.Tag = "TitleLabel";
            this.label1.Text = "Import/Export";
            // 
            // panelBack
            // 
            this.panelBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(49)))), ((int)(((byte)(56)))));
            this.panelBack.Controls.Add(this.panel3);
            this.panelBack.Controls.Add(this.panelTitle);
            this.panelBack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBack.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelBack.Location = new System.Drawing.Point(0, 0);
            this.panelBack.Name = "panelBack";
            this.panelBack.Padding = new System.Windows.Forms.Padding(25);
            this.panelBack.Size = new System.Drawing.Size(823, 618);
            this.panelBack.TabIndex = 3;
            this.panelBack.Tag = "BackPanel";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(59)))), ((int)(((byte)(66)))));
            this.panel3.Controls.Add(this.panel8);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(25, 92);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(25);
            this.panel3.Size = new System.Drawing.Size(773, 501);
            this.panel3.TabIndex = 9;
            this.panel3.Tag = "MainPanel";
            // 
            // panel8
            // 
            this.panel8.AutoSize = true;
            this.panel8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(32)))), ((int)(((byte)(36)))));
            this.panel8.Controls.Add(this.panel6);
            this.panel8.Controls.Add(this.tableLayoutPanel2);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(25, 25);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(723, 451);
            this.panel8.TabIndex = 6;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(32)))), ((int)(((byte)(36)))));
            this.panel6.Controls.Add(this.panel10);
            this.panel6.Controls.Add(this.label3);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(0, 280);
            this.panel6.Name = "panel6";
            this.panel6.Padding = new System.Windows.Forms.Padding(25);
            this.panel6.Size = new System.Drawing.Size(723, 171);
            this.panel6.TabIndex = 16;
            this.panel6.Tag = "SubPanel";
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.lvLogs);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel10.Location = new System.Drawing.Point(25, 43);
            this.panel10.Name = "panel10";
            this.panel10.Padding = new System.Windows.Forms.Padding(0, 25, 0, 0);
            this.panel10.Size = new System.Drawing.Size(673, 103);
            this.panel10.TabIndex = 6;
            this.panel10.Tag = "SubPanel";
            // 
            // lvLogs
            // 
            this.lvLogs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(59)))), ((int)(((byte)(66)))));
            this.lvLogs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvLogs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnDateTime,
            this.columnLog});
            this.lvLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvLogs.ForeColor = System.Drawing.Color.Gainsboro;
            this.lvLogs.FullRowSelect = true;
            this.lvLogs.HideSelection = false;
            this.lvLogs.Location = new System.Drawing.Point(0, 25);
            this.lvLogs.Name = "lvLogs";
            this.lvLogs.Size = new System.Drawing.Size(673, 78);
            this.lvLogs.Sorting = System.Windows.Forms.SortOrder.Descending;
            this.lvLogs.TabIndex = 2;
            this.lvLogs.Tag = "InnerSubPanel";
            this.lvLogs.UseCompatibleStateImageBehavior = false;
            this.lvLogs.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 0;
            // 
            // columnDateTime
            // 
            this.columnDateTime.Text = "Date/Time";
            this.columnDateTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnDateTime.Width = 336;
            // 
            // columnLog
            // 
            this.columnLog.Text = "Log";
            this.columnLog.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnLog.Width = 337;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label3.ForeColor = System.Drawing.Color.LightGray;
            this.label3.Location = new System.Drawing.Point(25, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(673, 18);
            this.label3.TabIndex = 5;
            this.label3.Tag = "FrontSubTitleLabel";
            this.label3.Text = "Logs";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(723, 280);
            this.tableLayoutPanel2.TabIndex = 10;
            this.tableLayoutPanel2.Tag = "SubPanel";
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(32)))), ((int)(((byte)(36)))));
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.panel11);
            this.panel1.Controls.Add(this.panel12);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(365, 4);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(25);
            this.panel1.Size = new System.Drawing.Size(354, 272);
            this.panel1.TabIndex = 9;
            this.panel1.Tag = "SubPanel";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(32)))), ((int)(((byte)(36)))));
            this.panel4.Controls.Add(this.cmbIdentity);
            this.panel4.Controls.Add(this.cmbExportFormat);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(25, 47);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(0, 25, 0, 0);
            this.panel4.Size = new System.Drawing.Size(304, 85);
            this.panel4.TabIndex = 9;
            this.panel4.Tag = "SubPanel";
            // 
            // cmbIdentity
            // 
            this.cmbIdentity.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmbIdentity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbIdentity.Enabled = false;
            this.cmbIdentity.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbIdentity.FormattingEnabled = true;
            this.cmbIdentity.Location = new System.Drawing.Point(92, 45);
            this.cmbIdentity.Name = "cmbIdentity";
            this.cmbIdentity.Size = new System.Drawing.Size(121, 21);
            this.cmbIdentity.TabIndex = 2;
            this.cmbIdentity.SelectedIndexChanged += new System.EventHandler(this.cmbIdentity_SelectedIndexChanged);
            // 
            // cmbExportFormat
            // 
            this.cmbExportFormat.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmbExportFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbExportFormat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbExportFormat.FormattingEnabled = true;
            this.cmbExportFormat.Items.AddRange(new object[] {
            "CSV",
            "JSON",
            "Encrypted (Backup)"});
            this.cmbExportFormat.Location = new System.Drawing.Point(92, 18);
            this.cmbExportFormat.Name = "cmbExportFormat";
            this.cmbExportFormat.Size = new System.Drawing.Size(121, 21);
            this.cmbExportFormat.TabIndex = 1;
            this.cmbExportFormat.SelectedIndexChanged += new System.EventHandler(this.cmbExportFormat_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(32)))), ((int)(((byte)(36)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label4.ForeColor = System.Drawing.Color.LightGray;
            this.label4.Location = new System.Drawing.Point(25, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(304, 22);
            this.label4.TabIndex = 7;
            this.label4.Tag = "FrontSubTitleLabel";
            this.label4.Text = "Export data";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label4.UseMnemonic = false;
            // 
            // panel11
            // 
            this.panel11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(49)))), ((int)(((byte)(56)))));
            this.panel11.Controls.Add(this.rdbAllData);
            this.panel11.Controls.Add(this.rdbIdentity);
            this.panel11.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel11.Location = new System.Drawing.Point(25, 132);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(304, 50);
            this.panel11.TabIndex = 10;
            this.panel11.Tag = "SmallSubPanel";
            // 
            // rdbAllData
            // 
            this.rdbAllData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.rdbAllData.AutoSize = true;
            this.rdbAllData.Checked = true;
            this.rdbAllData.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbAllData.ForeColor = System.Drawing.Color.Gainsboro;
            this.rdbAllData.Location = new System.Drawing.Point(165, 17);
            this.rdbAllData.Name = "rdbAllData";
            this.rdbAllData.Size = new System.Drawing.Size(64, 17);
            this.rdbAllData.TabIndex = 3;
            this.rdbAllData.TabStop = true;
            this.rdbAllData.Text = "All data";
            this.rdbAllData.UseVisualStyleBackColor = true;
            this.rdbAllData.CheckedChanged += new System.EventHandler(this.rdbAllData_CheckedChanged);
            // 
            // rdbIdentity
            // 
            this.rdbIdentity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.rdbIdentity.AutoSize = true;
            this.rdbIdentity.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbIdentity.ForeColor = System.Drawing.Color.Gainsboro;
            this.rdbIdentity.Location = new System.Drawing.Point(76, 17);
            this.rdbIdentity.Name = "rdbIdentity";
            this.rdbIdentity.Size = new System.Drawing.Size(81, 17);
            this.rdbIdentity.TabIndex = 2;
            this.rdbIdentity.Text = "An identity";
            this.rdbIdentity.UseVisualStyleBackColor = true;
            this.rdbIdentity.CheckedChanged += new System.EventHandler(this.rdbIdentity_CheckedChanged);
            // 
            // panel12
            // 
            this.panel12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(59)))), ((int)(((byte)(66)))));
            this.panel12.Controls.Add(this.btnExport);
            this.panel12.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel12.Location = new System.Drawing.Point(25, 182);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(304, 65);
            this.panel12.TabIndex = 11;
            this.panel12.Tag = "InnerSubPanel";
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btnExport.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnExport.Enabled = false;
            this.btnExport.FlatAppearance.BorderSize = 0;
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.ForeColor = System.Drawing.Color.White;
            this.btnExport.Location = new System.Drawing.Point(102, 17);
            this.btnExport.MaximumSize = new System.Drawing.Size(100, 30);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(100, 30);
            this.btnExport.TabIndex = 3;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(32)))), ((int)(((byte)(36)))));
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.panel13);
            this.panel2.Controls.Add(this.panel9);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(25);
            this.panel2.Size = new System.Drawing.Size(354, 272);
            this.panel2.TabIndex = 8;
            this.panel2.Tag = "SubPanel";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(32)))), ((int)(((byte)(36)))));
            this.panel5.Controls.Add(this.cmbImportFormat);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(25, 47);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(0, 25, 0, 0);
            this.panel5.Size = new System.Drawing.Size(304, 85);
            this.panel5.TabIndex = 9;
            this.panel5.Tag = "SubPanel";
            // 
            // cmbImportFormat
            // 
            this.cmbImportFormat.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmbImportFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbImportFormat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbImportFormat.FormattingEnabled = true;
            this.cmbImportFormat.Items.AddRange(new object[] {
            "CSV",
            "JSON",
            "Encrypted (Backup)"});
            this.cmbImportFormat.Location = new System.Drawing.Point(92, 33);
            this.cmbImportFormat.Name = "cmbImportFormat";
            this.cmbImportFormat.Size = new System.Drawing.Size(121, 21);
            this.cmbImportFormat.TabIndex = 0;
            this.cmbImportFormat.SelectedIndexChanged += new System.EventHandler(this.cmbImportFormat_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(32)))), ((int)(((byte)(36)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label6.ForeColor = System.Drawing.Color.LightGray;
            this.label6.Location = new System.Drawing.Point(25, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(304, 22);
            this.label6.TabIndex = 7;
            this.label6.Tag = "FrontSubTitleLabel";
            this.label6.Text = "Import data";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label6.UseMnemonic = false;
            // 
            // panel13
            // 
            this.panel13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(49)))), ((int)(((byte)(56)))));
            this.panel13.Controls.Add(this.rdbReplace);
            this.panel13.Controls.Add(this.rdbDoNotReplace);
            this.panel13.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel13.Location = new System.Drawing.Point(25, 132);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(304, 50);
            this.panel13.TabIndex = 12;
            this.panel13.Tag = "SmallSubPanel";
            // 
            // rdbReplace
            // 
            this.rdbReplace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.rdbReplace.AutoSize = true;
            this.rdbReplace.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbReplace.ForeColor = System.Drawing.Color.Gainsboro;
            this.rdbReplace.Location = new System.Drawing.Point(51, 17);
            this.rdbReplace.Name = "rdbReplace";
            this.rdbReplace.Size = new System.Drawing.Size(93, 17);
            this.rdbReplace.TabIndex = 6;
            this.rdbReplace.Text = "Replace vault";
            this.rdbReplace.UseVisualStyleBackColor = true;
            this.rdbReplace.CheckedChanged += new System.EventHandler(this.rdbReplace_CheckedChanged);
            // 
            // rdbDoNotReplace
            // 
            this.rdbDoNotReplace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.rdbDoNotReplace.AutoSize = true;
            this.rdbDoNotReplace.Checked = true;
            this.rdbDoNotReplace.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbDoNotReplace.ForeColor = System.Drawing.Color.Gainsboro;
            this.rdbDoNotReplace.Location = new System.Drawing.Point(152, 17);
            this.rdbDoNotReplace.Name = "rdbDoNotReplace";
            this.rdbDoNotReplace.Size = new System.Drawing.Size(101, 17);
            this.rdbDoNotReplace.TabIndex = 5;
            this.rdbDoNotReplace.TabStop = true;
            this.rdbDoNotReplace.Text = "Do not replace";
            this.rdbDoNotReplace.UseVisualStyleBackColor = true;
            this.rdbDoNotReplace.UseWaitCursor = true;
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(59)))), ((int)(((byte)(66)))));
            this.panel9.Controls.Add(this.btnImport);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel9.Location = new System.Drawing.Point(25, 182);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(304, 65);
            this.panel9.TabIndex = 11;
            this.panel9.Tag = "InnerSubPanel";
            // 
            // btnImport
            // 
            this.btnImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btnImport.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnImport.Enabled = false;
            this.btnImport.FlatAppearance.BorderSize = 0;
            this.btnImport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImport.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImport.ForeColor = System.Drawing.Color.White;
            this.btnImport.Location = new System.Drawing.Point(102, 17);
            this.btnImport.MaximumSize = new System.Drawing.Size(100, 30);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(100, 30);
            this.btnImport.TabIndex = 3;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = false;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog_FileOk);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog_FileOk);
            // 
            // FormImportExport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(823, 618);
            this.Controls.Add(this.panelBack);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FormImportExport";
            this.Text = "ImportExport";
            this.Resize += new System.EventHandler(this.FormImportExport_Resize);
            this.panelTitle.ResumeLayout(false);
            this.panelBack.ResumeLayout(false);
            this.panelBack.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            this.panel12.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel13.ResumeLayout(false);
            this.panel13.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTitle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelBack;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.RadioButton rdbAllData;
        private System.Windows.Forms.RadioButton rdbIdentity;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.ComboBox cmbIdentity;
        private System.Windows.Forms.ComboBox cmbExportFormat;
        private System.Windows.Forms.ComboBox cmbImportFormat;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ListView lvLogs;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnDateTime;
        private System.Windows.Forms.ColumnHeader columnLog;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.RadioButton rdbDoNotReplace;
        private System.Windows.Forms.RadioButton rdbReplace;
    }
}