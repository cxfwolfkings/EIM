using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Kingdee.CAPP.Componect;

namespace Kingdee.CAPP.UI.ProcessDataManagement
{
    /// <summary>
    /// 类型说明：物料列表界面
    /// 作    者：jason.tang
    /// 完成时间：2013-03-06
    /// </summary>
    public partial class MaterialListFrm : BaseForm
    {
        #region 变量和属性声明

        /// <summary>
        /// 物料列表
        /// </summary>
        private DataTable data;
        public DataTable MaterialData
        {
            get
            {
                return data;
            }
            set
            {
                data = value;
            }
        }

        /// <summary>
        /// 是否新建PBOM需要的ID
        /// </summary>
        public string CategoryId
        {
            get;
            set;
        }


        /// <summary>
        /// 分类ID
        /// </summary>
        public string TypeId
        {
            get;
            set;
        }

        /// <summary>
        /// 业务类型ID
        /// </summary>
        public string CategoryTypeId
        {
            get;
            set;
        }

        #endregion

        #region 窗体控件事件

        public MaterialListFrm()
        {
            InitializeComponent();
        }

        private void MaterialListFrm_Load(object sender, EventArgs e)
        {
            dgvMaterial.AutoGenerateColumns = false;    
            RefreshData(data);
        }

        /// <summary>
        /// 数据加载完触发事件
        /// </summary>
        private void dgvMaterial_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            SetImageColumn();
        }

        /// <summary>
        /// 确定(选择项)
        /// </summary>
        private void btnConfirm_Click(object sender, EventArgs e)
        {    
            //FormCollection collection = Application.OpenForms;
            //foreach (Form form in collection)
            //{
            //    if (form.Name == "MaterialChooseNavigate")
            //    {
            //        form.Close();
            //        break;
            //    }
            //}

            //this.Close();
            List<string> listCategoryTypeId = SelectMaterial(CategoryId);
            if (listCategoryTypeId == null)
            {
                return;
            }

            if (!string.IsNullOrEmpty(CategoryId))
            {
                //CategoryId = string.Join(",",listCategoryTypeId.ToArray());
                FormCollection collection = Application.OpenForms;
                foreach (Form form in collection)
                {
                    if (form.Name == "ProductListFrm")
                    {
                        ((ProductListFrm)form).AddPbom(listCategoryTypeId[0], listCategoryTypeId[1] + listCategoryTypeId[2]);
                        form.Select();
                    }
                }
 
            }
            else
            {
                WeifenLuo.WinFormsUI.Docking.DockContent content = MainFrm.mainFrm.CheckFormIsOpened(typeof(MaterialStructureNavigate).Name);

                if (content != null)
                {                    
                    MainFrm.mainFrm.CloseModule(content);
                }
                MaterialStructureNavigate frm = new MaterialStructureNavigate();
                frm.CategoryTypeIds = listCategoryTypeId;                
                MainFrm.mainFrm.OpenNavigate(frm, WeifenLuo.WinFormsUI.Docking.DockState.DockLeft);
            }
        }

