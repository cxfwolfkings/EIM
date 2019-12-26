using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using System.Collections;

namespace Kingdee.CAPP.Autocad.DrawRectanglePlugIn
{

    public enum ResultTreeType
    {
        Node,
        List,
        DottedPair,
        RelationalOperator,
        LogicalOperator,
    }

    /// <summary>
    /// TypedValue Tree
    /// </summary>
    public class ResultTree : IEnumerable<ResultTree>, IFormattable
    {
        ResultTreeType _treeType;
        ResultTree _owner;
        TypedValue _typedValue;
        List<ResultTree> _lst = new List<ResultTree>();
        static readonly List<string> _relationalOperatorNames =
            new List<string> { "not", "and", "or", "xor" };
        #region Properties
        public ResultTree this[int index]
        {
            get
            {
                if (_lst.Count == 0)
                {
                    if (index == 0)
                    {
                        return this;
                    }
                }
                else
                {
                    if (index >= 0 && index < _lst.Count)
                    {
                        return _lst[index];
                    }
                }
                return null;
            }
        }
        public ResultTreeType TreeType
        {
            get { return _treeType; }
        }
        public TypedValue TypedValue
        {
            get { return _typedValue; }
        }
        public short TypeCode
        {
            get { return _typedValue.TypeCode; }
        }
        public LispDataType LispDataType
        {
            get { return (LispDataType)TypeCode; }
        }
        public DxfCode DxfCode
        {
            get { return (DxfCode)TypeCode; }
        }
        public object Value
        {
            get { return _typedValue.Value; }
        }
        public T GetValue<T>()
        {
            return (T)_typedValue.Value;
        }
        #endregion
        #region Constructor
        public ResultTree()
        {
            _treeType = ResultTreeType.Node;
        }
        public ResultTree(TypedValue value)
            : this()
        {
            _typedValue = value;
        }
        public ResultTree(int typeCode, object obj)
            : this(new TypedValue(typeCode, obj))
        { }
        public ResultTree(LispDataType type, object obj)
            : this(new TypedValue((int)type, obj))
        { }
        public ResultTree(DxfCode type, object obj)
            : this(new TypedValue((int)type, obj))
        { }

        public ResultTree(string operatorContext)
        {
            operatorContext = operatorContext.ToLower();
            _treeType =
                _relationalOperatorNames.Contains(operatorContext) ?
                ResultTreeType.RelationalOperator : ResultTreeType.LogicalOperator;
            _typedValue = new TypedValue(-4, operatorContext);
        }
        public ResultTree(ResultTreeType type)
        {
            _treeType = type;
        }
        private enum ResultNodeType
        {
            Node,
            ListBegin,
            ListEnd,
            DottedPairEnd,
            LogicalOperator,
            RelationalOperatorBegin,
            RelationalOperatorEnd,
        }
        private ResultNodeType GetNodeType(TypedValue tvalue, out ResultTree rt)
        {
            short typeCode = tvalue.TypeCode;
            object value = tvalue.Value;
            rt = null;

            if (typeCode == -4)
            {
                string s = ((string)value).ToLower();
                if (s[0] == '<' && _relationalOperatorNames.Contains(s.Substring(1)))
                {
                    rt = new ResultTree(s.Substring(1));
                    return ResultNodeType.RelationalOperatorBegin;
                }
                else if (s[s.Length - 1] == '>' && _relationalOperatorNames.Contains(s.Substring(0, s.Length - 1)))
                {
                    return ResultNodeType.RelationalOperatorEnd;
                }
                else
                {
                    rt = new ResultTree(s);
                    return ResultNodeType.LogicalOperator;
                }
            }
            else if (typeCode == (short)LispDataType.ListBegin)
            {
                rt = new ResultTree(ResultTreeType.List);
                return ResultNodeType.ListBegin;
            }
            else if (typeCode == (short)LispDataType.ListEnd)
            {
                return ResultNodeType.ListEnd;
            }
            else if (typeCode == (short)LispDataType.DottedPair)
            {
                return ResultNodeType.DottedPairEnd;
            }
            else
            {
                rt = new ResultTree(tvalue);
                return ResultNodeType.Node;
            }
        }
        public ResultTree(ResultBuffer rb)
        {
            ResultTree rt = this;
            foreach (TypedValue tv in rb)
            {
                ResultTree trt;
                switch (GetNodeType(tv, out trt))
                {
                    case ResultNodeType.LogicalOperator:
                    case ResultNodeType.RelationalOperatorBegin:
                    case ResultNodeType.ListBegin:
                        rt = rt.Add(trt);
                        break;
                    case ResultNodeType.DottedPairEnd:
                        rt._treeType = ResultTreeType.DottedPair;
                        rt = rt._owner;
                        break;
                    case ResultNodeType.RelationalOperatorEnd:
                    case ResultNodeType.ListEnd:
                        rt = rt._owner;
                        break;
                    default:
                        rt.Add(trt);
                        if (rt._treeType == ResultTreeType.RelationalOperator)
                            rt = rt._owner;
                        break;
                }
            }
            if (_lst.Count == 1)
            {
                rt = _lst[0];
                _lst.Remove(rt);
                _lst.AddRange(rt);
                _treeType = rt._treeType;
                _typedValue = rt._typedValue;
            }
        }
        #endregion
        #region Set
        public void Set(int typeCode, object obj)
        {
            _typedValue = new TypedValue(typeCode, obj);
        }
        public void Set(LispDataType type, object obj)
        {
            _typedValue = new TypedValue((int)type, obj);
        }
        public void Set(DxfCode type, object obj)
        {
            _typedValue = new TypedValue((int)type, obj);
        }

