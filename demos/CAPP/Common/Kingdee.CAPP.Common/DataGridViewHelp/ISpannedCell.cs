using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Kingdee.CAPP.Common
{
    /// <summary>
    /// 接口说明：行列合并接口
    /// 作   者：jason.tang
    /// 完成时间：2012-12-17
    /// </summary>
    interface ISpannedCell
    {
        int ColumnSpan { get; }
        int RowSpan { get; }
        DataGridViewCell OwnerCell { get; }
    }
}
