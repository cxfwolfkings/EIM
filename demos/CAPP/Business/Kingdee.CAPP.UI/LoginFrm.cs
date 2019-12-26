using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Kingdee.CAPP.UI.Resource;
using System.Configuration;
using System.Data.SqlClient;
using Kingdee.CAPP.Common;

namespace Kingdee.CAPP.UI
{
    public partial class LoginFrm : BaseSkinForm
    {
        public static string UserName { get; set; }

        #region 窗体控件事件

        public LoginFrm()
        {
            InitializeComponent();
            this.MinimunSize = false;
            this.MaximumSize = false;
        }

        private void LoginFrm_Load(object sender, EventArgs e)
        {
            SetComboBoxByConfig();
            if (comboServer.Items.Count > 0)
            {
                comboServer.SelectedIndex = 0;
            }
            if (comboDataBase.Items.Count > 0)
            {
                comboDataBase.SelectedIndex = 0;
            }
            if (comboLoginName.Items.Count > 0)
            {
                comboLoginName.SelectedIndex = 0;
            }
        }        

        private void lblCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 登录按键事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserName.Text))
            {
                MessageBox.Show(GlobalResource.UserNameIsEmpty);
                txtUserName.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show(GlobalResource.PasswordIsEmpty);
                txtPassword.Focus();
                return;
            }

            string userName = txtUserName.Text.Trim();
            string password = userName.ToUpper() + txtPassword.Text.Trim();

            //加密Password
            //string encyPass = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(password, "md5");
            string encyPass = CommonHelper.Md5Hash(password);

            DataTable dt = BLL.SqlServerControllerBLL.GetUserInfo(userName);

            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show(GlobalResource.UserNotExist);
                txtUserName.Focus();
                return;
            }

           string pass = dt.Rows[0]["Password"].ToString().Trim();
            if (pass.Equals(encyPass) )
            {
                DialogResult = DialogResult.OK;
                UserName = txtUserName.Text.Trim();
                Close();
            }
            else
            {
                MessageBox.Show(GlobalResource.PasswordError);
                txtPassword.Clear();
                txtPassword.Focus();
            }            
        }

        /// <summary>
        /// 确认数据连接配置
        /// </summary>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            this.Height = 203;
            btnConfig.Text = "配置>>";

            if (!string.IsNullOrEmpty(comboServer.Text))
            {
                EditAppSettings(CAPP.Common.ComboBoxSourceHelper.SettingKey.Server.ToString(), comboServer.Text);
            }
            if (!string.IsNullOrEmpty(comboDataBase.Text))
            {
                EditAppSettings(CAPP.Common.ComboBoxSourceHelper.SettingKey.Database.ToString(), comboDataBase.Text);
            }
            if (!string.IsNullOrEmpty(comboLoginName.Text))
            {
                EditAppSettings(CAPP.Common.ComboBoxSourceHelper.SettingKey.Uid.ToString(), comboLoginName.Text);
            }
        }

        /// <summary>
        /// 配置
        /// </summary>
        private void btnConfig_Click(object sender, EventArgs e)
        {
            if (btnConfig.Text.Contains(">"))
            {
                Height = 395;
                btnConfig.Text = "配置<<";
            }
            else
            {
                Height = 203;
                btnConfig.Text = "配置>>";
            }
        }

        /// <summary>
        /// 测试连接
        /// </summary>
        private void btnTestConnect_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            string sql = string.Format("server={0};database={1};uid={2};pwd={3};", comboServer.Text, comboDataBase.Text,
                comboLoginName.Text, txtLoginPassword.Text);
            conn.ConnectionString = sql;
            try
            {
                conn.Open();
                MessageBox.Show("数据连接成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("数据连接失败，请检查各项配置输入", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                comboServer.Focus();
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 移动窗体
        /// </summary>
        private void LoginFrm_MouseDown(object sender, MouseEventArgs e)
        {            
            //调用移动无窗体控件函数
            Kingdee.CAPP.Common.CommonHelper.MoveNoneBorderForm(this);
        }

        private void lblTitle_MouseDown(object sender, MouseEventArgs e)
        {
            //调用移动无窗体控件函数
            Kingdee.CAPP.Common.CommonHelper.MoveNoneBorderForm(this);
        }

        private void txtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if ((int)e.KeyCode == 13)
            {
                txtPassword.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if ((int)e.KeyCode == 13)
            {
                btnLogin_Click(null, null);
                e.SuppressKeyPress = true;
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 方法说明：设置下拉框配置
        /// 作      者：jason.tang
        /// 完成时间：2013-04-07
        /// </summary>
        private void SetComboBoxByConfig()
        {
            comboServer.Items.Clear();
            comboDataBase.Items.Clear();
            comboLoginName.Items.Clear();

            // For read access you do not need to call the OpenExeConfiguraton
            foreach (string key in ConfigurationManager.AppSettings)
            {
                string value = ConfigurationManager.AppSettings[key];
                //服务器地址
                if (key.StartsWith(CAPP.Common.ComboBoxSourceHelper.SettingKey.Server.ToString()))
                {
                    comboServer.Items.Add(value);
                }
                //数据库名称
                if (key.StartsWith(CAPP.Common.ComboBoxSourceHelper.SettingKey.Database.ToString()))
                {
                    comboDataBase.Items.Add(value);
                }
                //登录名
                if (key.StartsWith(CAPP.Common.ComboBoxSourceHelper.SettingKey.Uid.ToString()))
                {
                    comboLoginName.Items.Add(value);
                }
            }
        }

        /// <summary>
        /// 方法说明：编辑AppSettings
        /// 作      者：jason.tang
        /// 完成时间：2013-04-07
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        private void EditAppSettings(string key, string value)
        {
            // Open App.Config of executable
            System.Configuration.Configuration config =
              ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            Dictionary<string, bool> dicSettings = CheckValueExist(key, value);
            if(dicSettings == null || dicSettings.Count == 0)
            {
                return;
            }

            foreach (string k in dicSettings.Keys)
            {
                if (!dicSettings[k])
                {
                    // Add an Application Setting.
                    config.AppSettings.Settings.Add(k, value);
                }
                //else
                //{
                //    //Modify an Application Setting.
                //    config.AppSettings.Settings[k].Value = value;
                //}
            }

            // Save the configuration file.
            config.Save(ConfigurationSaveMode.Modified, true);
            // Force a reload of a changed section.
            ConfigurationManager.RefreshSection("appSettings");

            SetComboBoxByConfig();
        }

        /// <summary>
        /// 方法说明：检查对应的值是否已经存在
        /// 作      者：jason.tang
        /// 完成时间：2013-04-07
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        private Dictionary<string, bool> CheckValueExist(string key, string value)
        {
            Dictionary<string, bool> dicSettings = new Dictionary<string, bool>();
            int count = 1;
            foreach (string k in ConfigurationManager.AppSettings)
            {
                string keyValue = ConfigurationManager.AppSettings[k];
                if (k.StartsWith(key))
                {
                    if(value == keyValue)
                    {
                        dicSettings.Add(k, true);
                        return dicSettings;
                    }
                    count++;
                }
            }
            dicSettings.Add(string.Format("{0}{1}",key,count), false);
            return dicSettings;
        }

        #endregion

    }
}
