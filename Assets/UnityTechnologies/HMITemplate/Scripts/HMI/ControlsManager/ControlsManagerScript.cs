using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using HMI.Utilities;

namespace HMI.ControlsManager
{
    /// <summary>
    /// This Script will use the ControlsData scriptable object. This will use the values and names assigned in there in order to perform actions
    /// based on key press that matches the mapped assigned key.
    /// </summary>
    public class ControlsManagerScript : MonoBehaviour, ISerializationCallbackReceiver
    {
        /// <summary>
        /// Class for KeyMappingEvents
        /// string ControlMappingName is a string that must match the name of your control in your ControlsData
        /// DesiredAction are user defined actions that will occur when button is pressed. 
        /// </summary>
        [Serializable]
        public class KeyMappingEvents
        {
            [ListToPopup(typeof(ControlsManagerScript), "StaticControlMappingList")]
            public string ControlMappingName;

            [ReadOnly] public string KeyMappedControl = "Press Refresh button to Update Keymapped Control";
            public UnityEvent DesiredAction;
        }

        /// <summary>
        /// Controls_ScriptableObject data
        /// </summary>
        public Controls_ScriptableObject ControlsData;

        /// <summary>
        /// Static list used to create our dropdown drawer
        /// </summary>
        public static List<string> StaticControlMappingList;

        /// <summary>
        /// List of all control names in our controls data
        /// </summary>
        [HideInInspector]
        public List<string> AllControlNamesList = new List<string>();

        /// <summary>
        /// List of the KeyMappingEvents that user will generate
        /// </summary>   
        public List<KeyMappingEvents> KeyPressEvents = new List<KeyMappingEvents>();

        /// <summary>
        /// Unity Start callback
        /// </summary>
        private void Start()
        {
            UpdateAllControlsList();
        }

        /// <summary>
        /// Unity Update callback
        /// </summary>
        private void Update()
        {
            // Listens for Key down of all Keys in ControlsData.ControlsMappingList
            // on key down that matches assigned keycode, will perform the unitevents that are assigned to that named task
            foreach (var mapping in ControlsData.ControlsMappingList)
            {
                if (mapping.Continuous)
                {
                    if (Input.GetKey(mapping.AssignedKey))
                    {
                        //Debug.Log("Key Recognized and Pressed: " + ControlsData.ControlsMappingList[i].AssignedKey);
                        DoTask(mapping.Name);
                    }
                }
                else
                {
                    if (Input.GetKeyDown(mapping.AssignedKey))
                    {
                        //Debug.Log("Key Recognized and Pressed: " + ControlsData.ControlsMappingList[i].AssignedKey);
                        DoTask(mapping.Name);
                    }
                }
            }
        }

        /// <summary>
        /// Invokes the unityevents assigned to the task
        /// </summary>
        private void DoTask(string TaskName)
        {
            if (!string.IsNullOrEmpty(TaskName))
            {
                for (var i = 0; i < KeyPressEvents.Count; i++)
                {
                    if (KeyPressEvents[i].ControlMappingName == TaskName)
                    {
                        KeyPressEvents[i].DesiredAction.Invoke();
                    }
                }
            }
        }

        /// <summary>
        /// An example task demonstrating a keypress
        /// </summary>
        public void TestDoTask()
        {
            Debug.Log("Task Successful");
        }

        /// <summary>
        /// Serializes the dynamic list AllControlNamesList to be our new static list StaticControlMappingList
        /// </summary>
        public void OnBeforeSerialize()
        {
            if (ControlsData != null)
            {
                if (StaticControlMappingList != AllControlNamesList)
                {
                    StaticControlMappingList = AllControlNamesList;
                }
            }
        }

        /// <summary>
        /// After serialize callback
        /// </summary>
        public void OnAfterDeserialize()
        {
        }

        /// <summary>
        /// If the control data isnt null, then clears the current list
        /// Populates the new list with data
        /// </summary>
        public void UpdateAllControlsList()
        {
            if (ControlsData != null)
            {
                AllControlNamesList.Clear();
                for (var j = 0; j < ControlsData.ControlsMappingList.Count; j++)
                {
                    AllControlNamesList.Add(ControlsData.ControlsMappingList[j].Name);
                }

                for (var i = 0; i < KeyPressEvents.Count; i++)
                {
                    UpdateKeyMappedButton(KeyPressEvents[i]);
                }
            }
        }

        /// <summary>
        /// Update the key button
        /// </summary>
        public void UpdateKeyMappedButton(KeyMappingEvents CurrentClass)
        {
            if (ControlsData != null)
            {
                for (var i = 0; i < ControlsData.ControlsMappingList.Count; i++)
                {
                    if (CurrentClass.ControlMappingName == ControlsData.ControlsMappingList[i].Name)
                    {
                        CurrentClass.KeyMappedControl = ControlsData.ControlsMappingList[i].AssignedKey.ToString();
                    }
                }
            }
        }
    }
}