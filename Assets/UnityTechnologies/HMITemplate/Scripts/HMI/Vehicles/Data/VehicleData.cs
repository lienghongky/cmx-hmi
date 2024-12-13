using UnityEngine;

namespace HMI.Vehicles.Data
{
    /// <summary>
    /// Vehicle information set
    /// </summary>
    public class VehicleData : ScriptableObject
    {
        public DragData Drag;
        public EngineData Engine;
        public BrakeData Brake;
        public TransmissionData Transmission;
        public OdometerData Odometer;
    }
}
