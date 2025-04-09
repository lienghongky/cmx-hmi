using UnityEngine;

namespace HMI.Vehicles.Behaviours.Base
{
    /// <summary>
    /// Odometer represents distance traveled
    /// </summary>
    public abstract class OdometerBase : MonoBehaviour
    {
        /// <summary>
        /// The distance the vehicle has travelled in total in kilometers
        /// </summary>
        public abstract double DistanceTraveled { get; }

        /// <summary>
        /// The distance the vehicle has travelled this trip in kilometers
        /// </summary>
        public abstract double TripDistanceTraveled { get; }
    }
}
