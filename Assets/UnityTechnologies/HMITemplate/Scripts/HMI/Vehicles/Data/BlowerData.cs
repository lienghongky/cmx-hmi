using HMI.Vehicles.Data.Interfaces;
using UnityEngine;

namespace HMI.Vehicles.Data
{
    /// <summary>
    /// Blower data for a single blower
    /// </summary>
    [CreateAssetMenu(fileName = "Blower.asset", menuName = "HMI/Vehicle/HVAC/Blower", order = 1)]
    public class BlowerData : ScriptableObject, ILevelProvider
    {
        /// <summary>
        /// Name of the blower(eg driver, passenger, backseat, etc.)
        /// </summary>
        [Tooltip("Name of the blower(eg driver, passenger, backseat, etc.)")]
        public string BlowerName;

        /// <summary>
        /// Number of blow speed levels supported by the blower (if 1 is supported, it only has on/off)
        /// </summary>
        [Tooltip("Number of blow speed levels supported by the blower")]
        [Range(1, 10)]
        public int NumBlowerLevels;

        /// <summary>
        /// Current level of the blower
        /// </summary>
        [Tooltip("Current level of the blower")]
        [Range(1, 10)]
        public int CurrentBlowerLevel;

        /// <summary>
        /// Name of the provider
        /// </summary>
        public string Name => BlowerName;

        /// <summary>
        /// Number of levels on the provider
        /// </summary>
        public int Levels => NumBlowerLevels;

        /// <summary>
        /// Current level of the blower 
        /// </summary>
        public int CurrentLevel { get => CurrentBlowerLevel; set => CurrentBlowerLevel = value; }

        /// <summary>
        /// Increase level
        /// </summary>
        public void Increase()
        {
            var newLevel = CurrentLevel + 1;
            CurrentLevel = newLevel > Levels ? 0 : newLevel;
        }

        /// <summary>
        /// Decrease level
        /// </summary>
        public void Decrease()
        {
            var newLevel = CurrentLevel - 1;
            CurrentLevel = newLevel < Levels ? Levels : newLevel;
        }
    }
}
