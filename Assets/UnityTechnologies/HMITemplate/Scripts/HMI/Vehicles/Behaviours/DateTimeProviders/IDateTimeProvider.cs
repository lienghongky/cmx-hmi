using System;

namespace HMI.Vehicles.Behaviours.DateTimeProviders
{
    /// <summary>
    /// The date time provider allows the user to create different implementations
    /// for the vehicle clock.
    /// </summary>
    public interface IDateTimeProvider
    {
        /// <summary>
        /// Provide the datetime
        /// </summary>
        /// <returns>DateTime</returns>
        public DateTime GetDateTime();
    }
}
