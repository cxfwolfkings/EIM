using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SolidWorks.Interop.sldworks;

namespace Kingdee.CAPP.Solidworks.RoutingPlugIn
{
    public partial class NewProcessLine : Form
    {
        BusinessCategoryContext bcontext = null;
        RoutingContext rcontext = null;

        private string routingId = string.Empty;
        IModelDoc2 pDoc = null;
        public NewProcessLine(string routingId, IModelDoc2 pDoc)
        {
            InitializeComponent();

            this.routingId = routingId;
            this.pDoc = pDoc;

            bcontext = new BusinessCategoryContext(DbConfig.Connection);
            rcontext = new RoutingContext(DbConfig.Connection);

            List<NameValue> nvList = GetNameValues();

            cbxRoutingCategory.DataSource = nvList;
            cbxRoutingCategory.DisplayMember = "Name";
            cbxRoutingCategory.ValueMember = "Id";
            cbxRoutingCategory.SelectedIndex = 0;
        }


        List<NameValue> GetNameValues()
        {

            string rootId = "290000000000000000000000000000000000";

            var qlist = (from t in bcontext.BusinessCategories
                         where t.ObjectOption == 29 && t.CategoryId != rootId
                         select t).ToList<BusinessCategory>();


            List<NameValue> nameValues = new List<NameValue>();

            foreach (var i in qlist)
            {
                /// Recurse find child node
                GetCurrentNodeAllChildNode(qlist, nameValues, i, 1);
                nameValues.Add(new NameValue { Id = i.CategoryId, Name = i.CategoryName });
            }

            return nameValues;
        }
        /// <summary>
        /// Recurse obtain current node all child node
        /// </summary>
        /// <param name="qList">business category list</param>
        /// <param name="nameValues">namevalue list</param>
        /// <param name="currentNode">current node</param>
        void GetCurrentNodeAllChildNode(
            IEnumerable<BusinessCategory> qList,
            List<NameValue> nameValues,
            BusinessCategory currentNode,
            int currentLevel)
        {
            foreach (var i in qList)
            {
                if (currentNode.CategoryId == i.ParentCategory)
                {
                    StringBuilder sb = new StringBuilder();
                    for (int m = 0; m < currentLevel; m++)
                    {
                        sb.Append("--");
                    }
                    ///Recurse find child node
                    GetCurrentNodeAllChildNode(qList, nameValues, i, currentLevel++);
                    nameValues.Add(new NameValue { Id = i.CategoryId, Name = sb.ToString() + i.CategoryName });
                }
            }
        }


        /// <summary>
        /// Category name and id
        /// </summary>
        public class NameValue
        {
            public string Name { get; set; }
            public string Id { get; set; }
        }

        /// <summary>
        /// Confirm add Routing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfrim_Click_1(object sender, EventArgs e)
        {
            if (StringHelper.IsValidEmpty(tbxRoutingName.Text.Trim()))
            {
                MessageBox.Show(UIResource.ROUTINGNAMECANOTNULL);
                return;
            }
            if (StringHelper.IsValidEmpty(tbxRoutingCode.Text.Trim()))
            {
                MessageBox.Show(UIResource.ROUTINGCODECANOTNULL);
                return;
            }

            Routing routing = new Routing();
            routing.RoutingId = Guid.NewGuid().ToString();
            routing.CategoryId = cbxRoutingCategory.SelectedValue.ToString();
            routing.Code = tbxRoutingCode.Text;
            routing.Name = tbxRoutingName.Text;
            routing.CostingType = 0;
            routing.Averageworking = 0;
            routing.Status = 2;
            routing.Creator = "FFB0AC2D-C1B5-49E2-89B2-F4058523DF18";
            routing.CreateDate = DateTime.Now.ToString();
            routing.UpdatePerson = "FFB0AC2D-C1B5-49E2-89B2-F4058523DF18";
            routing.UpdateDate = DateTime.Now.ToString();
            routing.ObjectIconPath = @"../skins/ObjectIcon/assembly.gif";
            routing.StateIconPath = @"../skins/ObjectState/3.gif";
            routing.BatchFrom = 0;
            routing.BatchTo = 0;
            routing.Remark = string.Empty;


            // add component in assembly of solidworks
            MessageBox.Show(pDoc.GetTitle());

            //try
            //{
            //    rcontext.Routings.InsertOnSubmit(routing);
            //    rcontext.SubmitChanges();

            //    MessageBox.Show(UIResource.NEWADDROUTINGSUCESS);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(UIResource.NEWADDROUTINGFAILURE + ex.Message + "!");
            //}
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
    }
}
