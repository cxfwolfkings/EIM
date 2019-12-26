using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Kingdee.CAPP.Common;

namespace Kingdee.CAPP.Model
{
    /// <summary>
    /// 类型说明：物料实体类
    /// 作    者：jason.tang
    /// 完成时间：2013-03-05
    /// </summary>
    [TypeConverter(typeof(PropertySorter))]
    public class MaterialModule
    {
        /// <summary>
        /// 物料类型ID
        /// </summary>
        [CategoryAttribute("物料属性"), DescriptionAttribute("类别ID"), DisplayName("类别ID"), PropertyOrder(12)]
        [ReadOnlyAttribute(false)]
        public string TypeId { get; set; }
        /// <summary>
        /// 物料类型名称
        /// </summary>
        [CategoryAttribute("物料属性"), DescriptionAttribute("物料类型名称"), DisplayName("物料类型名称"), PropertyOrder(13)]
        [ReadOnlyAttribute(false)]
        public string TypeName { get; set; }

        #region V_MAT_MATERIALVERSION视图

        /// <summary>
        /// 物料编码
        /// </summary>
        [CategoryAttribute("物料属性"), DescriptionAttribute("物料编码"), DisplayName("物料编码"), PropertyOrder(10)]
        [ReadOnlyAttribute(false)]
        public string code { get; set; }
        /// <summary>
        /// 物料名称
        /// </summary>
        [CategoryAttribute("物料属性"), DescriptionAttribute("物料名称"), DisplayName("物料名称"), PropertyOrder(11)]
        [ReadOnlyAttribute(false)]
        public string name { get; set; }
        /// <summary>
        /// 图号
        /// </summary>
        [CategoryAttribute("物料属性"), DescriptionAttribute("图号"), DisplayName("图号"), PropertyOrder(14)]
        [ReadOnlyAttribute(false)]
        public string drawnumber { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        [CategoryAttribute("物料属性"), DescriptionAttribute("规格"), DisplayName("规格"), PropertyOrder(15)]
        [ReadOnlyAttribute(false)]
        public string spec { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        [CategoryAttribute("物料属性"), DescriptionAttribute("版本号"), DisplayName("版本号"), PropertyOrder(16)]
        [ReadOnlyAttribute(false)]
        public string vercode { get; set; }
        /// <summary>
        /// 生产方式
        /// </summary>
        [CategoryAttribute("物料属性"), DescriptionAttribute("生产方式"), DisplayName("生产方式"), PropertyOrder(17)]
        [ReadOnlyAttribute(false)]
        public string intproductmode { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [CategoryAttribute("物料属性"), DescriptionAttribute("创建时间"), DisplayName("创建时间"), PropertyOrder(18)]
        [ReadOnlyAttribute(false)]
        public string createdate { get; set; }
        /// <summary>
        /// 组件数量
        /// </summary>
        [CategoryAttribute("物料属性"), DescriptionAttribute("组件数量"), DisplayName("组件数量"), PropertyOrder(19)]
        [ReadOnlyAttribute(false)]
        public int count { get; set; }
        /// <summary>
        /// 归属产品
        /// </summary>
        [CategoryAttribute("物料属性"), DescriptionAttribute("归属产品"), DisplayName("归属产品"), PropertyOrder(20)]
        [ReadOnlyAttribute(false)]
        public string productname { get; set; }
        /// <summary>
        /// 业务类型
        /// </summary>
        [CategoryAttribute("物料属性"), DescriptionAttribute("业务类型"), DisplayName("业务类型"), PropertyOrder(21)]
        [ReadOnlyAttribute(false)]
        public string categoryname { get; set; }
        /// <summary>
        /// 设计虚件
        /// </summary>
        [CategoryAttribute("物料属性"), DescriptionAttribute("设计虚件"), DisplayName("设计虚件"), PropertyOrder(22)]
        [ReadOnlyAttribute(false)]
        public string isvirtualdesign { get; set; }
        /// <summary>
        /// 通用类别
        /// </summary>
        [CategoryAttribute("物料属性"), DescriptionAttribute("通用类别"), DisplayName("通用类别"), PropertyOrder(23)]
        [ReadOnlyAttribute(false)]
        public string typename { get; set; }
        /// <summary>
        /// 图纸张数
        /// </summary>
        [CategoryAttribute("物料属性"), DescriptionAttribute("图纸张数"), DisplayName("图纸张数"), PropertyOrder(24)]
        [ReadOnlyAttribute(false)]
        public int papercount { get; set; }
        /// <summary>
        /// 成员规格
        /// </summary>
        [CategoryAttribute("物料属性"), DescriptionAttribute("成员规格"), DisplayName("成员规格"), PropertyOrder(25)]
        [ReadOnlyAttribute(false)]
        public string memberspec { get; set; }

        [Browsable(false)]
        public string materialverid { get; set; }

        [Browsable(false)]
        public string baseid { get; set; }

        [Browsable(false)]
        public string pbomid { get; set; }


        #endregion
    }
}
