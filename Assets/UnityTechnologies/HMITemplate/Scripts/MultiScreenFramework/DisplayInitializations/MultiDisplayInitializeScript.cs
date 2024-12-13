using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using HMI.Utilities;

namespace MultiScreenFramework.DisplayInitializations
{
    /// <summary>
    /// Sets up additively loaded scenes on different displays
    /// </summary>
    public class MultiDisplayInitializeScript : MonoBehaviour
    {
        /// <summary>
        /// The Class to store the data of each of the screens to be added for multi screen
        /// </summary>
        [Serializable]
        public class MultiDisplaySetup
        {
            /// <summary>
            /// Local name for user
            /// </summary>
            public string Name;
            /// <summary>
            /// The Display data of the intended display this screen will be on.
            /// </summary>
            public ScreenSetup_ScriptableObject DisplayData;
            /// <summary>
            /// The Exact scene name that will be used in builds to load scene
            /// </summary>
            public string SceneNameToLoad;
        }

        /// <summary>
        /// The enum for choice of fullscreenmode to be intialized on launch. 
        /// </summary>
        public FullScreenMode Mode;

        /// <summary>
        /// Setting this bool to true will force the editor to ignore screen setup and instead will additively load all other scenes in MyDisplays.
        /// Must Deselect before building or additional display data will not be used when setting up scene. 
        /// </summary>
        [Tooltip("Select true if you wish to see all scenes load in the editor. Must deselect before building.")]
        public bool DebugEditor_AsycLoadScenesListedInMyDisplays = false;

        /// <summary>
        /// List of all async loading occuring for tracking of loading completion
        /// </summary>
        private readonly List<AsyncOperation> AllAsyncLoads = new List<AsyncOperation>();

        /// <summary>
        /// List of MultiDisplaySetup class
        /// </summary>
        public List<MultiDisplaySetup> MyDisplays = new List<MultiDisplaySetup>();

        /// <summary>
        /// Unity Awake callback
        /// </summary>
        private void Awake()
        {
            // If MultiDisplaySetup is empty, then setup a default display.
            if (MyDisplays.Count == 0)
            {
                Screen.SetResolution(1920, 1080, FullScreenMode.FullScreenWindow);
            }
            // If MultiDisplaySetup is not empty, then setup the intial display
            else
            {
                Screen.SetResolution(MyDisplays[0].DisplayData.DisplayData.Screen_WidthResolution, MyDisplays[0].DisplayData.DisplayData.Screen_HeightResolution, Mode);
            }

            if (!DebugEditor_AsycLoadScenesListedInMyDisplays)
            {
                // For each of the iterations in MyDisplays List 
                // Activate the iterated display and set its values to those of the iteration of the class.
                for (var i = 0; i < MyDisplays.Count; i++)
                {
                    if (Display.displays.Length > i)
                    {
                        RefreshRate displayRate = new RefreshRate();
                        displayRate.denominator = (uint)MyDisplays[i].DisplayData.DisplayData.Screen_RefreshRate;
                        displayRate.numerator = 1;
                        Display.displays[i].Activate(MyDisplays[i].DisplayData.DisplayData.Screen_WidthResolution, MyDisplays[i].DisplayData.DisplayData.Screen_HeightResolution, displayRate);

                        // If the user inputted string of the scene name isnt null or empty, 
                        // And if the scene can be loaded, then load the scene async additively.
                        if (!string.IsNullOrEmpty(MyDisplays[i].SceneNameToLoad))
                        {
                            if (Application.CanStreamedLevelBeLoaded(MyDisplays[i].SceneNameToLoad))
                            {
                                LoadAsyncScene(MyDisplays[i].SceneNameToLoad);
                            }
                            else
                            {
                                // If the scene can't be loaded, then throw a warning to the user to resolve their naming issue.
                                Debug.LogWarning("Your identified scene name could not be found or loaded in build settings. Please double check you have the scene added to build settings and spelling matches your scene name call");
                            }
                        }
                        else
                        {
                            // If the scene can't be loaded, then throw a warning to the user to resolve their naming issue.
                            Debug.LogWarning("Your identified scene name could not be found or loaded in build settings. Please double check you have the scene added to build settings and spelling matches your scene name call");
                        }

                    }
                }
            }
            else
            {
                // For each of the iterations in MyDisplays List 
                // If the scene name entered is not null or empty, and it can be loaded, then 
                // asyc load the scene from that iteration
                foreach (var display in MyDisplays)
                {
                    if (!string.IsNullOrEmpty(display.SceneNameToLoad))
                    {
                        if (Application.CanStreamedLevelBeLoaded(display.SceneNameToLoad))
                        {
                            LoadAsyncScene(display.SceneNameToLoad);
                        }
                        else
                        {
                            // If the scene can't be loaded, then throw a warning to the user to resolve their naming issue.
                            Debug.LogWarning("Your identified scene name could not be found or loaded in build settings. Please double check you have the scene added to build settings and spelling matches your scene name call");
                        }
                    }
                    else
                    {
                        // If the scene can't be loaded, then throw a warning to the user to resolve their naming issue.
                        Debug.LogWarning("Your identified scene name could not be found or loaded in build settings. Please double check you have the scene added to build settings and spelling matches your scene name call");
                    }
                }
            }

            StartCoroutine(CheckIfAsyncLoadingIsCompelted());
            CleanUpSceneOfAudioListenersAndEventSystems();
        }

