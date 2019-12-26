using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using Kingdee.CAPP.Common;
using System.Reflection;
using Kingdee.CAPP.Componect;

namespace Kingdee.CAPP.UI
{
    /// <summary>
    /// 类型说明：单元格属性导航窗体
    /// 作    者：jason.tang
    /// 完成时间：2013-01-07
    /// </summary>
    public partial class PropertiesNavigate : DockContent
    {
        #region 变量声明

        /// <summary>
        /// 定义一个窗体传值的委托
        /// </summary>
        /// <param name="pParam">传递的对象</param>
        public delegate void DelegateProperty(object pParam);
        public event DelegateProperty InvokePropertyEvent;

        /// <summary>
        /// 保存Visible属性名
        /// </summary>
        private List<string> listVisiblePropertyName;

        public static BaseForm CurrentForm { get; set; }

        #endregion

        #region 属性声明

        private Dictionary<string, object> _dicProperties;
        public Dictionary<string, object> PropertyGridValues
        {
            get
            {
                return _dicProperties;
            }
        }

        #endregion

        #region 窗体控件的事件

        protected virtual void OnSetPropertyEvent(object obj)
        {
            if (InvokePropertyEvent != null)
            {
                InvokePropertyEvent(obj);
            }
        }

        /// <summary>
        /// PropertyGrid属性变化时，动态改变单元格的属性
        /// </summary>
        private void pgrdCellProperty_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            object obj = pgrdCellProperty.SelectedObject;
            if (obj != null)
            {
                if (obj.GetType() == typeof(DataGridViewTextBoxCellEx))
                {
                    bool readOnly = ((DataGridViewTextBoxCellEx)obj).CellEditType != (ComboBoxSourceHelper.CellType)Enum.Parse(typeof(ComboBoxSourceHelper.CellType), "0");
                    bool deailReadOnly = ((DataGridViewTextBoxCellEx)obj).CellEditType != (ComboBoxSourceHelper.CellType)Enum.Parse(typeof(ComboBoxSourceHelper.CellType), "2");
                    if (readOnly)
                    {
                        ((DataGridViewTextBoxCellEx)obj).CellContent = (ComboBoxSourceHelper.CellContent)Enum.Parse(typeof(ComboBoxSourceHelper.CellContent), "0");
                    }

                    //固定单元格时显示内容选项
                    PropertyDescriptorCollection props = TypeDescriptor.GetProperties(obj);
                    foreach (PropertyDescriptor pd in props)
                    {
                        if (pd.Name == "LeftTopRightBottom" || pd.Name == "CellContent" ||
                            pd.Name == "LeftBottomRightTop")
                        {
                            SetPropertyReadOnly(obj, pd.Name, readOnly);
                        }

                        if (pd.Name == "SerialStep" || pd.Name == "SpaceRows")
                        {
                            SetPropertyReadOnly(obj, pd.Name, deailReadOnly);
                        }
                    }

                    #region 如果设置了宽度，默认给设置颜色值

                    Color color = ((DataGridViewTextBoxCellEx)obj).TopBorderColor;
                    int width = ((DataGridViewTextBoxCellEx)obj).TopBorderWidth;
                    if (e.ChangedItem.Label == "上边框宽度")
                    {
                        if ((color == Color.Empty || color == Color.FromArgb(0, 0, 0, 0)) && width > 0)
                        {
                            ((DataGridViewTextBoxCellEx)obj).TopBorderColor = Color.Black;
                        }
                        else if (width == 0)
                        {
                            ((DataGridViewTextBoxCellEx)obj).TopBorderColor = Color.Empty;
                        }
                    }
                    if (e.ChangedItem.Label == "下边框宽度")
                    {
                        color = ((DataGridViewTextBoxCellEx)obj).BottomBorderColor;
                        width = ((DataGridViewTextBoxCellEx)obj).BottomBorderWidth;
                        if ((color == Color.Empty || color == Color.FromArgb(0, 0, 0, 0)) && width > 0)
                        {
                            ((DataGridViewTextBoxCellEx)obj).BottomBorderColor = Color.Black;
                        }
                        else if (width == 0)
                        {
                            ((DataGridViewTextBoxCellEx)obj).BottomBorderColor = Color.Empty;
                        }
                    }
                    if (e.ChangedItem.Label == "左边框宽度")
                    {
                        color = ((DataGridViewTextBoxCellEx)obj).LeftBorderColor;
                        width = ((DataGridViewTextBoxCellEx)obj).LeftBorderWidth;
                        if ((color == Color.Empty || color == Color.FromArgb(0, 0, 0, 0)) && width > 0)
                        {
                            ((DataGridViewTextBoxCellEx)obj).LeftBorderColor = Color.Black;
                        }
                        else if (width == 0)
                        {
                            ((DataGridViewTextBoxCellEx)obj).LeftBorderColor = Color.Empty;
                        }
                    }
                    if (e.ChangedItem.Label == "右边框宽度")
                    {
                        color = ((DataGridViewTextBoxCellEx)obj).RightBorderColor;
                        width = ((DataGridViewTextBoxCellEx)obj).RightBorderWidth;
                        if ((color == Color.Empty || color == Color.FromArgb(0, 0, 0, 0)) && width > 0)
                        {
                            ((DataGridViewTextBoxCellEx)obj).RightBorderColor = Color.Black;
                        }
                        else if (width == 0)
                        {
                            ((DataGridViewTextBoxCellEx)obj).RightBorderColor = Color.Empty;
                        }
                    }

                    #endregion

                    #region 如果设置了线条样式，默认设置颜色和宽度

                    if (e.ChangedItem.Label == "上边框线条样式")
                    {
                        color = ((DataGridViewTextBoxCellEx)obj).TopBorderColor;
                        width = ((DataGridViewTextBoxCellEx)obj).TopBorderWidth;
                        if ((color == Color.Empty || color == Color.FromArgb(0, 0, 0, 0)))
                        {
                            ((DataGridViewTextBoxCellEx)obj).TopBorderColor = Color.Black;
                        }
                        if (width == 0)
                        {
                            ((DataGridViewTextBoxCellEx)obj).TopBorderWidth = 1;
                        }
                    }

                    if (e.ChangedItem.Label == "下边框线条样式")
                    {
                        color = ((DataGridViewTextBoxCellEx)obj).BottomBorderColor;
                        width = ((DataGridViewTextBoxCellEx)obj).BottomBorderWidth;
                        if ((color == Color.Empty || color == Color.FromArgb(0, 0, 0, 0)))
                        {
                            ((DataGridViewTextBoxCellEx)obj).BottomBorderColor = Color.Black;
                        }
                        if (width == 0)
                        {
                            ((DataGridViewTextBoxCellEx)obj).BottomBorderWidth = 1;
                        }
                    }

                    if (e.ChangedItem.Label == "左边框线条样式")
                    {
                        color = ((DataGridViewTextBoxCellEx)obj).LeftBorderColor;
                        width = ((DataGridViewTextBoxCellEx)obj).LeftBorderWidth;
                        if ((color == Color.Empty || color == Color.FromArgb(0, 0, 0, 0)))
                        {
                            ((DataGridViewTextBoxCellEx)obj).LeftBorderColor = Color.Black;
                        }
                        if (width == 0)
                        {
                            ((DataGridViewTextBoxCellEx)obj).LeftBorderWidth = 1;
                        }
                    }

                    if (e.ChangedItem.Label == "右边框线条样式")
                    {
                        color = ((DataGridViewTextBoxCellEx)obj).RightBorderColor;
                        width = ((DataGridViewTextBoxCellEx)obj).RightBorderWidth;
                        if ((color == Color.Empty || color == Color.FromArgb(0, 0, 0, 0)))
                        {
                            ((DataGridViewTextBoxCellEx)obj).RightBorderColor = Color.Black;
                        }
                        if (width == 0)
                        {
                            ((DataGridViewTextBoxCellEx)obj).RightBorderWidth = 1;
                        }
                    }

                    #endregion

                    #region 如果设置了颜色，默认给宽度设置值

                    color = ((DataGridViewTextBoxCellEx)obj).TopBorderColor;
                    width = ((DataGridViewTextBoxCellEx)obj).TopBorderWidth;
                    if (color != Color.Empty && width == 0)
                    {
                        ((DataGridViewTextBoxCellEx)obj).TopBorderWidth = 1;
                    }
                    else if (color.IsEmpty)
                    {
                        ((DataGridViewTextBoxCellEx)obj).TopBorderWidth = 0;
                    }
                    color = ((DataGridViewTextBoxCellEx)obj).LeftBorderColor;
                    width = ((DataGridViewTextBoxCellEx)obj).LeftBorderWidth;
                    if (color != Color.Empty && width == 0)
                    {
                        ((DataGridViewTextBoxCellEx)obj).LeftBorderWidth = 1;
                    }
                    else if (color.IsEmpty)
                    {
                        ((DataGridViewTextBoxCellEx)obj).LeftBorderWidth = 0;
                    }
                    color = ((DataGridViewTextBoxCellEx)obj).RightBorderColor;
                    width = ((DataGridViewTextBoxCellEx)obj).RightBorderWidth;
                    if (color != Color.Empty && width == 0)
                    {
                        ((DataGridViewTextBoxCellEx)obj).RightBorderWidth = 1;
                    }
                    else if (color.IsEmpty)
                    {
                        ((DataGridViewTextBoxCellEx)obj).RightBorderWidth = 0;
                    }
                    color = ((DataGridViewTextBoxCellEx)obj).BottomBorderColor;
                    width = ((DataGridViewTextBoxCellEx)obj).BottomBorderWidth;
                    if (color != Color.Empty && width == 0)
                    {
                        ((DataGridViewTextBoxCellEx)obj).BottomBorderWidth = 1;
                    }
                    else if (color.IsEmpty)
                    {
                        ((DataGridViewTextBoxCellEx)obj).BottomBorderWidth = 0;
                    }

                    color = ((DataGridViewTextBoxCellEx)obj).AllBorderColor;
                    width = ((DataGridViewTextBoxCellEx)obj).AllBorderWidth;
                    System.Drawing.Drawing2D.DashStyle dashstyle = ((DataGridViewTextBoxCellEx)obj).AllBorderStyle;
                    //if (color != Color.Empty && color != Color.FromArgb(0,0,0,0) && width == 0)
                    //{
                    //    ((DataGridViewTextBoxCellEx)obj).AllBorderWidth = 1;
                    //    ((DataGridViewTextBoxCellEx)obj).AllBorderColor = Color.Empty;
                    //    ((DataGridViewTextBoxCellEx)obj).AllBorderStyle = System.Drawing.Drawing2D.DashStyle.Solid;

                    //    color = Color.Empty;
                    //    width = 1;
                    //    dashstyle = System.Drawing.Drawing2D.DashStyle.Solid;
                    //}
                    //else if (color.IsEmpty || color == Color.FromArgb(0, 0, 0, 0))
                    //{
                    //    if (width > 0)
                    //    {
                    //        ((DataGridViewTextBoxCellEx)obj).AllBorderColor = Color.Black;
                    //        color = Color.Black;
                    //    }
                    //    else
                    //    {
                    //        ((DataGridViewTextBoxCellEx)obj).AllBorderWidth = 0;
                    //        width = 0;
                    //    }
                    //}

                    if (e.ChangedItem.PropertyDescriptor.Category == "全边框")
                    {
                        if (e.OldValue.GetType() == typeof(Color) &&
                            (Color)e.OldValue != color)
                        {
                            ((DataGridViewTextBoxCellEx)obj).TopBorderColor = color;
                            ((DataGridViewTextBoxCellEx)obj).LeftBorderColor = color;
                            ((DataGridViewTextBoxCellEx)obj).RightBorderColor = color;
                            ((DataGridViewTextBoxCellEx)obj).BottomBorderColor = color;
                                                        
                            if (color != Color.Empty && width == 0)
                            {
                                ((DataGridViewTextBoxCellEx)obj).TopBorderWidth = 1;
                            }
                            else if (color.IsEmpty)
                            {
                                ((DataGridViewTextBoxCellEx)obj).TopBorderWidth = 0;
                            }
                            
                            if (color != Color.Empty && width == 0)
                            {
                                ((DataGridViewTextBoxCellEx)obj).LeftBorderWidth = 1;
                            }
                            else if (color.IsEmpty)
                            {
                                ((DataGridViewTextBoxCellEx)obj).LeftBorderWidth = 0;
                            }
                            
                            if (color != Color.Empty && width == 0)
                            {
                                ((DataGridViewTextBoxCellEx)obj).RightBorderWidth = 1;
                            }
                            else if (color.IsEmpty)
                            {
                                ((DataGridViewTextBoxCellEx)obj).RightBorderWidth = 0;
                            }
                            
                            if (color != Color.Empty && width == 0)
                            {
                                ((DataGridViewTextBoxCellEx)obj).BottomBorderWidth = 1;
                            }
                            else if (color.IsEmpty)
                            {
                                ((DataGridViewTextBoxCellEx)obj).BottomBorderWidth = 0;
                            }

                            if (color != Color.Empty && width == 0)
                            {
                                ((DataGridViewTextBoxCellEx)obj).AllBorderWidth = 1;
                            }
                            else if (color.IsEmpty)
                            {
                                ((DataGridViewTextBoxCellEx)obj).AllBorderWidth = 0;
                            }                            
                        }

                        if (e.OldValue.GetType() == typeof(int) &&
                            (int)e.OldValue != width)
                        {                         
                            ((DataGridViewTextBoxCellEx)obj).TopBorderWidth = width;
                            ((DataGridViewTextBoxCellEx)obj).LeftBorderWidth = width;
                            ((DataGridViewTextBoxCellEx)obj).RightBorderWidth = width;
                            ((DataGridViewTextBoxCellEx)obj).BottomBorderWidth = width;

                            color = ((DataGridViewTextBoxCellEx)obj).TopBorderColor;
                            if ((color == Color.Empty || color == Color.FromArgb(0, 0, 0, 0)))
                            {
                                ((DataGridViewTextBoxCellEx)obj).TopBorderColor = Color.Black;
                            }
                            if (width == 0)
                            {
                                ((DataGridViewTextBoxCellEx)obj).TopBorderColor = Color.Empty;
                                ((DataGridViewTextBoxCellEx)obj).TopBorderWidth = 1;
                            }

                            color = ((DataGridViewTextBoxCellEx)obj).BottomBorderColor;
                            if ((color == Color.Empty || color == Color.FromArgb(0, 0, 0, 0)))
                            {
                                ((DataGridViewTextBoxCellEx)obj).BottomBorderColor = Color.Black;
                            }
                            if (width == 0)
                            {
                                ((DataGridViewTextBoxCellEx)obj).BottomBorderColor = Color.Empty;
                                ((DataGridViewTextBoxCellEx)obj).BottomBorderWidth = 1;
                            }

                            color = ((DataGridViewTextBoxCellEx)obj).LeftBorderColor;
                            if ((color == Color.Empty || color == Color.FromArgb(0, 0, 0, 0)))
                            {
                                ((DataGridViewTextBoxCellEx)obj).LeftBorderColor = Color.Black;
                            }
                            if (width == 0)
                            {
                                ((DataGridViewTextBoxCellEx)obj).LeftBorderColor = Color.Empty;
                                ((DataGridViewTextBoxCellEx)obj).LeftBorderWidth = 1;
                            }

                            color = ((DataGridViewTextBoxCellEx)obj).RightBorderColor;
                            if ((color == Color.Empty || color == Color.FromArgb(0, 0, 0, 0)))
                            {
                                ((DataGridViewTextBoxCellEx)obj).RightBorderColor = Color.Black;
                            }
                            if (width == 0)
                            {
                                ((DataGridViewTextBoxCellEx)obj).RightBorderColor = Color.Empty;
                                ((DataGridViewTextBoxCellEx)obj).RightBorderWidth = 1;
                            }

                            color = ((DataGridViewTextBoxCellEx)obj).AllBorderColor;
                            if ((color == Color.Empty || color == Color.FromArgb(0, 0, 0, 0)))
                            {
                                ((DataGridViewTextBoxCellEx)obj).AllBorderColor = Color.Black;
                            }
                            if (width == 0)
                            {
                                ((DataGridViewTextBoxCellEx)obj).AllBorderColor = Color.Empty;
                                ((DataGridViewTextBoxCellEx)obj).AllBorderWidth = 1;
                            }
                        }

                        if (e.OldValue.GetType() == typeof(System.Drawing.Drawing2D.DashStyle) &&
                            (System.Drawing.Drawing2D.DashStyle)e.OldValue != dashstyle)
                        {
                            ((DataGridViewTextBoxCellEx)obj).TopBorderStyle = dashstyle;
                            ((DataGridViewTextBoxCellEx)obj).LeftBorderStyle = dashstyle;
                            ((DataGridViewTextBoxCellEx)obj).RightBorderStyle = dashstyle;
                            ((DataGridViewTextBoxCellEx)obj).BottomBorderStyle = dashstyle;

                            color = ((DataGridViewTextBoxCellEx)obj).TopBorderColor;
                            width = ((DataGridViewTextBoxCellEx)obj).TopBorderWidth;
                            if ((color == Color.Empty || color == Color.FromArgb(0, 0, 0, 0)))
                            {
                                ((DataGridViewTextBoxCellEx)obj).TopBorderColor = Color.Black;
                            }
                            if (width == 0)
                            {
                                ((DataGridViewTextBoxCellEx)obj).TopBorderWidth = 1;
                            }

                            color = ((DataGridViewTextBoxCellEx)obj).BottomBorderColor;
                            width = ((DataGridViewTextBoxCellEx)obj).BottomBorderWidth;
                            if ((color == Color.Empty || color == Color.FromArgb(0, 0, 0, 0)))
                            {
                                ((DataGridViewTextBoxCellEx)obj).BottomBorderColor = Color.Black;
                            }
                            if (width == 0)
                            {
                                ((DataGridViewTextBoxCellEx)obj).BottomBorderWidth = 1;
                            }

                            color = ((DataGridViewTextBoxCellEx)obj).LeftBorderColor;
                            width = ((DataGridViewTextBoxCellEx)obj).LeftBorderWidth;
                            if ((color == Color.Empty || color == Color.FromArgb(0, 0, 0, 0)))
                            {
                                ((DataGridViewTextBoxCellEx)obj).LeftBorderColor = Color.Black;
                            }
                            if (width == 0)
                            {
                                ((DataGridViewTextBoxCellEx)obj).LeftBorderWidth = 1;
                            }

                            color = ((DataGridViewTextBoxCellEx)obj).RightBorderColor;
                            width = ((DataGridViewTextBoxCellEx)obj).RightBorderWidth;
                            if ((color == Color.Empty || color == Color.FromArgb(0, 0, 0, 0)))
                            {
                                ((DataGridViewTextBoxCellEx)obj).RightBorderColor = Color.Black;
                            }
                            if (width == 0)
                            {
                                ((DataGridViewTextBoxCellEx)obj).RightBorderWidth = 1;
                            }

                            color = ((DataGridViewTextBoxCellEx)obj).AllBorderColor;
                            width = ((DataGridViewTextBoxCellEx)obj).AllBorderWidth;
                            if ((color == Color.Empty || color == Color.FromArgb(0, 0, 0, 0)))
                            {
                                ((DataGridViewTextBoxCellEx)obj).AllBorderColor = Color.Black;
                            }
                            if (width == 0)
                            {
                                ((DataGridViewTextBoxCellEx)obj).AllBorderWidth = 1;
                            }
                        }
                    }

                    #endregion
                }
                else if (obj.GetType() == typeof(DataGridViewCustomerCellStyle))
                {
                    PropertyDescriptorCollection props = TypeDescriptor.GetProperties(obj);
                    foreach (PropertyDescriptor pd in props)
                    {
                        if (pd.Name == "CardName")
                        {
                            SetPropertyVisibility(obj, pd.Name, true);
                        }
                    }
                }
                pgrdCellProperty.SelectedObject = obj;
            }

