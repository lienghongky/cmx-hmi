using UnityEngine;

namespace HMI.Vehicles.Data
{
    /// <summary>
    /// Dataset representing the battery specifications
    /// </summary>
    [CreateAssetMenu(fileName = "BatteryData.asset", menuName = "HMI/Vehicle/Battery", order = 1)]
    public class BatteryData : ScriptableObject
    {
        /// <summary>
        /// The total battery capacity in Kwh
        /// </summary>
        [Tooltip("The total battery capacity in Kwh")]
        [Range(0, 200)]
        public float Capacity = 100;

        /// <summary>
        /// The current battery capacity in Kwh
        /// </summary>
        [Tooltip("The current battery capacity in Kwh")]
        [Range(0, 200)]
        public float StartCapacity = 100;
    }
}
