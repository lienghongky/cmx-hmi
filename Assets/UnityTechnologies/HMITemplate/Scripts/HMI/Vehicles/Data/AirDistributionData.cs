using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HMI.Vehicles.Data
{
    /// <summary>
    /// Air distribution settings
    /// </summary>
    [CreateAssetMenu(fileName = "AirDistributionData.asset", menuName = "HMI/Vehicle/HVAC/Air Distribution", order = 1)]
    public class AirDistributionData : ScriptableObject
    {
        /// <summary>
        /// The modes in this setup
        /// </summary>
        [SerializeField]
        private List<AirDistributionModeData> ModesData = new List<AirDistributionModeData>();

        /// <summary>
        /// The names of the modes in this air distribution setup
        /// </summary>
        public List<string> ModeNames
        {
            get
            {
                return ModesData.Select(x => x.Name).ToList();
            }
        }

        /// <summary>
        /// The modes in  this air distribution setup
        /// </summary>
        public List<AirDistributionModeData> Modes
        {
            get
            {
                return ModesData;
            }
        }

        /// <summary>
        /// Get a specific mode from the air distribution data
        /// </summary>
        /// <param name="key">Name of the mode</param>
        /// <returns>Data of the mode</returns>
        public AirDistributionModeData this[string key] { get => ModesData.FirstOrDefault(x => x.Name == key); set => throw new NotSupportedException("List is read only"); }

        /// <summary>
        /// Numbers of modes in this setup
        /// </summary>
        public int Count => ModesData.Count;

        /// <summary>
        /// Check if the air distribution system contains a mode with the specified name
        /// </summary>
        /// <param name="key">Name of the mode</param>
        /// <returns>True if the setup contained mode, false otherwise</returns>
        public bool ContainsKey(string key)
        {
            return ModesData.Any(x => x.Name == key);
        }

        /// <summary>
        /// Try to get a mode with a specified name from the setup
        /// </summary>
        /// <param name="key">Name of the air distribution mode</param>
        /// <param name="value">Data of the air distribution mode</param>
        /// <returns>True if the setup contained mode, false otherwise</returns>
        public bool TryGetValue(string key, out AirDistributionModeData value)
        {
            value = ModesData.FirstOrDefault(x => x.Name == key);
            return value != null;
        }
    }
}
