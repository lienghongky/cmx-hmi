using HMI.Vehicles.Data;
using UnityEngine.Events;

namespace HMI.Vehicles.Behaviours.Base
{
    /// <summary>
    /// An engine of a vehicle
    /// </summary>
    public abstract class EngineBase : EnergyInfluencer
    {
        /// <summary>
        /// Engine State changed
        /// </summary>
        public UnityEvent EngineStateChanged;

        /// <summary>
        /// Maximum speed of the engine
        /// </summary>
        public abstract float MaxSpeed { get; }

        /// <summary>
        /// Is the engine on?
        /// </summary>
        public abstract bool IsEngineOn { get; set; }

        /// <summary>
        /// Accelerate the vehicle
        /// </summary>
        /// <param name="vehicle">Vehicle that this engine is a part of</param>
        /// <param name="strength">How deep the driver is pressing the accelerate [0.0..1.0]</param>
        /// <param name="gear">What gear the transmission of the vehicle is in</param>
        public abstract void Accelerate(VehicleBase vehicle, float strength, GearData gear);
        
        /// <summary>
        /// Turns the engine on
        /// </summary>
        public abstract void TurnEngineOn();

        /// <summary>
        /// Turns the engine off
        /// </summary>
        public abstract void TurnEngineOff();
    }
}
