using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Kingdee.CAPP.Model
{
    /// <summary>
    /// 类型说明：明细框单元格属性
    /// </summary>
    [Serializable]
    public class CustomerCellStyle
    {
        /// <summary>
        /// 卡片名称
        /// </summary>
        [XmlAttribute("CardName")]
        public string CardName
        {
            get;
            set;
        }

        /// <summary>
        /// 背景颜色
        /// </summary>
        [XmlAttribute("BackColor")]
        public string BackColor
        {
            get;
            set;
        }

        /// <summary>
        /// 单元格类型
        /// </summary>
        [XmlAttribute("CellType")]
        public int CellType
        {
            get;
            set;
        }

        /// <summary>
        /// 单元格内的字体大小
        /// </summary>
        [XmlAttribute("FontSize")]
        public string FontSize { get; set; }

        /// <summary>
        /// 单元格内的字体样式
        /// </summary>
        [XmlAttribute("FontStyle")]
        public string FontStyle { get; set; }

        /// <summary>
        /// 单元格内的字体
        /// </summary>
        [XmlAttribute("FontFamily")]
        public string FontFamily { get; set; }

        /// <summary>
        /// 字体颜色
        /// </summary>
        [XmlAttribute("ForeColor")]
        public string ForeColor
        {
            get;
            set;
        }

        /// <summary>
        /// 明细单元格对齐
        /// </summary>
        [XmlAttribute("RichAlignment")]
        public string RichAlignment
        {
            get;
            set;
        }

        /// <summary>
        /// 非明细单元格对齐
        /// </summary>
        [XmlAttribute("Alignment")]
        public string Alignment
        {
            get;
            set;
        }

        /// <summary>
        /// 边距
        /// </summary>
        [XmlAttribute("Padding")]
        public string Padding
        {
            get;
            set;
        }

        /// <summary>
        /// 是否换行
        /// </summary>
        [XmlAttribute("WrapMode")]
        public bool WrapMode
        {
            get;
            set;
        }

        /// <summary>
        /// 是否新加入的空行 0-否 1-是
        /// </summary>
        [XmlAttribute("EmptyRow")]
        public int EmptyRow
        {
            get;
            set;
        }
    } 
}