        /// <summary>
        /// 结合CellValueChanged事件设置复选框单选
        /// </summary>
        private void dgvMaterial_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (dgv.IsCurrentCellDirty)
            {
                dgv.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        /// <summary>
        /// 结合CurrentCellDirtyStateChanged事件设置复选框单选
        /// </summary>
        private void dgvMaterial_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                DataGridView dgv = (DataGridView)sender;
                DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                bool value = (bool)checkCell.Value;

                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (row.Index != e.RowIndex && value)
                    {
                        row.Cells[e.ColumnIndex].Value = false;
                    }
                }
                dgv.Invalidate();
            }
        }

        private void txtFieldValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                FilterData();
                e.SuppressKeyPress = true;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            FilterData();
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
                                (row.Cells["colDisginStateIconPath"].Value != null ||
                                row.Cells["colTechnicsStateIconPath"].Value != null))
                            {
                                string dPath = string.Empty;
                                string tPath = string.Empty;
                                Image imgDisign = null;
                                Image imgTechnics = null;
                                int dWidth = 0;
                                int tWidth = 0;
                                int height = 0;
                                if (row.Cells["colDisginStateIconPath"].Value != null)
                                {
                                    dPath = row.Cells["colDisginStateIconPath"].Value.ToString().Remove(0, 2).Replace("/", "\\");
                                    imgDisign = Image.FromFile(string.Format(startupPath + @"{0}", dPath));
                                    dWidth = imgDisign.Width;
                                    height = imgDisign.Height;
                                }
                                if (row.Cells["colTechnicsStateIconPath"].Value != null)
                                {
                                    tPath = row.Cells["colTechnicsStateIconPath"].Value.ToString().Remove(0, 2).Replace("/", "\\");
                                    imgTechnics = Image.FromFile(string.Format(startupPath + @"{0}", tPath));
                                    tWidth = imgTechnics.Width;
                                    height = imgTechnics.Height;
                                }
                                //将两张图片合并到一张
                                Bitmap ImageToDisplayInColumn = new Bitmap(dWidth + tWidth, height);
                                using (Graphics graphicsObject = Graphics.FromImage(ImageToDisplayInColumn))
                                {
                                    if (imgDisign != null)
                                    {
                                        graphicsObject.DrawImage(imgDisign, new Point(0, 0));
                                    }
                                    if (imgTechnics != null)
                                    {
                                        graphicsObject.DrawImage(imgTechnics, new Point(dWidth, 0));
                                    }
                                }
                                cell.Value = ImageToDisplayInColumn;
                            }
                        }
                    }
                }
            }
            catch
            { }
        }

        /// <summary>
        /// 方法说明：得到当前选中行的分类ID
        /// 作    者：jason.tang
        /// 完成时间：2013-03-06
        /// </summary>
        /// <returns>分类ID集合</returns>
        private List<string> SelectMaterial(string pbom)
        {
            List<string> listCategoryTypeId = new List<string>();
            foreach (DataGridViewRow row in dgvMaterial.Rows)
            {
                if (!string.IsNullOrEmpty(pbom))
                {
                    if ((bool)row.Cells[0].EditedFormattedValue == true &&
                    row.Cells["colBaseId"].Value != null)
                    {
                        listCategoryTypeId.Add(row.Cells["colBaseId"].Value.ToString());
                        listCategoryTypeId.Add(row.Cells["colCategoryName"].Value.ToString());
                        listCategoryTypeId.Add(row.Cells["colCode"].Value.ToString());
                    }
                }
                else
                {
                    if ((bool)row.Cells[0].EditedFormattedValue == true &&
                    row.Cells["colCategoryTypeId"].Value != null)
                    {
                        listCategoryTypeId.Add(row.Cells["colCategoryTypeId"].Value.ToString());
                        listCategoryTypeId.Add(row.Cells["colCode"].Value.ToString());
                    }
                }                
            }

            if (listCategoryTypeId.Count == 0)
            {
                MessageBox.Show("请选择一个物料", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }

            return listCategoryTypeId;
        }

        /// <summary>
        /// 方法说明：更新数据源
        /// 作    者：jason.tang
        /// 完成时间：2013-03-06
        /// </summary>
        /// <param name="dt"></param>
        public void RefreshData(DataTable dt)
        {
            dgvMaterial.DataSource = dt;
            data = dt;
            if (dgvMaterial != null && dgvMaterial.Columns.Count > 0)
            {
                Dictionary<string, string> dicColumns = new Dictionary<string, string>();
                foreach (DataGridViewColumn column in dgvMaterial.Columns)
                {
                    if (!string.IsNullOrEmpty(column.HeaderText) &&
                        !dicColumns.ContainsKey(column.HeaderText) &&
                        column.Visible)
                    {
                        dicColumns.Add(column.HeaderText, column.DataPropertyName);
                    }
                }
                BindingSource bs = new BindingSource();
                bs.DataSource = dicColumns;
                comboField.DisplayMember = "Key";
                comboField.ValueMember = "Value";
                comboField.DataSource = bs;
            }
        }

        /// <summary>
        /// 方法说明：过滤数据
        /// </summary>
        private void FilterData()
        {
            //if (string.IsNullOrEmpty(CategoryIdTypeId))
            //{
                if (string.IsNullOrEmpty(txtFieldValue.Text))
                {
                    dgvMaterial.DataSource = data;
                }
                else
                {
                    string conditions = string.Format(" and {0} like '%{1}%'", comboField.SelectedValue, txtFieldValue.Text);
                    DataTable dt = Kingdee.CAPP.BLL.MaterialModuleBLL.GetMaterialModuleDataByCategoryId(TypeId, CategoryTypeId, conditions);
                    dgvMaterial.DataSource = dt;
                }
            //}
            //else
            //{
            //    DataView dv = data.DefaultView;
            //    if (!string.IsNullOrEmpty(txtFieldValue.Text))
            //    {
            //        dv.RowFilter = string.Format("{0} like '%{1}%'", comboField.SelectedValue, txtFieldValue.Text);
            //    }
            //    else
            //    {
            //        dv.RowFilter = null;
            //    }
            //    dgvMaterial.DataSource = dv.ToTable();
            //}
        }

        #endregion

           
    }
}