        /// <summary>
        /// Loads the scene async as an additive scene to the current scene.
        /// <param name="sceneName"> the string name of the scene. </param>
        /// </summary>
        public void LoadAsyncScene(string sceneName)
        {
            AllAsyncLoads.Add(SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive));
        }

        /// <summary>
        /// Unloads the scene async as an additive scene to the current scene.
        /// <param name="sceneName"> the string name of the scene. </param>
        /// </summary>
        public static void UnloadAsyncScene(string sceneName)
        {
            SceneManager.UnloadSceneAsync(sceneName);
        }

        /// <summary>
        /// Clean up scene
        /// </summary>
        private void CleanUpSceneOfAudioListenersAndEventSystems()
        {
            // Find all cameras in the open scenes. 
            var AllCameras = GameObject.FindGameObjectsWithTag("MainCamera");

            // search through all cameras and destroy any audio listeners in the scene that isn't the first audio listener. 
            for (var i = 0; i < AllCameras.Length; i++)
            {
                if (i != 0)
                {
                    Destroy(AllCameras[i].GetComponent<AudioListener>());
                }
            }

            // Find all EventSystems in the open scenes. 
            var EventSystems = GameObject.FindObjectsOfType<EventSystem>();

            // search through all cameras and destroy any event systems in the scene that isn't the first event system. 
            for (var k = 0; k < EventSystems.Length; k++)
            {
                if (k != 0)
                {
                    Destroy(EventSystems[k].gameObject);
                }
            }
        }

        /// <summary>
        /// Coroutine to check for scene loading and elimation of additional Event Systems and Audio listeners in the scene. 
        /// </summary>
        private IEnumerator CheckIfAsyncLoadingIsCompelted()
        {
            // bool to track if loading is completed 
            var CompletedLoading = false;
            do
            {
                // bool to track if still loading
                var StillLoading = false;

                // Checking AllAsyncLoads to see if anything is still loading
                for (var k = 0; k < AllAsyncLoads.Count; k++)
                {
                    if (!AllAsyncLoads[k].isDone)
                    {
                        StillLoading = true;
                    }
                }

                // If loading is completed then we end the do while by setting CompletedLoading to true, otherwise we wait til end of frame and do the loop again
                if (!StillLoading)
                {
                    CompletedLoading = true;
                }
                else
                {
                    yield return new WaitForEndOfFrame();
                }

            } 
            while (!CompletedLoading);

            // Wait til end of frame again to allow for proper loading
            yield return new WaitForEndOfFrame();
            CleanUpSceneOfAudioListenersAndEventSystems();
        }
    }
}