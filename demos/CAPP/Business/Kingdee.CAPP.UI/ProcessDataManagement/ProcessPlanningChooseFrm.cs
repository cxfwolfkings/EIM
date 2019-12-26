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

namespace Kingdee.CAPP.UI.ProcessDataManagement
{
    public partial class ProcessPlanningChooseFrm : BaseSkinForm
    {
        #region 属性声明

        /// <summary>
        /// 视图类型
        /// </summary>
        public int ViewType { get; set; }

        private List<Model.ProcessPlanning> processPlanningList = new List<ProcessPlanning>();

        #endregion

        public ProcessPlanningChooseFrm()
        {
            InitializeComponent();
        }

        private void ProcessPlanningChooseFrm_Load(object sender, EventArgs e)
        {
            ShowProcessPlanningTreeFromView();
        }

        /// <summary>
        /// 查看时调用， 显示工艺规程和卡片模版树
        /// </summary>
        /// <param name="processCardModules"></param>
        public void ShowProcessPlanningTreeFromView()
        {
            processPlanningList
                = ProcessPlanningBLL.GetProcesPlanningList(null);

            foreach (var pp in processPlanningList)
            {
                TreeNode planningNode = new TreeNode();
                planningNode.Text = pp.Name;
                planningNode.Tag = pp.ProcessPlanningId + "@" + pp.FolderId;                
                planningNode.ImageKey = "planning";
                planningNode.Name = pp.Sort.ToString();
                planningNode.Expand();

                /// 调用
                ShowProcessCardByProcessPlanningId(pp.ProcessPlanningId, planningNode);


                tvProcessProcedure.Nodes.Add(planningNode);
            }
        }

        /// <summary>
        /// Show process card module by process planning Id
        /// 根据工艺规程Id，显示卡片
        /// </summary>
        /// <param name="processPlanningId"></param>
        public void ShowProcessCardByProcessPlanningId(Guid processPlanningId, TreeNode node)
        {
            List<ProcessCard> processCardModules
                = PlanningCardRelationBLL.GetProcessCardListByProcessPlanningId(processPlanningId);

            foreach (var pc in processCardModules)
            {
                TreeNode processCardNode = new TreeNode();
                processCardNode.Text = pc.Name;
                processCardNode.Tag = string.Format("{0}@{1}", pc.ID, pc.CardModuleId);
                processCardNode.ImageKey = "card";
                processCardNode.Name = pc.CardSort.ToString();
                processCardNode.Collapse();

                node.Nodes.Add(processCardNode);
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (tvProcessProcedure.SelectedNode.SelectedImageKey == "card")
            {
                string tag = tvProcessProcedure.SelectedNode.Parent.Tag.ToString();
                string processPlanningId = tag.Substring(0, tag.IndexOf("@"));
                string foderId = tag.Substring(tag.IndexOf("@") + 1);

                List<ProcessCard> processCards
                = PlanningCardRelationBLL.GetProcessCardListByProcessPlanningId(new Guid(processPlanningId));

                string cardId = tvProcessProcedure.SelectedNode.Tag.ToString().Substring(0,tvProcessProcedure.SelectedNode.Tag.ToString().IndexOf("@"));

                ProcessCard card = processCards.FirstOrDefault(c => c.ID == new Guid(cardId));

                if (card != null)
                {
                    //添加节点
                    string baseid = MaterialStructureNavigate.materialNavigateFrm.AddNodeInMaterial(card, foderId);
                    if (string.IsNullOrEmpty(baseid))
                    {
                        return;
                    }
                    MaterialCardRelation materialCardRelation = new MaterialCardRelation();
                    materialCardRelation.MaterialCardRelationId = Guid.NewGuid();
                    materialCardRelation.MaterialId = new Guid(baseid);
                    materialCardRelation.ProcessCardId = card.ID;
                    materialCardRelation.Type = ViewType;
                    Kingdee.CAPP.BLL.MaterialCardRelationBLL.AddMaterialCardRelationData(materialCardRelation);
                }
            }
            else if (tvProcessProcedure.SelectedNode.SelectedImageKey == "planning")
            {
                string tag = tvProcessProcedure.SelectedNode.Tag.ToString();
                string processPlanningId = tag.Substring(0, tag.IndexOf("@"));
                string foderId = tag.Substring(tag.IndexOf("@") + 1);

                List<ProcessCard> processCards
                = PlanningCardRelationBLL.GetProcessCardListByProcessPlanningId(new Guid(processPlanningId));

                if (processCards.Count > 0)
                {
                    foreach (ProcessCard card in processCards)
                    {
                        //添加节点
                        string baseid = MaterialStructureNavigate.materialNavigateFrm.AddNodeInMaterial(card, foderId);
                        if (string.IsNullOrEmpty(baseid))
                        {
                            continue;
                        }
                        MaterialCardRelation materialCardRelation = new MaterialCardRelation();
                        materialCardRelation.MaterialCardRelationId = Guid.NewGuid();
                        materialCardRelation.MaterialId = new Guid(baseid);
                        materialCardRelation.ProcessCardId = card.ID;
                        materialCardRelation.Type = ViewType;
                        Kingdee.CAPP.BLL.MaterialCardRelationBLL.AddMaterialCardRelationData(materialCardRelation);
                    }
                }
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private Point p;
        private void tvProcessProcedure_MouseDown(object sender, MouseEventArgs e)
        {
            p = new Point(e.X, e.Y);
            TreeNode selectedNode = tvProcessProcedure.GetNodeAt(p);
            if (selectedNode == null) return;
            selectedNode.SelectedImageKey = selectedNode.ImageKey;            
        }
    }
}
