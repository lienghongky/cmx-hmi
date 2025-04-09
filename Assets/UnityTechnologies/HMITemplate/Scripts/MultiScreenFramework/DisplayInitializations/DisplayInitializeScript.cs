using MultiScreenFramework.SplitScreenSetup;
using System.Collections.Generic;
using UnityEngine;
using HMI.Utilities;

namespace MultiScreenFramework.DisplayInitializations
{
    /// <summary>
    /// Each scene has this script on the scene manager. Used to manage the display settings for a specific scene
    /// </summary>
    public class DisplayInitializeScript : MonoBehaviour
    {
        /// <summary>
        /// Exact name of the current scene to be inputted by user
        /// </summary>
        public string CurrentSceneName;

        /// <summary>
        /// Display data to be used for setting up the display when scene is launched. 
        /// </summary>
        public ScreenSetup_ScriptableObject DisplayData;

        /// <summary>
        /// The enum for choice of fullscreenmode to be intialized on single display launch. 
        /// </summary>
        public FullScreenMode Mode;

        /// <summary>
        /// The main camera of the current scene. If its a single display initialization and camera is set in this variable,
        /// then target display of the camera will be set to display 0.
        /// </summary>
        public Camera MainCamera;

        /// <summary>
        /// A list of canvases the user can add, if scene opens on a single display initialization and canvas is set in this variable,
        /// then target display of the canvas will be set to display 0.
        /// </summary>
        public List<Canvas> ListOfAllCanvasShowingOnMainCamera = new List<Canvas>();

        /// <summary>
        /// Unity Awake callback
        /// </summary>
        private void Awake()
        {
            // On awake, checking if there is another setup script occuring, if there isn't then we initialize a single display
            // based on user inputted variables.
            // FoundDisplaySetUpScript(): Checking for if there is a Multi Screen Display Setup Script, indicating that there is a mutlti display setup 
            // FoundSplitScreenSetUpScript(): Checking for if there is a Split Screen Setup Script, indicating that there is a mutlti display setup 
            if (FoundDisplaySetUpScript())
            {
                // Finds the MultiScreenDisplaysSetup gameobject in the scene and gets the script component MultiDisplayInitializeScript.
                var DisplayInitializationScript = GameObject.Find("MultiScreenDisplaysSetup").GetComponent<MultiDisplayInitializeScript>();

                // Sets the mode to FullScreenWindow to allow for multiscreen setup.
                Mode = FullScreenMode.FullScreenWindow;

                // Sets the Main camera to target display
                SetMainCameraToTargetDisplay(ReturnSceneInList(DisplayInitializationScript, CurrentSceneName));

                // Sets the Canvases to target display
                SetAllCanvasToTargetDisplay(ReturnSceneInList(DisplayInitializationScript, CurrentSceneName));
            }
            else if (FoundSplitScreenSetUpScript())
            {
                // Sets the Main camera and all canvases to target display of 0.
                SetMainCameraToTargetDisplay(0);
                SetAllCanvasToTargetDisplay(0);
            }
            else
            {
                // Sets the resolution of the screen, width and heigh, and the fullscreenwindow mode. 
                Screen.SetResolution(DisplayData.DisplayData.Screen_WidthResolution, DisplayData.DisplayData.Screen_HeightResolution, Mode);

                // Sets the Main camera and all canvases to target display of 0.
                SetMainCameraToTargetDisplay(0);
                SetAllCanvasToTargetDisplay(0);
            }
        }

        /// <summary>
        /// Sets the Main camera to the target display
        /// <param name="TargetDisplay"> int of the target display that the user would like the targetdisplay of the main camera set to. </param>
        /// </summary>
        private void SetMainCameraToTargetDisplay(int TargetDisplay)
        {
            if (MainCamera != null)
            {
                MainCamera.targetDisplay = TargetDisplay;
            }
        }

        /// <summary>
        /// sets all of the target displays in the ListOfAllCanvasShowingOnMainCamera to the TargetDisplay
        /// <param name="TargetDisplay"> int of the target display that the user would like the targetdisplay of the canvas set to. </param>
        /// </summary>
        private void SetAllCanvasToTargetDisplay(int TargetDisplay)
        {
            for (var i = 0; i < ListOfAllCanvasShowingOnMainCamera.Count; i++)
            {
                if (ListOfAllCanvasShowingOnMainCamera[i] != null)
                {
                    ListOfAllCanvasShowingOnMainCamera[i].targetDisplay = TargetDisplay;
                }
            }
        }

        /// <summary>
        /// Checking the isntanced MultiDisplayInitializeScript for a scene that matches the intended SceneName and
        /// returned the location of that scene in the list.
        /// <param name="Script"> The display intialization script for multidisplay setup. </param>
        /// <param name="SceneName"> The exact string name of the target scene. </param>
        /// </summary>
        private int ReturnSceneInList(MultiDisplayInitializeScript Script, string SceneName)
        {
            for (var i = 0; i < Script.MyDisplays.Count; i++)
            {
                if (Script.MyDisplays[i].SceneNameToLoad == SceneName)
                {
                    return i;
                }
            }

            return 0;
        }

        /// <summary>
        /// Checking for if there is a Multi Screen Display Setup Script, indicating that there is a mutlti display setup.
        /// If a MultiDisplayInitializeScript is found then returns true, otherwise return false;
        /// </summary>
        private bool FoundDisplaySetUpScript()
        {
            if (GameObject.Find("MultiScreenDisplaysSetup") != null)
            {
                if (GameObject.Find("MultiScreenDisplaysSetup").GetComponent<MultiDisplayInitializeScript>() != null)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Checking for if there is a Split Screen Setup Script, indicating that there is a mutlti display setup.
        /// If a SplitScreenSetupConfiguration is found then returns true, otherwise return false;
        /// </summary>
        private bool FoundSplitScreenSetUpScript()
        {
            if (GameObject.Find("SplitScreenSetup") != null)
            {
                if (GameObject.Find("SplitScreenSetup").GetComponent<SplitScreenSetupConfiguration>() != null)
                {
                    return true;
                }
            }

            return false;
        }
    }
}