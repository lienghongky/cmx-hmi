using HMI.Vehicles.Behaviours.Adas;
using HMI.Vehicles.Behaviours.Base;

using UnityEngine;

namespace HMI.Vehicles.Services
{
    /// <summary>
    /// Singleton implementation of the Vehicle
    /// </summary>
    public class VehicleService : MonoBehaviour
    {
        /// <summary>
        /// Vehicle prefab that will be instantiated
        /// </summary>
        public GameObject VehiclePrefab;

        /// <summary>
        /// Single instance of the vehicle
        /// </summary>
        private static GameObject Instance;

        /// <summary>
        /// Unit Awake callback
        /// </summary>
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = Instantiate(VehiclePrefab);
                Instance.transform.parent = transform;
            }
        }

        /// <summary>
        /// Get the transmission on the vehicle
        /// </summary>
        public static TransmissionBase GetTransmission()
        {
            if (Instance == null)
            {
                return null;
            }

            return Instance.GetComponentInChildren<TransmissionBase>();
        }

        /// <summary>
        /// Get the energy consumption logger of the vehicle
        /// </summary>
        public static EnergyConsumptionLoggerBase GetEnergyConsumptionLogger()
        {
            if (Instance == null)
            {
                return null;
            }

            return Instance.GetComponentInChildren<EnergyConsumptionLoggerBase>();
        }

        /// <summary>
        /// Get the vehicle
        /// </summary>
        public static VehicleBase GetVehicle()
        {
            if (Instance == null)
            {
                return null;
            }

            return Instance.GetComponentInChildren<VehicleBase>();
        }

        /// <summary>
        /// Get the vehicle engine
        /// </summary>
        public static EngineBase GetEngine()
        {
            if(Instance == null)
            {
                return null;
            }

            return Instance.GetComponentInChildren<EngineBase>();
        }

        /// <summary>
        /// Get the brakes on the vehicle
        /// </summary>
        public static BrakesBase GetBrakes()
        {
            if (Instance == null)
            {
                return null;
            }

            return Instance.GetComponentInChildren<BrakesBase>();
        }

        /// <summary>
        /// Get the battery on the vehicle
        /// </summary>
        public static BatteryBase GetBattery()
        {
            if (Instance == null)
            {
                return null;
            }

            return Instance.GetComponentInChildren<BatteryBase>();
        }

        /// <summary>
        /// Get the automatic speed controller
        /// </summary>
        public static AdasVehicleSpeedController GetSpeedController()
        {
            if (Instance == null)
            {
                return null;
            }

            return Instance.GetComponentInChildren<AdasVehicleSpeedController>();
        }
    }
}
