using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace Kingdee.CAPP.Controls
{
    /// <summary>
    /// 类型说明：自定义MenuStrip的样式
    /// 作      者：jason.tang
    /// 完成时间：2013-07-16
    /// </summary>
    public class CustomMenuRender : ToolStripProfessionalRenderer
    {
        ColorConfig colorconfig = new ColorConfig();//创建颜色配置类
        /// <summary>
        /// 渲染整个背景
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            //如果是下拉
            if (e.ToolStrip is ToolStripDropDown)
            {
                e.Graphics.FillRectangle(new SolidBrush(colorconfig.DropDownItemBackColor), e.AffectedBounds);
                e.ToolStrip.ForeColor = colorconfig.MarginFontColor;
            }
            //如果是菜单项
            else if (e.ToolStrip is MenuStrip)
            {
                e.ToolStrip.ForeColor = colorconfig.FontColor;
                /*
                    渐变常用于使形状内部平滑染色。 
                    混合图案是由两个数组（Factors 和 Positions）定义的，每一数组都包含相同的元素数目。 
                    每一 Positions 数组的每一元素都代表沿渐变线的距离的比例。 
                    Factors 数组的每一元素都表示在沿渐变线位置处（由 Positions 数组中的相应元素表示）的渐变混合中起始色和结束色的比例。
                    例如，如果 Positions 和 Factors 数组的相应元素分别为 0.2 和 0.3，
                    对于从蓝到红沿 100 像素线的线性渐变，沿该直线 20 像素处的颜色（该距离的 20%）由 30% 的蓝和 70% 的红组成。
                 */
                Blend blend = new Blend();
                float[] fs = new float[5] { 0f, 0.3f, 0.5f, 0.8f, 1f };
                float[] f = new float[5] { 0f, 0.5f, 0.9f, 0.5f, 0f };
                blend.Positions = fs;
                blend.Factors = f;
                //FillLineGradient(e.Graphics, e.AffectedBounds, colorconfig.MainMenuStartColor, colorconfig.MainMenuEndColor, 90f, blend);
            }
            else
            {
                base.OnRenderToolStripBackground(e);
            }
        }
        /// <summary>
        /// 渲染下拉左侧图标区域
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
        {
            FillLineGradient(e.Graphics, e.AffectedBounds, colorconfig.MarginStartColor, colorconfig.MarginEndColor, 0f, null);
        }
        /// <summary>
        /// 渲染菜单项的背景
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            if (e.ToolStrip is MenuStrip)
            {
                //如果被选中或被按下
                if (e.Item.Selected || e.Item.Pressed)
                {
                    Blend blend = new Blend();
                    float[] fs = new float[5] { 0f, 0.3f, 0.5f, 0.8f, 1f };
                    float[] f = new float[5] { 0f, 0.5f, 1f, 0.5f, 0f };
                    blend.Positions = fs;
                    blend.Factors = f;
                    DrawRectangle(e.Graphics, new Rectangle(0, 0, e.Item.Size.Width, e.Item.Size.Height));
                    e.Item.ForeColor = Color.Black;
                }
                else
                {
                    base.OnRenderMenuItemBackground(e);
                    e.Item.ForeColor = Color.Black;
                }
            }
            else if (e.ToolStrip is ToolStripDropDown)
            {
                if (e.Item.Selected)
                {
                    FillLineGradient(e.Graphics, new Rectangle(0, 0, e.Item.Size.Width, e.Item.Size.Height), colorconfig.DropDownItemStartColor, colorconfig.DropDownItemEndColor, 90f, null);
                }
            }
            else
            {
                base.OnRenderMenuItemBackground(e);
                //如果被选中或被按下
                if (e.Item.Selected || e.Item.Pressed)
                {
                    Blend blend = new Blend();
                    float[] fs = new float[5] { 0f, 0.3f, 0.5f, 0.8f, 1f };
                    float[] f = new float[5] { 0f, 0.5f, 1f, 0.5f, 0f };
                    blend.Positions = fs;
                    blend.Factors = f;
                    DrawRectangle(e.Graphics, new Rectangle(0, 0, e.Item.Size.Width, e.Item.Size.Height));
                    e.Item.ForeColor = Color.Black;
                }
                else
                {
                    base.OnRenderMenuItemBackground(e);
                    e.Item.ForeColor = Color.Black;
                }
            }
        }

        /// <summary>
        /// 渲染菜单项的分隔线
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            if (e.ToolStrip is ToolStripDropDown)
            {
                e.Graphics.DrawLine(new Pen(colorconfig.SeparatorColor), 0, 2, e.Item.Width, 2);
            }
            else
            {
                //base.OnRenderSeparator(e);
                e.Graphics.DrawLine(new Pen(colorconfig.SeparatorColor), 0, 2, 0, e.Item.Height - 4);
            }
        }
        /// <summary>
        /// 渲染边框
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            if (e.ToolStrip is ToolStripDropDown)
            {
                e.Graphics.DrawRectangle(new Pen(colorconfig.DropDownBorder), new Rectangle(0, 0, e.AffectedBounds.Width - 1, e.AffectedBounds.Height - 1));
            }
            else
            {
                base.OnRenderToolStripBorder(e);
            }
        }
        /// <summary>
        /// 渲染箭头
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
        {
            e.ArrowColor = Color.FromArgb(134, 137, 153); ;//还可以 画出各种形状
            base.OnRenderArrow(e);
        }
        /// <summary>
        /// 填充线性渐变
        /// </summary>
        /// <param name="g">画布</param>
        /// <param name="rect">填充区域</param>
        /// <param name="startcolor">开始颜色</param>
        /// <param name="endcolor">结束颜色</param>
        /// <param name="angle">角度</param>
        /// <param name="blend">对象的混合图案</param>
        private void FillLineGradient(Graphics g, Rectangle rect, Color startcolor, Color endcolor, float angle, Blend blend)
        {
            LinearGradientBrush linebrush = new LinearGradientBrush(rect, startcolor, endcolor, angle);
            if (blend != null)
            {
                linebrush.Blend = blend;
            }
            GraphicsPath path = new GraphicsPath();
            path.AddRectangle(rect);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.FillPath(linebrush, path);
        }

        /// <summary>
        /// 绘制主菜单区域
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        private void DrawRectangle(Graphics g, Rectangle rect)
        {
            GraphicsPath oPath = new GraphicsPath();
            int x = 0;
            int y = 0;
            int w = rect.Width - 1;
            int h = rect.Height - 1;
            Brush black = new SolidBrush(Color.FromArgb(232, 232, 236));
            Pen p = new Pen(Color.FromArgb(232, 232, 236));
            g.DrawRectangle(p, x, y, w, h);
            g.FillRectangle(black, new Rectangle(x, y, w, h));
        }
    }

    /// <summary>
    /// 颜色配置类
    /// </summary>
    public class ColorConfig
    {
        private Color _fontcolor = Color.Black;
        /// <summary>
        /// 菜单字体颜色
        /// </summary>
        public Color FontColor
        {
            get { return _fontcolor; }
            set { _fontcolor = value; }
        }
        private Color _earginFontColor = Color.Black;
        /// <summary>
        /// 下来菜单的字体颜色
        /// </summary>
        public Color MarginFontColor
        {
            get { return _earginFontColor; }
            set { _earginFontColor = value; }
        }
        private Color _marginstartcolor = Color.FromArgb(232, 232, 236);
        /// <summary>
        /// 下拉菜单坐标图标区域开始颜色
        /// </summary>
        public Color MarginStartColor
        {
            get { return _marginstartcolor; }
            set { _marginstartcolor = value; }
        }
        private Color _marginendcolor = Color.FromArgb(232, 232, 236);
        /// <summary>
        /// 下拉菜单坐标图标区域结束颜色
        /// </summary>
        public Color MarginEndColor
        {
            get { return _marginendcolor; }
            set { _marginendcolor = value; }
        }
        private Color _dropdownitembackcolor = Color.FromArgb(232, 232, 236);
        /// <summary>
        /// 下拉项背景颜色
        /// </summary>
        public Color DropDownItemBackColor
        {
            get { return _dropdownitembackcolor; }
            set { _dropdownitembackcolor = value; }
        }
        private Color _dropdownitemstartcolor = Color.FromArgb(246, 246, 246);
        /// <summary>
        /// 下拉项选中时开始颜色
        /// </summary>
        public Color DropDownItemStartColor
        {
            get { return _dropdownitemstartcolor; }
            set { _dropdownitemstartcolor = value; }
        }
        private Color _dorpdownitemendcolor = Color.FromArgb(246, 246, 246);
        /// <summary>
        /// 下拉项选中时结束颜色
        /// </summary>
        public Color DropDownItemEndColor
        {
            get { return _dorpdownitemendcolor; }
            set { _dorpdownitemendcolor = value; }
        }
        private Color _menuitemstartcolor = Color.Blue;
        /// <summary>
        /// 主菜单项选中时的开始颜色
        /// </summary>
        public Color MenuItemStartColor
        {
            get { return _menuitemstartcolor; }
            set { _menuitemstartcolor = value; }
        }
        private Color _menuitemendcolor = Color.Blue;
        /// <summary>
        /// 主菜单项选中时的结束颜色
        /// </summary>
        public Color MenuItemEndColor
        {
            get { return _menuitemendcolor; }
            set { _menuitemendcolor = value; }
        }
        private Color _separatorcolor = Color.FromArgb(246, 246, 246);
        /// <summary>
        /// 分割线颜色
        /// </summary>
        public Color SeparatorColor
        {
            get { return _separatorcolor; }
            set { _separatorcolor = value; }
        }
        private Color _mainmenubackcolor = Color.Transparent;
        /// <summary>
        /// 主菜单背景色
        /// </summary>
        public Color MainMenuBackColor
        {
            get { return _mainmenubackcolor; }
            set { _mainmenubackcolor = value; }
        }
        private Color _mainmenustartcolor = Color.Transparent;
        /// <summary>
        /// 主菜单背景开始颜色
        /// </summary>
        public Color MainMenuStartColor
        {
            get { return _mainmenustartcolor; }
            set { _mainmenustartcolor = value; }
        }
        private Color _mainmenuendcolor = Color.Transparent;
        /// <summary>
        /// 主菜单背景结束颜色
        /// </summary>
        public Color MainMenuEndColor
        {
            get { return _mainmenuendcolor; }
            set { _mainmenuendcolor = value; }
        }
        private Color _dropdownborder = Color.FromArgb(232, 232, 236);
        /// <summary>
        /// 下拉区域边框颜色
        /// </summary>
        public Color DropDownBorder
        {
            get { return _dropdownborder; }
            set { _dropdownborder = value; }
        }
    }
}
