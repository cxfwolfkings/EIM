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
    /// 类型说明：典型工艺分类新增
    /// 作      者：jason.tang
    /// 完成时间：2013-06-20
    /// </summary>
    public partial class AddTypicalProcessFrm : BaseSkinForm
    {
        public AddTypicalProcessFrm()
        {
            InitializeComponent();
        }

        #region 属性

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
        public AddTypicalProcessFrm(TypicalProcess typicalProcess)
        {
            InitializeComponent();
            this._typicalProcessMod = typicalProcess;
        }

        public Dictionary<string, TypicalProcess> NewNodeNameDic
        {
            get;
            set;
        }

        #endregion

        #region 窗体控件事件

        private void btnAdd_Click(object sender, EventArgs e)
        {
            TypicalProcess typicalProcess = new TypicalProcess();
            typicalProcess.BusinessId = Guid.NewGuid();
            typicalProcess.BType = BusinessType.Planning;
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
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
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
