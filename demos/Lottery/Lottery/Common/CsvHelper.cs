using Lottery.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace Lottery.Common
{
    public class CsvHelper
    {
        /// <summary>
        /// 读取到 List 中
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static List<User> CsvToList(string filePath)
        {
            List<User> dt = null;
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader sr = new StreamReader(fs, Encoding.Default))
                {
                    dt = new List<User>();
                    string strLine = "";//记录每次读取的一行记录
                    string[] aryLine = null; //记录每行记录中的各字段内容
                    int columnCount = 2;  //标示列数
                    bool IsFirst = true;//标示是否是读取的第一行
                    while ((strLine = sr.ReadLine()) != null)//逐行读取CSV中的数据
                    {
                        if (IsFirst == true)
                        {
                            IsFirst = false;
                        }
                        else
                        {
                            aryLine = strLine.Split(',');
                            if (aryLine.Length == columnCount)
                            {
                                User user = new User();
                                user.UserNo = aryLine[0];
                                user.UserName = aryLine[1];
                                dt.Add(user);
                            }
                        }
                    }
                }
            }
            return dt;
        }

        public static bool ListToCSV(IList<User> dtCSV, string csvFileFullName, bool append)
        {
            try
            {
                if ((null != dtCSV) && (dtCSV.Count > 0))
                {
                    StringBuilder tmpLineText = new StringBuilder();
                    tmpLineText.AppendLine("工号,姓名");
                    // Write rows
                    foreach (var user in dtCSV)
                    {
                        tmpLineText.AppendLine($"{user.UserNo},{user.UserName}");
                    }
                    // WriteFile
                    WriteFile(csvFileFullName, tmpLineText.ToString(), append);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 读取CSV文件到DataTable中
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static DataTable CsvToDataTable(string filePath)
        {
            DataTable dt = null;
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader sr = new StreamReader(fs, Encoding.Default))
                {
                    dt = new DataTable();
                    string strLine = "";//记录每次读取的一行记录
                    string[] aryLine = null; //记录每行记录中的各字段内容
                    string[] tableHead = null;
                    int columnCount = 0;  //标示列数
                    bool IsFirst = true;//标示是否是读取的第一行
                    while ((strLine = sr.ReadLine()) != null)//逐行读取CSV中的数据
                    {
                        if (IsFirst == true)
                        {
                            tableHead = strLine.Split(',');
                            IsFirst = false;
                            columnCount = tableHead.Length;
                            for (int i = 0; i < columnCount; i++)  //创建列
                            {
                                DataColumn dc = new DataColumn(tableHead[i]);
                                dt.Columns.Add(dc);
                            }
                        }
                        else
                        {
                            aryLine = strLine.Split(',');
                            if (aryLine.Length == columnCount)
                            {
                                DataRow dr = dt.NewRow();
                                for (int j = 0; j < columnCount; j++)
                                {
                                    dr[j] = aryLine[j];
                                }
                                dt.Rows.Add(dr);
                            }
                        }
                    }
                }
            }
            return dt;
        }


        /// <summary>
        /// DataTable写入CSV
        /// 调用条件：内存数据先转成有header的DataTable对象，然后调用该方法写入CSV
        /// 如果该文件存在，则可以将其覆盖或向其追加。
        /// 如果该文件不存在，将创建一个新文件。
        /// </summary>
        /// <param name="dtCSV"></param>
        /// <param name="csvFileFullName"></param>
        /// <param name="append">true：在原内容后面追加的方式；false：直接覆盖</param>
        /// <param name="delimeter"></param>
        /// <returns></returns>
        public static bool DataTableToCSV(DataTable dtCSV, string csvFileFullName, bool append, string delimeter = ",")
        {
            try
            {
                if ((null != dtCSV) && (dtCSV.Rows.Count > 0))
                {
                    StringBuilder tmpLineText = new StringBuilder();

                    //Write header
                    if (IsNeedWriteHeader(csvFileFullName, append))
                    {
                        for (int i = 0; i < dtCSV.Columns.Count; i++)
                        {
                            string tmpColumnValue = dtCSV.Columns[i].ColumnName;
                            if (tmpColumnValue.Contains(delimeter))
                            {
                                tmpColumnValue = "\"" + tmpColumnValue + "\"";
                            }
                            if (i == dtCSV.Columns.Count - 1)
                            {
                                tmpLineText.Append(tmpColumnValue);
                            }
                            else
                            {
                                tmpLineText.Append(tmpColumnValue + delimeter);
                            }
                        }
                        if (tmpLineText.Length > 0)
                        {
                            tmpLineText.Append(Environment.NewLine);
                        }
                    }

                    //Write rows
                    for (int j = 0; j < dtCSV.Rows.Count; j++)
                    {
                        for (int k = 0; k < dtCSV.Columns.Count; k++)
                        {
                            string tmpRowValue = dtCSV.Rows[j][k].ToString();
                            if (tmpRowValue.Contains(delimeter))
                            {
                                tmpRowValue = "\"" + tmpRowValue + "\"";
                            }
                            if (k == dtCSV.Columns.Count - 1)
                            {
                                tmpLineText.Append(tmpRowValue);
                            }
                            else
                            {
                                tmpLineText.Append(tmpRowValue + delimeter);
                            }
                        }
                        tmpLineText.Append(Environment.NewLine);
                    }

                    //WriteFile
                    WriteFile(csvFileFullName, tmpLineText.ToString(), append);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 是否需要写header
        /// </summary>
        /// <param name="csvFileFullName"></param>
        /// <param name="append">true：在原内容后面追加的方式；false：直接覆盖</param>
        /// <returns></returns>
        private static bool IsNeedWriteHeader(string csvFileFullName, bool append)
        {
            try
            {
                if (!File.Exists(csvFileFullName)) return true;
                if (!append) return true;    //只要是覆盖模式，就要写header
                                             //追加模式，根据文件长度判断
                FileInfo info = new FileInfo(csvFileFullName);
                return info.Length == 0;//文件长度为0b则认为应该写header,
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private static void WriteFile(string fileFullName, string message, bool append)
        {
            try
            {
                string directory = Path.GetDirectoryName(fileFullName);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                using (StreamWriter sw = new StreamWriter(fileFullName, append, Encoding.UTF8))
                {
                    sw.Write(message);
                    sw.Flush();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}