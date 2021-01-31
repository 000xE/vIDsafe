namespace vIDsafe
{
    partial class FormHome
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnPasswordManager = new System.Windows.Forms.Button();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlNavigation = new System.Windows.Forms.Panel();
            this.btnLogOut = new System.Windows.Forms.Button();
            this.pnlDataSubMenu = new System.Windows.Forms.Panel();
            this.btnMasterAccount = new System.Windows.Forms.Button();
            this.btnImportExport = new System.Windows.Forms.Button();
            this.btnData = new System.Windows.Forms.Button();
            this.pnlPMSubMenu = new System.Windows.Forms.Panel();
            this.btnGeneratePassword = new System.Windows.Forms.Button();
            this.btnVault = new System.Windows.Forms.Button();
            this.btnIdentities = new System.Windows.Forms.Button();
            this.btnOverview = new System.Windows.Forms.Button();
            this.pnlProgressBack = new System.Windows.Forms.Panel();
            this.lblHealthScore = new System.Windows.Forms.Label();
            this.pnlProgressBar = new System.Windows.Forms.Panel();
            this.pnlMasterName = new System.Windows.Forms.Panel();
            this.lblMAName = new System.Windows.Forms.Label();
            this.panelForm = new System.Windows.Forms.Panel();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateAPasswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.windowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hideToTrayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.onCloseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.onMinimizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.onStartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alwaysOnTopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.themeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmbTheme = new System.Windows.Forms.ToolStripComboBox();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.pnlMain.SuspendLayout();
            this.pnlNavigation.SuspendLayout();
            this.pnlDataSubMenu.SuspendLayout();
            this.pnlPMSubMenu.SuspendLayout();
            this.pnlProgressBack.SuspendLayout();
            this.pnlMasterName.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnPasswordManager
            // 
            this.btnPasswordManager.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(59)))), ((int)(((byte)(66)))));
            this.btnPasswordManager.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPasswordManager.FlatAppearance.BorderSize = 0;
            this.btnPasswordManager.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPasswordManager.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnPasswordManager.ForeColor = System.Drawing.Color.White;
            this.btnPasswordManager.Location = new System.Drawing.Point(0, 0);
            this.btnPasswordManager.Name = "btnPasswordManager";
            this.btnPasswordManager.Size = new System.Drawing.Size(225, 45);
            this.btnPasswordManager.TabIndex = 0;
            this.btnPasswordManager.Tag = "SubMenuButton";
            this.btnPasswordManager.Text = "Password manager";
            this.btnPasswordManager.UseVisualStyleBackColor = false;
            this.btnPasswordManager.Click += new System.EventHandler(this.btnPasswordManager_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.pnlMain.Controls.Add(this.pnlNavigation);
            this.pnlMain.Controls.Add(this.pnlProgressBack);
            this.pnlMain.Controls.Add(this.pnlMasterName);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlMain.Location = new System.Drawing.Point(0, 24);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(225, 657);
            this.pnlMain.TabIndex = 0;
            this.pnlMain.Tag = "MainPanel";
            // 
            // pnlNavigation
            // 
            this.pnlNavigation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(32)))), ((int)(((byte)(36)))));
            this.pnlNavigation.Controls.Add(this.btnLogOut);
            this.pnlNavigation.Controls.Add(this.pnlDataSubMenu);
            this.pnlNavigation.Controls.Add(this.btnData);
            this.pnlNavigation.Controls.Add(this.pnlPMSubMenu);
            this.pnlNavigation.Controls.Add(this.btnPasswordManager);
            this.pnlNavigation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlNavigation.Location = new System.Drawing.Point(0, 129);
            this.pnlNavigation.Name = "pnlNavigation";
            this.pnlNavigation.Size = new System.Drawing.Size(225, 528);
            this.pnlNavigation.TabIndex = 2;
            this.pnlNavigation.Tag = "NavigationPanel";
            // 
            // btnLogOut
            // 
            this.btnLogOut.BackColor = System.Drawing.Color.IndianRed;
            this.btnLogOut.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnLogOut.FlatAppearance.BorderSize = 0;
            this.btnLogOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogOut.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogOut.ForeColor = System.Drawing.Color.White;
            this.btnLogOut.Location = new System.Drawing.Point(0, 488);
            this.btnLogOut.Name = "btnLogOut";
            this.btnLogOut.Size = new System.Drawing.Size(225, 40);
            this.btnLogOut.TabIndex = 10;
            this.btnLogOut.Text = "Logout";
            this.btnLogOut.UseVisualStyleBackColor = false;
            this.btnLogOut.Click += new System.EventHandler(this.btnLogOut_Click);
            // 
            // pnlDataSubMenu
            // 
            this.pnlDataSubMenu.Controls.Add(this.btnMasterAccount);
            this.pnlDataSubMenu.Controls.Add(this.btnImportExport);
            this.pnlDataSubMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDataSubMenu.Location = new System.Drawing.Point(0, 250);
            this.pnlDataSubMenu.Name = "pnlDataSubMenu";
            this.pnlDataSubMenu.Size = new System.Drawing.Size(225, 80);
            this.pnlDataSubMenu.TabIndex = 2;
            // 
            // btnMasterAccount
            // 
            this.btnMasterAccount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(32)))), ((int)(((byte)(36)))));
            this.btnMasterAccount.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnMasterAccount.FlatAppearance.BorderSize = 0;
            this.btnMasterAccount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMasterAccount.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMasterAccount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnMasterAccount.Location = new System.Drawing.Point(0, 40);
            this.btnMasterAccount.Name = "btnMasterAccount";
            this.btnMasterAccount.Size = new System.Drawing.Size(225, 40);
            this.btnMasterAccount.TabIndex = 7;
            this.btnMasterAccount.Tag = "NavButton";
            this.btnMasterAccount.Text = "Manage master account";
            this.btnMasterAccount.UseVisualStyleBackColor = false;
            this.btnMasterAccount.Click += new System.EventHandler(this.btnMasterAccount_Click);
            // 
            // btnImportExport
            // 
            this.btnImportExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(32)))), ((int)(((byte)(36)))));
            this.btnImportExport.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnImportExport.FlatAppearance.BorderSize = 0;
            this.btnImportExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImportExport.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportExport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnImportExport.Location = new System.Drawing.Point(0, 0);
            this.btnImportExport.Name = "btnImportExport";
            this.btnImportExport.Size = new System.Drawing.Size(225, 40);
            this.btnImportExport.TabIndex = 6;
            this.btnImportExport.Tag = "NavButton";
            this.btnImportExport.Text = "Import/Export data";
            this.btnImportExport.UseVisualStyleBackColor = false;
            this.btnImportExport.Click += new System.EventHandler(this.btnImportExport_Click);
            // 
            // btnData
            // 
            this.btnData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(59)))), ((int)(((byte)(66)))));
            this.btnData.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnData.FlatAppearance.BorderSize = 0;
            this.btnData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnData.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnData.ForeColor = System.Drawing.Color.White;
            this.btnData.Location = new System.Drawing.Point(0, 205);
            this.btnData.Name = "btnData";
            this.btnData.Size = new System.Drawing.Size(225, 45);
            this.btnData.TabIndex = 5;
            this.btnData.Tag = "SubMenuButton";
            this.btnData.Text = "Data";
            this.btnData.UseVisualStyleBackColor = false;
            this.btnData.Click += new System.EventHandler(this.btnData_Click);
            // 
            // pnlPMSubMenu
            // 
            this.pnlPMSubMenu.Controls.Add(this.btnGeneratePassword);
            this.pnlPMSubMenu.Controls.Add(this.btnVault);
            this.pnlPMSubMenu.Controls.Add(this.btnIdentities);
            this.pnlPMSubMenu.Controls.Add(this.btnOverview);
            this.pnlPMSubMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPMSubMenu.Location = new System.Drawing.Point(0, 45);
            this.pnlPMSubMenu.Name = "pnlPMSubMenu";
            this.pnlPMSubMenu.Size = new System.Drawing.Size(225, 160);
            this.pnlPMSubMenu.TabIndex = 1;
            // 
            // btnGeneratePassword
            // 
            this.btnGeneratePassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(32)))), ((int)(((byte)(36)))));
            this.btnGeneratePassword.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnGeneratePassword.FlatAppearance.BorderSize = 0;
            this.btnGeneratePassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGeneratePassword.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGeneratePassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnGeneratePassword.Location = new System.Drawing.Point(0, 120);
            this.btnGeneratePassword.Name = "btnGeneratePassword";
            this.btnGeneratePassword.Size = new System.Drawing.Size(225, 40);
            this.btnGeneratePassword.TabIndex = 4;
            this.btnGeneratePassword.Tag = "NavButton";
            this.btnGeneratePassword.Text = "Generate a password";
            this.btnGeneratePassword.UseVisualStyleBackColor = false;
            this.btnGeneratePassword.Click += new System.EventHandler(this.btnGeneratePassword_Click);
            // 
            // btnVault
            // 
            this.btnVault.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(32)))), ((int)(((byte)(36)))));
            this.btnVault.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnVault.FlatAppearance.BorderSize = 0;
            this.btnVault.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVault.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVault.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnVault.Location = new System.Drawing.Point(0, 80);
            this.btnVault.Name = "btnVault";
            this.btnVault.Size = new System.Drawing.Size(225, 40);
            this.btnVault.TabIndex = 3;
            this.btnVault.Tag = "NavButton";
            this.btnVault.Text = "Vault";
            this.btnVault.UseVisualStyleBackColor = false;
            this.btnVault.Click += new System.EventHandler(this.btnVault_Click);
            // 
            // btnIdentities
            // 
            this.btnIdentities.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(32)))), ((int)(((byte)(36)))));
            this.btnIdentities.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnIdentities.FlatAppearance.BorderSize = 0;
            this.btnIdentities.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIdentities.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIdentities.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnIdentities.Location = new System.Drawing.Point(0, 40);
            this.btnIdentities.Name = "btnIdentities";
            this.btnIdentities.Size = new System.Drawing.Size(225, 40);
            this.btnIdentities.TabIndex = 2;
            this.btnIdentities.Tag = "NavButton";
            this.btnIdentities.Text = "Identities";
            this.btnIdentities.UseVisualStyleBackColor = false;
            this.btnIdentities.Click += new System.EventHandler(this.btnIdentities_Click);
            // 
            // btnOverview
            // 
            this.btnOverview.BackColor = System.Drawing.Color.Gainsboro;
            this.btnOverview.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnOverview.FlatAppearance.BorderSize = 0;
            this.btnOverview.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOverview.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOverview.ForeColor = System.Drawing.Color.Black;
            this.btnOverview.Location = new System.Drawing.Point(0, 0);
            this.btnOverview.Name = "btnOverview";
            this.btnOverview.Size = new System.Drawing.Size(225, 40);
            this.btnOverview.TabIndex = 1;
            this.btnOverview.Tag = "NavButton selected";
            this.btnOverview.Text = "Overview";
            this.btnOverview.UseVisualStyleBackColor = false;
            this.btnOverview.Click += new System.EventHandler(this.btnOverview_Click);
            // 
            // pnlProgressBack
            // 
            this.pnlProgressBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.pnlProgressBack.Controls.Add(this.lblHealthScore);
            this.pnlProgressBack.Controls.Add(this.pnlProgressBar);
            this.pnlProgressBack.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlProgressBack.Location = new System.Drawing.Point(0, 100);
            this.pnlProgressBack.Name = "pnlProgressBack";
            this.pnlProgressBack.Size = new System.Drawing.Size(225, 29);
            this.pnlProgressBack.TabIndex = 1;
            // 
            // lblHealthScore
            // 
            this.lblHealthScore.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHealthScore.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblHealthScore.Location = new System.Drawing.Point(225, 0);
            this.lblHealthScore.Name = "lblHealthScore";
            this.lblHealthScore.Size = new System.Drawing.Size(0, 29);
            this.lblHealthScore.TabIndex = 3;
            this.lblHealthScore.Text = "100%";
            this.lblHealthScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlProgressBar
            // 
            this.pnlProgressBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(200)))), ((int)(((byte)(0)))));
            this.pnlProgressBar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlProgressBar.Location = new System.Drawing.Point(0, 0);
            this.pnlProgressBar.MaximumSize = new System.Drawing.Size(225, 35);
            this.pnlProgressBar.Name = "pnlProgressBar";
            this.pnlProgressBar.Size = new System.Drawing.Size(225, 29);
            this.pnlProgressBar.TabIndex = 2;
            // 
            // pnlMasterName
            // 
            this.pnlMasterName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(50)))));
            this.pnlMasterName.Controls.Add(this.lblMAName);
            this.pnlMasterName.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMasterName.Location = new System.Drawing.Point(0, 0);
            this.pnlMasterName.Name = "pnlMasterName";
            this.pnlMasterName.Size = new System.Drawing.Size(225, 100);
            this.pnlMasterName.TabIndex = 0;
            this.pnlMasterName.Tag = "MasterNamePanel";
            // 
            // lblMAName
            // 
            this.lblMAName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.lblMAName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMAName.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.lblMAName.ForeColor = System.Drawing.Color.White;
            this.lblMAName.Location = new System.Drawing.Point(0, 0);
            this.lblMAName.Name = "lblMAName";
            this.lblMAName.Size = new System.Drawing.Size(225, 100);
            this.lblMAName.TabIndex = 0;
            this.lblMAName.Tag = "MANameLabel";
            this.lblMAName.Text = "Name";
            this.lblMAName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelForm
            // 
            this.panelForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(49)))), ((int)(((byte)(56)))));
            this.panelForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelForm.Location = new System.Drawing.Point(225, 24);
            this.panelForm.Name = "panelForm";
            this.panelForm.Size = new System.Drawing.Size(839, 657);
            this.panelForm.TabIndex = 1;
            this.panelForm.Tag = "";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generateAPasswordToolStripMenuItem,
            this.quitToolStripMenuItem});
            this.fileToolStripMenuItem.ForeColor = System.Drawing.Color.Gainsboro;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // generateAPasswordToolStripMenuItem
            // 
            this.generateAPasswordToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control;
            this.generateAPasswordToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
            this.generateAPasswordToolStripMenuItem.Name = "generateAPasswordToolStripMenuItem";
            this.generateAPasswordToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.generateAPasswordToolStripMenuItem.Text = "Generate a password";
            this.generateAPasswordToolStripMenuItem.Click += new System.EventHandler(this.generateAPasswordToolStripMenuItem_Click);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control;
            this.quitToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.quitToolStripMenuItem.Text = "Exit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.windowToolStripMenuItem,
            this.themeToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1064, 24);
            this.menuStrip.TabIndex = 2;
            this.menuStrip.Text = "menuStrip1";
            // 
            // windowToolStripMenuItem
            // 
            this.windowToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hideToTrayToolStripMenuItem,
            this.alwaysOnTopToolStripMenuItem});
            this.windowToolStripMenuItem.ForeColor = System.Drawing.Color.Gainsboro;
            this.windowToolStripMenuItem.Name = "windowToolStripMenuItem";
            this.windowToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
            this.windowToolStripMenuItem.Text = "Window";
            // 
            // hideToTrayToolStripMenuItem
            // 
            this.hideToTrayToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control;
            this.hideToTrayToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.onCloseToolStripMenuItem,
            this.onMinimizeToolStripMenuItem,
            this.onStartToolStripMenuItem});
            this.hideToTrayToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
            this.hideToTrayToolStripMenuItem.Name = "hideToTrayToolStripMenuItem";
            this.hideToTrayToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.hideToTrayToolStripMenuItem.Text = "Hide to tray";
            // 
            // onCloseToolStripMenuItem
            // 
            this.onCloseToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control;
            this.onCloseToolStripMenuItem.CheckOnClick = true;
            this.onCloseToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
            this.onCloseToolStripMenuItem.Name = "onCloseToolStripMenuItem";
            this.onCloseToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.onCloseToolStripMenuItem.Text = "On close";
            this.onCloseToolStripMenuItem.CheckedChanged += new System.EventHandler(this.onCloseToolStripMenuItem_CheckedChanged);
            // 
            // onMinimizeToolStripMenuItem
            // 
            this.onMinimizeToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control;
            this.onMinimizeToolStripMenuItem.CheckOnClick = true;
            this.onMinimizeToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
            this.onMinimizeToolStripMenuItem.Name = "onMinimizeToolStripMenuItem";
            this.onMinimizeToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.onMinimizeToolStripMenuItem.Text = "On minimize";
            this.onMinimizeToolStripMenuItem.CheckedChanged += new System.EventHandler(this.onMinimizeToolStripMenuItem_CheckedChanged);
            // 
            // onStartToolStripMenuItem
            // 
            this.onStartToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control;
            this.onStartToolStripMenuItem.CheckOnClick = true;
            this.onStartToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
            this.onStartToolStripMenuItem.Name = "onStartToolStripMenuItem";
            this.onStartToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.onStartToolStripMenuItem.Text = "On start";
            this.onStartToolStripMenuItem.CheckedChanged += new System.EventHandler(this.onStartToolStripMenuItem_CheckedChanged);
            // 
            // alwaysOnTopToolStripMenuItem
            // 
            this.alwaysOnTopToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control;
            this.alwaysOnTopToolStripMenuItem.CheckOnClick = true;
            this.alwaysOnTopToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
            this.alwaysOnTopToolStripMenuItem.Name = "alwaysOnTopToolStripMenuItem";
            this.alwaysOnTopToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.alwaysOnTopToolStripMenuItem.Text = "Always on top";
            this.alwaysOnTopToolStripMenuItem.CheckedChanged += new System.EventHandler(this.alwaysOnTopToolStripMenuItem_CheckedChanged);
            // 
            // themeToolStripMenuItem
            // 
            this.themeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmbTheme});
            this.themeToolStripMenuItem.ForeColor = System.Drawing.Color.Gainsboro;
            this.themeToolStripMenuItem.Name = "themeToolStripMenuItem";
            this.themeToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.themeToolStripMenuItem.Text = "Theme";
            // 
            // cmbTheme
            // 
            this.cmbTheme.Name = "cmbTheme";
            this.cmbTheme.Size = new System.Drawing.Size(121, 23);
            this.cmbTheme.SelectedIndexChanged += new System.EventHandler(this.cmbTheme_SelectedIndexChanged);
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipText = "Test";
            this.notifyIcon.Text = "notifyIcon1";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // FormHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 681);
            this.Controls.Add(this.panelForm);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.menuStrip);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.MainMenuStrip = this.menuStrip;
            this.MinimumSize = new System.Drawing.Size(1080, 720);
            this.Name = "FormHome";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "vIDsafe";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormHome_FormClosing);
            this.Resize += new System.EventHandler(this.FormHome_Resize);
            this.pnlMain.ResumeLayout(false);
            this.pnlNavigation.ResumeLayout(false);
            this.pnlDataSubMenu.ResumeLayout(false);
            this.pnlPMSubMenu.ResumeLayout(false);
            this.pnlProgressBack.ResumeLayout(false);
            this.pnlMasterName.ResumeLayout(false);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlNavigation;
        private System.Windows.Forms.Panel pnlProgressBack;
        private System.Windows.Forms.Panel pnlProgressBar;
        private System.Windows.Forms.Panel pnlMasterName;
        private System.Windows.Forms.Button btnPasswordManager;
        private System.Windows.Forms.Panel pnlPMSubMenu;
        private System.Windows.Forms.Button btnOverview;
        private System.Windows.Forms.Button btnIdentities;
        private System.Windows.Forms.Button btnGeneratePassword;
        private System.Windows.Forms.Button btnVault;
        private System.Windows.Forms.Panel pnlDataSubMenu;
        private System.Windows.Forms.Button btnData;
        private System.Windows.Forms.Button btnMasterAccount;
        private System.Windows.Forms.Button btnImportExport;
        private System.Windows.Forms.Button btnLogOut;
        private System.Windows.Forms.Panel panelForm;
        private System.Windows.Forms.Label lblMAName;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem windowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hideToTrayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem onCloseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem onMinimizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem alwaysOnTopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem themeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem onStartToolStripMenuItem;
        private System.Windows.Forms.Label lblHealthScore;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ToolStripMenuItem generateAPasswordToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox cmbTheme;
    }
}

