using HMI.Vehicles.Behaviours.Base;
using HMI.Vehicles.Data;
using Util;
using UnityEngine;
using Vehicles.Behaviours.Base;

namespace HMI.Vehicles.Behaviours
{
    /// <summary>
    /// Odometer represents distance traveled
    /// </summary>
    [RequireComponent(typeof(VehicleBase))]
    [RequireComponent(typeof(VehicleDataProvider))]
    public class Odometer : OdometerBase
    {
        /// <summary>
        /// The dataset representing the odometer
        /// </summary>
        private OdometerData OdometerData;

        /// <summary>
        /// Vehicle that this odometer is a part of
        /// </summary>
        private VehicleBase Vehicle;

        /// <summary>
        /// Internal field for read only property
        /// </summary>
        private double DistanceTraveledInternal;

        /// <summary>
        /// The distance the vehicle has travelled in total in kilometers
        /// </summary>
        public override double DistanceTraveled { get { return DistanceTraveledInternal; } }

        /// <summary>
        /// Internal field for read only property
        /// </summary>
        private double TripDistanceTraveledInternal;

        /// <summary>
        /// The distance the vehicle has travelled this trip in kilometers
        /// </summary>
        public override double TripDistanceTraveled { get { return TripDistanceTraveledInternal; } }

        /// <summary>
        /// Unity Awake callback
        /// </summary>
        private void Awake()
        {
            Vehicle = GetComponent<VehicleBase>();
            OdometerData = GetComponent<VehicleDataProvider>().VehicleData.Odometer;
            DistanceTraveledInternal = OdometerData.DistanceTraveled;
            TripDistanceTraveledInternal = 0f;
        }

        /// <summary>
        /// Unity Update callback
        /// </summary>
        private void Update()
        {
            // make absolute, speed also indicates a neg. in reverse
            var currentSpeed = Mathf.Abs(Vehicle.CurrentSpeed);

            // speed is in kilometers/hour, convert to kilometers traveled between frames
            var kilometersTraveled = UnitConversion.HoursPerSecond * currentSpeed * Time.deltaTime;
            DistanceTraveledInternal += kilometersTraveled;
            TripDistanceTraveledInternal += kilometersTraveled;
        }
    }
}