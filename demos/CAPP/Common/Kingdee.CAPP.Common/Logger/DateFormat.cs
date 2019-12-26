using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/*******************************
 * Created By franco
 * Description: log type
 *******************************/


namespace Kingdee.CAPP.Common.Logger
{
    /// <summary>
    /// set date format
    /// </summary>
    class DateFormat : ILogDate
    {
        private DateFormat()
        {

        }

        private static ILogDate _logdate;

        public static ILogDate LogDate
        {
            get
            {
                if (_logdate == null)
                {
                    _logdate = new DateFormat();
                }
                return _logdate;
            }
        }

        /// <summary>
        /// char prefix
        /// </summary>
        private string prefix
        {
            get
            {
                DateTime dt = DateTime.Now;
                string _prefix = string.Format("{0,4}/{1,2}/{2,2} {3,2}:{4,2}:{5,4} ",
                Get<int>(dt.Year, 4, 4),
                Get<int>(dt.Month, 2, 4),
                Get<int>(dt.Day, 2, 4),
                Get<int>(dt.Hour, 2, 2),
                Get<int>(dt.Minute, 2, 2),
                Get<int>(dt.Millisecond, 4, 4));

                return _prefix;

            }
        }

        /// <summary>
        /// set input content length
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        private static string Get<T>(T t, int min, int max)
        {
            if (t.ToString().Length < min)
            {
                string v = t.ToString();
                for (int i = 0, k = min - v.Length; i < k; i++)
                {
                    v = "0" + v;
                }
                return v;
            }
            else if (t.ToString().Length > max)
            {
                return t.ToString().Remove(max) + "...";
            }
            return t.ToString();

        }


        public string GetLogDateFormat(DateTime dt)
        {
            return GetFormat("{1}/{2}/{0} {3}:{4}:{5}.{6}", dt);
        }

        public string GetDateName(DateTime dt)
        {
            return GetFormat("{0}{1}{2}", dt);
        }

        /// <summary>
        /// format char 
        /// </summary>
        /// <param name="format"></param>
        /// <param name="datetime"></param>
        /// <returns></returns>
        private static string GetFormat(string format, DateTime datetime)
        {
            string year = datetime.Year.ToString();

            string month = datetime.Month.ToString();
            if (month.Length == 1)
            {
                month = "0" + month;
            }

            string day = datetime.Day.ToString();
            if (day.Length == 1)
            {
                day = "0" + day;
            }

            string hour = datetime.Hour.ToString();
            if (hour.Length == 1)
            {
                hour = "0" + hour;
            }

            string minute = datetime.Minute.ToString();
            if (minute.Length == 1)
            {
                minute = "0" + minute;
            }

            string second = datetime.Second.ToString();
            if (second.Length == 1)
            {
                second = "0" + second;
            }

            string millisecond = datetime.Millisecond.ToString();
            if (millisecond.Length == 3)
            {
                millisecond = "0" + millisecond;
            }
            else if (millisecond.Length == 2)
            {
                millisecond = "00" + millisecond;
            }
            else if (millisecond.Length == 1)
            {
                millisecond = "000" + millisecond;
            }

            return string.Format(format, year, month, day, hour, minute, second, millisecond);
        }
    }
}
