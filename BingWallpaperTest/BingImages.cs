using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BingWallpaperTest
{

    [Serializable]
    [XmlRoot("images")]
    public class BingImages
    {
        [XmlElementAttribute("image")]
        public List<BingImage> images { get; set; }
    }
}
