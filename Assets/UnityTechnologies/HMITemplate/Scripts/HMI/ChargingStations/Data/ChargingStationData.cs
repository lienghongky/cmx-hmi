using UnityEngine;

namespace HMI.ChargingStations.Data
{
    /// <summary>
    /// Configuration data for a charging station
    /// </summary>
    [CreateAssetMenu(fileName = "ChargingStationData.asset", menuName = "HMI/Charging Stations/Charging Station", order = 1)]
    public class ChargingStationData : ScriptableObject
    {
        /// <summary>
        /// Power provided by the charging station in Kwh
        /// </summary>
        [Tooltip("Power provided by the charging station in Kwh")]
        [Range(1, 1000)]
        public float PowerGenerated;
    }
}
