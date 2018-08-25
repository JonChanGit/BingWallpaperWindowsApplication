using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BingWallpaperTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void tbFileDirectory_MouseDown(object sender, MouseEventArgs e)
        {
            fbdSaveImagesFolderLocation.ShowDialog();
            tbFileDirectory.Text = fbdSaveImagesFolderLocation.SelectedPath;
        }

        private void btnGetImage_Click(object sender, EventArgs e)
        {
            tip.Text = "";
            if (String.IsNullOrEmpty(tbFileDirectory.Text))
            {
                tip.Text = "请选择文件路径";
                tip.ForeColor = Color.OrangeRed;
                return;
            }
            string url = null;
            try {
                url = BingWallpaperService.getURL();
            }
            catch (Exception ex) {
                tip.Text = "获取地址失败：" + ex.Message;
                tip.ForeColor = Color.Red;
                return;
            }

            if (!String.IsNullOrEmpty(url)) {
                try
                {
                    BingWallpaperService.saveImage(url, tbFileDirectory.Text);
                }
                catch (Exception ex) {
                    tip.Text = "保存图片失败："+ex.Message;
                    tip.ForeColor = Color.Red;
                    return;
                }
            }
            tip.Text = "保存成功";
            tip.ForeColor = Color.Black;
        }

        private void openPath_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (String.IsNullOrEmpty(tbFileDirectory.Text))
            {
                return;
            }
            Process p = new Process();
            p.StartInfo.FileName = "explorer.exe";
            p.StartInfo.Arguments = @" /select, "+ tbFileDirectory.Text;
            p.Start();
        }
    }
}