        public void Set(int typeCode)
        {
            _typedValue = new TypedValue(typeCode);
        }
        public void Set(LispDataType type)
        {
            _typedValue = new TypedValue((int)type);
        }
        public void Set(DxfCode type)
        {
            _typedValue = new TypedValue((int)type);
        }
        #endregion
        #region Add
        public ResultTree Add(TypedValue value)
        {
            return Add(new ResultTree(value));
        }
        public ResultTree Add(int typeCode, object obj)
        {
            return Add(new ResultTree(typeCode, obj));
        }
        public ResultTree Add(LispDataType type, object obj)
        {
            return Add(new ResultTree(type, obj));
        }
        public ResultTree Add(DxfCode type, object obj)
        {
            return Add(new ResultTree(type, obj));
        }
        public ResultTree Add(ResultTree rt)
        {
            rt._owner = this;
            _lst.Add(rt);
            return rt;
        }
        #region Add LispData
        public ResultTree Add(short value)
        {
            return Add(new ResultTree(LispDataType.Int16, value));
        }
        public ResultTree Add(int value)
        {
            return Add(new ResultTree(LispDataType.Int32, value));
        }
        public ResultTree Add(double value)
        {
            return Add(new ResultTree(LispDataType.Double, value));
        }
        public ResultTree Add(string value)
        {
            return Add(new ResultTree(LispDataType.Text, value));
        }
        public ResultTree Add(Point2d value)
        {
            return Add(new ResultTree(LispDataType.Point2d, value));
        }
        public ResultTree Add(Point3d value)
        {
            return Add(new ResultTree(LispDataType.Point3d, value));
        }
        public ResultTree Add(ObjectId value)
        {
            return Add(new ResultTree(LispDataType.ObjectId, value));
        }
        public ResultTree Add(SelectionSet value)
        {
            return Add(new ResultTree(LispDataType.SelectionSet, value));
        }
        //public void Add(ResultType type, params object[] values)
        //{
        //    ResultTree rt = new ResultTree(type);
        //    foreach (object value in values)
        //    {
        //        if (value is short)
        //        {
        //            rt.Add((short)value);
        //        }
        //        else if (value is int)
        //        {
        //            rt.Add((int)value);
        //        }
        //        else if (value is double)
        //        {
        //            rt.Add((double)value);
        //        }
        //        else if (value is string)
        //        {
        //            rt.Add((string)value);
        //        }
        //        else if (value is Point2d)
        //        {
        //            rt.Add((Point2d)value);
        //        }
        //        else if (value is Point3d)
        //        {
        //            rt.Add((Point3d)value);
        //        }
        //        else if (value is ObjectId)
        //        {
        //            rt.Add((ObjectId)value);
        //        }
        //        else if (value is SelectionSet)
        //        {
        //            rt.Add((SelectionSet)value);
        //        }
        //    }
        //}
        #endregion
        #endregion
        #region Convert
        private void GetValues(List<TypedValue> values)
        {
            switch (_treeType)
            {
                case ResultTreeType.Node:
                    if (_lst.Count == 0)
                    {
                        values.Add(_typedValue);
                    }
                    else
                    {
                        _lst.ForEach(rtree => rtree.GetValues(values));
                    }
                    break;
                case ResultTreeType.List:
                    values.Add(new TypedValue((int)LispDataType.ListBegin));
                    _lst.ForEach(rtree => rtree.GetValues(values));
                    values.Add(new TypedValue((int)LispDataType.ListEnd));
                    break;
                case ResultTreeType.DottedPair:
                    values.Add(new TypedValue((int)LispDataType.ListBegin));
                    _lst.ForEach(rtree => rtree.GetValues(values));
                    values.Add(new TypedValue((int)LispDataType.DottedPair));
                    break;
                case ResultTreeType.LogicalOperator:
                    values.Add(_typedValue);
                    _lst.ForEach(rtree => rtree.GetValues(values));
                    break;
                case ResultTreeType.RelationalOperator:
                    values.Add(new TypedValue(-4, "<" + _typedValue.Value));
                    _lst.ForEach(rtree => rtree.GetValues(values));
                    values.Add(new TypedValue(-4, _typedValue.Value + ">"));
                    break;
            }
        }
        public TypedValue[] ToArray()
        {
            List<TypedValue> values = new List<TypedValue>();
            GetValues(values);
            return values.ToArray();
        }
        public static implicit operator SelectionFilter(ResultTree rtree)
        {
            return new SelectionFilter(rtree.ToArray());
        }
        public static implicit operator ResultBuffer(ResultTree rtree)
        {
            return new ResultBuffer(rtree.ToArray());
        }
        #endregion
        #region RelationalOperator
        private void MakeRelationalOperator(string operatorContext, ResultTree res)
        {
            if (_treeType == ResultTreeType.RelationalOperator && _typedValue.Value.ToString() == operatorContext)
            {
                res._lst.AddRange(_lst);
            }
            else
            {
                res.Add(this);
            }
        }
        public static ResultTree operator !(ResultTree rt1)
        {
            ResultTree rt = new ResultTree("not") { rt1 };
            return rt;
        }
        public static ResultTree operator &(ResultTree rt1, ResultTree rt2)
        {
            ResultTree rt = new ResultTree("and");
            rt1.MakeRelationalOperator("and", rt);
            rt2.MakeRelationalOperator("and", rt);
            return rt;
        }
        public static ResultTree operator |(ResultTree rt1, ResultTree rt2)
        {
            ResultTree rt = new ResultTree("or");
            rt1.MakeRelationalOperator("or", rt);
            rt2.MakeRelationalOperator("or", rt);
            return rt;
        }
        public static ResultTree operator ^(ResultTree rt1, ResultTree rt2)
        {
            return
                new ResultTree("xor")
                {
                    rt1,
                    rt2
                };
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
        #region IEnumerable<ResultTree> 成员
        #region IEnumerable 成员
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _lst.GetEnumerator();
        }
        #endregion
        IEnumerator<ResultTree> IEnumerable<ResultTree>.GetEnumerator()
        {
            return _lst.GetEnumerator();
        }
        #endregion
    }
}