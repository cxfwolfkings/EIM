using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/*******************************
 * Created By franco
 * Description:Card Manager model layer
 *******************************/

namespace Kingdee.CAPP.Model
{
    public class CardManager
    {
        /// <summary>
        /// 工艺模板ID
        /// </summary>
        public Guid ProcessModuleId { get; set; }
        /// <summary>
        /// 业务ID
        /// </summary>
        public Guid BusinessId { get; set; }
        /// <summary>
        /// 模板的名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 业务类型
        /// </summary>
        public BusinessType BType { get; set; }
        /// <summary>
        /// 父节点的级别
        /// </summary>
        public int ParentNode { get; set; }
        /// <summary>
        /// 当前节点的级别
        /// </summary>
        public int CurrentNode { get; set; }
    }

    /// <summary>
    /// 业务类型
    /// </summary>
    public enum BusinessType
    {
        /// <summary>
        /// 根目录
        /// </summary>
        Root = 0,
        /// <summary>
        /// 卡片
        /// </summary>
        Card = 1,
        /// <summary>
        /// 提示框
        /// </summary>
        FixTip = 2,
        /// <summary>
        /// 内容
        /// </summary>
        Detail = 3,
        /// <summary>
        /// 工艺规程
        /// </summary>
        Planning = 4,
        /// <summary>
        /// 文件夹
        /// </summary>
        Folder = 10
    }
}
