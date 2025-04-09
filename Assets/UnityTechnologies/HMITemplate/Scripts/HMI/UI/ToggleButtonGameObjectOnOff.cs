using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HMI.UI
{
    /// <summary>
    /// This Script will check for if a Toggle on the same gameobject as it isOn or not isOn and then change its gameobjects to reflect.  
    /// </summary>
    [RequireComponent(typeof(Toggle))]
    public class ToggleButtonGameObjectOnOff : MonoBehaviour
    {
        /// <summary>
        /// The gameobjects to be set if the Toggle isOn
        /// </summary>
        public List<GameObject> ToggleOn;

        /// <summary>
        /// The gameobjects to be set if the Toggle is not on 
        /// </summary>
        public List<GameObject> ToggleOff;

        /// <summary>
        /// A variable to get the Toggle component 
        /// </summary>
        private Toggle ToggleComponent;

        /// <summary>
        /// Unity Awake callback
        /// </summary>
        private void Awake()
        {
            // Sets the toggle component to this variable 
            ToggleComponent = GetComponent<Toggle>();
            ProcessToggle();
        }

        /// <summary>
        /// This method will be called from the Toggle component on boolean change of "isOn".
        /// </summary>
        public void ProcessToggle()
        {
            foreach (var go in ToggleOff)
            {
                go.SetActive(!ToggleComponent.isOn);
            }

            foreach (var go in ToggleOn)
            {
                go.SetActive(ToggleComponent.isOn);
            }
        }
    }

}