using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using BingWallpaperTest.Utils;
using System.Resources;
using System.Xml.Serialization;

namespace BingWallpaperTest
{
    class BingWallpaperService
    {

        public static BingImage getURL()
        {
            List<BingImage> images= getURL(Config.WallpaperInfoUrlBuild(1));
            return images[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url">WallpaperInfoUrl</param>
        /// <returns></returns>
        public static List<BingImage> getURL(String url) {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
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

            return analyticalXml(xmlDoc).images;

            // 使用正则表达式解析标签（字符串），当然你也可以使用XmlDocument类或XDocument类
            //Regex regex = new Regex("<Url>(?<MyUrl>.*?)</Url>", RegexOptions.IgnoreCase);
            //MatchCollection collection = regex.Matches(xmlDoc);
            // 取得匹配项列表
           // string ImageUrl = Config.UrlPer + collection[0].Groups["MyUrl"].Value;
           // if (true)
           // {
           //     ImageUrl = ImageUrl.Replace("1366x768", "1920x1080");
           // }
            // return ImageUrl;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="image">XML解析的Image对象</param>
        /// <param name="saveImagesFolderLocation">本地保存地址</param>
        public static void saveImage(BingImage image ,string saveImagesFolderLocation) {
            //设置墙纸
            Bitmap bmpWallpaper;
            WebRequest webreq = WebRequest.Create(Config.UrlPer+ image.Url);
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
                //设置文件名为例：bing2017816.jpg
                bmpWallpaper.Save(saveImagesFolderLocation + "\\bing" + image.StartDate + ".jpg", ImageFormat.Jpeg); //图片保存路径为相对路径，保存在程序的目录下
            }
        }

        /// <summary>
        /// 解析XML
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static BingImages analyticalXml(string xml) {
            XmlSerializer xs = new XmlSerializer(typeof (BingImages));
            byte[] array = Encoding.ASCII.GetBytes(xml);
            MemoryStream stream = new MemoryStream(array);  
            StreamReader reader = new StreamReader(stream);
            return xs.Deserialize(reader) as BingImages;
        }
    }
}
