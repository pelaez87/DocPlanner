namespace DocPlanner.Availability.Domain
{
    /// <summary>
    /// Helper to work with dates.
    /// </summary>
    public static class DateHelper
    {
        /// <summary>
        /// Gets the monday of the week.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>The date in the same week of value provided where DayOfWeek is Monday.</returns>
        public static DateTime GetMondayOfTheWeek(this DateTime date)
        {
            int diff = (7 + (date.DayOfWeek - DayOfWeek.Monday)) % 7;
            return date.AddDays(-1 * diff).Date;
        }

        /// <summary>
        /// Gets a date with the hour provided ignoring minutes and seconds.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="hour">The hour to be set.</param>
        /// <returns>The date with exact hour time.</returns>
        public static DateTime WithHour(this DateTime date, int hour)
        {
            return date.Date.AddHours(hour);
        }
    }
}