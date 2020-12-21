namespace vIDsafe
{
    partial class vIDsafe
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
            System.Windows.Forms.Button btnPasswordManager;
            this.panelMain = new System.Windows.Forms.Panel();
            this.panelNavigation = new System.Windows.Forms.Panel();
            this.btnLogOut = new System.Windows.Forms.Button();
            this.panelMiscSubMenu = new System.Windows.Forms.Panel();
            this.btnApplicationSettings = new System.Windows.Forms.Button();
            this.btnMisc = new System.Windows.Forms.Button();
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
            this.panelMaster = new System.Windows.Forms.Panel();
            this.panelForm = new System.Windows.Forms.Panel();
            btnPasswordManager = new System.Windows.Forms.Button();
            this.panelMain.SuspendLayout();
            this.panelNavigation.SuspendLayout();
            this.panelMiscSubMenu.SuspendLayout();
            this.panelDataSubMenu.SuspendLayout();
            this.panelPMSubMenu.SuspendLayout();
            this.panelProgressBack.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnPasswordManager
            // 
            btnPasswordManager.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            btnPasswordManager.Dock = System.Windows.Forms.DockStyle.Top;
            btnPasswordManager.FlatAppearance.BorderSize = 0;
            btnPasswordManager.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnPasswordManager.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnPasswordManager.ForeColor = System.Drawing.Color.White;
            btnPasswordManager.Location = new System.Drawing.Point(0, 0);
            btnPasswordManager.Name = "btnPasswordManager";
            btnPasswordManager.Size = new System.Drawing.Size(225, 45);
            btnPasswordManager.TabIndex = 0;
            btnPasswordManager.Text = "Password manager";
            btnPasswordManager.UseVisualStyleBackColor = false;
            btnPasswordManager.Click += new System.EventHandler(this.BtnPasswordManager_Click);
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.panelMain.Controls.Add(this.panelNavigation);
            this.panelMain.Controls.Add(this.panelProgressBack);
            this.panelMain.Controls.Add(this.panelMaster);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(225, 681);
            this.panelMain.TabIndex = 0;
            // 
            // panelNavigation
            // 
            this.panelNavigation.Controls.Add(this.btnLogOut);
            this.panelNavigation.Controls.Add(this.panelMiscSubMenu);
            this.panelNavigation.Controls.Add(this.btnMisc);
            this.panelNavigation.Controls.Add(this.panelDataSubMenu);
            this.panelNavigation.Controls.Add(this.btnData);
            this.panelNavigation.Controls.Add(this.panelPMSubMenu);
            this.panelNavigation.Controls.Add(btnPasswordManager);
            this.panelNavigation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelNavigation.Location = new System.Drawing.Point(0, 134);
            this.panelNavigation.Name = "panelNavigation";
            this.panelNavigation.Size = new System.Drawing.Size(225, 547);
            this.panelNavigation.TabIndex = 2;
            // 
            // btnLogOut
            // 
            this.btnLogOut.BackColor = System.Drawing.Color.IndianRed;
            this.btnLogOut.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnLogOut.FlatAppearance.BorderSize = 0;
            this.btnLogOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogOut.ForeColor = System.Drawing.Color.White;
            this.btnLogOut.Location = new System.Drawing.Point(0, 507);
            this.btnLogOut.Name = "btnLogOut";
            this.btnLogOut.Size = new System.Drawing.Size(225, 40);
            this.btnLogOut.TabIndex = 4;
            this.btnLogOut.Text = "Logout";
            this.btnLogOut.UseVisualStyleBackColor = false;
            // 
            // panelMiscSubMenu
            // 
            this.panelMiscSubMenu.Controls.Add(this.btnApplicationSettings);
            this.panelMiscSubMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMiscSubMenu.Location = new System.Drawing.Point(0, 375);
            this.panelMiscSubMenu.Name = "panelMiscSubMenu";
            this.panelMiscSubMenu.Size = new System.Drawing.Size(225, 40);
            this.panelMiscSubMenu.TabIndex = 3;
            // 
            // btnApplicationSettings
            // 
            this.btnApplicationSettings.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnApplicationSettings.FlatAppearance.BorderSize = 0;
            this.btnApplicationSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApplicationSettings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnApplicationSettings.Location = new System.Drawing.Point(0, 0);
            this.btnApplicationSettings.Name = "btnApplicationSettings";
            this.btnApplicationSettings.Size = new System.Drawing.Size(225, 40);
            this.btnApplicationSettings.TabIndex = 0;
            this.btnApplicationSettings.Text = "Application Settings";
            this.btnApplicationSettings.UseVisualStyleBackColor = true;
            this.btnApplicationSettings.Click += new System.EventHandler(this.BtnApplicationSettings_Click);
            // 
            // btnMisc
            // 
            this.btnMisc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnMisc.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnMisc.FlatAppearance.BorderSize = 0;
            this.btnMisc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMisc.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnMisc.ForeColor = System.Drawing.Color.White;
            this.btnMisc.Location = new System.Drawing.Point(0, 330);
            this.btnMisc.Name = "btnMisc";
            this.btnMisc.Size = new System.Drawing.Size(225, 45);
            this.btnMisc.TabIndex = 0;
            this.btnMisc.Text = "Misc";
            this.btnMisc.UseVisualStyleBackColor = false;
            this.btnMisc.Click += new System.EventHandler(this.BtnMisc_Click);
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
            this.btnMasterAccount.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnMasterAccount.FlatAppearance.BorderSize = 0;
            this.btnMasterAccount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMasterAccount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnMasterAccount.Location = new System.Drawing.Point(0, 40);
            this.btnMasterAccount.Name = "btnMasterAccount";
            this.btnMasterAccount.Size = new System.Drawing.Size(225, 40);
            this.btnMasterAccount.TabIndex = 0;
            this.btnMasterAccount.Text = "Manage master account";
            this.btnMasterAccount.UseVisualStyleBackColor = true;
            this.btnMasterAccount.Click += new System.EventHandler(this.BtnMasterAccount_Click);
            // 
            // btnImportExport
            // 
            this.btnImportExport.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnImportExport.FlatAppearance.BorderSize = 0;
            this.btnImportExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImportExport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnImportExport.Location = new System.Drawing.Point(0, 0);
            this.btnImportExport.Name = "btnImportExport";
            this.btnImportExport.Size = new System.Drawing.Size(225, 40);
            this.btnImportExport.TabIndex = 0;
            this.btnImportExport.Text = "Import/Export data";
            this.btnImportExport.UseVisualStyleBackColor = true;
            this.btnImportExport.Click += new System.EventHandler(this.BtnImportExport_Click);
            // 
            // btnData
            // 
            this.btnData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnData.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnData.FlatAppearance.BorderSize = 0;
            this.btnData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnData.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnData.ForeColor = System.Drawing.Color.White;
            this.btnData.Location = new System.Drawing.Point(0, 205);
            this.btnData.Name = "btnData";
            this.btnData.Size = new System.Drawing.Size(225, 45);
            this.btnData.TabIndex = 0;
            this.btnData.Text = "Data";
            this.btnData.UseVisualStyleBackColor = false;
            this.btnData.Click += new System.EventHandler(this.BtnData_Click);
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
            this.btnGeneratePassword.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnGeneratePassword.FlatAppearance.BorderSize = 0;
            this.btnGeneratePassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGeneratePassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnGeneratePassword.Location = new System.Drawing.Point(0, 120);
            this.btnGeneratePassword.Name = "btnGeneratePassword";
            this.btnGeneratePassword.Size = new System.Drawing.Size(225, 40);
            this.btnGeneratePassword.TabIndex = 0;
            this.btnGeneratePassword.Text = "Generate a password";
            this.btnGeneratePassword.UseVisualStyleBackColor = true;
            // 
            // btnVault
            // 
            this.btnVault.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnVault.FlatAppearance.BorderSize = 0;
            this.btnVault.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVault.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnVault.Location = new System.Drawing.Point(0, 80);
            this.btnVault.Name = "btnVault";
            this.btnVault.Size = new System.Drawing.Size(225, 40);
            this.btnVault.TabIndex = 0;
            this.btnVault.Text = "Vault";
            this.btnVault.UseVisualStyleBackColor = true;
            this.btnVault.Click += new System.EventHandler(this.BtnVault_Click);
            // 
            // btnIdentities
            // 
            this.btnIdentities.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnIdentities.FlatAppearance.BorderSize = 0;
            this.btnIdentities.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIdentities.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnIdentities.Location = new System.Drawing.Point(0, 40);
            this.btnIdentities.Name = "btnIdentities";
            this.btnIdentities.Size = new System.Drawing.Size(225, 40);
            this.btnIdentities.TabIndex = 0;
            this.btnIdentities.Text = "Identities";
            this.btnIdentities.UseVisualStyleBackColor = true;
            this.btnIdentities.Click += new System.EventHandler(this.BtnIdentities_Click);
            // 
            // btnOverview
            // 
            this.btnOverview.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnOverview.FlatAppearance.BorderSize = 0;
            this.btnOverview.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOverview.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.btnOverview.Location = new System.Drawing.Point(0, 0);
            this.btnOverview.Name = "btnOverview";
            this.btnOverview.Size = new System.Drawing.Size(225, 40);
            this.btnOverview.TabIndex = 0;
            this.btnOverview.Text = "Overview";
            this.btnOverview.UseVisualStyleBackColor = true;
            this.btnOverview.Click += new System.EventHandler(this.BtnOverview_Click);
            // 
            // panelProgressBack
            // 
            this.panelProgressBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.panelProgressBack.Controls.Add(this.panelProgressBar);
            this.panelProgressBack.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelProgressBack.Location = new System.Drawing.Point(0, 100);
            this.panelProgressBack.Name = "panelProgressBack";
            this.panelProgressBack.Size = new System.Drawing.Size(225, 34);
            this.panelProgressBack.TabIndex = 1;
            // 
            // panelProgressBar
            // 
            this.panelProgressBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(200)))), ((int)(((byte)(0)))));
            this.panelProgressBar.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelProgressBar.Location = new System.Drawing.Point(0, 0);
            this.panelProgressBar.Name = "panelProgressBar";
            this.panelProgressBar.Size = new System.Drawing.Size(154, 34);
            this.panelProgressBar.TabIndex = 2;
            // 
            // panelMaster
            // 
            this.panelMaster.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.panelMaster.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMaster.Location = new System.Drawing.Point(0, 0);
            this.panelMaster.Name = "panelMaster";
            this.panelMaster.Size = new System.Drawing.Size(225, 100);
            this.panelMaster.TabIndex = 0;
            // 
            // panelForm
            // 
            this.panelForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panelForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelForm.Location = new System.Drawing.Point(225, 0);
            this.panelForm.Name = "panelForm";
            this.panelForm.Size = new System.Drawing.Size(839, 681);
            this.panelForm.TabIndex = 1;
            // 
            // vIDsafe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 681);
            this.Controls.Add(this.panelForm);
            this.Controls.Add(this.panelMain);
            this.MinimumSize = new System.Drawing.Size(1080, 720);
            this.Name = "vIDsafe";
            this.Text = "vIDsafe";
            this.panelMain.ResumeLayout(false);
            this.panelNavigation.ResumeLayout(false);
            this.panelMiscSubMenu.ResumeLayout(false);
            this.panelDataSubMenu.ResumeLayout(false);
            this.panelPMSubMenu.ResumeLayout(false);
            this.panelProgressBack.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelNavigation;
        private System.Windows.Forms.Panel panelProgressBack;
        private System.Windows.Forms.Panel panelProgressBar;
        private System.Windows.Forms.Panel panelMaster;
        private System.Windows.Forms.Button btnPasswordManager;
        private System.Windows.Forms.Panel panelPMSubMenu;
        private System.Windows.Forms.Button btnOverview;
        private System.Windows.Forms.Button btnIdentities;
        private System.Windows.Forms.Button btnGeneratePassword;
        private System.Windows.Forms.Button btnVault;
        private System.Windows.Forms.Panel panelDataSubMenu;
        private System.Windows.Forms.Button btnData;
        private System.Windows.Forms.Button btnMisc;
        private System.Windows.Forms.Button btnMasterAccount;
        private System.Windows.Forms.Button btnImportExport;
        private System.Windows.Forms.Panel panelMiscSubMenu;
        private System.Windows.Forms.Button btnApplicationSettings;
        private System.Windows.Forms.Button btnLogOut;
        private System.Windows.Forms.Panel panelForm;
    }
}

