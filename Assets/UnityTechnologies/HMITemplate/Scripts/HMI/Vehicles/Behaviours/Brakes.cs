using HMI.Vehicles.Behaviours.Base;
using HMI.Vehicles.Data;
using UnityEngine;
using Vehicles.Behaviours.Base;

namespace HMI.Vehicles.Behaviours
{
    /// <summary>
    /// Brakes on a vehicle
    /// </summary>
    [RequireComponent(typeof(VehicleDataProvider))]
    public class Brakes : BrakesBase
    {
        /// <summary>
        /// Data of the brake
        /// </summary>
        private BrakeData BrakeData;

        /// <summary>
        /// True if the vehicle is currently braking (internal state)
        /// </summary>
        private bool BrakingCalled;

        /// <summary>
        /// Internal field for the readonly property
        /// </summary>
        private float CurrentBrakePowerInternal;

        /// <summary>
        /// The current brake power, 0 if not braking (Read Only).
        /// </summary>
        public override float CurrentBrakePower { get { return CurrentBrakePowerInternal; } }

        /// <summary>
        /// Internal field for the readonly property
        /// </summary>
        private bool IsBrakingInternal;

        /// <summary>
        /// Are the brakes currently braking (Read Only).
        /// </summary>
        public override bool IsBraking { get { return IsBrakingInternal; } }

        /// <summary>
        /// Unity Awake callback
        /// </summary>
        private void Awake()
        {
            var dataProvider = GetComponent<VehicleDataProvider>();
            BrakeData = dataProvider.VehicleData.Brake;
        }

        /// <summary>
        /// Activate the brakes
        /// </summary>
        /// <param name="vehicle">the vehicle the brakes are on.</param>
        /// <param name="strength">how powerful the driver is braking.</param>
        public override void Brake(VehicleBase vehicle, float strength)
        {
            vehicle.CurrentSpeed = BrakeData.Brake(
                multiplier: strength, speed: vehicle.CurrentSpeed, dt: Time.deltaTime);

            CurrentBrakePowerInternal = BrakeData.GetBrakePower(strength);
            IsBrakingInternal = true;
            BrakingCalled = true;
        }

        /// <summary>
        /// Unity Late Update
        /// </summary>
        private void LateUpdate()
        {
            if (!BrakingCalled)
            {
                IsBrakingInternal = false;
                CurrentBrakePowerInternal = 0f;
            }

            BrakingCalled = false;
        }
    }
}
