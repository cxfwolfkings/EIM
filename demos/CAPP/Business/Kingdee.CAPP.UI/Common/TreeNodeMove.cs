using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Kingdee.CAPP.UI.Common
{
    public class TreeNodeMove
    {
        /// <summary>
        /// 选中节点上移方法
        /// </summary>
        /// <param name="node"></param>
        private void SetTreeNodeUp(TreeNode node)
        {
            if ((node == null) || (node.PrevNode) == null)
                return;
            TreeNode newNode = (TreeNode)node.Clone();
            ////数据表的移动
            //if (node.Level == 1)
            //{
            //    object[] _rowData = SearchParentDataTable.Rows[node.Index].ItemArray;
            //    SearchParentDataTable.Rows[node.Index].ItemArray = SearchParentDataTable.Rows[node.Index - 1].ItemArray;
            //    SearchParentDataTable.Rows[node.Index - 1].ItemArray = _rowData;
            //}
            //if (node.Level == 2)
            //{
            //    DataTable dt = new DataTable();
            //    dt = (DataTable)SearchChildDataTableArr[node.Parent.Index];
            //    object[] _rowData = dt.Rows[node.Index].ItemArray;
            //    dt.Rows[node.Index].ItemArray = dt.Rows[node.Index - 1].ItemArray;
            //    dt.Rows[node.Index - 1].ItemArray = _rowData;
            //}
            //节点的移动
            if (node.PrevNode.PrevNode != null)
            {
                if (node.Parent != null)
                    node.Parent.Nodes.Insert(node.PrevNode.Index, newNode);
                else
                    node.TreeView.Nodes.Insert(node.PrevNode.Index, newNode);
            }
            else
                if (node.Parent != null)
                    node.Parent.Nodes.Insert(node.PrevNode.Index, newNode);
                else
                    node.TreeView.Nodes.Insert(node.PrevNode.Index, newNode);
            node.TreeView.SelectedNode = newNode;
            node.TreeView.Nodes.Remove(node);

        }
        /// <summary>
        /// 选中节点下移方法
        /// </summary>
        /// <param name="node"></param>
        private void SetTreeNodeDown(TreeNode node)
        {
            if ((node == null) || (node.NextNode) == null) return;
            TreeNode newNode = (TreeNode)node.Clone();
            //数据表的移动
            //if (node.Level == 1)
            //{
            //    object[] _rowData = SearchParentDataTable.Rows[node.Index].ItemArray;
            //    SearchParentDataTable.Rows[node.Index].ItemArray = SearchParentDataTable.Rows[node.Index + 1].ItemArray;
            //    SearchParentDataTable.Rows[node.Index + 1].ItemArray = _rowData;
            //}
            //if (node.Level == 2)
            //{
            //    DataTable dt = new DataTable();
            //    dt = (DataTable)SearchChildDataTableArr[node.Parent.Index];
            //    object[] _rowData = dt.Rows[node.Index].ItemArray;
            //    dt.Rows[node.Index].ItemArray = dt.Rows[node.Index + 1].ItemArray;
            //    dt.Rows[node.Index + 1].ItemArray = _rowData;
            //}
            //节点的移动
            if (node.NextNode.NextNode != null)
            {
                if (node.Parent != null)
                    node.Parent.Nodes.Insert(node.NextNode.NextNode.Index, newNode);
                else
                    node.TreeView.Nodes.Insert(node.NextNode.NextNode.Index, newNode);
            }
            else
                if (node.Parent != null)
                    node.Parent.Nodes.Add(newNode);
                else
                    node.TreeView.Nodes.Add(newNode);
            node.TreeView.SelectedNode = newNode;
            node.TreeView.Nodes.Remove(node);
        }

    }
}
