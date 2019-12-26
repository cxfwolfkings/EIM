using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Kingdee.CAPP.UI.ProcessDataManagement
{
    /// <summary>
    /// 类型说明：OLE对象选择窗体
    /// 作    者：jason.tang
    /// 完成时间：2013-02-25
    /// </summary>
    public partial class OleObjectSelectionFrm : BaseSkinForm
    {
        #region 属性声明

        /// <summary>
        /// OLE对象类型
        /// </summary>
        private string _type;
        public string OleType
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
            }
        }

        /// <summary>
        /// 文件名
        /// </summary>
        private string filename;
        public string FileName
        {
            get
            {
                return filename;
            }
            set
            {
                filename = value;
            }
        }

        #endregion

        #region 窗体控件事件

        public OleObjectSelectionFrm()
        {
            InitializeComponent();
        }

        private void OleObjectSelectionFrm_Load(object sender, EventArgs e)
        {
            Dictionary<string, string>  dicOleTypes = new Dictionary<string,string>();
            dicOleTypes.Add("AutoCad", "Auto CAD Drawing");
            //dicOleTypes.Add("AutoVue", "AutoVue Document");
            //dicOleTypes.Add("Graph", "Microsoft Graph 图表");
            //dicOleTypes.Add("ExcelSheet", "Microsoft Office Excel 工作表");
            //dicOleTypes.Add("ExcelGraph", "Microsoft Office Excel 图表");
            //dicOleTypes.Add("PPT", "Microsoft Power Point 幻灯片");
            //dicOleTypes.Add("PptDoc", "Microsoft Power Point 演示文稿");
            //dicOleTypes.Add("WordImage", "Microsoft Office Word 图片");
            //dicOleTypes.Add("WordDoc", "Microsoft Office Word 文档");
            //dicOleTypes.Add("Midi", "MIDI 序列");
            //dicOleTypes.Add("Think3", "think 3 think design");
            //dicOleTypes.Add("Xtdoc", "XTDoc13 Document");
            //dicOleTypes.Add("Xtpcad", "Xtpcad Document");
            //dicOleTypes.Add("Xtpdmp", "XTPDMP Document");
            //dicOleTypes.Add("Paint", "画笔图片");
            //dicOleTypes.Add("Media", "媒体剪辑");
            //dicOleTypes.Add("Vedio", "视频剪辑");
            dicOleTypes.Add("Bitmap", "位图图像");
            dicOleTypes.Add("AVI", "视频动画");
            //dicOleTypes.Add("Notpad", "写字板文档");
            //dicOleTypes.Add("Music", "音效");

            lbObjectType.DisplayMember = "Value";
            lbObjectType.ValueMember = "Key";

            lbObjectType.DataSource = new BindingSource(dicOleTypes, null);
        }

        /// <summary>
        /// 显示为图标
        /// </summary>
        private void cbDisplayAsIco_CheckedChanged(object sender, EventArgs e)
        {
            pbOle.Image = cbDisplayAsIco.Checked ? Properties.Resources.ole_ico : Properties.Resources.ole;
        }

        /// <summary>
        /// 链接到文件
        /// </summary>
        private void cbLinkToFile_CheckedChanged(object sender, EventArgs e)
        {
            SetImage();
        }

        /// <summary>
        /// 显示为图标
        /// </summary>
        private void cbDisplayAsIcon_CheckedChanged(object sender, EventArgs e)
        {
            SetImage();
        }

        /// <summary>
        /// 浏览
        /// </summary>
        private void btnBrowser_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "JPEG(*.jpg)|*.jpg|Bitmap(*.bmp)|*.bmp|CAD文件(*.dwg)|*.dwg|视频动画(*.avi)|*.avi";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtFileName.Text = dialog.FileName;
            }
        }

        /// <summary>
        /// 确认
        /// </summary>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            if (tcOleObject.SelectedTab == tpCreate)
            {
                _type = lbObjectType.SelectedValue.ToString();
                filename = null;
            }
            else
            {
                _type = null;
                filename = txtFileName.Text;

                if(filename.EndsWith("dwg"))
                {
                    _type = "autocad";
                }
                else if(filename.EndsWith("avi"))
                {
                    _type = "avi";
                }
                else if(filename.EndsWith("bmp") || filename.EndsWith("jpg"))
                {
                    _type = "bitmap";
                }
            }
            this.Close();
        }

        /// <summary>
        /// 取消
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// ListBox选择项变化
        /// </summary>
        private void lbObjectType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 方法说明：设置不同情形下的图标
        /// 作    者：jason.tang
        /// 完成时间：2013-02-25
        /// </summary>
        private void SetImage()
        {
            if (cbLinkToFile.Checked)
            {
                pbLink.Image = cbDisplayAsIcon.Checked ? Properties.Resources.olelink : Properties.Resources.olelink_ico;
            }
            else
            {
                pbLink.Image = cbDisplayAsIcon.Checked ? Properties.Resources.ole_ico : Properties.Resources.ole;
            }
        }

        #endregion
    }
}
