using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace Kingdee.CAPP.Common.DataGridViewHelp
{
    /// <summary>
    /// 类型说明：自定义编辑器，用于来源子属性框
    /// 作    者：jason.tang
    /// 完成时间：2013-01-15
    /// </summary>
    public class SourceEditor : UITypeEditor
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
            IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
            if (edSvc != null)
            {
                SourceFrm f = new SourceFrm();
                if (value != null)
                    f.ParameterSource = value.ToString();
                if (edSvc.ShowDialog(f) == System.Windows.Forms.DialogResult.OK)
                {
                    value = f.ParameterSource;
                }
            }
            return value;
        }

        public override bool GetPaintValueSupported(System.ComponentModel.ITypeDescriptorContext context)
        {
            return false;
        }
    }
}
