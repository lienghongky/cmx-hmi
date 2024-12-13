using HMI.Vehicles.Data.Interfaces;
using UnityEngine;

namespace HMI.Vehicles.Data
{
    /// <summary>
    /// Heating data for a single heating element
    /// </summary>
    [CreateAssetMenu(fileName = "HeatingData.asset", menuName = "HMI/Vehicle/HVAC/Heating", order = 1)]
    public class HeatingData : ScriptableObject, ILevelProvider
    {
        /// <summary>
        /// Name of the element being heated (eg driver, passenger, backseat, etc.)
        /// </summary>
        [Tooltip("Name of the element being heated (eg driver, passenger, backseat, steering wheel etc.)")]
        public string HeatingName;

        /// <summary>
        /// Number of heatlevels supported by the heating element (if 1 is supported, it only has on/off)
        /// </summary>
        [Tooltip("Number of heatlevels supported by the heating element (if 1 is supported, it only has on/off)")]
        [Range(1, 10)]
        public int NumHeatLevels;

        /// <summary>
        /// Current level of the heating
        /// </summary>
        [Tooltip("Current level of the heating")]
        [Range(1, 10)]
        public int CurrentHeatingLevel;

        /// <summary>
        /// Name of the provider
        /// </summary>
        public string Name => HeatingName;

        /// <summary>
        /// Number of levels on the provider
        /// </summary>
        public int Levels => NumHeatLevels;

        /// <summary>
        /// Current level of the heating data
        /// </summary>
        public int CurrentLevel { get => CurrentHeatingLevel; set => CurrentHeatingLevel = value; }

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
