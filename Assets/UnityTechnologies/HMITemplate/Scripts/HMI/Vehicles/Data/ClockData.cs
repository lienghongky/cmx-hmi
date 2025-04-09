using HMI.Vehicles.Behaviours.DateTimeProviders;
using HMI.Vehicles.Data.Interfaces;
using System;
using System.Globalization;
using UnityEngine;

namespace HMI.Vehicles.Data
{
    /// <summary>
    /// Internal clock
    /// </summary>
    [CreateAssetMenu(fileName = "Internal Clock.asset", menuName = "HMI/Vehicle/Internal Clock", order = 1)]
    public class ClockData : ScriptableObject, IClock
    {
        /// <summary>
        /// A datetime provider, by default the datetime.now is used
        /// </summary>
        public IDateTimeProvider DateTimeProvider = new DateTimeProviderNow();

        /// <summary>
        /// Current date/time
        /// </summary>
        public DateTime DateTime
        {
            get
            {
                return DateTimeProvider.GetDateTime().Date;
            }
        }

        /// <summary>
        /// Current day of the month
        /// </summary>
        public int DayOfMonth
        {
            get
            {
                return DateTimeProvider.GetDateTime().Day;
            }
        }

        /// <summary>
        /// Indicates if the current time is am or pm (Uppercase)
        /// </summary>
        public string AmPm
        {
            get
            {
                return DateTimeProvider.GetDateTime().ToString("tt", CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// 12 hour time indicator
        /// </summary>
        public string DateTime12hr
        {
            get
            {
                return DateTimeProvider.GetDateTime().ToString("h:mm", CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Day of the week name
        /// </summary>
        public string DayOfWeekName
        {
            get
            {
                return DateTimeProvider.GetDateTime().ToString("dddd", CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Day of the week name 3 character substring
        /// </summary>
        public string DayOfWeekNameAbbreviated
        {
            get
            {
                return DateTimeProvider.GetDateTime().ToString("ddd", CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Month name 
        /// </summary>
        public string MonthName
        {
            get
            {
                return DateTimeProvider.GetDateTime().ToString("MMMM", CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Month name 3 character substring
        /// </summary>
        public string MonthNameAbbreviated
        {
            get
            {
                return DateTimeProvider.GetDateTime().ToString("MMM", CultureInfo.InvariantCulture);
            }
        }
    }
}