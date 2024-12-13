using UnityEngine;

namespace HMI.Vehicles.Behaviours.Base
{
    /// <summary>
    /// Brakes on a vehicle
    /// </summary>
    public abstract class BrakesBase : MonoBehaviour
    {
        /// <summary>
        /// The current brake power, 0 if not braking (Read Only).
        /// </summary>
        public abstract float CurrentBrakePower { get; }

        /// <summary>
        /// Are the brakes currently braking (Read Only).
        /// </summary>
        public abstract bool IsBraking { get; }

        /// <summary>
        /// Activate the brakes
        /// </summary>
        /// <param name="vehicle">the vehicle the brakes are on.</param>
        /// <param name="strength">how powerful the driver is braking.</param>
        public abstract void Brake(VehicleBase vehicle, float strength);
    }
}
