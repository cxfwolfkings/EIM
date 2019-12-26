using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using Kingdee.CAPP.Model;

namespace Kingdee.CAPP.UI.Common
{
    /// <summary>
    /// 排序的逻辑
    /// </summary>
    public class NodeSorter: IComparer
    {
        /// <summary>
        /// Comparer PlanningCardRelation 'Sort
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(object x, object y)
        {
            TreeNode tnX = x as TreeNode;
            TreeNode tnY = y as TreeNode;

            int x1 = Convert.ToInt32(tnX.Name);
            int y1 = Convert.ToInt32(tnY.Name);

            if (x1 > y1)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
