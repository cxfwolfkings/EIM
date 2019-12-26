using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
/*******************************
 * Created By franco
 * Description: 
 * Row Entity
 *******************************/

namespace Kingdee.CAPP.Model
{
    [Serializable]
    public class Row
    {
        /// <summary>
        /// 行 id
        /// </summary>
        [XmlAttribute("Id")] 
        public int Id { get; set; }
        /// <summary>
        /// 高度
        /// </summary>
        [XmlAttribute("Height")] 
        public double Height { get; set; }
        /// <summary>
        /// 背景
        /// </summary>
        [XmlAttribute("BackGround")] 
        public string BackGround { get; set; }
        /// <summary>
        /// 边框的颜色
        /// </summary>
        [XmlAttribute("BorderColor")] 
        public string BorderColor { get; set; }
        /// <summary>
        /// 边框人宽度
        /// </summary>
        [XmlAttribute("BorderWidth")] 
        public double BorderWidth { get; set; }
        /// <summary>
        /// 单元格数
        /// </summary>
        [XmlElementAttribute(ElementName ="Cell",IsNullable=false)] 
        public Cell[] Cells { get; set; }
    }
}
