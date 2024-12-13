using UnityEngine;

namespace HMI.Vehicles.Data
{
    /// <summary>
    /// Data for the configuration of a regenerative brake
    /// </summary>
    [CreateAssetMenu(fileName = "RegenerativeBrakingData.asset", menuName = "HMI/Vehicle/Regenerative Braking", order = 1)]
    public class RegenerativeBrakingData : ScriptableObject
    {
        /// <summary>
        /// Curve that transforms the brake power into KWh regenerated
        /// </summary>
        [Tooltip("Curve that transforms the brake power into KWh regenerated.")]
        public AnimationCurve BrakePowerToKwh;

        /// <summary>
        /// Calculate the amount of Kwh generated based on the provided brakepower
        /// </summary>
        /// <param name="brakepower">How powerfully the vehicle is braking</param>
        /// <returns>the energy generated in Kwh by the regenerative brake</returns>
        public float CalculateKwhGenerated(float brakepower)
        {
            return BrakePowerToKwh.Evaluate(brakepower);
        }
    }
}
