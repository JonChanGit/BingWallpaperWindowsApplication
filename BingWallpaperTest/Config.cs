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
        public static String WallpaperInfoUrlTemplate = "http://cn.bing.com/HPImageArchive.aspx?idx={0:D1}&n=1";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index">需要获取的张数，只能获取到还是有效期内的张数</param>
        /// <returns></returns>
        public static string WallpaperInfoUrlBuild(int index) {
            return string.Format(WallpaperInfoUrlTemplate, index - 1);
        }
    }
}
