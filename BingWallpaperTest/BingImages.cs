using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace BingWallpaperTest
{

    [Serializable]
    [DataContract]
    [XmlRoot("images")]
    public class BingImages
    {
        [XmlElementAttribute("image")]
        [DataMember]
        public List<BingImage> images { get; set; }
    }
}
