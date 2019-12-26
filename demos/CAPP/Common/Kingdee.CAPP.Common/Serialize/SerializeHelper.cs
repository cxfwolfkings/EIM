using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.IO;
/*******************************
 * Created By franco
 * Description: Serialize helper
 *******************************/

namespace Kingdee.CAPP.Common.Serialize
{
    public class SerializeHelper
    {
        /// <summary>
        /// Deserialize for object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static T DeserializeXMLChar<T>(string xml)
            where T: class
        {
            XmlSerializer xmlserialize = new XmlSerializer(typeof(T));

            try
            {
                using (StringReader reader = new StringReader(xml))
                {
                    T t = (T)xmlserialize.Deserialize(reader);
                    return t;
                }

                #region UTF8
                //byte[] buffer = Encoding.UTF8.GetBytes(xml);
                //using (MemoryStream mstream = new MemoryStream())
                //{
                //    mstream.Write(buffer, 0, buffer.Length);
                //    mstream.Seek(0, SeekOrigin.Begin);

                //    T t = (T)xmlserialize.Deserialize(mstream);
                //    return t;
                //} 
                #endregion
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Serialize object to char
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        public static string Serialize<T>(T t)
            where T : class
        {
            try
            {
                XmlSerializer serialize = new XmlSerializer(typeof(T));

                using (StringWriter writer = new StringWriter())
                {
                    serialize.Serialize(writer, t);
                    return writer.ToString();
                }
                #region UTF8
                //using (MemoryStream stream = new MemoryStream())
                //{
                //    XmlTextWriter writer = new XmlTextWriter(stream, Encoding.UTF8);
                //    writer.Formatting = Formatting.Indented;
                //    writer.Indentation = 4;
                //    serialize.Serialize(stream, t);

                //    writer.Flush();
                //    writer.Close();

                //    UTF8Encoding utf8Encoding = new UTF8Encoding(false, true);
                //    result = Encoding.UTF8.GetString(stream.ToArray());
                //    return result;
                //} 
                #endregion

            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// Serialize to file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        public static void Serialize<T>(T t,string path)
            where T : class
        { 
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            try
            {
                using (StreamWriter stream = new StreamWriter(path))
                {
                    serializer.Serialize(stream, t);
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// serialize binary file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="path"></param>
        public static void BinarySerialize<T>(T t,string path)
        {
            try
            {
                using (FileStream stream = new FileStream(
                            path,FileMode.OpenOrCreate,FileAccess.ReadWrite))
                {
                    BinaryFormatter serialize = new BinaryFormatter();
                    serialize.Serialize(stream, t);
                }

            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// deserialize object from filename
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static T BinaryDeSerialize<T>(string path)
        {
            BinaryFormatter serialize = new BinaryFormatter();

            try
            {
                using (FileStream stream = new FileStream(
                            path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    T t = (T)serialize.Deserialize(stream);
                    return t;
                }

            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlPath"></param>
        /// <returns></returns>
        public static T DeserializeXMLFile<T>(string xmlPath)
            where T : class
        { 
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            try
            {
                using (FileStream stream = new FileStream(xmlPath, FileMode.Open))
                {
                    T t = (T)serializer.Deserialize(stream);
                    return t;
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
