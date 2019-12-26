using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Security.Cryptography;

namespace Kingdee.CAPP.Common
{
    /// <summary>
    /// 类型说明：通用帮助类
    /// 作    者：jason.tang
    /// 完成时间：2012-12-20
    /// </summary>
    public static class CommonHelper
    {
        //拖动无无边框的窗体
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;

        /// <summary>
        /// 方法说明：通用的文本框输入校验方法，可以校验转换错误。
        ///          一般在TextBox对象的Leave事件委托函数中调用此方法。
        ///          转换成功返回true，表示输入合法；否则返回false。
        /// 作    者：jason.tang
        /// 完成时间：2012-12-20
        /// </summary>
        /// <param name="tb">要校验的文本框</param>
        /// <param name="allowedType">允许输入的类型</param>
        /// <param name="parseErrorMsg">转换失败的错误提示</param>
        /// <returns></returns>
        public static bool ValidateTextBoxInput(TextBox tb, Type allowedType, string parseErrorMsg)
        {
            string txt = tb.Text;

            //使用反射技术获取目标类型的TryParse方法
            System.Reflection.MethodInfo methodTryParse = allowedType.GetMethod("TryParse", new Type[] { typeof(string), allowedType.MakeByRefType() });
            //实例化一个目标类型对象。TryParse方法需要这样的一个out参数。
            object value = Activator.CreateInstance(allowedType);
            //调用TryParse方法，其返回值标志着是否转换成功。
            object parseSuccess = methodTryParse.Invoke(null, new object[] { txt, value });
            if (Convert.ToBoolean(parseSuccess))//若转换成功，什么也不做
            {
                return true;
            }
            else//若转换失败，提示错误，并使此文本框获得焦点，以便重新输入
            {
                if (!string.IsNullOrEmpty(parseErrorMsg))
                {
                    MessageBox.Show(parseErrorMsg, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tb.Focus();
                }
                return false;
            }

        }

        /// <summary>
        /// 方法说明：校验输入是否为空
        /// 作    者：jason.tang
        /// 完成时间：2012-12-26
        /// </summary>
        /// <param name="controls">控件集合</param>
        /// <returns></returns>
        public static bool CheckNullInput(List<Control> controls)
        {
            foreach (Control control in controls)
            {
                if (control is TextBox)
                {
                    if (string.IsNullOrEmpty(((TextBox)control).Text))
                    {
                        string name = ResourceNotice.ResourceManager.GetString(control.Name.Substring(3));
                        MessageBox.Show(string.Format("{0}不能为空", name), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        control.Focus();
                        return false;
                    }
                }
                else if (control is ComboBox)
                {
                    if (string.IsNullOrEmpty(((ComboBox)control).SelectedItem.ToString()))
                    {
                        string name = ResourceNotice.ResourceManager.GetString(control.Name.Substring(5));
                        MessageBox.Show(string.Format("{0}不能为空", name), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        control.Focus();
                        return false;
                    }
                }
            }
            return true;
        }

        public static void MoveNoneBorderForm(Form form)
        {
            ReleaseCapture();
            SendMessage(form.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        public static string Md5Hash(string input, Guid salt)
        {
            return Md5Hash(string.Format(CultureInfo.InvariantCulture, "{0}{1}", input, salt));
        }

        public static string Md5Hash(string input)
        {
            using (var md5Hash = MD5.Create())
            {
                // Convert the input string to a byte array and compute the hash. 
                var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // BitConverter类将基础数据类型与字节数组相互转换
                // ToString()将指定的字节数组的每个元素的数值转换为它的等效十六进制字符串表示形式(以"-"分隔)。
                return BitConverter.ToString(data).Replace("-", null);
            }
        }
    }
}
