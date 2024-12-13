using HMI.Vehicles.Data;
using System.Collections.Generic;
using UnityEngine;

namespace HMI.Vehicles.Behaviours.Base
{
    /// <summary>
    /// Transmission system of the vehicle
    /// </summary>
    public abstract class TransmissionBase : MonoBehaviour
    {
        /// <summary>
        /// Gear names
        /// </summary>
        public abstract IList<string> GearNames { get; }

        /// <summary>
        /// Get the current gear (Read Only).
        /// </summary>
        public abstract GearData CurrentGear { get; }

        /// <summary>
        /// Switches the transmission to the next gear
        /// </summary>
        public abstract void SwitchToNextGear();

        /// <summary>
        /// Switches the transmission to the previous gear
        /// </summary>
        public abstract void SwitchToPreviousGear();

        /// <summary>
        /// Switches the transmission to the desired gear
        /// </summary>
        public abstract void SwitchToDesiredGear(string name);

        /// <summary>
        /// Switches the transmission to the desired gear
        /// </summary>
        public abstract void SwitchToDesiredGear(int idx);
    }
}
