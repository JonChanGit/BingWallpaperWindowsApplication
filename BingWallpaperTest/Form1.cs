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
            tbFileDirectory.Text = Environment.GetFolderPath(Environment.SpecialFolder.CommonPictures);
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
            BingImage image = null;
            try {
                image = BingWallpaperService.getURL();
            }
            catch (Exception ex) {
                tip.Text = "获取地址失败：" + ex.Message;
                tip.ForeColor = Color.Red;
                return;
            }

            try
            {
                BingWallpaperService.saveImage(image, tbFileDirectory.Text);
            }
            catch (Exception ex)
            {
                tip.Text = "保存图片失败：" + ex.Message;
                tip.ForeColor = Color.Red;
                return;
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

        private void cbUseWatermark_CheckedChanged(object sender, EventArgs e)
        {
            tip.Text = "暂未实现";
            tip.ForeColor = Color.Green;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            tip.Text = "暂未实现";
            tip.ForeColor = Color.Green;
        }

        private void btnGetRecentImage_Click(object sender, EventArgs e)
        {
            getImage();
            tip.Text = "开始获取图片";
        }

        private async void getImage() {
            await Task.Run(() => startGetImage());
        }

        /// <summary>
        /// 开始异步获取图片
        /// </summary>
        private async void startGetImage() {
            for (int x = 0; x < 8; x++) {
                getImage(x);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="index">张数</param>
        private void getImage(int index)
        {
            String str  = Config.WallpaperInfoUrlBuild(index);
            List<BingImage> images = BingWallpaperService.getURL(str);

            foreach(BingImage iamge in images) {
                try
                {
                    BingWallpaperService.saveImage(iamge, tbFileDirectory.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            
        }
    }
}