            ////如果当前事件不为空，则注销该事件，防止多次注册事件
            //if (this.InvokePropertyEvent != null)
            //{
            //    this.InvokePropertyEvent = null;
            //}

            //注册事件，并传入当前窗体的委托
            if (this.InvokePropertyEvent == null)
            {
                this.InvokePropertyEvent += new DelegateProperty(CurrentForm.SetPropertyEvent);
            }
            else
            {
                this.InvokePropertyEvent = null;
                //this.InvokePropertyEvent -= new DelegateProperty(CurrentForm.SetPropertyEvent);
                this.InvokePropertyEvent += new DelegateProperty(CurrentForm.SetPropertyEvent);
            }
            OnSetPropertyEvent(pgrdCellProperty.SelectedObject);
        }

        /// <summary>
        /// 窗体Load事件
        /// </summary>
        private void PropertiesNavigate_Load(object sender, EventArgs e)
        {
            #region 列举DataGridViewTextBoxCellEx的所有自定义属性

            listVisiblePropertyName = new List<string>();
            listVisiblePropertyName.Add("CellEditType");
            listVisiblePropertyName.Add("CellContent");
            listVisiblePropertyName.Add("CellTag");
            //listPropertyName.Add("CellBorderLeft");
            //listPropertyName.Add("CellBorderTop");
            //listPropertyName.Add("CellBorderRight");
            //listPropertyName.Add("CellBorderBottom");
            listVisiblePropertyName.Add("LeftBorderColor");
            listVisiblePropertyName.Add("TopBorderColor");
            listVisiblePropertyName.Add("RightBorderColor");
            listVisiblePropertyName.Add("BottomBorderColor");
            listVisiblePropertyName.Add("LeftBorderWidth");
            listVisiblePropertyName.Add("TopBorderWidth");
            listVisiblePropertyName.Add("RightBorderWidth");
            listVisiblePropertyName.Add("BottomBorderWidth");
            listVisiblePropertyName.Add("LeftBorderStyle");
            listVisiblePropertyName.Add("TopBorderStyle");
            listVisiblePropertyName.Add("RightBorderStyle");
            listVisiblePropertyName.Add("BottomBorderStyle");
            listVisiblePropertyName.Add("DetailProperty");
            listVisiblePropertyName.Add("LeftTopRightBottom");
            listVisiblePropertyName.Add("LeftBottomRightTop");
            listVisiblePropertyName.Add("AllBorderColor");
            listVisiblePropertyName.Add("AllBorderStyle");
            listVisiblePropertyName.Add("AllBorderWidth");
            listVisiblePropertyName.Add("CellSource");

            listVisiblePropertyName.Add("SerialStep");
            listVisiblePropertyName.Add("SpaceRows");

            #endregion
        }
        
