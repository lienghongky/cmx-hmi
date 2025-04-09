using System;

namespace HMI.Vehicles.Data.Interfaces
{
    /// <summary>
    /// clock
    /// </summary>
    public interface IClock
    {
        /// <summary>
        /// Current date/time
        /// </summary>
        DateTime DateTime { get; }

        /// <summary>
        /// Current day of the month
        /// </summary>
        int DayOfMonth { get; }

        /// <summary>
        /// Indicates if the current time is am or pm (Uppercase)
        /// </summary>
        string AmPm { get; }

        /// <summary>
        /// 12 hour time indicator
        /// </summary>
        string DateTime12hr { get; }

        /// <summary>
        /// Day of the week name
        /// </summary>
        string DayOfWeekName { get; }

        /// <summary>
        /// Day of the week name 3 character substring
        /// </summary>
        string DayOfWeekNameAbbreviated { get; }

        /// <summary>
        /// Month name 
        /// </summary>
        string MonthName { get; }

        /// <summary>
        /// Month name 3 character substring
        /// </summary>
        string MonthNameAbbreviated { get; }
    }
}
