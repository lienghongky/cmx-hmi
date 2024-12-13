using HMI.Vehicles.Behaviours.Base;
using HMI.Vehicles.Services;
using UnityEngine;

namespace HMI.UI.Cluster
{
    /// <summary>
    /// The battery charge progress bar shows how much energy the battery is charging
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public class ClusterBatteryCharge : MonoBehaviour
    {
        /// <summary>
        /// Maximum of the scale that usage is shown on
        /// </summary>
        public float MaxUsage = 20f;

        /// <summary>
        /// Transition speed is used to smooth the transition between values
        /// </summary>
        public float TransitionSpeed = 20f;

        /// <summary>
        /// Battery connected to this progress bar
        /// </summary>
        private BatteryBase BatteryDataSource;

        /// <summary>
        /// Sprite renderer
        /// </summary>
        private SpriteRenderer SpriteRenderer;

        /// <summary>
        /// Indicates last frame's usage, used for progress bar smoothing
        /// </summary>
        private float LastUsage;

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
            var usage = BatteryDataSource.CurrentEnergyUsage;
            usage = Mathf.SmoothStep(LastUsage, usage, Time.deltaTime * TransitionSpeed);
            LastUsage = usage;

            if (usage >= 0f)
            {
                usage /= MaxUsage;
            }
            else
            {
                usage = 0f;
            }

            SpriteRenderer.material.SetFloat("Fill", usage);
        }
    }
}
