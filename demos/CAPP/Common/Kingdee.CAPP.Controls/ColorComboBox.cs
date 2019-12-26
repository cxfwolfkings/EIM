using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Kingdee.CAPP.Controls
{
    /// <summary>
    /// 控件说明：自定义颜色下拉框控件
    /// 作    者：jason.tang
    /// 完成时间：2012-12-21
    /// </summary>
    public partial class ColorComboBox : ComboBox
    {
        public ColorComboBox()
        {
            InitializeComponent();
            InitItems();
        }

        public ColorComboBox(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
            InitItems();
        }

        /// <summary>
        /// 初始化项目
        /// </summary>
        private void InitItems()
        {
            this.DrawMode = DrawMode.OwnerDrawFixed;//手动绘制所有元素
            this.DropDownStyle = ComboBoxStyle.DropDownList;//下拉框样式设置为不能编辑
            this.Items.Clear();//清空原有项 
        }

        /// <summary>
        /// 重绘项目
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (e.Index >= 0)//判断是否需要重绘
            {
                string colorName = this.Items[e.Index].ToString();//获取颜色名
                SolidBrush brush = new SolidBrush(Color.FromName(colorName));//定义画刷

                Rectangle rect = e.Bounds;
                rect.Inflate(-1, -1);//缩放一定大小

                Rectangle rectColor = new Rectangle(rect.Location, new Size(this.Size.Width - 25, rect.Height));
                //第一个参数不用具体数值，这样做的好处就是下拉框的内容可以根据ComboBox的宽度自己进行调节

                e.Graphics.FillRectangle(brush, rectColor);//填充颜色
                e.Graphics.DrawRectangle(Pens.Black, rectColor);//绘制边框
            }
        }
    }
}
