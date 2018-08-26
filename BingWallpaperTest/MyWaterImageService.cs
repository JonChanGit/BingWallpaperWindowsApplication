using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingWallpaperTest
{/// <summary>
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
    class MyWaterImageService
    {
        public static int Padding = 85;
        public static void Process(string imagePath,  ImagePosition position,string targetPicPath) {
            Image imgPhoto = Image.FromFile(imagePath);
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
            Image imgWatermark = Resource1.login_wi;

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
                new float[] {0.0f, 0.0f, 0.0f, .6f, 0.0f},//透明度   
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
            try
            {
                imgPhoto.Save(imagePath, ImageFormat.Jpeg);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            imgPhoto.Dispose();
            imgWatermark.Dispose();
        }
    }
}
