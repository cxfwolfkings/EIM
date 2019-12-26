using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Kingdee.CAPP.Common
{
    /// <summary>
    /// 类型说明：通用控件移动、大小改变
    /// 作    者：jason.tang
    /// 完成时间：2012-12-26
    /// </summary>
    public class ControlOperator
    {
        #region 移动命令

        /// <summary>
        /// 命令枚举
        /// </summary>
        private enum MoveCommand
        {
            None,
            Move,
            SizeLeft,
            SizeRight,
            SizeUp,
            SizeDown,
            SizeLeftUp,
            SizeLeftDown,
            SizeRightUp,
            SizeRightDown
        }

        /// <summary> 
        /// 当前命令 
        /// </summary> 
        private MoveCommand m_MoveCommand = MoveCommand.None;
        
        #endregion

        private Control m_ParentControl;
        private Control m_MoveControl;
        private Point m_MousePoint = Point.Empty;
        private Point m_MouseRight = Point.Empty;

        private int m_SamllSizeTop = 3;
        private int m_SamllSizeLeft = 5;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="p_MoveControl">需要移动的控件</param>
        public ControlOperator(Control p_MoveControl)
        {
            if (p_MoveControl.Parent == null) return;

            m_ParentControl = p_MoveControl.Parent;
            m_MoveControl = p_MoveControl;

            p_MoveControl.MouseDown += new MouseEventHandler(p_MoveControl_MouseDown);
            p_MoveControl.MouseLeave += new EventHandler(p_MoveControl_MouseLeave);
            p_MoveControl.MouseMove += new MouseEventHandler(p_MoveControl_MouseMove);
            p_MoveControl.MouseUp += new MouseEventHandler(p_MoveControl_MouseUp);

            System.Reflection.PropertyInfo _BorderStyleInfo = p_MoveControl.GetType().GetProperty("BorderStyle");
            if (_BorderStyleInfo == null) return;

            try
            {
                if ((BorderStyle)_BorderStyleInfo.GetValue(p_MoveControl, new object[] { }) == BorderStyle.Fixed3D) m_SamllSizeLeft = 8;
            }
            catch
            {

            }

        }

        /// <summary>
        /// 鼠标键弹起
        /// </summary>
        void p_MoveControl_MouseUp(object sender, MouseEventArgs e)
        {
            m_MoveCommand = MoveCommand.None;
            m_MoveControl.Cursor = Cursors.Hand;
            if (MoveEnd != null) MoveEnd(m_MoveControl);
        }

        /// <summary>
        /// 鼠标移动
        /// </summary>
        void p_MoveControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (m_MoveCommand == MoveCommand.None)
            {
                GetCursor(new Point(e.X, e.Y));
                return;
            }

            switch (m_MoveCommand)
            {
                case MoveCommand.Move:
                    int _PointX = m_MousePoint.X - e.X;
                    int _PointY = m_MousePoint.Y - e.Y;
                    if (m_Min)
                    {
                        if (m_MoveControl.Location.X - _PointX <= 0) _PointX = 0;
                        if (m_MoveControl.Location.Y - _PointY <= 0) _PointY = 0;
                    }
                    if (m_Max)
                    {
                        if (m_MoveControl.Location.X - _PointX + m_MoveControl.Width >= m_ParentControl.Width) _PointX = 0;
                        if (m_MoveControl.Location.Y - _PointY + m_MoveControl.Height >= m_ParentControl.Height) _PointY = 0;
                    }

                    m_MoveControl.Location = new Point(m_MoveControl.Location.X - _PointX, m_MoveControl.Location.Y - _PointY);
                    break;

                #region 上下左右尺寸改变

                case MoveCommand.SizeRight:
                    if ((m_MoveControl.Width + e.X - m_MousePoint.X) < 10) break;
                    if (m_Max)
                    {
                        int _Max = (m_MoveControl.Width + e.X - m_MousePoint.X) + m_MoveControl.Location.X;
                        if (_Max >= m_ParentControl.Width) break;
                    }
                    m_MoveControl.Width = m_MoveControl.Width + e.X - m_MousePoint.X;
                    m_MousePoint.X = e.X;
                    m_MousePoint.Y = e.Y;
                    break;

                case MoveCommand.SizeDown:
                    if ((m_MoveControl.Height + e.Y - m_MousePoint.Y) < 10) break;
                    if (m_Max)
                    {
                        int _Max = (m_MoveControl.Height + e.Y - m_MousePoint.Y) + m_MoveControl.Location.Y;
                        if (_Max >= m_ParentControl.Height) break;
                    }
                    m_MoveControl.Height = m_MoveControl.Height + e.Y - m_MousePoint.Y;
                    m_MousePoint.X = e.X;
                    m_MousePoint.Y = e.Y;
                    break;
                case MoveCommand.SizeUp:
                    if ((m_MoveControl.Height - (e.Y - m_MousePoint.Y)) < 10) break;
                    if (m_Min)
                    {
                        int _Mix = m_MoveControl.Top + (e.Y - m_MousePoint.Y);
                        if (_Mix < 0) break;
                    }
                    m_MoveControl.Top = m_MoveControl.Top + (e.Y - m_MousePoint.Y);
                    m_MoveControl.Height = m_MoveControl.Height - (e.Y - m_MousePoint.Y);
                    break;
                case MoveCommand.SizeLeft:
                    if ((m_MoveControl.Width - (e.X - m_MousePoint.X)) < 10) break;
                    if (m_Min)
                    {
                        int _Mix = m_MoveControl.Left + e.X - m_MousePoint.X;
                        if (_Mix < 0) break;
                    }
                    m_MoveControl.Left = m_MoveControl.Left + e.X - m_MousePoint.X;
                    m_MoveControl.Width = m_MoveControl.Width - (e.X - m_MousePoint.X);
                    break;

                #endregion

                #region 通过四个边角改变尺寸

                case MoveCommand.SizeLeftUp:
                    int _Left = m_MoveControl.Left + e.X - m_MousePoint.X;
                    int _Top = m_MoveControl.Top + (e.Y - m_MousePoint.Y);
                    int _Width = m_MoveControl.Width - (e.X - m_MousePoint.X);
                    int _Height = m_MoveControl.Height - (e.Y - m_MousePoint.Y);

                    if (_Width < 10)        //这里如果错误 换成直接返回break  
                    {
                        _Width = 10;
                        _Left = m_MoveControl.Left;
                    }
                    if (_Height < 10)
                    {
                        _Height = 10;
                        _Top = m_MoveControl.Top;
                    }

                    if (m_Min)
                    {
                        if (_Left < 0)
                        {
                            _Left = 0;
                            _Width = m_MoveControl.Width;

                        }
                        if (_Top < 0)
                        {
                            _Top = 0;
                            _Height = m_MoveControl.Height;
                        }
                    }
                    m_MoveControl.Left = _Left;
                    m_MoveControl.Top = _Top;
                    m_MoveControl.Width = _Width;
                    m_MoveControl.Height = _Height;
                    break;
                case MoveCommand.SizeRightDown:

                    if ((m_MoveControl.Width + e.X - m_MousePoint.X) < 10) break;
                    if ((m_MoveControl.Height + e.Y - m_MousePoint.Y) < 10) break;
                    if (m_Max)
                    {
                        int _Max = (m_MoveControl.Height + e.Y - m_MousePoint.Y) + m_MoveControl.Location.Y;
                        if (_Max >= m_ParentControl.Height) break;
                        _Max = (m_MoveControl.Width + e.X - m_MousePoint.X) + m_MoveControl.Location.X;
                        if (_Max >= m_ParentControl.Width) break;
                    }
                    m_MoveControl.Width = m_MoveControl.Width + e.X - m_MousePoint.X;
                    m_MoveControl.Height = m_MoveControl.Height + e.Y - m_MousePoint.Y;
                    m_MousePoint.X = e.X;
                    m_MousePoint.Y = e.Y; //'记录光标拖动的当前点 
                    break;

                case MoveCommand.SizeRightUp:
                    if ((m_MoveControl.Width + (e.X - m_MousePoint.X)) < 10) break;
                    if ((m_MoveControl.Height - (e.Y - m_MouseRight.Y)) < 10) break;
                    if (m_Min)
                    {
                        if ((m_MoveControl.Top + (e.Y - m_MouseRight.Y)) < 0) break;
                    }
                    m_MoveControl.Top = m_MoveControl.Top + (e.Y - m_MouseRight.Y);
                    m_MoveControl.Width = m_MoveControl.Width + (e.X - m_MousePoint.X);
                    m_MoveControl.Height = m_MoveControl.Height - (e.Y - m_MouseRight.Y);
                    m_MousePoint.X = e.X;
                    m_MousePoint.Y = e.Y; //'记录光标拖动的当前点 
                    break;

                case MoveCommand.SizeLeftDown:
                    if ((m_MoveControl.Width - (e.X - m_MouseRight.X)) < 10) break;
                    if ((m_MoveControl.Height + e.Y - m_MousePoint.Y) < 10) break;

                    if (m_Min)
                    {
                        if ((m_MoveControl.Left + e.X - m_MouseRight.X) < 0) break;
                    }
                    if (m_Max)
                    {
                        int _Max = (m_MoveControl.Height + e.Y - m_MousePoint.Y) + m_MoveControl.Location.Y;
                        if (_Max >= m_ParentControl.Height) break;
                    }

                    m_MoveControl.Left = m_MoveControl.Left + e.X - m_MouseRight.X;
                    m_MoveControl.Width = m_MoveControl.Width - (e.X - m_MouseRight.X);
                    m_MoveControl.Height = m_MoveControl.Height + e.Y - m_MousePoint.Y;
                    m_MousePoint.X = e.X;
                    m_MousePoint.Y = e.Y; //'记录光标拖动的当前点 
                    break;

                #endregion
            }

        }

        /// <summary>
        /// 鼠标离开控件还原默认鼠标指针
        /// </summary>
        void p_MoveControl_MouseLeave(object sender, EventArgs e)
        {
            m_MoveControl.Cursor = Cursors.Default;
        }

        /// <summary>
        /// 鼠标左键按下
        /// </summary>
        void p_MoveControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                m_MoveCommand = GetCommand(new Point(e.X, e.Y));
                m_MousePoint = new Point(e.X, e.Y);
                m_MouseRight = new Point(e.X, e.Y);
                switch (m_MoveCommand)
                {
                    case MoveCommand.Move:
                        m_MoveControl.Cursor = Cursors.SizeAll;
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary> 
        /// 根据鼠标位置获取执行的命令 
        /// </summary> 
        /// <param name="p_MousePoint">鼠标位置</param> 
        private MoveCommand GetCommand(Point p_MousePoint)
        {
            #region 四个边角
            if (p_MousePoint.X <= m_SamllSizeTop && p_MousePoint.Y <= m_SamllSizeTop) if (m_Size) return MoveCommand.SizeLeftUp;
            if (p_MousePoint.X <= m_SamllSizeTop && p_MousePoint.Y >= m_MoveControl.Height - m_SamllSizeLeft) if (m_Size) return MoveCommand.SizeLeftDown;
            if (p_MousePoint.X >= m_MoveControl.Width - m_SamllSizeLeft && p_MousePoint.Y <= m_SamllSizeTop) if (m_Size) return MoveCommand.SizeRightUp;
            if (p_MousePoint.X >= m_MoveControl.Width - m_SamllSizeLeft && p_MousePoint.Y >= m_MoveControl.Height - m_SamllSizeLeft) if (m_Size) return MoveCommand.SizeRightDown;
            #endregion

            #region 上下左右
            if (p_MousePoint.X <= m_SamllSizeTop) if (m_Size) return MoveCommand.SizeLeft;
            if (p_MousePoint.Y <= m_SamllSizeTop) if (m_Size) return MoveCommand.SizeUp;
            if (p_MousePoint.X >= m_MoveControl.Width - m_SamllSizeLeft) if (m_Size) return MoveCommand.SizeRight;
            if (p_MousePoint.Y >= m_MoveControl.Height - m_SamllSizeLeft) if (m_Size) return MoveCommand.SizeDown;
            #endregion


            if (m_Move) return MoveCommand.Move;
            return MoveCommand.None;

        }
        
        /// <summary> 
        /// 根据不同位置设置鼠标样式 
        /// </summary> 
        /// <param name="p_MousePoint">鼠标位置</param> 
        private void GetCursor(Point p_MousePoint)
        {
            MoveCommand _Command = GetCommand(p_MousePoint);
            switch (_Command)
            {
                #region 四个边角
                case MoveCommand.Move:
                    m_MoveControl.Cursor = Cursors.Hand;
                    return;
                case MoveCommand.SizeLeftUp:
                    m_MoveControl.Cursor = Cursors.SizeNWSE;
                    return;
                case MoveCommand.SizeLeftDown:
                    m_MoveControl.Cursor = Cursors.SizeNESW;
                    return;
                case MoveCommand.SizeRightUp:
                    m_MoveControl.Cursor = Cursors.SizeNESW;
                    return;
                case MoveCommand.SizeRightDown:
                    m_MoveControl.Cursor = Cursors.SizeNWSE;
                    return;
                #endregion

                #region 上下左右
                case MoveCommand.SizeLeft:
                    m_MoveControl.Cursor = Cursors.SizeWE;
                    return;
                case MoveCommand.SizeUp:
                    m_MoveControl.Cursor = Cursors.SizeNS;
                    return;
                case MoveCommand.SizeRight:
                    m_MoveControl.Cursor = Cursors.SizeWE;
                    return;
                case MoveCommand.SizeDown:
                    m_MoveControl.Cursor = Cursors.SizeNS;
                    return;
                #endregion
            }
        }

        #region 控制属性

        private bool m_Move = true;
        /// <summary> 
        /// 是否能移动控件 
        /// </summary> 
        public bool Move { get { return m_Move; } set { m_Move = value; } }

        private bool m_Min = true;
        /// <summary> 
        /// 是否移动到最小区域 
        /// </summary> 
        public bool Min { get { return m_Min; } set { m_Min = value; } }

        private bool m_Max = true;
        /// <summary> 
        /// 是否移动到最大区域 
        /// </summary> 
        public bool Max { get { return m_Max; } set { m_Max = value; } }

        private bool m_Size = true;
        /// <summary> 
        /// 是否能设置大小 
        /// </summary> 
        public bool Size { get { return m_Size; } set { m_Size = value; } }
        #endregion
        
        public delegate void ControlMoveEnd(Control sender);
        public event ControlMoveEnd MoveEnd;
    }
}
