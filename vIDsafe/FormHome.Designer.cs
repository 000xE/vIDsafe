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
            this.btnPasswordManager = new System.Windows.Forms.Button();
            this.panelMain = new System.Windows.Forms.Panel();
            this.panelNavigation = new System.Windows.Forms.Panel();
            this.btnLogOut = new System.Windows.Forms.Button();
            this.panelDataSubMenu = new System.Windows.Forms.Panel();
            this.btnMasterAccount = new System.Windows.Forms.Button();
            this.btnImportExport = new System.Windows.Forms.Button();
            this.btnData = new System.Windows.Forms.Button();
            this.panelPMSubMenu = new System.Windows.Forms.Panel();
            this.btnGeneratePassword = new System.Windows.Forms.Button();
            this.btnVault = new System.Windows.Forms.Button();
            this.btnIdentities = new System.Windows.Forms.Button();
            this.btnOverview = new System.Windows.Forms.Button();
            this.panelProgressBack = new System.Windows.Forms.Panel();
            this.panelProgressBar = new System.Windows.Forms.Panel();
            this.panelMasterName = new System.Windows.Forms.Panel();
            this.lblMAName = new System.Windows.Forms.Label();
            this.panelForm = new System.Windows.Forms.Panel();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lockToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.windowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hideToTrayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.onCloseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.onMinimiseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.onStartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alwaysOnTopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.themeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            this.timeoutsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clipboardTimeoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.vaultTimeoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTextBox2 = new System.Windows.Forms.ToolStripTextBox();
            this.panelMain.SuspendLayout();
            this.panelNavigation.SuspendLayout();
            this.panelDataSubMenu.SuspendLayout();
            this.panelPMSubMenu.SuspendLayout();
            this.panelProgressBack.SuspendLayout();
            this.panelMasterName.SuspendLayout();
            this.menuStrip1.SuspendLayout();
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
            this.btnPasswordManager.Text = "Password manager";
            this.btnPasswordManager.UseVisualStyleBackColor = false;
            this.btnPasswordManager.Click += new System.EventHandler(this.btnPasswordManager_Click);
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.panelMain.Controls.Add(this.panelNavigation);
            this.panelMain.Controls.Add(this.panelProgressBack);
            this.panelMain.Controls.Add(this.panelMasterName);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMain.Location = new System.Drawing.Point(0, 24);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(225, 657);
            this.panelMain.TabIndex = 0;
            // 
            // panelNavigation
            // 
            this.panelNavigation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(32)))), ((int)(((byte)(36)))));
            this.panelNavigation.Controls.Add(this.btnLogOut);
            this.panelNavigation.Controls.Add(this.panelDataSubMenu);
            this.panelNavigation.Controls.Add(this.btnData);
            this.panelNavigation.Controls.Add(this.panelPMSubMenu);
            this.panelNavigation.Controls.Add(this.btnPasswordManager);
            this.panelNavigation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelNavigation.Location = new System.Drawing.Point(0, 135);
            this.panelNavigation.Name = "panelNavigation";
            this.panelNavigation.Size = new System.Drawing.Size(225, 522);
            this.panelNavigation.TabIndex = 2;
            // 
            // btnLogOut
            // 
            this.btnLogOut.BackColor = System.Drawing.Color.IndianRed;
            this.btnLogOut.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnLogOut.FlatAppearance.BorderSize = 0;
            this.btnLogOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogOut.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogOut.ForeColor = System.Drawing.Color.White;
            this.btnLogOut.Location = new System.Drawing.Point(0, 482);
            this.btnLogOut.Name = "btnLogOut";
            this.btnLogOut.Size = new System.Drawing.Size(225, 40);
            this.btnLogOut.TabIndex = 10;
            this.btnLogOut.Text = "Logout";
            this.btnLogOut.UseVisualStyleBackColor = false;
            this.btnLogOut.Click += new System.EventHandler(this.btnLogOut_Click);
            // 
            // panelDataSubMenu
            // 
            this.panelDataSubMenu.Controls.Add(this.btnMasterAccount);
            this.panelDataSubMenu.Controls.Add(this.btnImportExport);
            this.panelDataSubMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelDataSubMenu.Location = new System.Drawing.Point(0, 250);
            this.panelDataSubMenu.Name = "panelDataSubMenu";
            this.panelDataSubMenu.Size = new System.Drawing.Size(225, 80);
            this.panelDataSubMenu.TabIndex = 2;
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
            this.btnMasterAccount.Tag = "navButton";
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
            this.btnImportExport.Tag = "navButton";
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
            this.btnData.Text = "Data";
            this.btnData.UseVisualStyleBackColor = false;
            this.btnData.Click += new System.EventHandler(this.btnData_Click);
            // 
            // panelPMSubMenu
            // 
            this.panelPMSubMenu.Controls.Add(this.btnGeneratePassword);
            this.panelPMSubMenu.Controls.Add(this.btnVault);
            this.panelPMSubMenu.Controls.Add(this.btnIdentities);
            this.panelPMSubMenu.Controls.Add(this.btnOverview);
            this.panelPMSubMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelPMSubMenu.Location = new System.Drawing.Point(0, 45);
            this.panelPMSubMenu.Name = "panelPMSubMenu";
            this.panelPMSubMenu.Size = new System.Drawing.Size(225, 160);
            this.panelPMSubMenu.TabIndex = 1;
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
            this.btnGeneratePassword.Tag = "navButton";
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
            this.btnVault.Tag = "navButton";
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
            this.btnIdentities.Tag = "navButton";
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
            this.btnOverview.Tag = "navButton";
            this.btnOverview.Text = "Overview";
            this.btnOverview.UseVisualStyleBackColor = false;
            this.btnOverview.Click += new System.EventHandler(this.btnOverview_Click);
            // 
            // panelProgressBack
            // 
            this.panelProgressBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.panelProgressBack.Controls.Add(this.panelProgressBar);
            this.panelProgressBack.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelProgressBack.Location = new System.Drawing.Point(0, 100);
            this.panelProgressBack.Name = "panelProgressBack";
            this.panelProgressBack.Size = new System.Drawing.Size(225, 35);
            this.panelProgressBack.TabIndex = 1;
            // 
            // panelProgressBar
            // 
            this.panelProgressBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(200)))), ((int)(((byte)(0)))));
            this.panelProgressBar.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelProgressBar.Location = new System.Drawing.Point(0, 0);
            this.panelProgressBar.Name = "panelProgressBar";
            this.panelProgressBar.Size = new System.Drawing.Size(154, 35);
            this.panelProgressBar.TabIndex = 2;
            // 
            // panelMasterName
            // 
            this.panelMasterName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(50)))));
            this.panelMasterName.Controls.Add(this.lblMAName);
            this.panelMasterName.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMasterName.Location = new System.Drawing.Point(0, 0);
            this.panelMasterName.Name = "panelMasterName";
            this.panelMasterName.Size = new System.Drawing.Size(225, 100);
            this.panelMasterName.TabIndex = 0;
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
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lockToolStripMenuItem});
            this.fileToolStripMenuItem.ForeColor = System.Drawing.Color.Gainsboro;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // lockToolStripMenuItem
            // 
            this.lockToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.lockToolStripMenuItem.ForeColor = System.Drawing.Color.Gainsboro;
            this.lockToolStripMenuItem.Name = "lockToolStripMenuItem";
            this.lockToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
            this.lockToolStripMenuItem.Text = "Lock";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.windowToolStripMenuItem,
            this.settingsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1064, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
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
            this.hideToTrayToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.hideToTrayToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.onCloseToolStripMenuItem,
            this.onMinimiseToolStripMenuItem,
            this.onStartToolStripMenuItem});
            this.hideToTrayToolStripMenuItem.ForeColor = System.Drawing.Color.Gainsboro;
            this.hideToTrayToolStripMenuItem.Name = "hideToTrayToolStripMenuItem";
            this.hideToTrayToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.hideToTrayToolStripMenuItem.Text = "Hide to tray";
            // 
            // onCloseToolStripMenuItem
            // 
            this.onCloseToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.onCloseToolStripMenuItem.CheckOnClick = true;
            this.onCloseToolStripMenuItem.ForeColor = System.Drawing.Color.Gainsboro;
            this.onCloseToolStripMenuItem.Name = "onCloseToolStripMenuItem";
            this.onCloseToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.onCloseToolStripMenuItem.Text = "On close";
            // 
            // onMinimiseToolStripMenuItem
            // 
            this.onMinimiseToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.onMinimiseToolStripMenuItem.CheckOnClick = true;
            this.onMinimiseToolStripMenuItem.ForeColor = System.Drawing.Color.Gainsboro;
            this.onMinimiseToolStripMenuItem.Name = "onMinimiseToolStripMenuItem";
            this.onMinimiseToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.onMinimiseToolStripMenuItem.Text = "On minimise";
            // 
            // onStartToolStripMenuItem
            // 
            this.onStartToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.onStartToolStripMenuItem.ForeColor = System.Drawing.Color.Gainsboro;
            this.onStartToolStripMenuItem.Name = "onStartToolStripMenuItem";
            this.onStartToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.onStartToolStripMenuItem.Text = "On start";
            // 
            // alwaysOnTopToolStripMenuItem
            // 
            this.alwaysOnTopToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.alwaysOnTopToolStripMenuItem.CheckOnClick = true;
            this.alwaysOnTopToolStripMenuItem.ForeColor = System.Drawing.Color.Gainsboro;
            this.alwaysOnTopToolStripMenuItem.Name = "alwaysOnTopToolStripMenuItem";
            this.alwaysOnTopToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.alwaysOnTopToolStripMenuItem.Text = "Always on top";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.themeToolStripMenuItem,
            this.timeoutsToolStripMenuItem});
            this.settingsToolStripMenuItem.ForeColor = System.Drawing.Color.Gainsboro;
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // themeToolStripMenuItem
            // 
            this.themeToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.themeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBox1});
            this.themeToolStripMenuItem.ForeColor = System.Drawing.Color.Gainsboro;
            this.themeToolStripMenuItem.Name = "themeToolStripMenuItem";
            this.themeToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.themeToolStripMenuItem.Text = "Theme";
            // 
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            this.toolStripComboBox1.Size = new System.Drawing.Size(121, 23);
            // 
            // timeoutsToolStripMenuItem
            // 
            this.timeoutsToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.timeoutsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clipboardTimeoutToolStripMenuItem,
            this.vaultTimeoutToolStripMenuItem});
            this.timeoutsToolStripMenuItem.ForeColor = System.Drawing.Color.Gainsboro;
            this.timeoutsToolStripMenuItem.Name = "timeoutsToolStripMenuItem";
            this.timeoutsToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.timeoutsToolStripMenuItem.Text = "Timeouts";
            // 
            // clipboardTimeoutToolStripMenuItem
            // 
            this.clipboardTimeoutToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.clipboardTimeoutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBox1});
            this.clipboardTimeoutToolStripMenuItem.ForeColor = System.Drawing.Color.Gainsboro;
            this.clipboardTimeoutToolStripMenuItem.Name = "clipboardTimeoutToolStripMenuItem";
            this.clipboardTimeoutToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.clipboardTimeoutToolStripMenuItem.Text = "Clipboard timeout";
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(100, 23);
            // 
            // vaultTimeoutToolStripMenuItem
            // 
            this.vaultTimeoutToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.vaultTimeoutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBox2});
            this.vaultTimeoutToolStripMenuItem.ForeColor = System.Drawing.Color.Gainsboro;
            this.vaultTimeoutToolStripMenuItem.Name = "vaultTimeoutToolStripMenuItem";
            this.vaultTimeoutToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.vaultTimeoutToolStripMenuItem.Text = "Vault timeout";
            // 
            // toolStripTextBox2
            // 
            this.toolStripTextBox2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripTextBox2.Name = "toolStripTextBox2";
            this.toolStripTextBox2.Size = new System.Drawing.Size(100, 23);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 681);
            this.Controls.Add(this.panelForm);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(1080, 720);
            this.Name = "Form1";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "vIDsafe";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.panelMain.ResumeLayout(false);
            this.panelNavigation.ResumeLayout(false);
            this.panelDataSubMenu.ResumeLayout(false);
            this.panelPMSubMenu.ResumeLayout(false);
            this.panelProgressBack.ResumeLayout(false);
            this.panelMasterName.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelNavigation;
        private System.Windows.Forms.Panel panelProgressBack;
        private System.Windows.Forms.Panel panelProgressBar;
        private System.Windows.Forms.Panel panelMasterName;
        private System.Windows.Forms.Button btnPasswordManager;
        private System.Windows.Forms.Panel panelPMSubMenu;
        private System.Windows.Forms.Button btnOverview;
        private System.Windows.Forms.Button btnIdentities;
        private System.Windows.Forms.Button btnGeneratePassword;
        private System.Windows.Forms.Button btnVault;
        private System.Windows.Forms.Panel panelDataSubMenu;
        private System.Windows.Forms.Button btnData;
        private System.Windows.Forms.Button btnMasterAccount;
        private System.Windows.Forms.Button btnImportExport;
        private System.Windows.Forms.Button btnLogOut;
        private System.Windows.Forms.Panel panelForm;
        private System.Windows.Forms.Label lblMAName;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem windowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hideToTrayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem onCloseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem onMinimiseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem alwaysOnTopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem themeToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox1;
        private System.Windows.Forms.ToolStripMenuItem timeoutsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clipboardTimeoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripMenuItem vaultTimeoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox2;
        private System.Windows.Forms.ToolStripMenuItem lockToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem onStartToolStripMenuItem;
    }
}

