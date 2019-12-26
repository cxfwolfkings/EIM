using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lottery.Common
{
    public class JsonHelper
    {
        public static void SaveToJson(Dictionary<string, string> dicResult, string filePath)
        {
            string values = JsonConvert.SerializeObject(dicResult);
            if (values.Contains("\",\""))
            {
                values = values.Replace("\",\"", $"\",{Environment.NewLine}\"");
            }
            using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                sw.Write(values);
            }
        }

        public static Dictionary<string, string> ReadFromJson(string filePath)
        {
            Dictionary<string, string> dicResult = new Dictionary<string, string>();
            if (File.Exists(filePath))
            {
                using (StreamReader sr = new StreamReader(filePath, Encoding.UTF8))
                {
                    string read = sr.ReadToEnd();

                    var temp = JsonConvert.DeserializeObject<Dictionary<string, string>>(read);
                    foreach (var tempItem in temp)
                    {
                        dicResult.Add(tempItem.Key, tempItem.Value);
                    }
                }
            }
            return dicResult;
        }
    }
}