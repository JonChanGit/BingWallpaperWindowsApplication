using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BingWallpaperTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            tbFileDirectory.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)+"\\bing";
        }

        private void tbFileDirectory_MouseDown(object sender, MouseEventArgs e)
        {
            fbdSaveImagesFolderLocation.ShowDialog();
            tbFileDirectory.Text = fbdSaveImagesFolderLocation.SelectedPath;
        }

        /// <summary>
        /// 按钮——获取1图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetImage_Click(object sender, EventArgs e)
        {
            GetImage();
        }

        private void GetImage() {
            tip.Text = "";
            if (String.IsNullOrEmpty(tbFileDirectory.Text))
            {
                tip.Text = "请选择文件路径";
                tip.ForeColor = Color.OrangeRed;
                return;
            }
            BingImage image = null;
            try
            {
                image = BingWallpaperService.getURL(Config.SiteType.znCN,1)[0];
            }
            catch (Exception ex)
            {
                tip.Text = "获取地址失败：" + ex.Message;
                tip.ForeColor = Color.Red;
                return;
            }

            try
            {
                string location = BingWallpaperService.saveImage(image, tbFileDirectory.Text);
                if (checkBox1.Checked)
                {
                    setWallpaperApi(location);
                    tip.Text = "壁纸设置成功";
                }
                else
                {
                    tip.Text = "保存成功";
                }
                tip.ForeColor = Color.Black;
            }
            catch (Exception ex)
            {
                tip.Text = "发生错误：" + ex.Message;
                tip.ForeColor = Color.Red;
                return;
            }
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
            tip.Text = "";
            tip.ForeColor = Color.Green;
        }

        /// <summary>
        /// 按钮——批量获取图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            getImage(8);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="index">张数</param>
        private void getImage(int index)
        { 
            List<BingImage> images = BingWallpaperService.getURL(Config.SiteType.znCN,index);

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

        //利用系统的用户接口设置壁纸
        [DllImport("user32.dll", EntryPoint = "SystemParametersInfo")]
        public static extern int SystemParametersInfo(
                int uAction,
                int uParam,
                string lpvParam,
                int fuWinIni
                );
        public static void setWallpaperApi(string strSavePath)
        {
            SystemParametersInfo(20, 1, strSavePath, 1);
        }

        private void cbWindowOpacity_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            if (cb.Checked)
            {
                this.Opacity = 0.8;
            }
            else {
                this.Opacity = 1;
            }
        }
    }
}
