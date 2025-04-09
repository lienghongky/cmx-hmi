using HMI.Vehicles.Behaviours.Base;
using HMI.Vehicles.Services;
using UnityEngine;

namespace HMI.UI.IVI.Charge
{
    /// <summary>
    /// Gets the battery level and sets the correct animation from on the battery on screen
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class BatteryLevelToAnimation : MonoBehaviour
    {
        /// <summary>
        /// Battery that this progress bar is attached to
        /// </summary>
        private BatteryBase BatteryDataSource;

        /// <summary>
        /// Animation the progress is set on
        /// </summary>
        private Animator Animator;

        /// <summary>
        /// Unity awake callback
        /// </summary>
        private void Awake()
        {
            BatteryDataSource = VehicleService.GetBattery();
            Animator = GetComponent<Animator>();
        }

        /// <summary>
        /// Unity Update callback
        /// </summary>
        private void Update()
        {
            var batteryLevel = Mathf.Clamp01(BatteryDataSource.CurrentRelativeBatteryLevel);
            Animator.speed = 0f;
            Animator.Play("Default", 0, batteryLevel);
        }
    }
}
