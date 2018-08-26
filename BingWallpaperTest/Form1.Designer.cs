namespace BingWallpaperTest
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnGetImage = new System.Windows.Forms.Button();
            this.tbFileDirectory = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.fbdSaveImagesFolderLocation = new System.Windows.Forms.FolderBrowserDialog();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tip = new System.Windows.Forms.Label();
            this.openPath = new System.Windows.Forms.LinkLabel();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.cbUseWatermark = new System.Windows.Forms.CheckBox();
            this.btnGetRecentImage = new System.Windows.Forms.Button();
            this.cbInternational = new System.Windows.Forms.CheckBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.label6 = new System.Windows.Forms.Label();
            this.bgw = new System.ComponentModel.BackgroundWorker();
            this.cbAutoExecute = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGetImage
            // 
            this.btnGetImage.Location = new System.Drawing.Point(26, 116);
            this.btnGetImage.Name = "btnGetImage";
            this.btnGetImage.Size = new System.Drawing.Size(87, 23);
            this.btnGetImage.TabIndex = 0;
            this.btnGetImage.Text = "获取最新壁纸";
            this.btnGetImage.UseVisualStyleBackColor = true;
            this.btnGetImage.Click += new System.EventHandler(this.btnGetImage_Click);
            // 
            // tbFileDirectory
            // 
            this.tbFileDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFileDirectory.Location = new System.Drawing.Point(87, 36);
            this.tbFileDirectory.Name = "tbFileDirectory";
            this.tbFileDirectory.Size = new System.Drawing.Size(315, 21);
            this.tbFileDirectory.TabIndex = 1;
            this.tbFileDirectory.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tbFileDirectory_MouseDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "图片目录";
            // 
            // fbdSaveImagesFolderLocation
            // 
            this.fbdSaveImagesFolderLocation.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(121, 75);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(76, 21);
            this.textBox1.TabIndex = 3;
            this.textBox1.Text = "1920";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "分辨率";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(262, 78);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(84, 21);
            this.textBox2.TabIndex = 5;
            this.textBox2.Text = "1080";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(85, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "X:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(226, 81);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "Y:";
            // 
            // tip
            // 
            this.tip.AutoSize = true;
            this.tip.Location = new System.Drawing.Point(26, 9);
            this.tip.Name = "tip";
            this.tip.Size = new System.Drawing.Size(0, 12);
            this.tip.TabIndex = 7;
            // 
            // openPath
            // 
            this.openPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.openPath.AutoSize = true;
            this.openPath.Location = new System.Drawing.Point(422, 39);
            this.openPath.Name = "openPath";
            this.openPath.Size = new System.Drawing.Size(53, 12);
            this.openPath.TabIndex = 8;
            this.openPath.TabStop = true;
            this.openPath.Text = "打开目录";
            this.openPath.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.openPath_LinkClicked);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(26, 158);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(186, 16);
            this.checkBox1.TabIndex = 9;
            this.checkBox1.Text = "获取1张成功后自动设置为壁纸";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // cbUseWatermark
            // 
            this.cbUseWatermark.AutoSize = true;
            this.cbUseWatermark.Location = new System.Drawing.Point(250, 180);
            this.cbUseWatermark.Name = "cbUseWatermark";
            this.cbUseWatermark.Size = new System.Drawing.Size(96, 16);
            this.cbUseWatermark.TabIndex = 10;
            this.cbUseWatermark.Text = "使用Bing水印";
            this.cbUseWatermark.UseVisualStyleBackColor = true;
            this.cbUseWatermark.CheckedChanged += new System.EventHandler(this.cbUseWatermark_CheckedChanged);
            // 
            // btnGetRecentImage
            // 
            this.btnGetRecentImage.Location = new System.Drawing.Point(137, 116);
            this.btnGetRecentImage.Name = "btnGetRecentImage";
            this.btnGetRecentImage.Size = new System.Drawing.Size(106, 23);
            this.btnGetRecentImage.TabIndex = 11;
            this.btnGetRecentImage.Text = "获取最近8张壁纸";
            this.btnGetRecentImage.UseVisualStyleBackColor = true;
            this.btnGetRecentImage.Click += new System.EventHandler(this.btnGetRecentImage_Click);
            // 
            // cbInternational
            // 
            this.cbInternational.AutoSize = true;
            this.cbInternational.Checked = true;
            this.cbInternational.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbInternational.Location = new System.Drawing.Point(26, 180);
            this.cbInternational.Name = "cbInternational";
            this.cbInternational.Size = new System.Drawing.Size(108, 16);
            this.cbInternational.TabIndex = 13;
            this.cbInternational.Text = "获取国际版壁纸";
            this.cbInternational.UseVisualStyleBackColor = true;
            this.cbInternational.CheckedChanged += new System.EventHandler(this.cbInternational_CheckedChanged);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(93, 224);
            this.trackBar1.Maximum = 100;
            this.trackBar1.Minimum = 20;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(104, 45);
            this.trackBar1.TabIndex = 14;
            this.trackBar1.TickFrequency = 10;
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar1.Value = 100;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(26, 224);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 15;
            this.label6.Text = "窗口透明";
            // 
            // bgw
            // 
            this.bgw.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgw_DoWork);
            this.bgw.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgw_RunWorkerCompleted);
            // 
            // cbAutoExecute
            // 
            this.cbAutoExecute.AutoSize = true;
            this.cbAutoExecute.Enabled = false;
            this.cbAutoExecute.Location = new System.Drawing.Point(250, 158);
            this.cbAutoExecute.Name = "cbAutoExecute";
            this.cbAutoExecute.Size = new System.Drawing.Size(120, 16);
            this.cbAutoExecute.TabIndex = 16;
            this.cbAutoExecute.Text = "每日自动获取壁纸";
            this.cbAutoExecute.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 257);
            this.Controls.Add(this.cbAutoExecute);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.cbInternational);
            this.Controls.Add(this.btnGetRecentImage);
            this.Controls.Add(this.cbUseWatermark);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.openPath);
            this.Controls.Add(this.tip);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbFileDirectory);
            this.Controls.Add(this.btnGetImage);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(503, 296);
            this.MinimumSize = new System.Drawing.Size(503, 296);
            this.Name = "Form1";
            this.Text = "获取Bing图片";
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGetImage;
        private System.Windows.Forms.TextBox tbFileDirectory;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FolderBrowserDialog fbdSaveImagesFolderLocation;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label tip;
        private System.Windows.Forms.LinkLabel openPath;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox cbUseWatermark;
        private System.Windows.Forms.Button btnGetRecentImage;
        private System.Windows.Forms.CheckBox cbInternational;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label label6;
        private System.ComponentModel.BackgroundWorker bgw;
        private System.Windows.Forms.CheckBox cbAutoExecute;
    }
}

