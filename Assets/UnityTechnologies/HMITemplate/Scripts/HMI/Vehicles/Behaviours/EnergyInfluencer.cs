using HMI.Vehicles.Behaviours.Base;
using UnityEngine;

namespace HMI.Vehicles.Behaviours
{
    /// <summary>
    /// An energy influencer is any source that either consumes or produces energy for a battery.
    /// </summary>
    public abstract class EnergyInfluencer : MonoBehaviour
    {
        /// <summary>
        /// Get the current energy influence in Kwh of the energy influencer
        /// </summary>
        /// <returns>A negative number means the influencer is consuming energy, positive is providing energy</returns>
        public abstract float GetCurrentEnergyInfluence(VehicleBase vehicle, BatteryBase battery);
    }
}
