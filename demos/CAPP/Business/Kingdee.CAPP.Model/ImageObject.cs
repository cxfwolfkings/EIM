using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Kingdee.CAPP.Model
{
    [Serializable]
    public class ImageObject
    {
        /// <summary>
        /// 图片路径
        /// </summary>
        [XmlAttribute("ImagePath")]
        public string ImagePath { get; set; }

        /// <summary>
        /// 图片宽度
        /// </summary>
        [XmlAttribute("Width")]
        public int Width { get; set; }

        /// <summary>
        /// 图片高度
        /// </summary>
        [XmlAttribute("Height")]
        public int Height { get; set; }
        
        /// <summary>
        /// X坐标
        /// </summary>
        [XmlAttribute("LocationX")]
        public int LocationX { get; set; }

        /// <summary>
        /// Y坐标
        /// </summary>
        [XmlAttribute("LocationY")]
        public int LocationY { get; set; }
    }
}
