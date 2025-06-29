using System;

namespace GTPriceImporterService
{
    internal static class CheckDate
    {
        internal static bool Is_Valid_Date(string date)
        {
            if (date != null && CheckString.IsValidString(date) == true)
            {
                if (IsDate(date) == true)
                {
                    return true;
                }
            }

            return false;
        }

        internal static bool Is_Valid_Date_Time(string date)
        {
            if (date != null && CheckString.IsValidString(date) == true)
            {
                if (IsDateTime(date) == true)
                {
                    return true;
                }
            }

            return false;
        }

        private static bool IsDate(string tempDate)
        {
            DateTime fromDateValue;
            var formats = new[]
            {
                "yyyy-MM-dd"
            };
            if (DateTime.TryParseExact(tempDate, formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out fromDateValue))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool IsDateTime(string tempDate)
        {
            DateTime fromDateValue;
            var formats = new[]
            {
                "yyyy-MM-dd HH:mm:ss"
            };
            if (DateTime.TryParseExact(tempDate, formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out fromDateValue))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal static DateTime StringToDateTime(string date)
        {
            return (DateTime)Convert.ChangeType(date, typeof(DateTime));
        }
    }
}