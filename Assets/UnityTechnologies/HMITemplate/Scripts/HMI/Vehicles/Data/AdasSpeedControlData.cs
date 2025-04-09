using UnityEngine;

namespace HMI.Vehicles.Data
{
    /// <summary>
    /// Adas speed control automatically adjust vehicle speed to a set goal speed.
    /// This is the data that controls the behaviour
    /// </summary>
    [CreateAssetMenu(fileName = "AdasSpeedControl.asset", menuName = "HMI/Vehicle/Adas Speed Control", order = 1)]
    public class AdasSpeedControlData : ScriptableObject
    {
        /// <summary>
        /// The increment that the speed control will increase/decrease with
        /// </summary>
        [Tooltip("The increment that the speed control will increase/decrease with")]
        public float Increment = 0f;

        /// <summary>
        /// Minimum Speed for the automated system
        /// </summary>
        [Tooltip("Minimum Speed for the automated system")]
        public float MinSpeed = 0f;

        /// <summary>
        /// Maximum Speed for the automated system
        /// </summary>
        [Tooltip("Maximum Speed for the automated system")]
        public float MaxSpeed;

        /// <summary>
        /// Goal Speed for the automated system
        /// </summary>
        [Tooltip("Goal Speed for the automated system")]
        public float GoalSpeed;

        /// <summary>
        /// Is the automatic speed control system active
        /// </summary>
        [Tooltip("Is the automatic speed control system active")]
        public bool IsSpeedControlActive;
    }
}