        #endregion

        #region 方法

        /// <summary>
        /// 方法说明：从单元格的属性设置PropertyGrid
        /// 作    者：jason.tang
        /// 完成时间：2013-01-10
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="multiselect">是否多个对象</param>
        public void SetPropertyGrid(object obj, bool multiselect, bool cardOrModule)
        {
            try
            {
                if (obj != null)
                {
                    if (!cardOrModule)
                    {
                        bool readOnly = ((DataGridViewTextBoxCellEx)obj).CellEditType != (ComboBoxSourceHelper.CellType)Enum.Parse(typeof(ComboBoxSourceHelper.CellType), "0");
                        bool deailReadOnly = ((DataGridViewTextBoxCellEx)obj).CellEditType != (ComboBoxSourceHelper.CellType)Enum.Parse(typeof(ComboBoxSourceHelper.CellType), "2");
                        //多选时，只显示单元格基本样式属性
                        PropertyDescriptorCollection props = TypeDescriptor.GetProperties(obj);
                        foreach (PropertyDescriptor pd in props)
                        {
                            if (pd.Name != "CustStyle" && pd.Name != "CellEditType" && PropertyExists(pd.Name))
                            {
                                SetPropertyVisibility(obj, pd.Name, !multiselect);
                            }

                            if (pd.Name == "LeftTopRightBottom" || pd.Name == "CellContent" ||
                                pd.Name == "LeftBottomRightTop")
                            {
                                SetPropertyReadOnly(obj, pd.Name, readOnly);
                            }

                            if (pd.Name == "SerialStep" || pd.Name == "SpaceRows")
                            {
                                SetPropertyReadOnly(obj, pd.Name, deailReadOnly);
                            }                            
                        }
                    }
                    else
                    {
                        PropertyDescriptorCollection props = TypeDescriptor.GetProperties(obj);
                        int type = 0;
                        if (obj.GetType() == typeof(DataGridViewCustomerCellStyle))
                        {
                            type = ((DataGridViewCustomerCellStyle)obj).CellType;
                        }
                        foreach (PropertyDescriptor pd in props)
                        {
                            if (pd.Name == "CardName")
                            {
                                SetPropertyVisibility(obj, pd.Name, true);
                            }

                            if (type == 2)
                            {
                                if (pd.Name == "Alignment")
                                {
                                    SetPropertyReadOnly(obj, pd.Name, true);
                                }
                                if (pd.Name == "RichAlignment")
                                {
                                    SetPropertyReadOnly(obj, pd.Name, false);
                                }
                            }

                            else if (type == 1)
                            {
                                if (pd.Name == "Alignment")
                                {
                                    SetPropertyReadOnly(obj, pd.Name, false);
                                }
                                if (pd.Name == "RichAlignment")
                                {
                                    SetPropertyReadOnly(obj, pd.Name, true);
                                }
                            }
                        }
                    }
                }
                pgrdCellProperty.SelectedObject = obj;
            }
            catch
            { }
        }

