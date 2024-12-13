using HMI.Vehicles.Behaviours.Base;
using HMI.Vehicles.Data;
using System.Collections.Generic;
using UnityEngine;

namespace HMI.Vehicles.Behaviours
{
    /// <summary>
    /// Logs how much energy is being used from a battery
    /// </summary>
    [RequireComponent(typeof(BatteryBase))]
    [RequireComponent(typeof(OdometerBase))]
    [RequireComponent(typeof(ElectricVehicleDataProvider))]
    public class EnergyConsumptionLogger : EnergyConsumptionLoggerBase
    {
        /// <summary>
        /// Configuration data for the logger
        /// </summary>
        private EnergyConsumptionLoggerData EnergyConsumptionLoggerData;

        /// <summary>
        /// Entries in the log
        /// </summary>
        private readonly LinkedList<EnergyConsumptionLogEntry> Entries = new LinkedList<EnergyConsumptionLogEntry>();

        /// <summary>
        /// Countdown for adding a log entry in meters
        /// </summary>
        private long EntryCount;

        /// <summary>
        /// The battery that is being logged
        /// </summary>
        private BatteryBase Battery;

        /// <summary>
        /// The odometer used to log the current trip distance for the odometer record
        /// </summary>
        private OdometerBase Odometer;

        /// <summary>
        /// Returns the latest energy consumption entry
        /// </summary>
        public override EnergyConsumptionLogEntry LatestEntry
        {
            get
            {
                return Entries.First?.Value;
            }
        }

        /// <summary>
        /// The list of log entries.
        /// </summary>
        public override IEnumerable<EnergyConsumptionLogEntry> LogEntries
        {
            get { return Entries; }
        }

        /// <summary>
        /// Unity Awake
        /// </summary>
        private void Awake()
        {
            EnergyConsumptionLoggerData = GetComponent<ElectricVehicleDataProvider>().ElectricVehicleData.EnergyConsumptionLogger;
            EntryCount = 0;
            Battery = GetComponent<BatteryBase>();
            Odometer = GetComponent<OdometerBase>();
        }

        /// <summary>
        /// Unity Late Update
        /// </summary>
        private void LateUpdate()
        {
            var distanceTravelled = Odometer.TripDistanceTraveled;
            var minDistance = EntryCount * EnergyConsumptionLoggerData.Interval;

            // log a finalized entry
            if (distanceTravelled >= minDistance)
            {
                LogEntry(minDistance);
                EntryCount++;

                while (Entries.Count > EnergyConsumptionLoggerData.MaxEntries)
                {
                    Entries.RemoveLast();
                }
            }
            else
            {
                // keep the "head" entry up to date until it is finalized in the log
                var latest = LatestEntry;
                if (latest != null)
                {
                    latest.Distance = distanceTravelled;
                    latest.EnergyUsage = Battery.CurrentEnergyUsage;
                    latest.ConsumedEnergy = Battery.CurrentEnergyConsumption;
                    latest.ProducedEnergy = Battery.CurrentEnergyProduction;
                }
            }
        }

        /// <summary>
        /// Log a single entry
        /// </summary>
        private void LogEntry(double distanceTravelled)
        {
            var entry = new EnergyConsumptionLogEntry
            {
                Distance = distanceTravelled,
                EnergyUsage = Battery.CurrentEnergyUsage,
                ConsumedEnergy = Battery.CurrentEnergyConsumption,
                ProducedEnergy = Battery.CurrentEnergyProduction
            };

            Entries.AddFirst(entry);
        }
    }
}
