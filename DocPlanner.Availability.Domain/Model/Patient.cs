namespace DocPlanner.Availability.Domain.Model
{
    /// <summary>
    /// Patient model.
    /// </summary>
    public class Patient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Patient"/> class.
        /// </summary>
        /// <param name="name">The patient name.</param>
        /// <param name="secondName">The patient second name.</param>
        /// <param name="email">The patient email.</param>
        /// <param name="phoneNumber">The patient phone number.</param>
        public Patient(string name, string secondName, string email, string phoneNumber)
        {
            Name = name;
            SecondName = secondName;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        /// <summary>
        /// Gets the patient name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the patient second name.
        /// </summary>
        public string SecondName { get; }

        /// <summary>
        /// Gets the patient email.
        /// </summary>
        public string Email { get; }

        /// <summary>
        /// Gets the patient phone number.
        /// </summary>
        public string PhoneNumber { get; }
    }
}
