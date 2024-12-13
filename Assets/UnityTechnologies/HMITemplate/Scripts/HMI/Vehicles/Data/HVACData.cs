using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HMI.Vehicles.Data
{
    /// <summary>
    /// Data for the configuration of the hvac
    /// </summary>
    [CreateAssetMenu(fileName = "HVACData.asset", menuName = "HMI/Vehicle/HVAC/HVAC", order = 1)]
    public class HVACData : ScriptableObject
    {
        /// <summary>
        /// The temperature outside the vehicle in celsius
        /// </summary>
        [Tooltip("The temperature outside the vehicle in celsius")]
        [Range(-100, 100)]
        public float TemperatureOutside;

        /// <summary>
        /// Is the auto temp control activated
        /// </summary>
        [Tooltip("Is the auto temp control activated")]
        public bool IsAutoTempControlActivated;

        /// <summary>
        /// Is the air conditioning activated
        /// </summary>
        [Tooltip("Is the auto temp control activated")]
        public bool IsAirConditioningActivated;

        /// <summary>
        /// Is the Max air conditioning activated
        /// </summary>
        [Tooltip("Is the Max air conditioning activated")]
        public bool IsMaxAirConditioningActivated;

        /// <summary>
        /// Is the air recirculation function activated
        /// </summary>
        [Tooltip("Is the air recirculation function activated")]
        public bool IsRecirculationActivated;

        /// <summary>
        /// Is the air In function activated
        /// </summary>
        [Tooltip("Is the air In function activated")]
        public bool IsAirInActivated;

        /// <summary>
        /// Is the air In function activated
        /// </summary>
        [Tooltip("Is the air In function activated")]
        public bool IsSyncButtonActivated;

        /// <summary>
        /// Is the heating of windows activated
        /// </summary>
        [Tooltip("Is the heating of windows activated")]
        public bool IsDefrostFrontActivated;

        /// <summary>
        /// Is the heating of windows activated
        /// </summary>
        [Tooltip("Is the heating of windows activated")]
        public bool IsDefrostRearActivated;

        /// <summary>
        /// Is the left seat massage activated
        /// </summary>
        [Tooltip("s the left seat ventilation activated")]
        public bool IsLeftSeatVentilationActivated;

        /// <summary>
        /// Is the right seat massage activated
        /// </summary>
        [Tooltip("Is the right seat ventilation activated")]
        public bool IsRightSeatVentilationActivated;

        /// <summary>
        /// Air distribution System Data
        /// </summary>
        [Tooltip("Air distribution System Data")]
        public AirDistributionData AirDistributionData;

        /// <summary>
        /// The heating systems data integrated in the HVAC
        /// </summary>
        [Tooltip("The heating systems data integrated in the HVAC")]
        public List<HeatingData> HeatingSystemsData = new List<HeatingData>();

        /// <summary>
        /// The blowers data integrated in the HVAC
        /// </summary>
        [Tooltip("The blowers data integrated in the HVAC")]
        public List<BlowerData> BlowersData = new List<BlowerData>();

        /// <summary>
        /// Climate zones data integrated in the HVAC
        /// </summary>
        [Tooltip("Climate zones data integrated in the HVAC")]
        public List<ClimateZoneData> ClimateZonesData = new List<ClimateZoneData>();

        /// <summary>
        /// Get heating system with a specific name
        /// </summary>
        /// <param name="name">Name of the heating element</param>
        /// <returns>the heating element with a specific name, null if the heating element does not exist</returns>
        public HeatingData GetHeatingSystem(string name)
        {
            return HeatingSystemsData.FirstOrDefault(x => x.HeatingName == name);
        }

        /// <summary>
        /// Get a blower with a specific name
        /// </summary>
        /// <param name="name">Name of the blower</param>
        /// <returns>the blower with a specific name, null if the blower does not exist</returns>
        public BlowerData GetBlower(string name)
        {
            return BlowersData.FirstOrDefault(x => x.BlowerName == name);
        }

        /// <summary>
        /// Get a climate zone with a specific name
        /// </summary>
        /// <param name="name">Name of the blower</param>
        /// <returns>the blower with a specific name, null if the blower does not exist</returns>
        public ClimateZoneData GetClimateZone(string name)
        {
            return ClimateZonesData.FirstOrDefault(x => x.Name == name);
        }
    }
}