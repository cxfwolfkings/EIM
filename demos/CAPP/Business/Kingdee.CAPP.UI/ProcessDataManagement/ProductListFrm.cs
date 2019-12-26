using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Kingdee.CAPP.Componect;
using Kingdee.CAPP.BLL;

namespace Kingdee.CAPP.UI.ProcessDataManagement
{
    /// <summary>
    /// 类型说明：产品物料列表界面
    /// 作    者：jason.tang
    /// 完成时间：2013-03-06
    /// </summary>
    public partial class ProductListFrm : BaseForm
    {
        #region 变量和属性声明

        /// <summary>
        /// 物料列表
        /// </summary>
        private DataTable data;
        public string FolderId
        {
            get;
            set;
        }

        #endregion

        #region 界面控件事件

        public ProductListFrm()
        {
            InitializeComponent();
        }

        private void ProductListFrm_Load(object sender, EventArgs e)
        {
            dgvMaterial.AutoGenerateColumns = false;
            RefreshProductData(FolderId);
        }

        /// <summary>
        /// 数据加载完触发事件
        /// </summary>
        private void dgvMaterial_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            SetImageColumn();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 方法说明：设置图片列图片
        /// 作    者：jason.tang
        /// 完成时间按：2013-03-06
        /// </summary>
        private void SetImageColumn()
        {
            try
            {
                foreach (DataGridViewRow row in dgvMaterial.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {   //设置图片单元格图片路径
                        if (cell is DataGridViewImageCell)
                        {
                            string startupPath = Application.StartupPath + "\\Resources";

                            if (cell.ColumnIndex == 1 && row.Cells["colObjectIconPath"].Value != null)
                            {
                                string path = row.Cells["colObjectIconPath"].Value.ToString();                               
                                cell.Value = Image.FromFile(string.Format(startupPath + @"{0}", path.Remove(0, 2).Replace("/", "\\"))); //Application.StartupPath + @"\skins\ObjectIcon\Material-G2.gif");
                            }
                            else if (cell.ColumnIndex == 2 &&
                                row.Cells["colDisginStateIconPath"].Value != null)
                            {
                                string path = row.Cells["colDisginStateIconPath"].Value.ToString();
                                cell.Value = Image.FromFile(string.Format(startupPath + @"{0}", path.Remove(0, 2).Replace("/", "\\")));                                
                            }
                        }
                    }
                }
            }
            catch
            { }
        }

        /// <summary>
        /// 方法说明：更新数据源
        /// 作    者：jason.tang
        /// 完成时间：2013-03-06
        /// </summary>
        /// <param name="dt"></param>
        public void RefreshProductData(string folderid)
        {
            //根据文件夹ID获取物料列表
            if (!string.IsNullOrEmpty(folderid))
            {
                data = ProductModuleBLL.GetMaterialBomByFolderId(folderid);
            }
            dgvMaterial.DataSource = data;
        }

        /// <summary>
        /// 新增PBOM
        /// </summary>
        /// <param name="baseid">物料BaseId</param>
        /// <param name="groupid">PBOM文件夹GroupId</param>
        public void AddPbom(string baseid, string groupid)
        {
            if (!string.IsNullOrEmpty(baseid) && !string.IsNullOrEmpty(groupid))
            {
                Guid gid = ProductModuleBLL.InsertPBom(baseid, FolderId, groupid);
            }
        }

        #endregion

        private void btnNewPbom_Click(object sender, EventArgs e)
        {
            FormCollection collection = Application.OpenForms;
            bool isOpened = false;
            foreach (Form form in collection)
            {
                if (form.Name == "MaterialListFrm")
                {
                    isOpened = true;
                    ((MaterialListFrm)form).CategoryId = "PBOM";
                    form.Select();
                }
            }

            if (!isOpened)
            {
                MaterialChooseNavigate frm = new MaterialChooseNavigate();
                MainFrm.mainFrm.OpenNavigate(frm, WeifenLuo.WinFormsUI.Docking.DockState.DockLeft);
            }
        }
    }
}
