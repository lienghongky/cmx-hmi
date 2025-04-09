using HMI.Vehicles.Data;
using HMI.Vehicles.Services;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace HMI.Services
{

    /// <summary>
    /// Update the fan cycle in the HVAC UI
    /// </summary>
    public class FanCycle : MonoBehaviour
    {
        [Serializable]
        public class FanSpeedClass
        {
            public GameObject FanSpeedObj;
        }

        /// <summary>
        /// What climate zone is targetted
        /// </summary>
        public enum TargetFanSide
        {
            LeftSideClimateZone,
            RightSideClimateZone
        };

        /// <summary>
        /// Blower name for the driver seat
        /// </summary>
        private const string DriverSeatBlower = "DriverSeatBlower";

        /// <summary>
        /// Blower name for the passenger seat
        /// </summary>
        private const string PassengerSeatBlower = "PassengerSeatBlower";

        /// <summary>
        /// HVAC data source
        /// </summary>
        private HVACData HvacDataSource;

        /// <summary>
        /// What climate zone is used for this fan
        /// </summary>
        public TargetFanSide TargetClimateZone;

        /// <summary>
        /// List of fan speed objects (dots that show the current level)
        /// </summary>
        public List<FanSpeedClass> FanSpeedObjects = new List<FanSpeedClass>();

        /// <summary>
        /// Last fan speed is used to identify when to update the ui
        /// </summary>
        private int LastFanSpeed = -1;

        /// <summary>
        /// Increase fan speed
        /// </summary>
        public void FanSpeedIncrease()
        {
            BlowerData blower = null;

            switch (TargetClimateZone)
            {
                case TargetFanSide.LeftSideClimateZone:
                    blower = HvacDataSource.GetBlower(DriverSeatBlower);
                    break;
                case TargetFanSide.RightSideClimateZone:
                    blower = HvacDataSource.GetBlower(PassengerSeatBlower);
                    break;
                default:
                    Debug.LogWarning("Target Climate Zone was not found.");
                    break;
            }

            blower.Increase();
            UpdateFanSpeedLights();
        }

        /// <summary>
        /// Unity Awake callback
        /// </summary>
        private void Awake()
        {
            if (HvacDataSource == null)
            {
                HvacDataSource = HVACService.GetHVAC();
            }
        }

        /// <summary>
        /// Unity update callback
        /// </summary>
        private void Update()
        {
            UpdateFanSpeedLights();
        }

        /// <summary>
        /// Update lights on the fan ui
        /// </summary>
        private void UpdateFanSpeedLights()
        {
            var currentFanSpeed = GetCurrentFanSpeed();

            if (currentFanSpeed == LastFanSpeed)
            {
                return;
            }

            LastFanSpeed = currentFanSpeed;

            for (var i = 0; i < FanSpeedObjects.Count; i++)
            {
                if (currentFanSpeed == 0)
                {
                    if (FanSpeedObjects[i].FanSpeedObj.activeSelf)
                    {
                        FanSpeedObjects[i].FanSpeedObj.SetActive(false);
                    }
                }
                else
                {
                    if (i < currentFanSpeed)
                    {
                        if (!FanSpeedObjects[i].FanSpeedObj.activeSelf)
                        {
                            FanSpeedObjects[i].FanSpeedObj.SetActive(true);
                        }
                    }
                    else
                    {
                        if (FanSpeedObjects[i].FanSpeedObj.activeSelf)
                        {
                            FanSpeedObjects[i].FanSpeedObj.SetActive(false);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Based on Chosen enum, returns the current climage zone int from the HVAC script.
        /// </summary>
        private int GetCurrentFanSpeed()
        {
            switch (TargetClimateZone)
            {
                case TargetFanSide.LeftSideClimateZone:
                    return HvacDataSource.GetBlower(DriverSeatBlower).CurrentLevel;
                case TargetFanSide.RightSideClimateZone:
                    return HvacDataSource.GetBlower(PassengerSeatBlower).CurrentLevel;
                default:
                    Debug.LogWarning("Target Climate Zone was not found.");
                    return 0;
            }
        }
    }

}