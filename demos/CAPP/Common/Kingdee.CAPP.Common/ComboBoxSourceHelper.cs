using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Forms;

namespace Kingdee.CAPP.Common
{
    /// <summary>
    /// 类型说明：下拉框绑定帮助类
    /// 作    者：jason.tang
    /// 完成时间：2012-12-19
    /// </summary>
    public class ComboBoxSourceHelper
    {
        /// <summary> 
        /// 此处T要从struct继承,而不能是enum 
        /// </summary> 
        /// <param name="cmb">下拉框控件</param>
        /// <param name="selectIndex">默认选择项</param>
        public void BinderEnum<T>(ComboBox cmb, int selectIndex) where T : struct 
        { 
            Type type = typeof(T); 
            FieldInfo[] fields = type.GetFields();
            for (int i = 1; i < fields.Length; i++)
            {
                cmb.Items.Add(fields[i].Name);
            }
            if (cmb.Items.Count > 0 && selectIndex >= 0)
            {
                cmb.SelectedIndex = selectIndex;
            }
        }

        /// <summary>
        /// 纸张幅面尺寸
        /// </summary>
        public enum PageSize
        {
            A01 = 1189,
            A02 = 841,
            A11 = 841,
            A12 = 594,
            A21 = 594,
            A22 = 420,
            A31 = 420,
            A32 = 297,
            A41 = 297,
            A42 = 210,
            A51 = 210,
            A52 = 148            
        }

        /// <summary>
        /// 纸张幅面
        /// </summary>
        public enum PageBreadth
        {
            A0,
            A1,
            A2,
            A3,
            A4,
            A5,
            自定义
        }

        /// <summary>
        /// 卡片类型
        /// </summary>
        public enum CardType
        {
            工艺卡片,
            报表卡片,
            协作卡片
        }

        /// <summary>
        /// 单元格类型
        /// </summary>
        public enum CellType 
        { 
            固定提示框 = 0,
            标题填写框 = 1,
            明细填写框 = 2,
            页码显示框 = 3,
            页数显示框 = 4,
            签名名称框 = 5,
            签名日期框 = 6
        }

        /// <summary>
        /// 单元格类型
        /// </summary>
        public enum DataGridViewTextBoxCellType
        {
            Fixed = 0,
            Title = 1,
            Detail = 2,
            PageNum = 3,
            PageCount = 4,
            AutographName = 5,
            AutographDate = 6
        }

        /// <summary>
        /// 单元格样式
        /// </summary>
        public enum CellStyle
        {
            多行显示,
            缩小显示
        }

        /// <summary>
        /// 单元格内容
        /// </summary>
        public enum CellContent
        {
            文本对象 = 0,
            图形对象 = 1,
            OLE对象 = 2
        }

        /// <summary>
        /// 高级属性
        /// </summary>
        public enum AdvanceProperty
        {
            明细单元 = 0,
            自动序号 = 1,
            空明细列 = 2,
            隐藏明细列 = 3
        }

        /// <summary>
        /// 单元格边框
        /// </summary>
        public enum CellBorder
        {
            较粗 = 5,
            粗线 = 4,            
            中线 = 3,
            细线 = 2,
            较细 = 1
        }

        /// <summary>
        /// 边框线样式
        /// </summary>
        public enum BorderLineStyle
        {
            //实线
            Solid = 0,
            //虚线
            Dash = 1,
            //点线
            Dot = 2,
            //双线
            Doublet = 3
        }

        /// <summary>
        /// 窗体Title
        /// </summary>
        public enum FormText
        {
            标题栏 = 0,
            明细列 = 1
        }

        /// <summary>
        /// 图片类型
        /// </summary>
        public enum ImageType
        {
            BMP,
            JPEG,
            PNG
        }

        /// <summary>
        /// 粗糙度标注符号值
        /// </summary>
        public enum RoughnessSymbolValue
        {
            Ra,
            Ry,
            Rz
        }

        /// <summary>
        /// AppSettings键
        /// </summary>
        public enum SettingKey
        {
            Server,
            Database,
            Uid
        }

        /// <summary>
        /// 公差类型
        /// </summary>
        public enum ToleranceType
        {
            轴公差 = 0,
            孔公差 = 1,
            基轴制配合 = 2,
            基孔制配合 = 3
        }
    }
}
