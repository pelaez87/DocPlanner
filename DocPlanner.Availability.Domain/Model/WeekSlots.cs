namespace DocPlanner.Availability.Domain.Model
{
    /// <summary>
    /// Week slots model.
    /// </summary>
    public class WeekSlots
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WeekSlots" /> class.
        /// </summary>
        /// <param name="facility">The facility.</param>
        /// <param name="mondaySlots">The monday available slots.</param>
        /// <param name="tuesdaySlots">The tuesday available slots.</param>
        /// <param name="wednesdaySlots">The wednesday available slots.</param>
        /// <param name="thrusdaySlots">The thrusday available slots.</param>
        /// <param name="fridaySlots">The friday available slots.</param>
        /// <param name="saturdaySlots">The saturday available slots.</param>
        /// <param name="sundaySlots">The sunday available slots.</param>
        public WeekSlots(Facility facility,
                         DateTime[] mondaySlots,
                         DateTime[] tuesdaySlots,
                         DateTime[] wednesdaySlots,
                         DateTime[] thrusdaySlots,
                         DateTime[] fridaySlots,
                         DateTime[] saturdaySlots,
                         DateTime[] sundaySlots)
        {
            Facility = facility;
            Monday = mondaySlots;
            Tuesday = tuesdaySlots;
            Wednesday = wednesdaySlots;
            Thursday = thrusdaySlots;
            Friday = fridaySlots;
            Saturday = saturdaySlots;
            Sunday = sundaySlots;
        }

        /// <summary>
        /// Gets the facility.
        /// </summary>
        public Facility Facility { get; }

        /// <summary>
        /// Gets the Monday available slots.
        /// </summary>
        public DateTime[] Monday { get; }

        /// <summary>
        /// Gets the Tuesday available slots.
        /// </summary>
        public DateTime[] Tuesday { get; }

        /// <summary>
        /// Gets the Wednesday available slots.
        /// </summary>
        public DateTime[] Wednesday { get; }

        /// <summary>
        /// Gets the Thursday available slots.
        /// </summary>
        public DateTime[] Thursday { get; }

        /// <summary>
        /// Gets the Friday available slots.
        /// </summary>
        public DateTime[] Friday { get; }

        /// <summary>
        /// Gets the Saturday available slots.
        /// </summary>
        public DateTime[] Saturday { get; }

        /// <summary>
        /// Gets the Sunday available slots.
        /// </summary>
        public DateTime[] Sunday { get; }
    }
}