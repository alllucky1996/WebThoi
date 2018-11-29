using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Common.Helpers
{
    public static class DateTimeHelper
    {
        private static readonly Dictionary<string, int> Arrays = new Dictionary<string, int>()
                                                                     {
                                                                         {"CN", 0},
                                                                         {"Thứ 2", 1},
                                                                         {"Thứ 3", 2},
                                                                         {"Thứ 4", 3},
                                                                         {"Thứ 5", 4},
                                                                         {"Thứ 6", 5},
                                                                         {"Thứ 7", 6},
                                                                     };
        private static readonly Dictionary<int, string> ArrayDayOfWeek = new Dictionary<int, string>()
                                                                     {
                                                                         {0, "Chủ Nhật"},
                                                                         {1, "Thứ 2"},
                                                                         {2, "Thứ 3"},
                                                                         {3, "Thứ 4"},
                                                                         {4, "Thứ 5"},
                                                                         {5, "Thứ 6"},
                                                                         {6, "Thứ 7"},
                                                                     };

        public static string DayOfWeek(DateTime date)
        {
            var dayInWeek = (int)date.DayOfWeek;
            string result = ArrayDayOfWeek[dayInWeek];
            return result;
        }

        public static DateTime StartOfWeek(this DateTime dt)//starting of week is sun
        {
            var value = int.Parse(Arrays.Where(a => dt.DayOfWeek.ToString().StartsWith(a.Key)).Select(a => a.Value).FirstOrDefault().ToString());
            var temp = dt.AddDays(-value);

            return new DateTime(temp.Year, temp.Month, temp.Day, 0, 0, 0);
        }
        public static DateTime EndOfWeek(this DateTime dt)// ending of week is sat 
        {
            var temp = dt.StartOfWeek().AddDays(6);

            return new DateTime(temp.Year, temp.Month, temp.Day, 23, 59, 59);
        }

        public static String FormatTime(TimeSpan time)
        {
            string hourFormat = time.Hours.ToString();
            if (time.Hours < 10) hourFormat = "0" + time.Hours.ToString();

            string minuteFormat = time.Minutes.ToString();
            if (time.Minutes < 10) minuteFormat = "0" + time.Minutes.ToString();

            return hourFormat + ":" + minuteFormat;
        }

        public static bool IsDate(string date)
        {
            return Regex.IsMatch(date, "^([0]?[1-9]|[1|2][0-9]|[3][0|1])[./-]([0]?[1-9]|[1][0-2])[./-]([0-9]{4}|[0-9]{2})$");
        }

        /// <summary>
        /// Compare date with date now
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static bool IsDateWithNow(DateTime date)
        {
            if (date.CompareTo(DateTime.Today) != -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Compare time with time now
        /// </summary>
        /// <param name="fromTime"></param>
        /// <returns></returns>
        public static bool IsTimeWithNow(TimeSpan time)
        {
            if (time.CompareTo(DateTime.Now.TimeOfDay) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string DayOfDate(DateTime date)
        {
            int i = Convert.ToInt16(date.DayOfWeek.ToString());
            return "";
        }

        public static DateTime ToDataTime(string date)
        {
            try
            {
                string[] dateSplit = date.Split('/');
                var _date = new DateTime(Int16.Parse(dateSplit[2]), Int16.Parse(dateSplit[1]), Int16.Parse(dateSplit[0]));
                return _date;
            }
            catch { return ToMinValue(); }
        }

        public static bool CompareDate(DateTime? date, DateTime? date1)
        {
            if (!date.HasValue && date1.HasValue) return false;
            if (date.HasValue && !date1.HasValue) return false;
            if (date.HasValue)
                return string.Equals(((DateTime)date).ToString("ddMMyyyy"), ((DateTime)date1).ToString("ddMMyyyy"),
                    StringComparison.CurrentCultureIgnoreCase);
            return true;
        }

        public static DateTime ToDataTime(string date, string time)
        {
            try
            {
                string[] dateSplit = date.Split('/');
                string[] timeSplit = time.Split(':');
                var _date = new DateTime(Int16.Parse(dateSplit[2]), Int16.Parse(dateSplit[1]), Int16.Parse(dateSplit[0]), Int16.Parse(timeSplit[0]), Int16.Parse(timeSplit[1]), 0);
                return _date;
            }
            catch { return ToMinValue(); }
        }

        public static DateTime ToMinValue()
        {
            return new DateTime(1900, 1, 1, 0, 0, 0, 0);
        }

        public static DateTime ToMaxValue()
        {
            return new DateTime(3000, 1, 1, 0, 0, 0, 0);
        }
        public static string ShowDateTime(DateTime datetime, bool timeFlg = true)
        {
            string result = "";
            var dayInWeek = (int)datetime.DayOfWeek;
            if (timeFlg)
            {
                result = ArrayDayOfWeek[dayInWeek] + ", " + datetime.ToString("H:mm, dd/MM/yyyy");
            }
            else
            {
                result = ArrayDayOfWeek[dayInWeek] + ", " + datetime.ToString("dd/MM/yyyy");
            }
            return result;
        }

        public static DateTime FirstDayOfCurrentMonth()
        {
            return new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0, 0);
        }

        public static DateTime Today(int hour = 0, int minute = 0, int second = 0)
        {
            return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hour, minute, second, 0);
        }

        public static string ShowTwoDate(DateTime date1, DateTime date2)
        {
            string result = "";
            DateTime dateDefault = ToMinValue();
            if (date1.CompareTo(dateDefault) > 0 && date2.CompareTo(dateDefault) > 0)
            {
                result = date1.ToString("dd/MM/yyyy") + " ~ " + date2.ToString("dd/MM/yyyy");
            }
            else if (date1.CompareTo(dateDefault) > 0)
            {
                result = date1.ToString("dd/MM/yyyy");
            }
            else if (date2.CompareTo(dateDefault) > 0)
            {
                result = date2.ToString("dd/MM/yyyy");
            }
            return result;
        }

        public static string ShowDateData(DateTime data)
        {
            string result = "";
            DateTime dateDefault = ToMinValue();
            if (data.CompareTo(dateDefault) > 0)
            {
                result = data.ToString("dd/MM/yyyy");
            }
            return result;
        }
    }
}
