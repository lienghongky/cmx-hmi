using HMI.Vehicles.Behaviours.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HMI.Vehicles.Behaviours
{
    /// <summary>
    /// Responds to the engine start stop events, turns the transmission into P
    /// </summary>
    public class EngineStartStopResponse : MonoBehaviour
    {
        /// <summary>
        /// Vehicle Engine
        /// </summary>
        public EngineBase Engine;

        /// <summary>
        /// Vehicle Transmission
        /// </summary>
        public TransmissionBase Transmission;

        /// <summary>
        /// Engine State changed
        /// </summary>
        public void OnEngineStateChange()
        {
            if(Engine.IsEngineOn)
            {
                Transmission.SwitchToDesiredGear("P");
            }
            else
            {
                Transmission.SwitchToDesiredGear("P");
            }
        }
    }
}
