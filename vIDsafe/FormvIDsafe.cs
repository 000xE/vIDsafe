﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace vIDsafe
{
    public partial class FormvIDsafe : Form
    {
        private static NotifyIcon _notifyIcon;
        public MasterAccount User;
        public static FormvIDsafe Main;

        private static Panel _pnlChildForm;

        public FormvIDsafe()
        {
            InitializeComponent();
            GetFormComponents();
            LoadFormComponents();
        }

        private void GetFormComponents()
        {
            _notifyIcon = notifyIcon;
            _pnlChildForm = panelForm;

            Main = this;
        }

        private void LoadFormComponents()
        {
            OpenChildForm(new FormLogin());
        }

        public static void ShowNotification(ToolTipIcon icon, string title, string text)
        {
            _notifyIcon.Visible = true;
            _notifyIcon.Icon = SystemIcons.Application;

            _notifyIcon.ShowBalloonTip(1000, title, text, icon);
        }

        private void notifyIcon_BalloonTipClosed(object sender, EventArgs e)
        {
            _notifyIcon.Visible = false;
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            _notifyIcon.Visible = false;
        }

        //https://stackoverflow.com/a/28811266
        public static void OpenChildForm(Form frmChildForm)
        {
            while (_pnlChildForm.Controls.Count > 0)
            {
                _pnlChildForm.Controls[0].Dispose();
            }

            frmChildForm.TopLevel = false;
            frmChildForm.FormBorderStyle = FormBorderStyle.None;
            frmChildForm.Dock = DockStyle.Fill;
            _pnlChildForm.Enabled = true;
            _pnlChildForm.Controls.Add(frmChildForm);
            _pnlChildForm.Tag = frmChildForm;
            frmChildForm.BringToFront();
            frmChildForm.Show();
        }
    }
}
