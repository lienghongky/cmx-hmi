using HMI.Vehicles.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace HMI.Vehicles.Behaviours
{
    /// <summary>
    /// Air Distribution system for the vehicle, determines the direction of air flow
    /// </summary>
    public class AirDistribution : MonoBehaviour
    {
        /// <summary>
        /// Configuration of the air distribution system
        /// </summary>
        public AirDistributionData AirDistributionData;

        /// <summary>
        /// Names of the various modes supported by the air distribution system
        /// </summary>
        public List<string> ModeNames { get { return AirDistributionData.ModeNames; } }

        /// <summary>
        /// Is the air distribution mode on or off?
        /// </summary>
        private readonly Dictionary<string, bool> ModeStates = new Dictionary<string, bool>();

        /// <summary>
        /// Does the air distribution system contains a mode
        /// </summary>
        /// <param name="modeName">mode name</param>
        /// <returns>true/false if mode contains state</returns>
        public bool ContainsMode(string modeName)
        {
            return ModeStates.ContainsKey(modeName);
        }

        /// <summary>
        /// Set the mode of the air distribution system
        /// </summary>
        /// <param name="modeName">name of the air distribution mode</param>
        /// <param name="state">is the air distribution mode activated?</param>
        public void SetModeState(string modeName, bool state)
        {
            if (ModeStates.ContainsKey(modeName))
            {
                ModeStates[modeName] = state;
            }
        }

        /// <summary>
        /// Check the state of a specific mode
        /// </summary>
        /// <returns>the state of the mode (on or off)</returns>
        public bool GetModeState(string modeName)
        {
            if (ModeStates.ContainsKey(modeName))
            {
                return ModeStates[modeName];
            }
            else
            {
                throw new ArgumentException("Mode does not exist");
            }
        }

        /// <summary>
        /// Unity Start callback
        /// </summary>
        private void Start()
        {
            foreach (var mode in AirDistributionData.Modes)
            {
                ModeStates.Add(mode.Name, mode.DefaultState);
            }
        }
    }
}
