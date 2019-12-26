using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
/*******************************
 * Created By franco
 * Description: 
 * Cell Entity
 *******************************/

namespace Kingdee.CAPP.Model
{
    [Serializable]
    public class Cell
    {
        /// <summary>
        /// 单元格的ID
        /// </summary>
        [XmlAttribute("Id")] 
        public int Id { get; set; }
        /// <summary>
        /// 单元格的名称
        /// </summary>
        [XmlAttribute("Name")] 
        public string Name { get; set; }
        /// <summary>
        /// 单元格宽
        /// </summary>
        [XmlAttribute("Width")] 
        public double Width {get; set; }
        /// <summary>
        /// 单元格高
        /// </summary>
        [XmlAttribute("Height")]
        public double Height { get; set; }
        /// <summary>
        /// 行索引
        /// </summary>
        [XmlAttribute("PointX")]
        public int PointX { get; set; }
        /// <summary>
        /// 列索引
        /// </summary>
        [XmlAttribute("PointY")]
        public int PointY { get; set; }
        /// <summary>
        /// 不同类型的单元格
        /// </summary>
        [XmlAttribute("CellType")] 
        public string CellType { get; set; }
        /// <summary>
        /// 对齐
        /// </summary>
        [XmlAttribute("Alignment")]
        public string Alignment { get; set; }
        /// <summary>
        /// 边距
        /// </summary>
        [XmlAttribute("Padding")]
        public string Padding { get; set; }        
        /// <summary>
        /// 单元格背景色
        /// </summary>
        [XmlAttribute("BackGround")] 
        public string BackGround { get; set; }
        /// <summary>
        /// 左单元格边框的颜色
        /// </summary>
        [XmlAttribute("LeftBorderColor")] 
        public string LeftBorderColor { get; set; }
        /// <summary>
        /// 上单元格边框的颜色
        /// </summary>
        [XmlAttribute("TopBorderColor")]
        public string TopBorderColor { get; set; }
        /// <summary>
        /// 右单元格边框的颜色
        /// </summary>
        [XmlAttribute("RightBorderColor")]
        public string RightBorderColor { get; set; }
        /// <summary>
        /// 下单元格边框的颜色
        /// </summary>
        [XmlAttribute("BottomBorderColor")]
        public string BottomBorderColor { get; set; }
        /// <summary>
        /// 左单元格边框的宽度
        /// </summary>
        [XmlAttribute("LeftBorderWidth")] 
        public double LeftBorderWidth { get; set; }
        /// <summary>
        /// 上单元格边框的宽度
        /// </summary>
        [XmlAttribute("TopBorderWidth")]
        public double TopBorderWidth { get; set; }
        /// <summary>
        /// 右单元格边框的宽度
        /// </summary>
        [XmlAttribute("RightBorderWidth")]
        public double RightBorderWidth { get; set; }
        /// <summary>
        /// 下单元格边框的宽度
        /// </summary>
        [XmlAttribute("BottomBorderWidth")]
        public double BottomBorderWidth { get; set; }
        /// <summary>
        /// 是否只读
        /// </summary>
        [XmlAttribute("IsReadOnly")] 
        public bool IsReadOnly{get;set;}
        /// <summary>
        /// 单元格内的字体大小
        /// </summary>
        [XmlAttribute("FontSize")] 
        public string FontSize {get;set;}
        /// <summary>
        /// 单元格内的字体样式
        /// </summary>
        [XmlAttribute("FontStyle")] 
        public string FontStyle{get;set;}
        /// <summary>
        /// 单元格内的字体
        /// </summary>
        [XmlAttribute("FontFamily")] 
        public string FontFamily{get;set;}

        /// <summary>
        /// 单元格内的字体大小
        /// </summary>
        [XmlAttribute("ZoomFontSize")]
        public string ZoomFontSize { get; set; }

        /// <summary>
        /// 单元格内的字体颜色
        /// </summary>
        [XmlAttribute("ForeColor")] 
        public string ForeColor { get; set; }
        /// <summary>
        /// 当前单元格跨的行数
        /// </summary>
        [XmlAttribute("RowSpan")] 
        public int RowSpan { get; set; }
        /// <summary>
        /// 当前单元格跨的列数
        /// </summary>
        [XmlAttribute("ColSpan")] 
        public int ColSpan{get;set;}
        /// <summary>
        /// 当前合并单元格的起点坐标
        /// </summary>
        [XmlAttribute("SpanCell")]
        public string SpanCell { get; set; }
        /// <summary>
        /// 单元格的内容
        /// </summary>
        [XmlAttribute("Content")] 
        public string Content { get; set; }
        /// <summary>
        /// 数据来源
        /// </summary>
        [XmlAttribute("DataSrc")] 
        public string DataSrc { get; set; }
        /// <summary>
        ///  内容的类型（文本/OLE对象）
        /// </summary>
        [XmlAttribute("ContentType")] 
        public string ContentType { get; set; }

        /// <summary>
        ///  左上右下对角线
        /// </summary>
        [XmlAttribute("LeftTopRightBottom")]
        public int LeftTopRightBottom { get; set; }

        /// <summary>
        ///  左下右上对角线
        /// </summary>
        [XmlAttribute("LeftBottomRightTop")]
        public int LeftBottomRightTop { get; set; }

        /// <summary>
        ///  是否多行显示
        /// </summary>
        [XmlAttribute("WrapMode")]
        public bool WrapMode { get; set; }

        /// <summary>
        ///  单元格标识
        /// </summary>
        [XmlAttribute("CellTag")]
        public string CellTag { get; set; }

        /// <summary>
        ///  单元格源
        /// </summary>
        [XmlAttribute("CellSource")]
        public string CellSource { get; set; }

        /// <summary>
        /// 明细单元格
        /// </summary>
        [XmlElementAttribute(ElementName = "DetailCell", IsNullable = false)]
        public DetailCell[] DetailCells { get; set; }
    }
}
