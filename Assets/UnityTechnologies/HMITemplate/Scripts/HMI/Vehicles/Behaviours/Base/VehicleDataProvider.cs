using HMI.Vehicles.Data;
using UnityEngine;

namespace Vehicles.Behaviours.Base
{
    /// <summary>
    /// All vehicles are stored in the project data folder. This scripts makes sure 
    /// that all scripts in the scene use the same dataset and that the dataset
    /// can be reassigned in a central place (this script) without having to go
    /// through each individual script
    /// </summary>
    public abstract class VehicleDataProvider : MonoBehaviour
    {
        /// <summary>
        /// Vehicle data exposed by the provider
        /// </summary>
        public abstract VehicleData VehicleData { get; }
    }
}
