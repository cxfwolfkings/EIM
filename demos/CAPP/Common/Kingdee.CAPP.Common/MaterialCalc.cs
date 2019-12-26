using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Reflection;

namespace Kingdee.CAPP.Common
{
    /// <summary>
    /// 类型说明：材料定额计算类
    /// 作      者：jason.tang
    /// 完成时间：2013-07-10
    /// </summary>
    public class MaterialCalc
    {
        private string expression;
        public MaterialCalc()
        {
            expression = "0";
        }
        public MaterialCalc(string exp)
        {
            expression = exp;
        }

        /// <summary>
        /// 表达式
        /// </summary>
        public string Expression
        {
            set
            {
                Expression = value;
            }
            get
            {
                return (expression);
            }
        }

        /// <summary>
        /// 四则运算
        /// </summary>
        /// <returns>返回结果</returns>
        public double EvaluateExpression()
        {
            try
            {
                string myExp = expression + "=";             //表达式。。
                Stack<char> optr = new Stack<char>(myExp.Length);    //存放操作符栈。。
                Stack<double> opnd = new Stack<double>(myExp.Length);      //存放操作数栈。。
                optr.Push('=');
                int index = 0;                                //字符索引。。
                char c = myExp.ToCharArray()[index++];                   //读取每一个字符。。
                bool isFloat = false;                      //是否为小数。。
                bool isNum = false;                      //是否为数字。。
                int floatBit = 0;                             //小数数位。。
                double num1, num2;
                while ((c != '=') || (optr.Peek() != '='))
                {
                    if ((c >= '0') && (c <= '9'))
                    {
                        if (isNum)
                        {
                            if (isFloat)
                            {
                                floatBit++;
                                opnd.Push(opnd.Pop() + ((int)c - 48.0) / Math.Pow(10, floatBit));
                            }
                            else
                            {
                                opnd.Push(opnd.Pop() * 10 + (int)c - 48);
                            }
                        }
                        else
                        {
                            opnd.Push((int)c - 48);
                            isNum = true;
                        }
                        c = myExp.ToCharArray()[index++];
                    }
                    else
                    {
                        if ((c == '.') && (isNum))
                        {
                            isFloat = true;
                            floatBit = 0;
                            c = myExp.ToCharArray()[index++];
                        }
                        else
                        {
                            isNum = false;
                            isFloat = false;
                            switch (Precede(optr.Peek(), c))
                            {
                                case '<':
                                    optr.Push(c);
                                    c = myExp.ToCharArray()[index++];
                                    break;
                                case '=':
                                    optr.Pop();
                                    c = myExp.ToCharArray()[index++];
                                    break;
                                case '>':
                                    num2 = opnd.Pop();
                                    num1 = opnd.Pop();
                                    opnd.Push(Operate(num1, optr.Pop(), num2));
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
                return opnd.Pop();
            }
            catch (Exception)
            {
                return double.MinValue;
            }
        }

        //判断优先级。。
        private char Precede(char optr1, char optr2)
        {
            //定义一个比较结果(用二维数组存下来)。。
            char[,] optrTable = 
            {
                { '>', '>', '<', '<', '<', '>', '>' },
                { '>', '>', '<', '<', '<', '>', '>' },
                { '>', '>', '>', '>', '<', '>', '>' },
                { '>', '>', '>', '>', '<', '>', '>' },
                { '<', '<', '<', '<', '<', '=', '?' },
                { '>', '>', '>', '>', '?', '>', '>' },
                { '<', '<', '<', '<', '<', '?', '=' }
            };
            int x = 0, y = 0;//申明存符号转化后的整数。。
            //定义一个符号数组。。
            char[] optrs = { '+', '-', '*', '/', '(', ')', '=' };
            for (int i = 0; i < optrs.Length; ++i)
            {
                if (optr1 == optrs[i])
                    x = i;
                if (optr2 == optrs[i])
                    y = i;
            }
            if (optrTable[x, y] == '?')
            {
                throw new Exception("表达式不合法");
            }
            else
            {
                return optrTable[x, y];
            }
        }

        //计算两值，得出相应结果。。
        private double Operate(double a, char optr, double b)
        {
            double result = default(double);
            switch (optr)
            {
                case '+':
                    result = a + b;
                    break;
                case '-':
                    result = a - b;
                    break;
                case '*':
                    result = a * b;
                    break;
                case '/':
                    if (b < Math.Pow(10, 0.000001))
                    {
                        throw new Exception("除数为0");
                    }
                    result = a / b;
                    break;
                default:
                    break;
            }
            return result;
        }    

        ////利用动态类实现运算
        //public static double Function(string str)
        //{
        //    double result;

        //    StringBuilder classBody = new StringBuilder(400);
        //    classBody.AppendLine("using System;");
        //    classBody.AppendLine("public class TempClass");
        //    classBody.AppendLine("{");
        //    classBody.AppendLine("public double Fun()");
        //    classBody.AppendLine("{");
        //    classBody.AppendLine(string.Format(" return {0};", str));
        //    classBody.AppendLine("}");
        //    classBody.AppendLine("}");

        //    CSharpCodeProvider provider = new CSharpCodeProvider();
        //    CompilerParameters cp = new CompilerParameters();
        //    cp.GenerateExecutable = false;
        //    cp.GenerateInMemory = true;
        //    CompilerResults compResult = provider.CompileAssemblyFromSource(cp, classBody.ToString());
        //    object obj = compResult.CompiledAssembly.CreateInstance("TempClass");
        //    Type objType = obj.GetType();
        //    MethodInfo method = objType.GetMethod("Fun");
        //    result = Convert.ToDouble(method.Invoke(obj, null));
        //    return result;
        //}

    }
}
