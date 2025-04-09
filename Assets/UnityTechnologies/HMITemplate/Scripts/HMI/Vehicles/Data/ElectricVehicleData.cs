using UnityEngine;

namespace HMI.Vehicles.Data
{
    /// <summary>
    /// Vehicle information set
    /// </summary>
    [CreateAssetMenu(fileName = "ElectricVehicleData.asset", menuName = "HMI/Vehicle/Electric Vehicle", order = 1)]
    public class ElectricVehicleData : VehicleData
    {
        public BatteryData Battery;
        public EnergyConsumptionData EnergyConsumption;
        public RegenerativeBrakingData RegenerativeBraking;
        public EnergyConsumptionLoggerData EnergyConsumptionLogger;
        public AdasSpeedControlData SpeedControlData;
    }
}
