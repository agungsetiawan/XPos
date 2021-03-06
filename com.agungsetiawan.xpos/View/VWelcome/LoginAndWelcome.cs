﻿using com.agungsetiawan.xpos.Common;
using com.agungsetiawan.xpos.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.agungsetiawan.xpos.View.VWelcome
{
    public partial class LoginAndWelcome : Form
    {
        public MainForm ParentForm { get; set; }
        private PenggunaService penggunaService;
        public LoginAndWelcome()
        {
            InitializeComponent();
            penggunaService = new PenggunaService();
            this.ActiveControl = this.textBoxUsername;

            progressBar.Style = ProgressBarStyle.Marquee;
            progressBar.MarqueeAnimationSpeed = 50;
        }

        private void LoginAndWelcome_Paint(object sender, PaintEventArgs e)
        {
            System.Drawing.Rectangle rectUsername = new Rectangle(panelUsername.Location.X, panelUsername.Location.Y,
                                                          panelUsername.ClientSize.Width, panelUsername.ClientSize.Height);

            System.Drawing.Rectangle rectPassword = new Rectangle(panelPassword.Location.X, panelPassword.Location.Y,
                                                         panelPassword.ClientSize.Width, panelPassword.ClientSize.Height);

            rectUsername.Inflate(1, 1); // border thickness
            System.Windows.Forms.ControlPaint.DrawBorder(e.Graphics, rectUsername, Color.FromArgb(146, 202, 249), ButtonBorderStyle.Solid);

            rectPassword.Inflate(1, 1); // border thickness
            System.Windows.Forms.ControlPaint.DrawBorder(e.Graphics, rectPassword, Color.FromArgb(146, 202, 249), ButtonBorderStyle.Solid);

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            progressBar.Visible = true;
            backgroundWorker.RunWorkerAsync();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://agung-setiawan.com");
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var penggunaLogin = penggunaService.FindByUsernamePassword(textBoxUsername.Text, textBoxPassword.Text);
            LoginContext.Pengguna = penggunaLogin;
           
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar.Visible = false;
            var pengguna = LoginContext.Pengguna;

            if (pengguna == null)
            {
                labelWarning.Visible = true;
                return;
            }

            labelPengguna.Text = pengguna.Nama;
            labelRole.Text = pengguna.Role.NamaRole;
            panelWelcome.Visible = true;

            this.ParentForm.GetRights();
            progressBar.Style = ProgressBarStyle.Continuous;
            progressBar.MarqueeAnimationSpeed = 0;
        }
    }
}
