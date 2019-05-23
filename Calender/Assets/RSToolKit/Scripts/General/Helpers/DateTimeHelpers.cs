namespace RSToolkit.Helpers
{
    using System;

    public static class DateTimeHelpers
    {
    
        public static DateTime DuplicateDate(this DateTime value, DateTime from){
            return new DateTime(from.Year, from.Month, from.Day);
        }

        public static DateTime GetYearMonthStart(this DateTime value){
            return GetYearMonthStart(value.Year, value.Month);
        }
        public static DateTime GetYearMonthStart(int Year, int Month){
            return new DateTime(Year, Month, 1);
        }
        public static bool IsCurrentYear(this DateTime value){
            return (value.Year == DateTime.Today.Year);
        }
        public static bool IsCurrentYearMonth(this DateTime value){
            return (IsCurrentYear(value) && value.Month == DateTime.Today.Month);
        }

        public static bool IsPastYearMonth(this DateTime value){
            return (value.Year < DateTime.Today.Year)
                || (value.Year == DateTime.Today.Year && value.Month < DateTime.Today.Month);
        }

        public static bool IsPast(this DateTime value){
            return (value.IsPastYearMonth()
                    || (value.Year == DateTime.Today.Year && value.Month == DateTime.Today.Month
                        && value.Day < DateTime.Today.Day));
        }

        public static bool IsFutureYearMonth(this DateTime value){
            return (value.Year > DateTime.Today.Year)
                || (value.Year == DateTime.Today.Year && value.Month > DateTime.Today.Month);
        }

        public static int DaysInMonth(this DateTime value){
            return DateTime.DaysInMonth(value.Year, value.Month);
        }

        public static bool IsSameYearMonth(this DateTime value, int Year, int Month){
            return value.IsSameYearMonth(new DateTime(Year, Month, 1));
        }
        public static bool IsSameYearMonth(this DateTime value, DateTime compareTo){
            return value.Year == compareTo.Year && value.Month == compareTo.Month;
        }
        public static bool IsSameDate(this DateTime value, DateTime compareTo){
            return value.Day == compareTo.Day && value.IsSameYearMonth(compareTo);
        }

    }
}