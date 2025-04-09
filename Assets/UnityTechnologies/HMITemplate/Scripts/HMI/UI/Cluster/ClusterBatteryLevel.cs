using HMI.Vehicles.Behaviours.Base;
using HMI.Vehicles.Services;
using UnityEngine;

namespace HMI.UI.Cluster
{
    /// <summary>
    /// Progress bar that shows the current charge level of a battery
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public class ClusterBatteryLevel : MonoBehaviour
    {
        /// <summary>
        /// Battery that this progress bar is attached to
        /// </summary>
        private BatteryBase BatteryDataSource;

        /// <summary>
        /// Renderer of the progress bar
        /// </summary>
        private SpriteRenderer SpriteRenderer;

        /// <summary>
        /// Unity Awake callback
        /// </summary>
        private void Awake()
        {
            BatteryDataSource = VehicleService.GetBattery();
            SpriteRenderer = GetComponent<SpriteRenderer>();
        }

        /// <summary>
        /// Unity Update callback
        /// </summary>
        private void LateUpdate()
        {
            var batteryLevel = Mathf.Clamp01(BatteryDataSource.CurrentRelativeBatteryLevel);
            SpriteRenderer.material.SetFloat("Fill", batteryLevel);
        }
    }
}
