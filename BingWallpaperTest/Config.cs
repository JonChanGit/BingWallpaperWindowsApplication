using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingWallpaperTest
{
    class Config
    {
        
        /**
         * 地址前缀
         * */
        public static String UrlPer = "http://www.bing.com";

        /// <summary>
        /// 必应每日壁纸的接口模板
        /// </summary>
        public static String WallpaperInfoUrlTemplate = "http://cn.bing.com/HPImageArchive.aspx?format=js&idx=1&n={0:D1}";

        /// <summary>
        /// 国际版壁纸
        /// 返回格式Json
        /// </summary>
        public static String WallpaperInfoUrlInternationalTemplate = "http://www.bing.com/HPImageArchive.aspx?format=js&idx=0&n={0:D1}&IID=SERP.1052&ensearch=1";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index">需要获取的张数，只能获取到还是有效期内的张数</param>
        /// <returns></returns>
        public static string WallpaperInfoUrlBuild(int index) {
            return string.Format(WallpaperInfoUrlTemplate, index);
        }


        /// <summary>
        /// 国际版壁纸地址构造
        /// </summary>
        /// <param name="index">需要获取的张数，只能获取到还是有效期内的张数</param>
        /// <returns></returns>
        public static string WallpaperInfoUrlInternationalBuild(int index) {
            return string.Format(WallpaperInfoUrlInternationalTemplate, index);
        }
    }
}
