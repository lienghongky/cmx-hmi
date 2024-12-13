using HMI.Vehicles.Behaviours.Base;
using HMI.Vehicles.Data;
using UnityEngine;

namespace HMI.Vehicles.Behaviours
{
    /// <summary>
    /// An engine of a vehicle
    /// </summary>
    [RequireComponent(typeof(BatteryBase))]
    [RequireComponent(typeof(ElectricVehicleDataProvider))]
    public class ElectricEngine : EngineBase
    {
        /// <summary>
        /// Battery that this electric engine draws power from
        /// </summary>
        private BatteryBase Battery;

        /// <summary>
        /// The data representation of the engine
        /// </summary>
        private EngineData EngineData;

        /// <summary>
        /// The data representation of the energy consumption profile of the engine
        /// </summary>
        private EnergyConsumptionData EnergyConsumptionData;

        /// <summary>
        /// Maximum speed of the engine
        /// </summary>
        public override float MaxSpeed { get { return EngineData.MaxSpeed; } }

        /// <summary>
        /// Returs if the engine is on
        /// </summary>
        public override bool IsEngineOn
        {
            get 
            { 
                return EngineData.IsOn; 
            }
            set 
            { 
                if (EngineData.IsOn != value)
                {
                    EngineData.IsOn = value;
                    EngineStateChanged.Invoke();
                }
            }
        }

        /// <summary>
        /// Unity Awake callback
        /// </summary>
        private void Awake()
        {
            Battery = GetComponent<BatteryBase>();
            var dataProvider = GetComponent<ElectricVehicleDataProvider>();
            EngineData = dataProvider.ElectricVehicleData.Engine;
            EnergyConsumptionData = dataProvider.ElectricVehicleData.EnergyConsumption;
            IsEngineOn = true;
        }

        /// <summary>
        /// Accelerate the vehicle
        /// </summary>
        /// <param name="vehicle">Vehicle that this engine is a part of</param>
        /// <param name="strength">How deep the driver is pressing the accelerate [0.0..1.0]</param>
        /// <param name="gear">What gear the transmission of the vehicle is in</param>
        public override void Accelerate(VehicleBase vehicle, float strength, GearData gear)
        {
            // only allow acceleration if there is still juice in the battery
            if (!Battery.IsEmpty)
            {
                vehicle.CurrentSpeed = gear.Accelerate(
                multiplier: strength, currentSpeed: vehicle.CurrentSpeed, maxSpeed: MaxSpeed, dt: Time.deltaTime);
            }
        }

        /// <summary>
        /// Returns the energy influence of the engine
        /// </summary>
        /// <param name="vehicle">Vehicle that contains the engine</param>
        /// <param name="battery">the battery that the engine takes energy from</param>
        /// <returns>the amount of energy consumed(negative influence) in Kwh</returns>
        public override float GetCurrentEnergyInfluence(VehicleBase vehicle, BatteryBase battery)
        {
            if (!battery.IsEmpty)
            {
                return -EnergyConsumptionData.CalculateEnergyConsumption(vehicle.CurrentSpeed);
            }
            else
            {
                return 0f;
            }
        }

        /// <summary>
        /// Turn the engine on
        /// </summary>
        public override void TurnEngineOn()
        {
            IsEngineOn = true;
         }

        /// <summary>
        /// Turn the engine off
        /// </summary>
        public override void TurnEngineOff()
        {
            IsEngineOn = false;            
        }
    }
}