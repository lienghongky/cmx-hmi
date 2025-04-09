using System;
using UnityEngine;
using HMI.Services;

namespace HMI.UI.IVI
{
    /// <summary>
    /// This class uses delegate events to modify RectTrasform values when screen oruientation is changed.
    /// </summary>
    public class ScreenOrientationHelper : MonoBehaviour
    {
        /// <summary>
        /// This struct is a container for a gameobject and all of the values of its RectTransform
        /// </summary>
        [Serializable]
        public struct CanvasObject
        {
            public GameObject GameObject;
            public RectTransform Rect;
            public Vector2 Position;
            public Vector2 Size;
            public Vector2 AnchorMin;
            public Vector2 AnchorMax;
            public Vector2 Pivot;
            public Vector3 Rotation;
            public Vector3 Scale;
        }

        /// <summary>
        /// Array of CanvasObject structs for Landscape orientation
        /// </summary>
        [Header("Orientation Settings")]
        public CanvasObject[] LandscapeSettings;

        /// <summary>
        /// Array of CanvasObject structs for Portrait orientation
        /// </summary>
        public CanvasObject[] PortraitSettings;

        /// <summary>
        /// Bool to ignore delegate events
        /// </summary>
        [Header("Delegate Settings")]
        public bool IgnoreDelegateEvents = false;

        /// <summary>
        /// Unity Awake callback
        /// </summary>   
        private void Awake()
        {
            if (!IgnoreDelegateEvents)
            {
                // Subscribing to the SetScreenOrientatiopnToLandscape method to be called whenever orientationDelegateLandscape is Invoked 
                DelegateManager.OrientationDelegateLandscape += SetScreenOrientationToLandscape;

                // Subscribing to the SetScreenOrientatiopnToPortrait method to be called whenever buttonClickDelegateActive is Invoked 
                DelegateManager.OrientationDelegatePortrait += SetScreenOrientationToPortrait;
            }
        }

        /// <summary>
        /// Unity OnDestroy callback
        /// </summary>
        private void OnDestroy()
        {
            DelegateManager.OrientationDelegateLandscape -= SetScreenOrientationToLandscape;

            DelegateManager.OrientationDelegatePortrait -= SetScreenOrientationToPortrait;
        }

        /// <summary>
        /// Function to set canvas RectTransform values for objects in LandscapeSetting
        /// </summary>
        private void SetScreenOrientationToLandscape()
        {
            if (LandscapeSettings != null)
            {
                for (var i = 0; i < LandscapeSettings.Length; i++)
                {
                    //Position
                    LandscapeSettings[i].Rect.anchoredPosition = LandscapeSettings[i].Position;

                    //Size
                    LandscapeSettings[i].Rect.sizeDelta = LandscapeSettings[i].Size;

                    //Anchors
                    LandscapeSettings[i].Rect.anchorMax = LandscapeSettings[i].AnchorMax;
                    LandscapeSettings[i].Rect.anchorMin = LandscapeSettings[i].AnchorMin;

                    //Pivot
                    LandscapeSettings[i].Rect.pivot = LandscapeSettings[i].Pivot;

                    //rotation
                    LandscapeSettings[i].Rect.localEulerAngles = LandscapeSettings[i].Rotation;

                    //Size
                    LandscapeSettings[i].Rect.localScale = LandscapeSettings[i].Scale;
                }
            }
        }

        /// <summary>
        /// Function to set canvas RectTransform values for objects in PortraitSettings
        /// </summary>
        private void SetScreenOrientationToPortrait()
        {
            if (PortraitSettings != null)
            {
                for (var i = 0; i < PortraitSettings.Length; i++)
                {
                    //Position
                    PortraitSettings[i].Rect.anchoredPosition = PortraitSettings[i].Position;

                    //Size
                    PortraitSettings[i].Rect.sizeDelta = PortraitSettings[i].Size;

                    //Anchors
                    PortraitSettings[i].Rect.anchorMax = PortraitSettings[i].AnchorMax;
                    PortraitSettings[i].Rect.anchorMin = PortraitSettings[i].AnchorMin;

                    //Pivot
                    PortraitSettings[i].Rect.pivot = PortraitSettings[i].Pivot;

                    //rotation
                    PortraitSettings[i].Rect.localEulerAngles = PortraitSettings[i].Rotation;

                    //Size
                    PortraitSettings[i].Rect.localScale = PortraitSettings[i].Scale;
                }
            }
        }
    }
}

