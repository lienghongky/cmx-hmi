using UnityEngine;

namespace HMI.Vehicles.Data
{
    /// <summary>
    /// Data for a climate zone in the vehicle
    /// </summary>
    [CreateAssetMenu(fileName = "ClimateZone.asset", menuName = "HMI/Vehicle/HVAC/Climate Zone", order = 1)]
    public class ClimateZoneData : ScriptableObject
    {
        /// <summary>
        /// Name of the climate zone
        /// </summary>
        [Tooltip("Name of the climate zone")]
        public string Name;

        /// <summary>
        /// Temperature in the climate zone
        /// </summary>
        [Tooltip("Temperature in the climate zone")]
        [Range(0, 100)]
        public float Temperature;

        /// <summary>
        /// Minimum allowed temperature
        /// </summary>
        [Tooltip("Minimum allowed temperature")]
        [Range(0, 100)]
        public float MinTemperature;

        /// <summary>
        /// Maximum allowed temperature
        /// </summary>
        [Tooltip("Maximum allowed temperature")]
        [Range(1, 100)]
        public float MaxTemperature;

        /// <summary>
        /// Increase the temperature in the climate zone
        /// </summary>
        /// <param name="interval">Fow much to increase</param>
        public void IncreaseTemperature(float interval)
        {
            Temperature = Mathf.Clamp(Temperature + interval, MinTemperature, MaxTemperature);
        }

        /// <summary>
        /// Decrease the temperature in the climate zone
        /// </summary>
        /// <param name="interval">How much to decrease</param>
        public void DecreaseTemperature(float interval)
        {
            Temperature = Mathf.Clamp(Temperature - interval, MinTemperature, MaxTemperature);
        }
    }
}
