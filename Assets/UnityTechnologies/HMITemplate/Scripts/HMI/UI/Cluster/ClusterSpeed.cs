using HMI.Units.Data;
using HMI.Vehicles.Behaviours.Base;
using HMI.Vehicles.Services;
using Util;
using UnityEngine;

namespace HMI.UI.Cluster
{
    /// <summary>
    /// Visualized the speed the vehicle is traveling on
    /// </summary>
    public class ClusterSpeed : MonoBehaviour
    {
        /// <summary>
        /// Configuration of the cluster speed visualization
        /// </summary>
        public UnitConfiguration UnitConfiguration;

        /// <summary>
        /// Vehicle to represent the speed for
        /// </summary>
        private VehicleBase VehicleDataSource;

        /// <summary>
        /// Textual representation of speed of vehicle
        /// </summary>
        public TMPro.TextMeshPro Text;

        /// <summary>
        /// Unity Awake callback
        /// </summary>
        private void Awake()
        {
            VehicleDataSource = VehicleService.GetVehicle();
        }

        /// <summary>
        /// Unity Update Event
        /// </summary>
        private void Update()
        {
            var speed = VehicleDataSource.CurrentSpeed;

            if (UnitConfiguration.UnitOfLength == Clusters.Enums.UnitOfLengthType.Kilometers)
            {
                RenderSpeed(speed);
            }
            else
            {
                RenderSpeed(UnitConversion.KilometersToMiles(speed));
            }
        }

        /// <summary>
        /// Render vehicle speed
        /// </summary>
        private void RenderSpeed(float speed)
        {
            speed = Mathf.Abs(speed);
            Text.text = speed.ToString("F0");
        }
    }
}
