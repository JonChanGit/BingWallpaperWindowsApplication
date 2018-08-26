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

        /// <summary>
        /// 获取图片真实URL
        /// </summary>
        /// <param name="type">站点类型</param>
        /// <param name="imageSize">图片数量</param>
        /// <returns></returns>
        public static List<BingImage> getURL(Config.SiteType type,int imageSize)
        {
            List<BingImage> images =null;
            switch (type)
            {
                case Config.SiteType.International:
                    images = getURL(Config.WallpaperInfoUrlInternationalBuild(imageSize));
                    break;
                case Config.SiteType.znCN:
                    images = getURL(Config.WallpaperInfoUrlBuild(imageSize));
                    break;
                default:
                    images = new List<BingImage>(1);
                    break;
            }
             
            return images;
        }

        /// <summary>
        /// 获取图片真实URL
        /// </summary>
        /// <param name="url">WallpaperInfoUrl</param>
        /// <returns></returns>
        private static List<BingImage> getURL(String url) {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET"; request.ContentType = "application/json;charset=UTF-8";

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
        public static string saveImage(BingImage image ,string saveImagesFolderLocation,Boolean useWaterImage) {
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
                //设置文件名为例：bing_2017816_title.jpg
                location = saveImagesFolderLocation + "\\bing_" + image.StartDate +"_"+ image.Title + ".jpg";
                bmpWallpaper.Save(location, ImageFormat.Jpeg);
            }
            if (useWaterImage)
            {
                MyWaterImageService.Process(location, ImagePosition.RigthBottom, saveImagesFolderLocation);
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
