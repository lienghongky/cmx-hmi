using System;

namespace HMI.Vehicles.Behaviours.DateTimeProviders
{
    /// <summary>
    /// Provided the internal datetime now
    /// </summary>
    public class DateTimeProviderNow : IDateTimeProvider
    {
        /// <summary>
        /// Provide the datetime
        /// </summary>
        /// <returns>DateTime</returns>
        public DateTime GetDateTime()
        {
            return DateTime.Now;
        }
    }
}
