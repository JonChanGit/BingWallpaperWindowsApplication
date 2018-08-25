using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BingWallpaperTest
{
    [Serializable]
    [DataContract]
    public class BingImage
    {
        [XmlElementAttribute("url")]
        [DataMember(Name = "url")]
        public string Url { get; set; }
        [XmlElementAttribute("startdate")]
        [DataMember(Name = "startdate")]
        public string StartDate { get; set; }
        [XmlElementAttribute("title")]
        [DataMember(Name = "title")]
        public string Title { get; set; }
    }
}
