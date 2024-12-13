using UnityEngine;


namespace HMI.Utilities
{

    /// <summary>
    /// Check for if there was a change in MainCamera.rect compared to your PrevRect
    /// check for if there was a change in MainCamera.targetDisplay to your PrevTargetDisplay
    /// if you see a change then call UpdateRectAndTargetDisplay()
    /// </summary>
    public class CheckMainCamera : MonoBehaviour
    {
        /// <summary>
        /// User Assigns the main camera of the scene that will be used for modification of cameras
        /// this script is attached to
        /// </summary>
        public Camera MainCamera;

        /// <summary>
        /// Tracks the previous rect and compares to the main camera rect
        /// </summary>
        private Rect PrevRect = new Rect(0, 0, 0, 0);

        /// <summary>
        /// tracks the previous target display and compares to the main camera target display
        /// </summary>
        private int PrevTargetDisplay = 0;

        /// <summary>
        /// On start, call UpdateRectAndTargetDisplay()
        /// </summary>
        private void Start()
        {
            UpdateRectAndTargetDisplay();
        }

        /// <summary>
        /// Check for if there was a change in MainCamera.rect compared to your PrevRect
        /// check for if there was a change in MainCamera.targetDisplay to your PrevTargetDisplay
        /// if you see a change then call UpdateRectAndTargetDisplay()
        /// </summary>
        private void Update()
        {
            if (PrevRect != MainCamera.rect)
            {
                UpdateRectAndTargetDisplay();
            }

            if (PrevTargetDisplay != MainCamera.targetDisplay)
            {
                UpdateRectAndTargetDisplay();
            }
        }

        /// <summary>
        /// Update the camera this script is assigned to:
        /// - Match the main camera target display
        /// - Match the main camera rect
        /// Set our tracking variables to match the main camera values
        /// </summary>
        private void UpdateRectAndTargetDisplay()
        {
            GetComponent<Camera>().targetDisplay = MainCamera.targetDisplay;
            GetComponent<Camera>().rect = MainCamera.rect;
            PrevTargetDisplay = MainCamera.targetDisplay;
            PrevRect = MainCamera.rect;
        }
    }
}