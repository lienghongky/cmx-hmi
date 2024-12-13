using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HMI.Vehicles.Data
{
    /// <summary>
	/// Represents a dataset of gears that part of the vehicle's transmission
	/// </summary>
    [CreateAssetMenu(fileName = "TransmissionData.asset", menuName = "HMI/Vehicle/Transmission", order = 1)]

    public class TransmissionData : ScriptableObject
    {
        /// <summary>
        /// The gears in this transmission setup
        /// </summary>
        [SerializeField]
        private List<GearData> Gears = new List<GearData>();

        /// <summary>
        /// The names of the Gears in this transmission setup
        /// </summary>
        public List<string> Gearnames
        {
            get
            {
                return Gears.Select(x => x.Gear).ToList();
            }
        }

        /// <summary>
        /// Get a gear's data in the transmission dataset based on the name of the gear
        /// </summary>
        /// <param name="key">Name of the gear</param>
        /// <returns>Data of the gear</returns>
        public GearData this[string key] { get => Gears.FirstOrDefault(x => x.Gear == key); set => throw new NotSupportedException("List is read only"); }

        /// <summary>
        /// Numbers of gears in this transmission
        /// </summary>
        public int Count => Gears.Count;

        /// <summary>
        /// Check if the transmission contains a gear with the specified name
        /// </summary>
        /// <param name="key">Name of the gear</param>
        /// <returns>True if the transmission setup contains the gear, false otherwise</returns>
        public bool ContainsKey(string key)
        {
            return Gears.Any(x => x.Gear == key);
        }

        /// <summary>
        /// Try to get a gear with a specified name from the transmission setup
        /// </summary>
        /// <param name="key">Name of the gear</param>
        /// <param name="value">Data of the gear</param>
        /// <returns>True if transmission setup contained gear, false otherwise</returns>
        public bool TryGetValue(string key, out GearData value)
        {
            value = Gears.FirstOrDefault(x => x.Gear == key);
            return value != null;
        }
    }
}
