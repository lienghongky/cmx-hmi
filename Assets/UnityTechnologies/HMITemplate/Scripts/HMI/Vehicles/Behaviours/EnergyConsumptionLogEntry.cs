using System;

namespace HMI.Vehicles.Behaviours
{
    /// <summary>
    /// Single log entry in the energy consumption logger
    /// </summary>
    [Serializable]
    public class EnergyConsumptionLogEntry
    {
        /// <summary>
        /// Distance travelled in kilometers
        /// </summary>
        public double Distance;

        /// <summary>
        /// Energy usage in kWh/100km
        /// </summary>
        public float EnergyUsage;

        /// <summary>
        /// Produced energy in kWh/100km
        /// </summary>
        public float ProducedEnergy;

        /// <summary>
        /// Consumed energy in kWh/100km
        /// </summary>
        public float ConsumedEnergy;
    }
}
