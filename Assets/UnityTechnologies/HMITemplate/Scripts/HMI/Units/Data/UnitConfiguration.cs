using HMI.Clusters.Enums;
using UnityEngine;

namespace HMI.Units.Data
{
    /// <summary>
    /// Unit configuration for all screens
    /// </summary>
    [CreateAssetMenu(fileName = "UnitConfiguration.asset", menuName = "HMI/Unit Configuration", order = 1)]
    public class UnitConfiguration : ScriptableObject
    {
        /// <summary>
        /// The temperature unit type used in the cluster
        /// </summary>
        [Tooltip("The temperature unit type used")]
        public TemperatureType TemperatureType;

        /// <summary>
        /// The unit of length used (metric/imperial)
        /// </summary>
        [Tooltip("The unit of length used (metric/imperial)")]
        public UnitOfLengthType UnitOfLength;
    }
}
