using HMI.Vehicles.Behaviours.Base;
using HMI.Vehicles.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Vehicles.Behaviours.Base;

namespace HMI.Vehicles.Behaviours
{
    /// <summary>
    /// Transmission system of the vehicle
    /// </summary>
    [RequireComponent(typeof(VehicleDataProvider))]
    public class Transmission : TransmissionBase
    {
        /// <summary>
        /// Internal cache of the gear names
        /// </summary>
        private List<string> GearNamesInternal;

        /// <summary>
        /// Gear names (Read Only).
        /// </summary>
        public override IList<string> GearNames
        {
            get { return new List<string>(GearNamesInternal); }
        }

        /// <summary>
        /// Vehicle Data
        /// </summary>
        private TransmissionData TransmissionData;

        /// <summary>
        /// Internal variable for read only Current Gear property
        /// </summary>
        private GearData CurrentGearInternal;

        /// <summary>
        /// Get the current gear (Read Only).
        /// </summary>
        public override GearData CurrentGear { get { return CurrentGearInternal; } }

        /// <summary>
        /// Unity Awake callback
        /// </summary>
        private void Awake()
        {
            TransmissionData = GetComponent<VehicleDataProvider>().VehicleData.Transmission;
            GearNamesInternal = TransmissionData.Gearnames;

            if (GearNamesInternal == null || GearNamesInternal.Count == 0)
            {
                throw new ArgumentException("The transmission data contains no gears");
            }

            CurrentGearInternal = TransmissionData[GearNamesInternal.First()];
        }

        /// <summary>
        /// Switches the transmission to the next gear
        /// </summary>
        public override void SwitchToNextGear()
        {
            var idx = GearNamesInternal.IndexOf(CurrentGear.Gear);

            idx++;
            if (idx >= GearNamesInternal.Count)
            {
                idx = 0;
            }

            CurrentGearInternal = TransmissionData[GearNamesInternal[idx]];
        }

        /// <summary>
        /// Switches the transmission to the previous gear
        /// </summary>
        public override void SwitchToPreviousGear()
        {
            var idx = GearNamesInternal.IndexOf(CurrentGear.Gear);

            idx--;
            if (idx < 0)
            {
                idx = GearNamesInternal.Count - 1;
            }

            CurrentGearInternal = TransmissionData[GearNamesInternal[idx]];
        }

        /// <summary>
        /// Switches the transmission to the desired gear
        /// </summary>
        public override void SwitchToDesiredGear(string name)
        {
            var gear = GearNamesInternal.FirstOrDefault(x => x == name);

            if (gear != null)
            {
                CurrentGearInternal = TransmissionData[gear];
            }
            else
            {
                Debug.LogWarning("Gear not found");
            }
        }

        /// <summary>
        /// Switches the transmission to the desired gear
        /// </summary>
        public override void SwitchToDesiredGear(int idx)
        {
            CurrentGearInternal = TransmissionData[GearNamesInternal[idx]];
        }
    }
}