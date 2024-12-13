using HMI.Vehicles.Services;
using UnityEngine;
using System.Collections.Generic;
using HMI.Vehicles.Data;

namespace HMI.Vehicles.Behaviours
{
    /// <summary>
    /// Controls the vehicle
    /// </summary>
    public class VehicleController : MonoBehaviour
    {
        public IList<string> GearNames{

            get { return VehicleService.GetTransmission().GearNames; }

        }

        public List<string> GeerableNames{

            get { 
                //  filter out the current gear
                List<string> GearNames = new List<string>(VehicleService.GetTransmission().GearNames);
                GearNames.Remove(CurrentGear.Gear);
                return GearNames;
                }

        }

        public GearData CurrentGear{

            get { return VehicleService.GetTransmission().CurrentGear; }

        }
    
 
        /// <summary>
        /// Switch vehicle to next gear
        /// </summary>
        public void NextGear()
        {
            VehicleService.GetTransmission().SwitchToNextGear();
        }

        /// <summary>
        /// Switch vehicle to previous gear
        /// </summary>
        public void PreviousGear()
        {
            VehicleService.GetTransmission().SwitchToPreviousGear();
        }

        /// <summary>
        ///  SwitchToDesiredGear
        ///  </summary>
        public void SetGear(string gear)
        {
            //  check if the gear is valid
            if (!GearNames.Contains(gear))
            {
                Debug.LogError("Invalid gear: " + gear);
                return;
            }
            VehicleService.GetTransmission().SwitchToDesiredGear(gear);
        }

        /// <summary>
        /// Accelerate vehicle
        /// </summary>
        public void Accelerate()
        {
            VehicleService.GetVehicle().Accelerate(1f);
        }

        /// <summary>
        /// Brake vehicle
        /// </summary>
        public void Brake()
        {
            VehicleService.GetVehicle().Brake(1f);
        }

        /// <summary>
        /// Start/Stop Engine
        /// </summary>
        public static void StartStopEngine()
        {
           var engine = VehicleService.GetEngine();

            if(engine.IsEngineOn)
            {
                engine.TurnEngineOff();
            }
            else
            {
                engine.TurnEngineOn();
            }
        }

        /// <summary>
        /// Increase ADAS goal speed
        /// </summary>
        public static void AdasIncreaseGoalSpeed()
        {
            VehicleService.GetSpeedController().IncreaseGoalSpeed();
        }

        /// <summary>
        /// Decrease ADAS goal speed
        /// </summary>
        public static void AdasDecreaseGoalSpeed()
        {
            VehicleService.GetSpeedController().DecreaseGoalSpeed();
        }

        /// <summary>
        /// Set ADAS 
        /// </summary>
        public static void AdasSet()
        {
            VehicleService.GetSpeedController().SetAutomaticSpeedControl();
        }

        /// <summary>
        /// Cancel ADAS command
        /// </summary>
        public static void AdasCancel()
        {
            VehicleService.GetSpeedController().CancelAutomaticSpeedControl();
        }


        // <summary>
        /// Set togle for ADAS
        /// </summary>
        public static void AdasToggle()
        {
            if (VehicleService.GetSpeedController().Data.IsSpeedControlActive)
            {
                VehicleService.GetSpeedController().CancelAutomaticSpeedControl();
            }
            else
            {
                VehicleService.GetSpeedController().SetAutomaticSpeedControl();
            }
        }
    }
}
