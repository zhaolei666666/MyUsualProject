using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util.Other
{
    public class TimeHelper
    {
        #region 实现发表的时间显示
        /// <summary>
        /// 实现发表的时间显示为几个月,几天前,几小时前,几分钟前,或几秒前
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string DateStringFromNow(DateTime time)
        {
            TimeSpan span = DateTime.Now - time;
            if (span.TotalDays > 120)
            {
                return time.ToShortDateString();
            }
            else if (span.TotalDays > 90)
            {
                return "3个月前";
            }
            else if (span.TotalDays > 60)
            {
                return "2个月前";
            }
            else if (span.TotalDays > 30)
            {
                return "1个月前";
            }
            else if (span.TotalDays > 14)
            {
                return "2周前";
            }
            else if (span.TotalDays > 7)
            {
                return "1周前";
            }
            else if (span.TotalDays > 1)
            {
                return string.Format("{0}天前", (int)Math.Floor(span.TotalDays));
            }
            else if (span.TotalHours > 1)
            {
                return string.Format("{0}小时前", (int)Math.Floor(span.TotalHours));
            }
            else if (span.TotalMinutes > 1)
            {
                return string.Format("{0}分钟前", (int)Math.Floor(span.TotalMinutes));
            }
            else if (span.TotalSeconds >= 1)
            {
                return string.Format("{0}秒前", (int)Math.Floor(span.TotalSeconds));
            }
            else
            {
                return "1秒前";
            }
        }
        #endregion

        #region 根据指定的日期获取当前的星期一日期
        /// <summary>
        /// 根据指定的日期获取当前的星期一日期
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime GetMondayDate(DateTime dateTime)
        {
            int dd = Convert.ToInt32(dateTime.DayOfWeek);
            if (dd == 0) dd = 7;
            return dateTime.AddDays(-dd + 1);
        }
        #endregion

        #region 获取指定日期获取当前的星期天日期
        /// <summary>
        /// 获取指定日期获取当前的星期天日期
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime GetSundayDate(DateTime dateTime)
        {
            return dateTime.AddDays(1 - Convert.ToInt32(dateTime.DayOfWeek.ToString("d"))).AddDays(6);
        }
        #endregion

        #region 获取两者间的相隔天数
        /// <summary>
        /// 获取两者间的相隔天数
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static int GetApartDay(DateTime beginDate, DateTime endDate)
        {
            TimeSpan span = endDate - beginDate;
            return Convert.ToInt32(span.TotalDays);
        }
        #endregion

        #region get seconds from now to today end
        public static long GetSeconds()
        {
            DateTime dtnow = DateTime.Now;
            string dtmorning = dtnow.ToString("yyyy-MM-dd") + " 23:59:59";
            DateTime dtm = DateTime.Parse(dtmorning);

            TimeSpan ts = dtm.Subtract(dtnow);

            return (long)ts.TotalSeconds;
        }
        #endregion

    }
}
