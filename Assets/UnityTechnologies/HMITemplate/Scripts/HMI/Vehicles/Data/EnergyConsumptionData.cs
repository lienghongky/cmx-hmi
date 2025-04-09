using UnityEngine;

namespace HMI.Vehicles.Data
{
    /// <summary>
    /// Dataset representing the energy consumption profile of a vehicle
    /// </summary>
    [CreateAssetMenu(fileName = "EnergyConsumptionData.asset", menuName = "HMI/Vehicle/Energy Consumption", order = 1)]
    public class EnergyConsumptionData : ScriptableObject
    {
        /// <summary>
        /// Curve that calculates the energy consumption per kilometer traveled in Kwh based on the speed in Km/h
        /// </summary>
        [Tooltip("Curve that calculates the energy consumption in Kwh based on the speed in Km/h")]
        public AnimationCurve EnergyUsagePerKm;

        /// <summary>
        /// Mimics the average historical consumption, how many Kilometers the vehicle has traveled on a single Kwh. For example can be used to calculate range
        /// </summary>
        [Tooltip("Mimics the average historical consumption, how many kilometers the vehicle has traveled on a single Kwh")]
        [Range(1, 25)]
        public float HistoricalKmPerKwh;

        /// <summary>
        /// To have a visual impact on the battery this multiplier multiplies the influence of any battery influencer
        /// </summary>
        [Tooltip("To have a visual impact on the battery this multiplier multiplies the influence of any battery influencer")]
        [Range(1, 100)]
        public float ConsumptionMultiplier = 1;

        /// <summary>
        /// Calculate the energy efficiency in Kwh used based on the provided speed
        /// </summary>
        /// <param name="speed">Input speed used to determine power consumption in Kwh</param>
        /// <returns>Power consumption in Kwh</returns>
        public float CalculateEnergyConsumption(float speed)
        {
            return EnergyUsagePerKm.Evaluate(speed);
        }
    }
}
