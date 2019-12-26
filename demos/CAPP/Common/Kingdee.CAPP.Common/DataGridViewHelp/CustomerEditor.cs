using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace Kingdee.CAPP.Common.DataGridViewHelp
{
    /// <summary>
    /// 类型说明：自定义编辑器，用于PropertyGrid子属性框
    /// 作    者：jason.tang
    /// 完成时间：2013-01-10
    /// </summary>
    public class CustomerEditor : UITypeEditor
    {
        public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        /// <summary>
        /// 重写属性编辑值方法
        /// </summary>
        /// <param name="context"></param>
        /// <param name="provider"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, System.IServiceProvider provider, object value)
        {
            if (((Kingdee.CAPP.Common.DataGridViewTextBoxCellEx)(context.Instance)).CellEditType != (ComboBoxSourceHelper.CellType)Enum.Parse(typeof(ComboBoxSourceHelper.CellType),"2"))
            {
                return null;
            }            

            IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
            if (edSvc != null)
            {
                CardDetailPropertyFrm f = new CardDetailPropertyFrm();              
                if (value != null)
                {
                    f.PropertyDetailColumns = value as List<DetailGridViewTextBoxColumn>;
                }
                if (context.Instance != null)
                {
                    f.TotalWidth = ((Kingdee.CAPP.Common.DataGridViewTextBoxCellEx)(context.Instance)).CellWidth;
                }
                edSvc.ShowDialog(f);
                value = f.PropertyDetailColumns;

            }
            return value;
        }

        public override bool GetPaintValueSupported(System.ComponentModel.ITypeDescriptorContext context)
        {
            return false;
        }       

    }
}
