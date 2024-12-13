using UnityEngine;

namespace HMI.Vehicles.Data
{
    /// <summary>
    /// Brake data contains the characteristics of the brakes on the vehicle
    /// </summary>
    [CreateAssetMenu(fileName = "BrakeData.asset", menuName = "HMI/Vehicle/Brake", order = 1)]
    public class BrakeData : ScriptableObject
    {
        /// <summary>
        /// The power of the brake
        /// </summary>
        [Tooltip("The power of the brake")]
        [Range(0, 50)]
        public float BrakePower = 0.1f;

        /// <summary>
        /// Get the brake power
        /// </summary>
        /// <param name="multiplier">Multiplier determines relatively how much of the total brake power the driver is applying [0.0...1.0]</param>
        /// <returns>Brake power</returns>
        public float GetBrakePower(float multiplier)
        {
            return multiplier * BrakePower;
        }

        /// <summary>
        /// Apply the brake to the vehicle
        /// </summary>
        /// <param name="multiplier">Multiplier determines relatively how much of the total brake power the driver is applying [0.0...1.0]</param>
        /// <param name="speed">Speed of the vehicle in Km/h</param>
        /// <param name="dt">Deltatime</param>
        /// <returns>Adjusted speed with the brake power applied</returns>
        public float Brake(float multiplier, float speed, float dt)
        {
            var power = GetBrakePower(multiplier);
            power *= dt;

            if (speed >= 0f)
            {
                speed -= power;

                if (speed < 0f)
                {
                    speed = 0f;
                }
            }
            else
            {
                speed += power;

                if (speed > 0f)
                {
                    speed = 0f;
                }
            }

            return speed;
        }
    }
}
