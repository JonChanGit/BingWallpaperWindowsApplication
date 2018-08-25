using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BingWallpaperTest
{
    [Serializable]
    public class BingImage
    {
        [XmlElementAttribute("url")]
        public string Url { get; set; }
        [XmlElementAttribute("startdate")]
        public string StartDate { get; set; }
    }
}
