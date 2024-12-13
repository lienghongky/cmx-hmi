using System.Collections.Generic;
using UnityEngine;

namespace HMI.Vehicles.Behaviours.Base
{
    /// <summary>
    /// Logs how much energy is being used from a battery
    /// </summary>
    public abstract class EnergyConsumptionLoggerBase : MonoBehaviour
    {
        /// <summary>
        /// Returns the latest energy consumption entry
        /// </summary>
        public abstract EnergyConsumptionLogEntry LatestEntry { get; }

        /// <summary>
        /// The list of log entries.
        /// </summary>
        public abstract IEnumerable<EnergyConsumptionLogEntry> LogEntries { get; }
    }
}
