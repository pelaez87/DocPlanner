namespace DocPlanner.Availability.Domain.Model
{
    /// <summary>
    /// Facility model.
    /// </summary>
    public class Facility
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Facility" /> class.
        /// </summary>
        /// <param name="id">The facility identifier.</param>
        /// <param name="name">The facility name.</param>
        /// <param name="address">The facility address.</param>
        public Facility(Guid id, string name, string address)
        {
            Id = id;
            Name = name;
            Address = address;
        }

        /// <summary>
        /// Gets the facility identifier.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Gets the facility name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the facility address.
        /// </summary>
        public string Address { get; }
    }
}
