using HMI.Vehicles.Behaviours.Base;
using HMI.Vehicles.Data;
using UnityEngine;
using Vehicles.Behaviours.Base;

namespace HMI.Vehicles.Behaviours
{
    /// <summary>
    /// A vehicle powered by electric power
    /// </summary>
    [RequireComponent(typeof(EngineBase))]
    [RequireComponent(typeof(BrakesBase))]
    [RequireComponent(typeof(TransmissionBase))]
    [RequireComponent(typeof(VehicleDataProvider))]
    public class ElectricVehicle : VehicleBase
    {
        /// <summary>
        /// Engine of the vehicle
        /// </summary>
        private EngineBase Engine;

        /// <summary>
        /// Brakes of the vehicle
        /// </summary>
        private BrakesBase Brakes;

        /// <summary>
        /// Transmission system in the vehicle
        /// </summary>
        private TransmissionBase Transmission;

        /// <summary>
        /// Drag Data
        /// </summary>
        private DragData DragData;

        /// <summary>
        /// Current speed of the vehicle
        /// </summary>
        public override float CurrentSpeed
        {
            get; set;
        }

        /// <summary>
        /// Accelerate the vehicle
        /// </summary>
        /// <param name="strength">Relative strength of acceleration [0.0..1.0].</param>
        public override void Accelerate(float strength)
        {
            Engine.Accelerate(this, strength: strength, Transmission.CurrentGear);
        }

        /// <summary>
        /// Slow down the vehicle by braking
        /// </summary>
        /// <param name="strength">Relative strength of braking [0.0..1.0].</param>
        public override void Brake(float strength)
        {
            Brakes.Brake(this, strength: strength);
        }

        /// <summary>
        /// Switch to next gear
        /// </summary>
        public override void NextGear()
        {
            Transmission.SwitchToNextGear();
        }

        /// <summary>
        /// Switch to previous gear
        /// </summary>
        public override void PreviousGear()
        {
            Transmission.SwitchToPreviousGear();
        }

        /// <summary>
        /// Unity Awake callback
        /// </summary>
        private void Awake()
        {
            Engine = GetComponent<EngineBase>();
            Brakes = GetComponent<BrakesBase>();
            Transmission = GetComponent<TransmissionBase>();
            DragData = GetComponent<VehicleDataProvider>().VehicleData.Drag;
        }

        /// <summary>
        /// Unity Update callback
        /// </summary>
        private void Update()
        {
            CurrentSpeed = DragData.CalculateDrag(CurrentSpeed, Time.deltaTime);
        }
    }
}