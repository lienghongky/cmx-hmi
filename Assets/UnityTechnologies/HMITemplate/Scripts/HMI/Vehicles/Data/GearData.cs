using UnityEngine;

namespace HMI.Vehicles.Data
{
    /// <summary>
    /// Dataset representing a gear in a transmission
    /// </summary>
    [CreateAssetMenu(fileName = "GearData.asset", menuName = "HMI/Vehicle/Gear", order = 1)]
    public class GearData : ScriptableObject
    {
        /// <summary>
        /// Name of the gear
        /// </summary>
        [Tooltip("Name of the gear")]
        public string Gear;

        /// <summary>
        /// Curve determines how fast a car speeds up relative to its current speed, use negative value if the car is in reversed
        /// </summary>
        [Tooltip("Curve determines how fast a car speeds up relative to its current speed, use negative value if the car is in reversed")]
        public AnimationCurve AccelerationCurve = AnimationCurve.Linear(0, 0, 1, 0);

        /// <summary>
        /// Accelerate based on the transmission gear
        /// </summary>
        /// <param name="multiplier">Relative multiplier to the relative acceleration</param>
        /// <param name="currentSpeed">Current speed of the vehicle</param>
        /// <param name="maxSpeed">The maximum speed of the vehicle</param>
        /// <param name="dt">Delta time</param>
        /// <returns>New current speed</returns>
        public float Accelerate(float multiplier, float currentSpeed, float maxSpeed, float dt)
        {
            var relativeSpeed = currentSpeed / maxSpeed;
            var cv = AccelerationCurve.Evaluate(relativeSpeed);
            currentSpeed += multiplier * cv * dt;
            currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed, maxSpeed);
            return currentSpeed;
        }
    }
}
