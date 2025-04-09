using UnityEngine;

namespace HMI.Vehicles.Data
{
    /// <summary>
    /// Configuration dataset for a energy consumption logger
    /// </summary>
    [CreateAssetMenu(fileName = "EnergyConsumptionLoggerData.asset", menuName = "HMI/Vehicle/Energy Consumption Logger", order = 1)]
    public class EnergyConsumptionLoggerData : ScriptableObject
    {
        /// <summary>
        /// Interval between log entries in kilometers
        /// </summary>
        [Tooltip("Interval between log entries in kilometers")]
        [Range(0.001f, 10f)]
        public float Interval = 0.2f;

        /// <summary>
        /// Maximum number of entries in the log
        /// </summary>
        [Tooltip("Maximum number of entries in the log")]
        [Range(1, 1000)]
        public int MaxEntries = 100;
    }
}
