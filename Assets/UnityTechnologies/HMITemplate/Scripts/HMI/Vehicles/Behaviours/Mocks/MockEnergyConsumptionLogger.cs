using HMI.Vehicles.Behaviours.Base;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HMI.Vehicles.Behaviours.Mocks
{
    /// <summary>
    /// Example Mock implementation that logs how much energy is being used from a battery
    /// </summary>
    public class MockEnergyConsumptionLogger : EnergyConsumptionLoggerBase
    {
        /// <summary>
        /// Provided list of entries
        /// </summary>
        [SerializeField]
        private List<EnergyConsumptionLogEntry> MockLogEntries = new List<EnergyConsumptionLogEntry>();

        /// <summary>
        /// Returns the latest energy consumption entry
        /// </summary>
        public override EnergyConsumptionLogEntry LatestEntry => MockLogEntries.First();

        /// <summary>
        /// The list of log entries.
        /// </summary>
        public override IEnumerable<EnergyConsumptionLogEntry> LogEntries => MockLogEntries;
    }
}
