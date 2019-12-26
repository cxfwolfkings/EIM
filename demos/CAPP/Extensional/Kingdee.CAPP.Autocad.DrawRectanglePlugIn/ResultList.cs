using System;
using System.Collections.Generic;
using System.Text;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;

namespace Kingdee.CAPP.Autocad.DrawRectanglePlugIn
{
    public class ResultList : List<TypedValue>, IFormattable
    {
        public ResultList() { }
        public ResultList(IEnumerable<TypedValue> values)
            : base(values)
        { }
        public ResultList(ResultBuffer rb)
            : base(rb.AsArray())
        { }
        #region Add
        public void Add(int typeCode, object obj)
        {
            base.Add(new TypedValue(typeCode, obj));
        }
        public void Add(LispDataType type, object obj)
        {
            base.Add(new TypedValue((int)type, obj));
        }
        public void Add(DxfCode type, object obj)
        {
            base.Add(new TypedValue((int)type, obj));
        }
        #endregion
        #region Convert
        public static implicit operator TypedValue[](ResultList rlst)
        {
            return rlst.ToArray();
        }
        public static implicit operator ResultBuffer(ResultList rlst)
        {
            return new ResultBuffer(rlst.ToArray());
        }
        public static implicit operator SelectionFilter(ResultList rlst)
        {
            return new SelectionFilter(rlst.ToArray());
        }
        #endregion
        #region IFormattable 成员
        public override string ToString()
        {
            var rb = new ResultBuffer(ToArray());
            return rb.ToString();
        }
        string IFormattable.ToString(string format, IFormatProvider formatProvider)
        {
            var rb = new ResultBuffer(ToArray());
            return rb.ToString(format, formatProvider);
        }
        #endregion
    }
}
