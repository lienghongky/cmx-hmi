using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HMI.IVI.UI
{
    /// <summary>
    /// Simple class for grouping gameobjects for easy one function deactivation
    /// </summary>
    public class IVIVehicleContentHelper : MonoBehaviour
    {
        /// <summary>
        /// List of GameObjects
        /// </summary>
        public List<GameObject> ObjectList;

        /// <summary>
        /// Function to set all gameobjects in the list to inactive
        /// </summary>
        public void SetObjectsInactive()
        {
            if (ObjectList != null)
            {
                for (int i = 0; i < ObjectList.Count; i++)
                {
                    ObjectList[i].SetActive(false);
                }
            }
        }
    }
}

