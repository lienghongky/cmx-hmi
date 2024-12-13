using UnityEngine;

namespace HMI.Vehicles.Data
{
    /// <summary>
    /// Dataset representing an engine in a vehicle
    /// </summary>
    [CreateAssetMenu(fileName = "EngineData.asset", menuName = "HMI/Vehicle/Engine", order = 1)]
    public class EngineData : ScriptableObject
    {
        /// <summary>
        /// Maximum Speed of the Engine in Km/h
        /// </summary>
        [Tooltip("The maximum speed of the engine")]
        [Range(0, 300)]
        public float MaxSpeed = 100;

        /// <summary>
        /// Is the engine on?
        /// </summary>
        [Tooltip("Is the engine on?")]
        public bool IsOn;
    }
}