        /// <summary>
        /// 方法说明：提供给外围主窗体调用
        /// 作    者：jason.tang
        /// 完成时间：2013-01-11
        /// </summary>
        public void CellOperator(object merge)
        {
            if (pgrdCellProperty.SelectedObject != null)
            {
                ((DataGridViewTextBoxCellEx)pgrdCellProperty.SelectedObject).Merge = (int)merge;

                //如果当前事件不为空，则注销该事件，防止多次注册事件
                if (this.InvokePropertyEvent != null)
                {
                    this.InvokePropertyEvent = null;
                }

                
                //注册事件，并传入当前窗体的委托
                this.InvokePropertyEvent += new DelegateProperty(CurrentForm.SetPropertyEvent);
                OnSetPropertyEvent(pgrdCellProperty.SelectedObject);
            }
            //pgrdCellProperty_PropertyValueChanged(pgrdCellProperty, null);

        }

        /// <summary>
        /// 方法说明：在自定义属性中是否存在
        /// 作    者：jason.tang
        /// 完成时间：2013-01-11
        /// </summary>
        /// <param name="propertyName">属性名</param>
        /// <returns>True/False</returns>
        private bool PropertyExists(string propertyName)
        {
            if (listVisiblePropertyName.Contains(propertyName))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 方法说明：动态设置指定属性是否只读
        /// 作者：jason.tang
        /// 完成时间：2013-01-10
        /// </summary>
        /// <param name="obj">当前对象</param>
        /// <param name="propertyName">属性名</param>
        /// <param name="readOnly">是否只读</param>
        private void SetPropertyReadOnly(object obj, string propertyName, bool readOnly)
        {
            Type type = typeof(System.ComponentModel.ReadOnlyAttribute);
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(obj);
            AttributeCollection attrs = props[propertyName].Attributes;
            FieldInfo fld = type.GetField("isReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
            fld.SetValue(attrs[type], readOnly);          
        }

        /// <summary>
        /// 方法说明：动态设置指定属性是否可见
        /// 作者：jason.tang
        /// 完成时间：2013-01-11
        /// </summary>
        /// <param name="obj">当前对象</param>
        /// <param name="propertyName">属性名</param>
        /// <param name="visible">是否可见</param>
        private void SetPropertyVisibility(object obj, string propertyName, bool visible)
        {
            Type type = typeof(BrowsableAttribute);
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(obj);
            AttributeCollection attrs = props[propertyName].Attributes;
            FieldInfo fld = type.GetField("browsable", BindingFlags.Instance | BindingFlags.NonPublic);
            fld.SetValue(attrs[type], visible);
        } 

        #endregion

    }
}
