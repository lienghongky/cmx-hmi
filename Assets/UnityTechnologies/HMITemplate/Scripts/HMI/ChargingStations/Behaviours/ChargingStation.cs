using HMI.ChargingStations.Data;
using HMI.Vehicles.Behaviours;
using HMI.Vehicles.Behaviours.Base;

namespace HMI.ChargingStations.Behaviours
{
    /// <summary>
    /// A charging station for electric vehicles
    /// </summary>
    public class ChargingStation : EnergyInfluencer
    {
        /// <summary>
        /// Configuration data for a charging station
        /// </summary>
        public ChargingStationData ChargingStationData;

        public override float GetCurrentEnergyInfluence(VehicleBase vehicle, BatteryBase battery)
        {
            return ChargingStationData.PowerGenerated;
        }
    }
}
