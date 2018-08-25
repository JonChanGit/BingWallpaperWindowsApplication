using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BingWallpaperTest
{
    class BingWallpaperService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static String getURL() {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Config.WallpaperInfoUrl);
            request.Method = "GET"; request.ContentType = "text/html;charset=UTF-8";

            string xmlDoc;
            //使用using自动注销HttpWebResponse
            using (HttpWebResponse webResponse = (HttpWebResponse)request.GetResponse())
            {
                Stream stream = webResponse.GetResponseStream();
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    xmlDoc = reader.ReadToEnd();
                }
            }
            // 使用正则表达式解析标签（字符串），当然你也可以使用XmlDocument类或XDocument类
            Regex regex = new Regex("<Url>(?<MyUrl>.*?)</Url>", RegexOptions.IgnoreCase);
            MatchCollection collection = regex.Matches(xmlDoc);
            // 取得匹配项列表
            string ImageUrl = Config.UrlPer + collection[0].Groups["MyUrl"].Value;
            if (true)
            {
                ImageUrl = ImageUrl.Replace("1366x768", "1920x1080");
            }
            return ImageUrl;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url">图片HTTP地址</param>
        /// <param name="saveImagesFolderLocation">本地保存地址</param>
        public static void saveImage(string url ,string saveImagesFolderLocation) {
            //设置墙纸
            Bitmap bmpWallpaper;
            WebRequest webreq = WebRequest.Create(url);
            //Console.WriteLine(getURL());
            //Console.ReadLine();
            WebResponse webres = webreq.GetResponse();
            using (Stream stream = webres.GetResponseStream())
            {
                bmpWallpaper = (Bitmap)Image.FromStream(stream);
                //stream.Close();
                if (!Directory.Exists(saveImagesFolderLocation))
                {
                    Directory.CreateDirectory(saveImagesFolderLocation);
                }
                else {
                    throw new ApplicationException("文件已经存在");
                }
                //设置文件名为例：bing2017816.jpg
                bmpWallpaper.Save(saveImagesFolderLocation + "\\bing" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + ".jpg", ImageFormat.Jpeg); //图片保存路径为相对路径，保存在程序的目录下
            }
        }
    }
}
