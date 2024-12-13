using HMI.Clusters.Enums;
using HMI.Units.Data;
using HMI.Vehicles.Data;
using HMI.Vehicles.Services;
using UnityEngine;
using Util;

namespace HMI.Vehicles.Behaviours.Adas
{
    /// <summary>
    /// Adas speed control automatically adjust vehicle speed to a set goal speed.
    /// </summary>
    public class AdasVehicleSpeedController : MonoBehaviour
    {
        /// <summary>
        /// Data controlled by this speed controller
        /// </summary>
        public AdasSpeedControlData Data;

        /// <summary>
        /// Used to determine if the system increased by mph or km/h
        /// </summary>
        public UnitConfiguration UnitConfiguration;

        /// <summary>
        /// Helpt to smoothly accelerate when adas is turned on
        /// </summary>
        private float SmoothAcceleration = 0f;

        /// <summary>
        /// Unity Awake callback
        /// </summary>
        private void Awake()
        {
            // make sure the system needs to be user-activated
            Data.IsSpeedControlActive = false;    
        }

        /// <summary>
        /// Unity Update callback
        /// </summary>
        private void Update()
        {
            if (Data.IsSpeedControlActive)
            {
                Data.GoalSpeed = Mathf.Clamp(Data.GoalSpeed, Data.MinSpeed, Data.MaxSpeed);
                var vehicle = VehicleService.GetVehicle();

                if (vehicle == null)
                {
                    return;
                }

                // just hard limit the top speed, prevents jitter
                if (Mathf.Abs(vehicle.CurrentSpeed - Data.GoalSpeed) < 0.5f || 
                    (vehicle.CurrentSpeed - Data.GoalSpeed > 0.5f))
                {
                    vehicle.CurrentSpeed = Data.GoalSpeed;
                }
                else
                {
                    vehicle.Accelerate(SmoothAcceleration);
                    SmoothAcceleration += Time.deltaTime / 10f;
                    SmoothAcceleration = Mathf.Clamp01(SmoothAcceleration);
                }
            }
        }

        /// <summary>
        /// Set" will set ADAS to the current speed of car and enable it.
        /// </summary>
        public void SetAutomaticSpeedControl()
        {
            SmoothAcceleration = 0f;

            var vehicle = VehicleService.GetVehicle();

            if (vehicle == null)
            {
                return;
            }

            var transmission = VehicleService.GetTransmission();
            
            if(transmission.CurrentGear.Gear != "D")
            {
                Debug.Log("Car is currently not in Drive, so ADAS can't activate"); 
                return;
            }

            Data.IsSpeedControlActive = true;
            Data.GoalSpeed = vehicle.CurrentSpeed;
        }

        /// <summary>
        /// Pressing cancel while canceled will re-enable ADAS but resume 
        /// using the speed that was previously set.
        /// </summary>
        public void CancelAutomaticSpeedControl()
        {
            SmoothAcceleration = 0f;

            var transmission = VehicleService.GetTransmission();

            if (transmission.CurrentGear.Gear != "D")
            {
                Debug.Log("Car is currently not in Drive, so ADAS can't activate");
                return;
            }

            Data.IsSpeedControlActive = !Data.IsSpeedControlActive;
        }

        /// <summary>
        /// Increase goal speed
        /// </summary>
        public void IncreaseGoalSpeed()
        {
            Data.GoalSpeed += Data.Increment * GetOneUnit();
        }

        /// <summary>
        /// Decrease goal speed
        /// </summary>
        public void DecreaseGoalSpeed()
        {
            Data.GoalSpeed -= Data.Increment * GetOneUnit();
        }

        /// <summary>
        /// Determine how much a single unit increase means, since the base data is in celsius
        /// </summary>
        private float GetOneUnit()
        {
            if (UnitConfiguration.UnitOfLength == UnitOfLengthType.Kilometers)
            {
                return 1f;
            }
            else
            {
                return UnitConversion.KilometersToMiles(1f);
            }
        }
    }
}
