using UnityEngine;

namespace HMI.Vehicles.Data
{
    /// <summary>
    /// Air distribution mode settings
    /// </summary>
    [CreateAssetMenu(fileName = "AirDistributionModeData.asset", menuName = "HMI/Vehicle/HVAC/Air Distribution Mode", order = 1)]
    public class AirDistributionModeData : ScriptableObject
    {
        /// <summary>
        /// Name of the air distribution mode
        /// </summary>
        [Tooltip("Name of the air distribution mode")]
        public string Name;

        /// <summary>
        /// The default state of the mode (on or off)
        /// </summary>
        [Tooltip("The default state of the mode")]
        public bool DefaultState;
    }
}
