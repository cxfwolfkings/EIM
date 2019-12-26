using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Kingdee.CAPP.Componect;
using System.Configuration;

namespace Kingdee.CAPP.UI.ProcessDataManagement
{
    /// <summary>
    /// 类型说明：物料属性界面
    /// 作    者：jason.tang
    /// 完成时间：2013-03-07
    /// </summary>
    public partial class MaterialPropertyFrm : BaseForm
    {
        #region 变量和属性声明

        /// <summary>
        /// 物料实体对象
        /// </summary>
        private object materialObject;
        public object MaterialProperty
        {
            get
            {
                return materialObject;
            }
            set
            {
                materialObject = value;
            }
        }

        #endregion

        #region 窗体控件事件

        public MaterialPropertyFrm()
        {
            InitializeComponent();
        }

        private void MaterialPropertyFrm_Load(object sender, EventArgs e)
        {
            dgvMaterialObject.AutoGenerateColumns = false; 
            RefreshObject(materialObject);
        }

        public void RefreshObject(object obj)
        {
            pgMaterial.SelectedObject = obj;

            if (obj is Kingdee.CAPP.Model.MaterialModule)
            {
                DataTable dt = Kingdee.CAPP.BLL.MaterialModuleBLL.GetMaterialObjectByVerId(((Kingdee.CAPP.Model.MaterialModule)obj).materialverid);
                dgvMaterialObject.DataSource = dt;
            }
        }
        
        private void dgvMaterialObject_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            SetImageColumn();
        }

        private void dgvMaterialObject_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1 && 
                dgvMaterialObject.Rows[e.RowIndex].Cells["colObjOption"].Value != null && 
                dgvMaterialObject.Rows[e.RowIndex].Cells["colObjOption"].Value.ToString() == "0")
            {
                //打开文档
                if (dgvMaterialObject.Rows[e.RowIndex].Cells["colName"].Value != null)
                {
                    string fileName = dgvMaterialObject.Rows[e.RowIndex].Cells["colName"].Value.ToString();
                    string path = string.Format(@"C:\prowayplm\admin\Temp\{0}", fileName);
                    if (!System.IO.File.Exists(path))
                    {
                        string uploadUrl = ConfigurationManager.AppSettings["FtpUrl"].ToString();
                        string userId = ConfigurationManager.AppSettings["Uid"].ToString();
                        string password = ConfigurationManager.AppSettings["Pass"].ToString();
                        uploadUrl = uploadUrl.Replace(@"ftp://","");

                        object copyId = dgvMaterialObject.Rows[e.RowIndex].Cells["colCopyId"].Value;
                        if (copyId != null && !string.IsNullOrEmpty(copyId.ToString()))
                        {
                            int result = Kingdee.CAPP.BLL.MaterialModuleBLL.DownloadFile(copyId.ToString(), path, uploadUrl, userId, password);
                            if (result == 0)
                            {
                                MessageBox.Show(string.Format("文档[{0}]下载到本地失败", fileName), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show(string.Format("服务器上不存在文档[{0}]", fileName), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    try
                    {
                        System.Diagnostics.Process.Start(path);
                    }
                    catch { }
                }
            }
        }

        private void dgvMaterialObject_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                dgvMaterialObject.Cursor = Cursors.Hand;
            }
            else
                dgvMaterialObject.Cursor = Cursors.Default;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 方法说明：设置图片列图片
        /// 作    者：jason.tang
        /// 完成时间：2013-09-13
        /// </summary>
        private void SetImageColumn()
        {
            try
            {
                foreach (DataGridViewRow row in dgvMaterialObject.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {   //设置图片单元格图片路径
                        if (cell is DataGridViewImageCell)
                        {
                            string startupPath = Application.StartupPath + "\\Resources";

                            if (cell.ColumnIndex == 1 && row.Cells["colObjectIconPath"].Value != null)
                            {
                                string path = row.Cells["colObjectIconPath"].Value.ToString();
                                cell.Value = Image.FromFile(string.Format(startupPath + @"\{0}", path.Replace("../", "").Replace("/", "\\"))); //Application.StartupPath + @"\skins\ObjectIcon\Material-G2.gif");
                            }
                            else if (cell.ColumnIndex == 2)
                            {
                                string option = row.Cells["colObjOption"].Value.ToString();
                                string state = row.Cells["colState"].Value.ToString();
                                string ischeckout = row.Cells["colCheckOutState"].Value.ToString();
                                string path = GetObjectState(option, state, ischeckout);
                                cell.Value = Image.FromFile(string.Format(startupPath + @"\{0}", path.Replace("../", "").Replace("/", "\\")));
                            }
                        }
                    }
                }
            }
            catch
            { }
        }

        /// <summary>
        /// 方法说明：获取对象状态图标
        /// 作者：jason.tang
        /// 完成时间：2013-09-13
        /// </summary>
        /// <param name="pOption">对象类型</param>
        /// <param name="pState">状态</param>
        /// <param name="pIsCheckOut">是否签出</param>
        /// <returns></returns>
        public string GetObjectState(string pOption, string pState, string pIsCheckOut)
        {
            string path = "../skins/ObjectState/";

            if (pIsCheckOut == "1")
            {
                return path + "CheckOut.gif";
            }

            switch (pOption)
            {
                case "0"://文档
                case "5":
                case "8":
                case "13":
                case "1"://物料
                    switch (pState)
                    {
                        case "D1":
                            return path + "Material-D-1.gif";
                        case "D2":
                            return path + "Material-D-2.gif";
                        case "D3":
                            return path + "Material-D-3.gif";
                        case "D4":
                            return path + "Material-D-4.gif";
                        case "D5":
                            return path + "Material-D-5.gif";

                        case "P1":
                            return path + "Material-P-1.gif";
                        case "P2":
                            return path + "Material-P-2.gif";
                        case "P3":
                            return path + "Material-P-3.gif";
                        case "P4":
                            return path + "Material-P-4.gif";
                        case "P5":
                            return path + "Material-P-5.gif";

                        case "Q1":
                            return path + "Material-Q-1.gif";
                        case "Q2":
                            return path + "Material-Q-2.gif";
                        case "Q3":
                            return path + "Material-Q-3.gif";
                        case "Q4":
                            return path + "Material-Q-4.gif";
                        case "Q5":
                            return path + "Material-Q-5.gif";
                    }
                    break;
                case "11":
                    switch (pState)
                    {
                        case "1":
                            return path + "Submit.gif";
                        case "2":
                            return path + "Archiving.gif";
                        case "3":
                            return path + "Release.gif";
                    }
                    break;

            }

            return path + "Normal.gif";

        }

        #endregion

        

    }
}
