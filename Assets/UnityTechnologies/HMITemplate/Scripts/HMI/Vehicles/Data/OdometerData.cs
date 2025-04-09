using UnityEngine;

namespace HMI.Vehicles.Data
{
    /// <summary>
    /// Data for the configuration of an odometer
    /// </summary>
    [CreateAssetMenu(fileName = "OdometerData.asset", menuName = "HMI/Vehicle/Odometer", order = 1)]
    public class OdometerData : ScriptableObject
    {
        /// <summary>
        /// The starting distance the vehicle has travelled in kilometers
        /// </summary>
        [Range(0, 2000000)]
        public double DistanceTraveled = 100;
    }
}
