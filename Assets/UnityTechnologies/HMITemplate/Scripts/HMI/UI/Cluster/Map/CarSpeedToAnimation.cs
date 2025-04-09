using HMI.Vehicles.Behaviours.Base;
using HMI.Vehicles.Services;
using UnityEngine;

namespace HMI.UI.Cluster.Map
{
    /// <summary>
    /// Applies the car simulation data to the car moving along the map
    /// The car moves along the map based on an animation that is on the route
    /// </summary>
    public class CarSpeedToAnimation : MonoBehaviour
    {
        /// <summary>
        /// The vehicle that is used to move the car along the path on the map
        /// </summary>
        private VehicleBase VehicleDataSource;

        /// <summary>
        /// The animator for the vehicle on the map
        /// </summary>
        public Animator VehicleAnimator;

        /// <summary>
        /// How fast the car is moving along the path
        /// </summary>
        public float Multiplier = 0.01f;

        /// <summary>
        /// Offset used to determine how far on the path the car starts the animation
        /// </summary>
        public float StartProgress = 0f;

        /// <summary>
        /// Unity Awake callback
        /// </summary>
        private void Awake()
        {
            VehicleDataSource = VehicleService.GetVehicle();
        }

        /// <summary>
        /// Unity update delegate
        /// </summary>
        private void Update()
        {
            VehicleAnimator.SetFloat("CarSpeed", VehicleDataSource.CurrentSpeed * Multiplier);

            var animState = VehicleAnimator.GetCurrentAnimatorStateInfo(0);
            var currentTime = animState.normalizedTime % 1;

            // the car is before the start position, move car to start position
            if (currentTime < StartProgress)
            {
                VehicleAnimator.Play("Take 001", 0, StartProgress);
            }
        }
    }
}
