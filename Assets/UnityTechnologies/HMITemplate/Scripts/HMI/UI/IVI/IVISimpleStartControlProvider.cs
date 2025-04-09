using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HMI.UI.IVI
{
    /// <summary>
    /// Simple implementation of a start control provider. Provides start controls that 
    /// are configured through the inspector
    /// </summary>
    public class IVISimpleStartControlProvider : IVIStartControlProviderBase
    {
        /// <summary>
        /// Start control to return when in landscape mode
        /// </summary>
        public GameObject LandscapeStartControl;

        /// <summary>
        /// Start control to return when in portrait mode
        /// </summary>
        public GameObject PortraitStartControl;

        /// <summary>
        /// Get Portrait StartControl this can be used by the input manager to select a first control
        /// </summary>
        /// <returns>start control of a screen</returns>
        public override GameObject GetPortraitStartControl()
        {
            return PortraitStartControl;
        }

        /// <summary>
        /// Get Landscape StartControl this can be used by the input manager to select a first control
        /// </summary>
        /// <returns>start control of a screen</returns>
        public override GameObject GetLandscapeStartControl()
        {
            return LandscapeStartControl;
        }

        /// <summary>
        /// /// Changes the starting control item for Landscape mode
        /// </summary>
        /// <param name="NewStarter"></param>
        public void ChangeLandscapeStartControl(GameObject NewStarter)
        {
            LandscapeStartControl = NewStarter;
        }

        /// <summary>
        /// Changes the starting control item for Portrait mode
        /// </summary>
        /// <param name="NewStarter"></param>
        public void ChangePortraitStartControl(GameObject NewStarter)
        {
            PortraitStartControl = NewStarter;
        }
    }
}
