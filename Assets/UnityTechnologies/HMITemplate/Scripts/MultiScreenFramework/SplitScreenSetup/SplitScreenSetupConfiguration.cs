using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using HMI.Utilities;

namespace MultiScreenFramework.SplitScreenSetup
{
    [RequireComponent(typeof(SplitScreenVisualizerScript))]
    public class SplitScreenSetupConfiguration : MonoBehaviour
    {
        /// <summary>
        /// The Class to store the data of each of the screens to be added for split screen
        /// </summary>
        [Serializable]
        public class SplitScreenSetupClass
        {
            [Header("Exact scene name that will be used in builds to load scene")]
            /// <summary>
            /// The Exact scene name that will be used in builds to load scene
            /// </summary>
            public string SceneName;
            [Header("Scene object for editor loading")]
            /// <summary>
            /// Obj that allows for user to drag their scene directly in here, will work in editor development;
            /// Will not work when build is made.
            /// </summary>
            public UnityEngine.Object Scene;

            [Header("Camera Viewport Setup")]
            /// <summary>
            /// Slider between 0 and 1 that allows for the Camera Viewport X to be setup
            /// </summary>
            [Range(0, 1)]
            public float CameraViewport_X;
            /// <summary>
            /// Slider between 0 and 1 that allows for the Camera Viewport Y to be setup
            /// </summary>
            [Range(0, 1)]
            public float CameraViewport_Y;
            /// <summary>
            /// Slider between 0 and 1 that allows for the Camera Viewport W to be setup
            /// </summary>
            [Range(0, 1)]
            public float CameraViewport_W;
            /// <summary>
            /// Slider between 0 and 1 that allows for the Camera Viewport H to be setup
            /// </summary>
            [Range(0, 1)]
            public float CameraViewport_H;
        }

        [Header("Split Screen Display Initialization")]
        /// <summary>
        /// The intended Display settings of the Display from the scriptable Object 
        /// </summary>
        public ScreenSetup_ScriptableObject DisplayData;

        /// <summary>
        /// The intended FullScreenMode
        /// </summary>
        public FullScreenMode Mode;

        /// <summary>
        /// User generated list of SplitScreenSetupClass
        /// </summary>
        public List<SplitScreenSetupClass> ScenesList = new List<SplitScreenSetupClass>();

        /// <summary>
        /// List of all async loading occuring for tracking of loading completion
        /// </summary>
        public List<AsyncOperation> AllAsyncLoads = new List<AsyncOperation>();

        /// <summary>
        /// Unity awake callback
        /// </summary>
        private void Awake()
        {
            // Initializes and sets the resolution of the current screen.
            Screen.SetResolution(DisplayData.DisplayData.Screen_WidthResolution, DisplayData.DisplayData.Screen_WidthResolution, Mode);
        }

        /// <summary>
        /// Unity Start callback
        /// </summary>
        private void Start()
        {
            // For each SplitScreenSetupClass within the ScenesList
            foreach (var scene in ScenesList)
            {
                // If the current iteration Scene Object isn't null
                if (scene.Scene != null)
                {
                    // Additively loading the ScenesList[i].Scene.name and tracking through adding it to AllAsyncLoads 
                    AllAsyncLoads.Add(SceneManager.LoadSceneAsync(scene.Scene.name, LoadSceneMode.Additive));
                }
                // else, if the current iteration Scene Name isn't null
                else if (!string.IsNullOrEmpty(scene.SceneName))
                {
                    /// <summary>
                    /// Additively loading the ScenesList[i].Scene.name and tracking through adding it to AllAsyncLoads 
                    /// </summary>
                    AllAsyncLoads.Add(SceneManager.LoadSceneAsync(scene.SceneName, LoadSceneMode.Additive));
                }
                else
                {
                    // If neither the Scene or SceneName matches, then we send a warning to the user to
                    // update their class with the correct information.
                    Debug.LogWarning("Both Scene and Scene Name are showing as null");
                }

            }

            // Start a coroutine to check for scene loading and elimation of additional Event Systems and Audio listeners in the scene. 
            StartCoroutine(CheckIfAllScenesAreaFinishedLoadingThenDeleteExtraAudioListeners());
        }

        /// <summary>
        /// Coroutine to check for scene loading and elimation of additional Event Systems and Audio listeners in the scene. 
        /// </summary>
        private IEnumerator CheckIfAllScenesAreaFinishedLoadingThenDeleteExtraAudioListeners()
        {
            // bool to track if loading is completed 
            var hasCompletedLoading = false;

            do
            { 
                var isStillLoading = false;

                ///Checking AllAsyncLoads to see if anything is still loading
                foreach (var asyncload in AllAsyncLoads)
                {
                    if (!asyncload.isDone)
                    {
                        isStillLoading = true;
                    }
                }

                // If loading is completed then we end the do while by setting CompletedLoading to true,
                // otherwise we wait til end of frame and do the loop again
                if (!isStillLoading)
                {
                    hasCompletedLoading = true;
                }
                else
                {
                    yield return new WaitForEndOfFrame();
                }

            } while (!hasCompletedLoading);

            // Wait til end of frame again to allow for proper loading
            yield return new WaitForEndOfFrame();

            // Find all cameras in the open scenes. 
            var allCameras = GameObject.FindGameObjectsWithTag("MainCamera");

            // Search through all cameras and destroy any audio listeners in the scene that isn't the first audio listener. 
            for (var i = 0; i < allCameras.Length; i++)
            {
                if (i != 0)
                {
                    Destroy(allCameras[i].GetComponent<AudioListener>());
                }
            }

            // Find all EventSystems in the open scenes. 
            var EventSystems = GameObject.FindObjectsOfType<EventSystem>();

            // Search through all cameras and destroy any event systems in the scene that isn't the first event system. 
            for (var k = 0; k < EventSystems.Length; k++)
            {
                if (k != 0)
                {
                    Destroy(EventSystems[k].gameObject);
                }
            }

            // Clear our async loads when the loading is completed.
            AllAsyncLoads.Clear();
            // Set camera viewports using all cameras. 
            SetCameraViewports(allCameras);
        }

        /// <summary>
        /// Sets the cameras rect to the user entered x, y, w, and h of the ScenesList
        /// Sets the cameras targetdisplay to 0
        /// <param name="AllCameras"> the string path of the scene. </param>
        /// </summary>
        private void SetCameraViewports(GameObject[] AllCameras)
        {
            // Uses a camera assigned counter in order to check for how many cameras have been assigned that have the tag main camera. 
            var camerasAssignedCounter = 0;

            foreach (var camera in AllCameras)
            {
                // Checks for if the Camera has the Tag MainCamera before setting values
                if (camera.CompareTag("MainCamera"))
                {
                    if (ScenesList.Count > camerasAssignedCounter)
                    {
                        camera.GetComponent<Camera>().rect = new Rect(
                            ScenesList[camerasAssignedCounter].CameraViewport_X, 
                            ScenesList[camerasAssignedCounter].CameraViewport_Y, 
                            ScenesList[camerasAssignedCounter].CameraViewport_W, 
                            ScenesList[camerasAssignedCounter].CameraViewport_H);

                        camera.GetComponent<Camera>().targetDisplay = 0;
                        camerasAssignedCounter++;
                    }
                }
            }
        }

        /// <summary>
        /// Returns Display Data when called. 
        /// </summary>
        public ScreenSetup_ScriptableObject ReturnDisplayData()
        {
            return DisplayData;
        }
    }
}