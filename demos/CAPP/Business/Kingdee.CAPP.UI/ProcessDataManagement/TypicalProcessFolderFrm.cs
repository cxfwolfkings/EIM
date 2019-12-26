using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Kingdee.CAPP.Model;
using Kingdee.CAPP.BLL;

namespace Kingdee.CAPP.UI.ProcessDataManagement
{
    /// <summary>
    /// 类型说明：典型工艺文件夹新增界面
    /// 作      者：jason.tang
    /// 完成时间：2013-06-20
    /// </summary>
    public partial class TypicalProcessFolderFrm : BaseSkinForm
    {
        public TypicalProcessFolderFrm()
        {
            InitializeComponent();
        }

        #region 属性声明

        private TypicalProcess _typicalProcessMod;
        public TypicalProcess TypicalProcessMod
        {
            get
            {
                return _typicalProcessMod;
            }
            set
            {
                _typicalProcessMod = value;
            }
        }

        /// <summary>
        /// 添加工艺卡片名
        /// </summary>
        public Dictionary<string, TypicalProcess> NewNodeNameDic
        {
            get;
            set;
        }

        #endregion

        #region 界面控件事件

        public TypicalProcessFolderFrm(TypicalProcess typicalProcess)
        {
            InitializeComponent();

            _typicalProcessMod = typicalProcess;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            TypicalProcess typicalProcess = new TypicalProcess();
            typicalProcess.BusinessId = Guid.NewGuid();
            typicalProcess.BType = BusinessType.Folder;
            typicalProcess.Name = txtBusinessName.Text.Trim();
            typicalProcess.ParentNode = TypicalProcessMod.ParentNode;
            typicalProcess.TypicalProcessId = Guid.NewGuid();

            try
            {
                int currentNode
                    = TypicalProcessBLL.AddTypicalProcess(typicalProcess);

                NewNodeNameDic
                    = new Dictionary<string, TypicalProcess>() 
                        {
                            { txtBusinessName.Text.Trim(), typicalProcess } 
                        };
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.DialogResult = DialogResult.Cancel;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
            }
        }

        #endregion

    }
}
