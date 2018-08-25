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
using System.Runtime.Serialization.Json;

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
            BingImages images = null;
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(xmlDoc))) {
                DataContractJsonSerializer deseralizer = new DataContractJsonSerializer(typeof(BingImages));
                images = deseralizer.ReadObject(ms) as BingImages;

            }
            return images.images;// analyticalXml(xmlDoc).images;
             
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="image">XML解析的Image对象</param>
        /// <param name="saveImagesFolderLocation">本地保存地址</param>
        public static string saveImage(BingImage image ,string saveImagesFolderLocation) {
            string location = "";
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
                location = saveImagesFolderLocation + "\\bing" + image.StartDate + ".jpg";
                bmpWallpaper.Save(location, ImageFormat.Jpeg);
            }
            return location;
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
