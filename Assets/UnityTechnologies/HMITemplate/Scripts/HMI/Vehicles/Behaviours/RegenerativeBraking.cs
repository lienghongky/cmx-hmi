using HMI.Vehicles.Behaviours.Base;
using HMI.Vehicles.Data;
using UnityEngine;

namespace HMI.Vehicles.Behaviours
{
    /// <summary>
    /// Regenerative braking transfers power from the brakes back to the battery
    /// </summary>
    [RequireComponent(typeof(BrakesBase))]
    [RequireComponent(typeof(ElectricVehicleDataProvider))]
    public class RegenerativeBraking : EnergyInfluencer
    {
        /// <summary>
        /// Data for this brake
        /// </summary>
        private RegenerativeBrakingData RegenerativeBrakingData;

        /// <summary>
        /// Brakes that are used for energy regeneration
        /// </summary>
        private BrakesBase Brakes;

        /// <summary>
        /// Unity Awake callback
        /// </summary>
        private void Awake()
        {
            RegenerativeBrakingData = GetComponent<ElectricVehicleDataProvider>().ElectricVehicleData.RegenerativeBraking;
            Brakes = GetComponent<BrakesBase>();
        }

        /// <summary>
        /// Returns the energy influence of the regenerative braking system
        /// </summary>
        /// <param name="vehicle">Vehicle that contains the regenerative braking system</param>
        /// <param name="battery">the battery that the regenerative braking system supplies energy to</param>
        /// <returns>the amount of energy generated in Kwh</returns>
        public override float GetCurrentEnergyInfluence(VehicleBase vehicle, BatteryBase battery)
        {
            // vehicle needs to be moving to generate braking energy
            if (vehicle.CurrentSpeed > 0.5f || vehicle.CurrentSpeed < -0.5f)
            {
                return RegenerativeBrakingData.CalculateKwhGenerated(Brakes.CurrentBrakePower);
            }
            else
            {
                return 0f;
            }
        }
    }
}
