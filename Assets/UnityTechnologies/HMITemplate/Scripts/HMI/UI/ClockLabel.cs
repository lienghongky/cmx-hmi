using HMI.Vehicles.Data.Interfaces;
using HMI.Vehicles.Services;
using UnityEngine;

namespace HMI.UI
{
    /// <summary>
    /// The cluster clock shows the current date and time
    /// </summary>
    public class ClockLabel : MonoBehaviour
    {
        /// <summary>
        /// Clock logic that this cluster visualization is linked to
        /// </summary>
        private IClock ClockDataSource;

        /// <summary>
        /// Date UI text representation
        /// </summary>
        public TMPro.TMP_Text Date;

        /// <summary>
        /// Time UI text representation
        /// </summary>
        public TMPro.TMP_Text Time;

        /// <summary>
        /// Unity Awake callback
        /// </summary>
        private void Awake()
        {
            if (ClockDataSource == null)
            {
                ClockDataSource = ClockService.GetClock();
            }
        }

        /// <summary>
        /// Unity Update Event
        /// </summary>
        private void Update()
        {
            var txtColorSmall = ColorUtility.ToHtmlStringRGB(Time.color);
            txtColorSmall += "AA";

            Date.text = $@"{ClockDataSource.DayOfWeekNameAbbreviated} <font=""DinLight_Glow"" material=""DinLight_NoGlow"">{ClockDataSource.MonthNameAbbreviated} {ClockDataSource.DayOfMonth}</font>";
            Time.text = $@"{ClockDataSource.DateTime12hr}<font=""DinLight_Glow"" material=""DinLight_NoGlow""><size=60%><#{txtColorSmall}>{ClockDataSource.AmPm.ToLowerInvariant()}</size></font>";
        }
    }
}
