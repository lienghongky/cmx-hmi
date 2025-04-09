using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HMI.UI.IVI
{
    /// <summary>
    /// Select the highest control on the y-axis in a group of controls
    /// </summary>
    public class IVIMediaStartControlProvider : IVIStartControlProviderBase
    {
        /// <summary>
        /// Container for the landscape mode controls
        /// </summary>
        public GameObject LandscapeTrackContainer;

        /// <summary>
        /// Container for the portrait mode controls
        /// </summary>
        public GameObject PortraitTrackContainer;

        /// <summary>
        /// Get Portrait StartControl this can be used by the input manager to select a first control
        /// </summary>
        /// <returns>start control of a screen</returns>
        public override GameObject GetPortraitStartControl()
        {
            return GetHighestControl(PortraitTrackContainer.transform);
        }

        /// <summary>
        /// Get Landscape StartControl this can be used by the input manager to select a first control
        /// </summary>
        /// <returns>start control of a screen</returns>
        public override GameObject GetLandscapeStartControl()
        {
            return GetHighestControl(LandscapeTrackContainer.transform);
        }

        /// <summary>
        /// Get the highest control on the y-axis
        /// </summary>
        private GameObject GetHighestControl(Transform transform)
        {
            float minY = float.MinValue;
            GameObject selectedControl = null;

            for (int i = 0; i < transform.childCount; i++)
            {
                var item = transform.GetChild(i);
                var y = item.transform.localPosition.y;

                if (y > minY)
                {
                    selectedControl = item.gameObject;
                    minY = y;
                }
            }

            return selectedControl;
        }
    }
}