using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Kingdee.CAPP.Model
{
    /// <summary>
    /// 类型说明：明细框列属性
    /// </summary>
    [Serializable]
    public class DetailCell
    {
        /// <summary>
        /// 列名
        /// </summary>
        [XmlAttribute("Name")]
        public string Name { get; set; }
        /// <summary>
        /// 标识
        /// </summary>
        [XmlAttribute("Tag")]
        public string Tag { get; set; }
        /// <summary>
        /// 工序内显示明细横隔线
        /// </summary>
        [XmlAttribute("ProcessDetailLine")]
        public bool ProcessDetailLine { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        [XmlAttribute("SerialNumber")]
        public int SerialNumber { get; set; }
        /// <summary>
        /// 总行数
        /// </summary>
        [XmlAttribute("Rows")]
        public int Rows { get; set; }
        /// <summary>
        /// 明细横隔线
        /// </summary>
        [XmlAttribute("DetailLine")]
        public bool DetailLine { get; set; }
        /// <summary>
        /// 显示内容
        /// </summary>
        [XmlAttribute("Content")]
        public string Content { get; set; }
        /// <summary>
        /// 来源
        /// </summary>
        [XmlAttribute("Source")]
        public string Source { get; set; }
        /// <summary>
        /// 栏数
        /// </summary>
        [XmlAttribute("Lans")]
        public int Lans { get; set; }
        /// <summary>
        /// 显示样式
        /// </summary>
        [XmlAttribute("Type")]
        public string Type { get; set; }
        /// <summary>
        /// 每工序行
        /// </summary>
        [XmlAttribute("PerProcessRow")]
        public int PerProcessRow { get; set; }
        /// <summary>
        /// 长度
        /// </summary>
        [XmlAttribute("Length")]
        public int Length { get; set; }
        /// <summary>
        /// 间隔行数
        /// </summary>
        [XmlAttribute("SpaceRows")]
        public int SpaceRows { get; set; }
        /// <summary>
        /// 列头
        /// </summary>
        [XmlAttribute("HeaderText")]
        public string HeaderText { get; set; }
        /// <summary>
        /// 列宽
        /// </summary>
        [XmlAttribute("ColumnWidth")]
        public int ColumnWidth { get; set; }
        /// <summary>
        /// 列值(以逗号分隔)
        /// </summary>
        [XmlAttribute("ColumnValue")]
        public string ColumnValue { get; set; }
        /// <summary>
        /// 高级属性
        /// </summary>
        [XmlAttribute("AdvanceProperty")]
        public string AdvanceProperty { get; set; }
        /// <summary>
        /// 步长
        /// </summary>
        [XmlAttribute("SerialStep")]
        public int SerialStep { get; set; }

        /// <summary>
        /// 明细单元格样式
        /// </summary>
        [XmlElementAttribute(ElementName = "CustomerCellStyles", IsNullable = false)]
        public CustomerCellStyle[] CustomerCellStyles { get; set; }
    }
}
