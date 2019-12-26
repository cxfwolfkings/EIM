using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Kingdee.CAPP.BLL;
using Kingdee.CAPP.Model;
using Kingdee.CAPP.UI.Resource;

namespace Kingdee.CAPP.UI.SystemSetting
{
    public partial class AddNodeFrm : BaseSkinForm
    {
        public AddNodeFrm()
        {
            InitializeComponent();
        }

        public AddNodeFrm(CardManager cardManager)
        {
            InitializeComponent();
            _businessModule = cardManager;

            if (_businessModule.BType == BusinessType.Card)
            {
                Text = GlobalResource.AddNewModule;
            }
            else if(_businessModule.BType == BusinessType.Folder)
            {
                Text = GlobalResource.AddNewFolder;
            }
        }

        private CardManager _businessModule;
        public CardManager BusinessModule 
        {
            get
            {
                return _businessModule;
            }
            set
            {
                _businessModule = value;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                int currentNode = CardManagerBLL.AddBusiness(
                     txtBusinessName.Text.Trim(),
                     (int)BusinessModule.BType,
                     BusinessModule.ParentNode, BusinessModule.BusinessId);

               /// 当前节点的Level
               CurrentNode = currentNode;
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
            this.Close();
        }
        public string NewNodeName
        {
            get
            {
                return txtBusinessName.Text;
            }
            set
            {
                txtBusinessName.Text = value;
            }
        }
        public int CurrentNode
        {
            get;
            set;
        }        

        private void txtBusinessName_KeyDown(object sender, KeyEventArgs e)
        {
            if ((int)e.KeyCode == 13)
            {
                e.SuppressKeyPress = true;
            }
        }
    }
}
