using UnityEngine;

namespace HMI.Vehicles.Behaviours.Base
{
    /// <summary>
    /// Abstract base class of all vehicles
    /// </summary>
    public abstract class VehicleBase : MonoBehaviour
    {
        /// <summary>
        /// Accelerate the vehicle
        /// </summary>
        /// <param name="strength">Relative strength of acceleration [0.0..1.0].</param>
        public abstract void Accelerate(float strength);

        /// <summary>
        /// Slow down the vehicle by braking
        /// </summary>
        /// <param name="strength">Relative strength of braking [0.0..1.0].</param>
        public abstract void Brake(float strength);

        /// <summary>
        /// Switch to next gear
        /// </summary>
        public abstract void NextGear();

        /// <summary>
        /// Switch to previous gear
        /// </summary>
        public abstract void PreviousGear();

        /// <summary>
        /// Speed the vehicle is traveling at in Km/h. 
        /// Can be negative if the vehicle is traveling in reverse
        /// </summary>
        public abstract float CurrentSpeed { get; set; }
    }
}
