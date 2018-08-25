using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingWallpaperTest.Utils
{
    /// <summary>
    /// 图片位置
    /// </summary>
    public enum ImagePosition
    {
        LeftTop,    //左上
        LeftBottom,  //左下
        RightTop,    //右上
        RigthBottom, //右下
        TopMiddle,   //顶部居中
        BottomMiddle, //底部居中
        Center      //中心
    }

    /// <summary>
    /// 字体集
    /// </summary>
    public enum FontFamilys
    {
        CUSTOM,
        Arial,
        Batang,
        BatangChe,
        Calibri,
        Cambria,
        Candara,
        Consolas,
        Ebrima,
        Footlight_MT_Light,
        Kalinga,
        Kokila,
        Mangal,
        Symbol,
        Times_New_Roman,
        Webdings,
        仿宋,
        华文中宋,
        华文仿宋,
        华文宋体,
        华文彩云,
        华文新魏,
        华文楷体,
        华文琥珀,
        华文细黑,
        华文行楷,
        华文隶书,
        宋体,
        幼圆,
        微软雅黑,
        新宋体,
        方正姚体,
        方正舒体,
        楷体,
        隶书,
        黑体
    }

    /// <summary>
    /// 水印图片的操作管理
    /// </summary>
    public class WaterImageManager
    {
        private int padding = 0;//内容间隔
        private string targetPicName = "_mixture_pic";//默认生成图片的文件名字
        private string targetPicPath = "";//默认生成图片的目录
        private ImageFormat picFormat = ImageFormat.Png;//默认生成图片的格式

        public int Padding { get; set; }
        public string TargetPicName { get; set; }
        public string TargetPicPath { get; set; }
        public ImageFormat PicFormat { get; set; }

        /// <summary>
        /// 生成一个新的水印图片制作实例(默认)
        /// </summary>
        public WaterImageManager()
        {
            Padding = padding;
            TargetPicName = targetPicName;
            TargetPicPath = targetPicPath;
            PicFormat = picFormat;
        }

        /// <summary>
        /// 生成一个新的水印图片制作实例(有参)
        /// </summary>
        /// <param name="tragetPicName">生成合成图片的文件名称</param>
        /// <param name="tragetPicPath">生成合成图片的文件路径</param>
        /// <param name="padding">指定水印距离父容器边距</param>
        /// <param name="picFormat">指定生成合成图片的图片格式</param>
        public WaterImageManager(string targetPicName, string targetPicPath, int padding, ImageFormat picFormat)
        {
            this.Padding = padding;
            this.TargetPicName = targetPicName;
            this.TargetPicPath = targetPicPath.EndsWith(@"\") ? targetPicPath : targetPicPath + @"\";
            this.PicFormat = picFormat;
        }

        /// <summary>
        /// 合成图片
        /// </summary>
        /// <param name="sourcePictureName">源文件名(包括后缀)</param>
        /// <param name="sourcePicturePath">源文件路径</param>
        /// <param name="waterPictureName">水印文件名(包括后缀)</param>
        /// <param name="waterPicturePath">水印文件路径</param>
        /// <param name="alpha">透明度(0.1-1.0数值越小透明度越高)</param>
        /// <param name="position">位置</param>
        /// <returns>合成图片的完整路径</returns>
        public string DrawImage(string sourcePictureName,
                         string sourcePicturePath,
                         string waterPictureName,
                         string waterPicturePath,
                         float alpha,
                         ImagePosition position)
        {
            //
            // 判断参数是否有效
            //
            if (sourcePictureName == string.Empty || waterPictureName == string.Empty || alpha == 0.0 || sourcePicturePath == string.Empty || waterPicturePath == string.Empty)
            {
                return sourcePicturePath + sourcePictureName + "." + PicFormat.ToString().ToLower();
            }

            if (!sourcePicturePath.EndsWith(@"\"))
                sourcePicturePath = sourcePicturePath + @"\";
            if (!waterPicturePath.EndsWith(@"\"))
                waterPicturePath = waterPicturePath + @"\";

            //
            // 源图片，水印图片全路径
            //
            string _sourcePictureName = sourcePicturePath + sourcePictureName;
            string _waterPictureName = waterPicturePath + waterPictureName;

            if (!File.Exists(_sourcePictureName))
                throw new FileNotFoundException(_sourcePictureName + " file not found!");

            if (!File.Exists(_waterPictureName))
                throw new FileNotFoundException(_waterPictureName + " file not found!");

            string fileSourceExtension = System.IO.Path.GetExtension(_sourcePictureName).ToLower();
            string fileWaterExtension = System.IO.Path.GetExtension(_waterPictureName).ToLower();
            //
            // 判断文件是否存在,以及类型是否正确
            //
            if (System.IO.File.Exists(_sourcePictureName) == false ||
              System.IO.File.Exists(_waterPictureName) == false || (
              fileSourceExtension != ".gif" &&
              fileSourceExtension != ".jpg" &&
              fileSourceExtension != ".png") || (
              fileWaterExtension != ".gif" &&
              fileWaterExtension != ".jpg" &&
              fileWaterExtension != ".png")
              )
            {
                return sourcePicturePath + sourcePictureName + "." + PicFormat.ToString().ToLower();
            }

            //

            // 目标图片名称及全路径
            //
            TargetPicPath = TargetPicPath.EndsWith(@"\") ? TargetPicPath : TargetPicPath + @"\";
            string targetImage = TargetPicPath == string.Empty ?
                _sourcePictureName.Replace(System.IO.Path.GetExtension(_sourcePictureName), "") + TargetPicName + "." + PicFormat.ToString().ToLower() :
                TargetPicPath + TargetPicName + "." + PicFormat.ToString().ToLower();

            //
            // 将需要加上水印的图片装载到Image对象中
            //
            Image imgPhoto = Image.FromFile(_sourcePictureName);

            //
            // 确定其长宽
            //
            int phWidth = imgPhoto.Width;
            int phHeight = imgPhoto.Height;

            //
            // 封装 GDI+ 位图，此位图由图形图像及其属性的像素数据组成。
            //
            Bitmap bmPhoto = new Bitmap(phWidth, phHeight, imgPhoto.PixelFormat);//phWidth, phHeight, PixelFormat.Format24bppRgb  imgPhoto

            //
            // 设定分辨率
            // 
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            //
            // 定义一个绘图画面用来装载位图
            //
            Graphics grPhoto = Graphics.FromImage(bmPhoto);

            //
            //同样，由于水印是图片，我们也需要定义一个Image来装载它
            //
            Image imgWatermark = new Bitmap(_waterPictureName);

            //
            // 获取水印图片的高度和宽度
            //
            int wmWidth = imgWatermark.Width;
            int wmHeight = imgWatermark.Height;

            //SmoothingMode：指定是否将平滑处理（消除锯齿）应用于直线、曲线和已填充区域的边缘。
            // 成员名称  说明 
            // AntiAlias   指定消除锯齿的呈现。 
            // Default    指定不消除锯齿。

            // HighQuality 指定高质量、低速度呈现。 
            // HighSpeed  指定高速度、低质量呈现。 
            // Invalid    指定一个无效模式。 
            // None     指定不消除锯齿。 
            grPhoto.SmoothingMode = SmoothingMode.AntiAlias;

            //
            // 第一次描绘，将我们的底图描绘在绘图画面上
            //
            grPhoto.DrawImage(imgPhoto,
                          new Rectangle(0, 0, phWidth, phHeight),
                          0,
                          0,
                          phWidth,
                          phHeight,
                          GraphicsUnit.Pixel);

            //
            // 与底图一样，我们需要一个位图来装载水印图片。并设定其分辨率
            //
            Bitmap bmWatermark = new Bitmap(bmPhoto);
            bmWatermark.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            //
            // 继续，将水印图片装载到一个绘图画面grWatermark
            //
            Graphics grWatermark = Graphics.FromImage(bmWatermark);

            //
            //ImageAttributes 对象包含有关在呈现时如何操作位图和图元文件颜色的信息。
            //   

            ImageAttributes imageAttributes = new ImageAttributes();

            //
            //Colormap: 定义转换颜色的映射
            //
            ColorMap colorMap = new ColorMap();

            //
            //我的水印图被定义成拥有绿色背景色的图片被替换成透明
            //
            colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
            colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);

            ColorMap[] remapTable = { colorMap };

            imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);

            float[][] colorMatrixElements = {
                new float[] {1.0f, 0.0f, 0.0f, 0.0f, 0.0f}, //red红色
                new float[] {0.0f, 1.0f, 0.0f, 0.0f, 0.0f}, //green绿色
                new float[] {0.0f, 0.0f, 1.0f, 0.0f, 0.0f}, //blue蓝色    
                new float[] {0.0f, 0.0f, 0.0f, alpha, 0.0f},//透明度   
                new float[] {0.0f, 0.0f, 0.0f, 0.0f, 1.0f}};//

            // ColorMatrix:定义包含 RGBA 空间坐标的 5 x 5 矩阵。
            // ImageAttributes 类的若干方法通过使用颜色矩阵调整图像颜色。
            ColorMatrix wmColorMatrix = new ColorMatrix(colorMatrixElements);

            imageAttributes.SetColorMatrix(wmColorMatrix, ColorMatrixFlag.Default,
            ColorAdjustType.Bitmap);

            //
            //上面设置完颜色，下面开始设置位置
            //
            int xPosOfWm;
            int yPosOfWm;

            switch (position)
            {
                case ImagePosition.BottomMiddle:
                    xPosOfWm = (phWidth - wmWidth) / 2;
                    yPosOfWm = phHeight - wmHeight - Padding;
                    break;
                case ImagePosition.Center:
                    xPosOfWm = (phWidth - wmWidth) / 2;
                    yPosOfWm = (phHeight - wmHeight) / 2;
                    break;
                case ImagePosition.LeftBottom:
                    xPosOfWm = Padding;
                    yPosOfWm = phHeight - wmHeight - Padding;
                    break;
                case ImagePosition.LeftTop:
                    xPosOfWm = Padding;
                    yPosOfWm = Padding;
                    break;
                case ImagePosition.RightTop:
                    xPosOfWm = phWidth - wmWidth - Padding;
                    yPosOfWm = Padding;
                    break;
                case ImagePosition.RigthBottom:
                    xPosOfWm = phWidth - wmWidth - Padding;
                    yPosOfWm = phHeight - wmHeight - Padding;
                    break;
                case ImagePosition.TopMiddle:
                    xPosOfWm = (phWidth - wmWidth) / 2;
                    yPosOfWm = Padding;
                    break;
                default:
                    xPosOfWm = Padding;
                    yPosOfWm = phHeight - wmHeight - Padding;
                    break;
            }

            imgPhoto.Dispose();//释放底图，解决图片保存时 “GDI+ 中发生一般性错误。”
            // 第二次绘图，把水印印上去
            //
            grWatermark.DrawImage(imgWatermark,
             new Rectangle(xPosOfWm,
                       yPosOfWm,
                       wmWidth,
                       wmHeight),
                       0,
                       0,
                       wmWidth,
                       wmHeight,
                       GraphicsUnit.Pixel,
                       imageAttributes);

            imgPhoto = bmWatermark;
            grPhoto.Dispose();
            grWatermark.Dispose();

            //
            // 保存文件到服务器的文件夹里面
            //
            if (!System.IO.Directory.Exists(TargetPicPath))
                System.IO.Directory.CreateDirectory(TargetPicPath);
            try
            {
                imgPhoto.Save(targetImage, PicFormat);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            imgPhoto.Dispose();
            imgWatermark.Dispose();
            return TargetPicPath + TargetPicName + "." + PicFormat.ToString().ToLower();
        }

        /// <summary>
        /// 在图片上添加水印文字
        /// </summary>
        /// <param name="sourcePicture">源图片文件名(包含后缀)</param>
        /// <param name="waterWords">需要添加到图片上的文字</param>
        /// <param name="alpha">透明度（取值区间(0.0,1.0]）</param>
        /// <param name="position">位置</param>
        /// <param name="PicturePath">文件路径</param>
        /// <returns></returns>
        public string DrawWords(string sourcePictureName,
                         string sourcePicturePath,
                         string waterWords,
                         float alpha,
                         FontFamilys fontFamily,
                         FontStyle style,
                         ImagePosition position)
        {
            //
            // 判断参数是否有效
            //
            if (sourcePictureName == string.Empty || waterWords == string.Empty || alpha == 0.0 || sourcePicturePath == string.Empty)
            {
                return sourcePicturePath + sourcePictureName;
            }

            if (!sourcePicturePath.EndsWith(@"\"))
                sourcePicturePath = sourcePicturePath + @"\";
            //
            // 源图片全路径
            //
            string _sourcePictureName = sourcePicturePath + sourcePictureName;
            if (!File.Exists(_sourcePictureName))
                throw new FileNotFoundException(_sourcePictureName + " file not found!");
            string fileExtension = System.IO.Path.GetExtension(_sourcePictureName).ToLower();

            //
            // 判断文件是否存在,以及文件名是否正确
            //
            if (System.IO.File.Exists(_sourcePictureName) == false || (
              fileExtension != ".gif" &&
              fileExtension != ".jpg" &&
              fileExtension != ".png"))
            {
                return sourcePicturePath + sourcePictureName;
            }

            //
            // 目标图片名称及全路径
            //
            string targetImage = TargetPicPath == string.Empty ?
                _sourcePictureName.Replace(System.IO.Path.GetExtension(_sourcePictureName), "") + TargetPicName + "." + PicFormat.ToString().ToLower() :
                TargetPicPath + TargetPicName + "." + PicFormat.ToString().ToLower();

            //创建一个图片对象用来装载要被添加水印的图片
            Image imgPhoto = Image.FromFile(_sourcePictureName);

            //获取图片的宽和高
            int phWidth = imgPhoto.Width;
            int phHeight = imgPhoto.Height;

            //
            //建立一个bitmap，和我们需要加水印的图片一样大小
            Bitmap bmPhoto = new Bitmap(phWidth, phHeight, imgPhoto.PixelFormat);

            //SetResolution：设置此 Bitmap 的分辨率
            //这里直接将我们需要添加水印的图片的分辨率赋给了bitmap
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            //Graphics：封装一个 GDI+ 绘图图面。
            Graphics grPhoto = Graphics.FromImage(bmPhoto);

            //设置图形的品质
            grPhoto.SmoothingMode = SmoothingMode.AntiAlias;

            //将我们要添加水印的图片按照原始大小描绘（复制）到图形中
            grPhoto.DrawImage(
                imgPhoto,                    //  要添加水印的图片
                new Rectangle(0, 0, phWidth, phHeight), // 根据要添加的水印图片的宽和高
                0,                           // X方向从0点开始描绘
                0,                           // Y方向

                phWidth,                     // X方向描绘长度
                phHeight,                    // Y方向描绘长度
                GraphicsUnit.Pixel);         // 描绘的单位，这里用的是像素

            //根据图片的大小我们来确定添加上去的文字的大小
            //在这里我们定义一个数组来确定
            int[] sizes = new int[] { 16, 14, 12, 10, 8, 6, 4, 2 };

            //字体
            Font crFont = null;
            //矩形的宽度和高度，SizeF有三个属性，分别为Height高，width宽，IsEmpty是否为空
            SizeF crSize = new SizeF();

            //利用一个循环语句来选择我们要添加文字的型号
            //直到它的长度比图片的宽度小
            for (int i = 0; i < 8; i++)
            {
                crFont = new Font(fontFamily.ToString(), sizes[i], style);

                //测量用指定的 Font 对象绘制并用指定的 StringFormat 对象格式化的指定字符串。
                crSize = grPhoto.MeasureString(waterWords, crFont);

                // ushort 关键字表示一种整数数据类型
                if ((ushort)crSize.Width < (ushort)phWidth)
                    break;
            }

            //截边5%的距离，定义文字显示(由于不同的图片显示的高和宽不同，所以按百分比截取)
            int yPixlesFromBottom = (int)(phHeight * .05);

            //定义在图片上文字的位置
            float wmHeight = crSize.Height;
            float wmWidth = crSize.Width;

            float xPosOfWm;
            float yPosOfWm;

            switch (position)
            {
                case ImagePosition.BottomMiddle:
                    xPosOfWm = phWidth / 2;
                    yPosOfWm = phHeight - wmHeight - Padding;
                    break;
                case ImagePosition.Center:
                    xPosOfWm = phWidth / 2;
                    yPosOfWm = phHeight / 2;
                    break;
                case ImagePosition.RigthBottom:
                    xPosOfWm = phWidth / 2 + wmWidth / 2;
                    yPosOfWm = phHeight - wmHeight - Padding;
                    break;
                case ImagePosition.RightTop:
                    xPosOfWm = phWidth / 2 + wmWidth / 2;
                    yPosOfWm = wmHeight / 2 + Padding;
                    break;
                case ImagePosition.LeftTop:
                    xPosOfWm = wmWidth / 2 + Padding;
                    yPosOfWm = wmHeight / 2 + Padding;
                    break;
                case ImagePosition.LeftBottom:
                    xPosOfWm = wmWidth / 2 + Padding;
                    yPosOfWm = phHeight - wmHeight - Padding;
                    break;
                case ImagePosition.TopMiddle:
                    xPosOfWm = phWidth / 2;
                    yPosOfWm = wmHeight / 2 + Padding;
                    break;
                default:
                    xPosOfWm = wmWidth;
                    yPosOfWm = phHeight - wmHeight - Padding;
                    break;
            }

            imgPhoto.Dispose();//释放底图，解决图片保存时 “GDI+ 中发生一般性错误。”

            //封装文本布局信息（如对齐、文字方向和 Tab 停靠位），显示操作（如省略号插入和国家标准 (National) 数字替换）和 OpenType 功能。
            StringFormat StrFormat = new StringFormat();

            //定义需要印的文字居中对齐
            StrFormat.Alignment = StringAlignment.Center;

            //SolidBrush:定义单色画笔。画笔用于填充图形形状，如矩形、椭圆、扇形、多边形和封闭路径。
            //这个画笔为描绘阴影的画笔，呈灰色
            int m_alpha = Convert.ToInt32(255 * alpha);
            SolidBrush semiTransBrush2 = new SolidBrush(Color.FromArgb(m_alpha, 0, 0, 0));

            //描绘文字信息，这个图层向右和向下偏移一个像素，表示阴影效果
            //DrawString 在指定矩形并且用指定的 Brush 和 Font 对象绘制指定的文本字符串。
            grPhoto.DrawString(waterWords,                        //string of text
                          crFont,                                 //font
                          semiTransBrush2,                        //Brush
                          new PointF(xPosOfWm + 1, yPosOfWm + 1), //Position
                          StrFormat);

            //从四个 ARGB 分量（alpha、红色、绿色和蓝色）值创建 Color 结构，这里设置透明度为153
            //这个画笔为描绘正式文字的笔刷，呈白色
            SolidBrush semiTransBrush = new SolidBrush(Color.FromArgb(153, 255, 255, 255));

            //第二次绘制这个图形，建立在第一次描绘的基础上
            grPhoto.DrawString(waterWords,                //string of text
                          crFont,                         //font
                          semiTransBrush,                 //Brush
                          new PointF(xPosOfWm, yPosOfWm), //Position
                          StrFormat);

            //imgPhoto是我们建立的用来装载最终图形的Image对象
            //bmPhoto是我们用来制作图形的容器，为Bitmap对象
            imgPhoto = bmPhoto;
            //释放资源，将定义的Graphics实例grPhoto释放，grPhoto功德圆满
            grPhoto.Dispose();

            //将grPhoto保存
            if (!System.IO.Directory.Exists(TargetPicPath))
                System.IO.Directory.CreateDirectory(TargetPicPath);
            try
            {
                imgPhoto.Save(targetImage, PicFormat);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            imgPhoto.Dispose();

            return TargetPicPath + TargetPicName + "." + PicFormat.ToString().ToLower();
        }

        /// 无损压缩图片    
        /// <param name="sFile">原图片</param>    
        /// <param name="dFile">压缩后保存位置</param>    
        /// <param name="dHeight">高度</param>    
        /// <param name="dWidth">宽度</param>    
        /// <param name="flag">压缩质量(数字越小压缩率越高) 1-100</param>    
        /// <returns></returns>    

        public bool GetPicThumbnail(string sFile, string dFile, int dHeight, int dWidth, int flag)
        {
            System.Drawing.Image iSource = System.Drawing.Image.FromFile(sFile);
            ImageFormat tFormat = iSource.RawFormat;
            int sW = 0, sH = 0;

            //按比例缩放  
            Size tem_size = new Size(iSource.Width, iSource.Height);

            if (tem_size.Width > dHeight || tem_size.Width > dWidth)
            {
                if ((tem_size.Width * dHeight) > (tem_size.Width * dWidth))
                {
                    sW = dWidth;
                    sH = (dWidth * tem_size.Height) / tem_size.Width;
                }
                else
                {
                    sH = dHeight;
                    sW = (tem_size.Width * dHeight) / tem_size.Height;
                }
            }
            else
            {
                sW = tem_size.Width;
                sH = tem_size.Height;
            }

            Bitmap ob = new Bitmap(dWidth, dHeight);
            Graphics g = Graphics.FromImage(ob);

            g.Clear(Color.WhiteSmoke);
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(iSource, new Rectangle((dWidth - sW) / 2, (dHeight - sH) / 2, sW, sH), 0, 0, iSource.Width, iSource.Height, GraphicsUnit.Pixel);

            g.Dispose();
            //以下代码为保存图片时，设置压缩质量    
            EncoderParameters ep = new EncoderParameters();
            long[] qy = new long[1];
            qy[0] = flag;//设置压缩的比例1-100    
            EncoderParameter eParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qy);
            ep.Param[0] = eParam;
            try
            {
                ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo jpegICIinfo = null;
                for (int x = 0; x < arrayICI.Length; x++)
                {
                    if (arrayICI[x].FormatDescription.Equals("JPEG"))
                    {
                        jpegICIinfo = arrayICI[x];
                        break;
                    }
                }
                if (jpegICIinfo != null)
                {
                    ob.Save(dFile, jpegICIinfo, ep);//dFile是压缩后的新路径    
                }
                else
                {
                    ob.Save(dFile, tFormat);
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {
                iSource.Dispose();
                ob.Dispose();
            }
        }
    }

    /// <summary>
    /// 装载水印图片的相关信息
    /// </summary>
    public class WaterImage
    {
        public WaterImage()
        {

        }

        private string m_sourcePicture;
        /// <summary>
        /// 源图片地址名字(带后缀)

        /// </summary>
        public string SourcePicture
        {
            get { return m_sourcePicture; }
            set { m_sourcePicture = value; }
        }

        private string m_waterImager;
        /// <summary>
        /// 水印图片名字(带后缀)
        /// </summary>
        public string WaterPicture
        {
            get { return m_waterImager; }
            set { m_waterImager = value; }
        }

        private float m_alpha;
        /// <summary>
        /// 水印图片文字的透明度
        /// </summary>
        public float Alpha
        {
            get { return m_alpha; }
            set { m_alpha = value; }
        }

        private ImagePosition m_postition;
        /// <summary>
        /// 水印图片或文字在图片中的位置
        /// </summary>
        public ImagePosition Position
        {
            get { return m_postition; }
            set { m_postition = value; }
        }

        private string m_words;
        /// <summary>
        /// 水印文字的内容
        /// </summary>
        public string Words
        {
            get { return m_words; }
            set { m_words = value; }
        }

    }

}
