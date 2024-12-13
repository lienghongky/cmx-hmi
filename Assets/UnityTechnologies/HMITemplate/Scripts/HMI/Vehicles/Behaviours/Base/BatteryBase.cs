using HMI.ChargingStations.Behaviours;
using UnityEngine;

namespace HMI.Vehicles.Behaviours.Base
{
    /// <summary>
    /// Battery of an electric vehicle
    /// </summary>
    public abstract class BatteryBase : MonoBehaviour
    {
        /// <summary>
        /// The charging status of the vehicle
        /// </summary>
        public abstract BatteryChargingStatus ChargingStatus { get; }

        /// <summary>
        /// The battery is empty
        /// </summary>
        public abstract bool IsEmpty { get; }

        /// <summary>
        /// The battery is full
        /// </summary>
        public abstract bool IsFull { get; }

        /// <summary>
        /// Returns the current battery level within [0.0..1.0] (range is inclusive) (Read Only).
        /// </summary>
        public abstract float CurrentRelativeBatteryLevel { get; }

        /// <summary>
        /// Returns the current battery level in Kwh (Read Only).
        /// </summary>
        public abstract float CurrentBatteryLevel { get; }

        /// <summary>
        /// Returns the maximum range if the battery would be fully charged (Read Only).
        /// </summary>
        public abstract float MaxRange { get; }

        /// <summary>
        /// Returns the range of the battery based on its current charge (Read Only).
        /// </summary>
        public abstract float RemainingRange { get; }

        /// <summary>
        /// Retrieve the current battery usage in kWh/100km (negative means consumption, positive production).
        /// </summary>
        public abstract float CurrentEnergyUsage { get; }

        /// <summary>
        /// Retrieve the current battery energy producers in kWh/100km.
        /// </summary>
        public abstract float CurrentEnergyProduction { get; }

        /// <summary>
        /// Retrieve the current battery energy consumers in kWh/100km.
        /// </summary>
        public abstract float CurrentEnergyConsumption { get; }

        /// <summary>
        /// Plug the battery into a charging station
        /// </summary>
        public abstract void PlugIn(ChargingStation station);

        /// <summary>
        /// Unplug the battery from a charging station
        /// </summary>
        public abstract void Unplug(ChargingStation station);
    }
}
