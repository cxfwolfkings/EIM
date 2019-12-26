using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
/*******************************
 * Created By franco
 * Description: log type
 *******************************/


namespace CacheManager
{
    /// <summary>
    /// serialize by binaryFormatter
    /// </summary>
    internal static  class SerializationHelper
    {
        
        /// <summary>
        /// serialize object to byte[]
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] Serialize(object obj)
        {
            using (var stream = new MemoryStream())
             
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, obj);
                return stream.ToArray();
            }
        }
        
        /// <summary>
        /// Deserialize To object
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static object Deserialize(byte[] buffer)
        {
            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                stream.Write(buffer, 0, buffer.Length);
                stream.Position = 0;

                return formatter.Deserialize(stream);
            }
        }

    }
}
