using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace updater.ClientUI
{
    public partial class UrlConfigForm : Form
    {
        public UrlConfigForm()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if(this.textBox1.Text.Trim().Length < 1)
            {
                MessageBox.Show("路径不能为空！");
                return;
            }

            if (this.textBox1.Text.Trim() == this.initUrl)
            {
                this.Close();
                return;
            }

            bool bret = VersionChecker.WriteServerPath(configFile, this.textBox1.Text.Trim());
            if (bret)
            {
                //MessageBox.Show("保存成功！\n重启后生效。");

                    updater.VersionCheck.ConfigurationValues.UpdateServerPath = this.textBox1.Text.Trim();

                this.Close();
            }
            else
            {
                MessageBox.Show("保存失败！");
            }

        }

        private string initUrl = string.Empty;
        private string configFile = string.Empty;
        private void UrlConfigForm_Load(object sender, EventArgs e)
        {
            try
            {
                configFile = Application.ExecutablePath  + ".config";
                initUrl = VersionChecker.GetServerPath( configFile );

                this.textBox1.Text = initUrl;
            }
            catch
            {
                this.textBox1.Text = "";
            }         
        }
    }
}