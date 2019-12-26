using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
/*******************************
 * Created By franco
 * Description: Card Module Content
 *******************************/

namespace Kingdee.CAPP.Model
{
    [Serializable]
    [XmlRoot("CardModuleXML")]
    public class CardModuleXML 
    {
        /// <summary>
        /// 卡片的ID
        /// </summary>
        [XmlAttribute("Id")] 
        public int Id { get; set; }
        /// <summary>
        /// 卡片的名称
        /// </summary>
        [XmlAttribute("Name")] 
        public string Name { get; set; }
        /// <summary>
        /// 卡片的宽度
        /// </summary>
        [XmlAttribute("Width")] 
        public double Width { get; set; }
        /// <summary>
        /// 卡片的高度
        /// </summary>
        [XmlAttribute("Height")] 
        public double Height { get; set; }
        /// <summary>
        /// 卡片的背景色
        /// </summary>
        [XmlAttribute("BackGround")] 
        public string BackGround { get; set; }
        /// <summary>
        /// 边框的颜色
        /// </summary>
        [XmlAttribute("BorderColor")] 
        public string BorderColor { get; set; }
        /// <summary>
        /// 边框的宽度
        /// </summary>
        [XmlAttribute("BorderWidth")] 
        public double BorderWidth { get; set; }
        /// <summary>
        /// 卡片的幅面
        /// </summary>
        [XmlAttribute("CardRange")] 
        public string CardRange { get; set; }
        /// <summary>
        /// 卡片方向
        /// </summary>
        [XmlAttribute("CardDirection")] 
        public string CardDirection { get; set; }
        /// <summary>
        /// 左边距
        /// </summary>
        [XmlAttribute("MarginLeft")] 
        public string MarginLeft { get; set; }
        /// <summary>
        /// 顶边距
        /// </summary>
        [XmlAttribute("MarginTop")] 
        public string MarginTop { get; set; }
        /// <summary>
        /// 右边距
        /// </summary>
        [XmlAttribute("MarginRight")] 
        public string MarginRight { get; set; }
        /// <summary>
        /// 底边距
        /// </summary>
        [XmlAttribute("MarginBottom")] 
        public string MarginBottom { get; set; }
        /// <summary>
        /// 打印比例
        /// </summary>
        [XmlAttribute("PrintScale")] 
        public int PrintScale { get; set; }
        /// <summary>
        /// 打印向上偏移
        /// </summary>
        [XmlAttribute("PrintAboveOffset")] 
        public int PrintAboveOffset { get; set; }
        /// <summary>
        /// 打印向下偏移
        /// </summary>
        [XmlAttribute("PrintUnderOffset")] 
        public int PrintUnderOffset { get; set; }

        /// <summary>
        ///  图片路径
        /// </summary>
        [XmlElementAttribute(ElementName = "ImageObject", IsNullable = false)]
        public ImageObject[] ImageObjects { get; set; }

        /// <summary>
        /// 卡面的行数
        /// </summary>
        [XmlElementAttribute(ElementName = "Row", IsNullable = false)]
        public Row[] Rows { get; set; }
    }
}
