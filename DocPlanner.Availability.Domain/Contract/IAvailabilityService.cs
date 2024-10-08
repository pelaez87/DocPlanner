using DocPlanner.Availability.Domain.Model;

namespace DocPlanner.Availability.Domain.Contract
{
    /// <summary>
    /// Represents the availability service actions.
    /// </summary>
    public interface IAvailabilityService
    {
        /// <summary>
        /// Gets the available slots on a specified date.
        /// </summary>
        /// <param name="date">The date to retrieve available slots.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The week slots that are available.</returns>
        Task<WeekSlots> GetWeekAvailableSlots(DateTime date, CancellationToken cancellationToken);

        /// <summary>
        /// Reserves the slot in doctor's agenda.
        /// </summary>
        /// <param name="slotDate">The date where the slot is requested for.</param>
        /// <param name="comments">The slot comments.</param>
        /// <param name="patient">The patient details.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task TakeSlot(DateTime slotDate, string comments, Patient patient, CancellationToken cancellationToken);
    }
}
