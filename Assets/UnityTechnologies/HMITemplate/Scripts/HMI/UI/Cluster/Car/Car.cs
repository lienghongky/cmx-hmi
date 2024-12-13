using HMI.Vehicles.Behaviours;
using HMI.Vehicles.Behaviours.Base;
using HMI.Vehicles.Services;
using UnityEngine;

namespace HMI.UI.Cluster.Car
{
    /// <summary>
    /// Car visualization
    /// </summary>
    public class Car : MonoBehaviour
    {
        /// <summary>
        /// Data source for the car systems
        /// </summary>
        public CarSystems LightsDataSource;

        /// <summary>
        /// Data source for the vehicle
        /// </summary>
        private VehicleBase VehicleDataSource;

        /// <summary>
        /// Data source for vehicle brake information
        /// </summary>
        private BrakesBase BrakesDataSource;

        /// <summary>
        /// Position of the car when not accelerating or braking
        /// </summary>
        public Vector3 BasePosition;

        /// <summary>
        /// Position of the car when braking
        /// </summary>
        public Vector3 BrakePosition;

        /// <summary>
        /// Position of the car to speed up
        /// </summary>
        public Vector3 SpeedPosition;

        /// <summary>
        /// Object that contains the brake lights
        /// </summary>
        public GameObject BrakeLights;

        /// <summary>
        /// Object that contains the front lights
        /// </summary>
        public GameObject FrontLights;

        /// <summary>
        /// How fast the vehicle moves to the brake position when braking
        /// </summary>
        public float BrakeInterpolationSpeed = 5f;

        /// <summary>
        /// How fast the vehicle moves to the speed up position when accelerating
        /// </summary>
        public float SpeedInterpolationSpeed = 10f;

        /// <summary>
        /// How fast the car falls back to its neutral position when neither braking or accelerating
        /// </summary>
        public float ResetPositionSpeed = 0.2f;

        /// <summary>
        /// Current interpolation value of the brakes (how far the vehicle has moved toward the brake position)
        /// </summary>
        private float BrakeInterpolation = 0f;

        /// <summary>
        /// Current interpolation value of acceleration (how far the vehicle has moved toward the speed up position)
        /// </summary>
        private float SpeedInterpolation = 0f;

        /// <summary>
        /// used to check if the vehicle has changed speed since the last frame
        /// </summary>
        private float LastCarSpeed = 0f;

        /// <summary>
        /// Original position of the vehicle to brake/accelerate around
        /// </summary>
        private Vector3 OriginalPosition;

        /// <summary>
        /// Unity Awake callback
        /// </summary>
        private void Awake()
        {
            VehicleDataSource = VehicleService.GetVehicle();
            BrakesDataSource = VehicleService.GetBrakes();
            OriginalPosition = transform.localPosition;
        }

        /// <summary>
        /// Unity draw gizmos callback
        /// </summary>
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawSphere(transform.position + BasePosition, 1f);
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position + BrakePosition, 1f);
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(transform.position + SpeedPosition, 1f);
        }

        /// <summary>
        /// Unity update callback
        /// </summary>
        private void Update()
        {
            UpdateLights();
            UpdateBrakeInterpolation();
            UpdateSpeedInterpolation();
            UpdateCarPosition();
            LastCarSpeed = VehicleDataSource.CurrentSpeed;
        }

        /// <summary>
        /// Update the front and tail lights
        /// </summary>
        private void UpdateLights()
        {
            FrontLights.SetActive(LightsDataSource.Data.AreLowBeamHeadlightsOn || LightsDataSource.Data.AreHighBeamHeadlightsOn);
            BrakeLights.SetActive(BrakesDataSource.IsBraking);
        }

        /// <summary>
        /// Calculate how far towards the brake position the vehicle is located
        /// </summary>
        private void UpdateBrakeInterpolation()
        {
            if (BrakesDataSource.IsBraking || VehicleDataSource.CurrentSpeed < 0f)
            {
                BrakeInterpolation += Time.deltaTime * BrakeInterpolationSpeed;
            }
            else
            {
                BrakeInterpolation -= Time.deltaTime * BrakeInterpolationSpeed;
            }

            BrakeInterpolation = Mathf.Clamp01(BrakeInterpolation);
        }

        /// <summary>
        /// Calculate how far towards the speed position the vehicle is located
        /// </summary>
        private void UpdateSpeedInterpolation()
        {
            // speeding up?
            if (VehicleDataSource.CurrentSpeed > LastCarSpeed)
            {
                SpeedInterpolation += Time.deltaTime * SpeedInterpolationSpeed;
            }
            else
            {
                SpeedInterpolation -= Time.deltaTime * SpeedInterpolationSpeed;
            }

            SpeedInterpolation = Mathf.Clamp01(SpeedInterpolation);
        }

        /// <summary>
        /// Apply the newly calculated interpolations to the car position
        /// </summary>
        private void UpdateCarPosition()
        {
            var smoothBreak = Mathf.SmoothStep(0f, 1f, BrakeInterpolation);
            var smoothSpeed = Mathf.SmoothStep(0f, 1f, SpeedInterpolation);

            // first the car is pulled back by any brake power on the car
            var brakePullPosition = Vector3.Lerp(BasePosition, BrakePosition, smoothBreak);

            // any speeding up power pulls the car forward
            var finalPosition = Vector3.Lerp(brakePullPosition, SpeedPosition, smoothSpeed);
            transform.localPosition = OriginalPosition + finalPosition;
        }
    }
}
