using HMI.Vehicles.Data;
using Vehicles.Behaviours.Base;

namespace HMI.Vehicles.Behaviours
{
    /// <summary>
    /// All vehicles are stored in the project data folder. This scripts makes sure 
    /// that all scripts in the scene use the same dataset and that the dataset
    /// can be reassigned in a central place (this script) without having to go
    /// through each individual script
    /// </summary>
    public class ElectricVehicleDataProvider : VehicleDataProvider
    {
        /// <summary>
        /// Used electric vehicle data
        /// </summary>
        public ElectricVehicleData ElectricVehicleData;

        public override VehicleData VehicleData { get { return ElectricVehicleData; } }
    }
}
