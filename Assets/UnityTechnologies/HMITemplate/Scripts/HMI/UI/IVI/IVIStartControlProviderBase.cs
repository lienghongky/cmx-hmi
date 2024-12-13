using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HMI.UI.IVI
{
    /// <summary>
    /// Base class for implementation that provides a start control for input when a screen is selected
    /// </summary>
    public abstract class IVIStartControlProviderBase : MonoBehaviour
    {
        /// <summary>
        /// Get Portrait StartControl this can be used by the input manager to select a first control
        /// </summary>
        /// <returns>start control of a screen</returns>
        public abstract GameObject GetPortraitStartControl();

        /// <summary>
        /// Get Landscape StartControl this can be used by the input manager to select a first control
        /// </summary>
        /// <returns>start control of a screen</returns>
        public abstract GameObject GetLandscapeStartControl();
    }
}
