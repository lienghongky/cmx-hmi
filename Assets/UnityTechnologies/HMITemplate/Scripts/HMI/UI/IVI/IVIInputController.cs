using HMI.IVI.UI;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace HMI.UI.IVI
{
    /// <summary>
    /// IVI Input Controller for keyboard navigation through the IVI
    /// </summary>
    public class IVIInputController : MonoBehaviour
    {
        /// <summary>
        /// Ties a screen to a start control provider
        /// </summary>
        [Serializable]
        public class ScreenStartControl
        {
            /// <summary>
            /// Screen
            /// </summary>
            public GameObject Screen;

            /// <summary>
            /// Start control provider
            /// </summary>
            public IVIStartControlProviderBase StartControlProviderBase;
        }

        /// <summary>
        /// Respond to back key presses
        /// </summary>
        public UnityEvent BackPressed;

        /// <summary>
        /// Used to respond to screen switches by the navigation manager
        /// </summary>
        public ScreenNavigation ScreenNavigationManager;

        /// <summary>
        /// Cached axis event
        /// </summary>
        private AxisEventData AxisEventData;

        /// <summary>
        /// Set of screen start controls, when the navigation manager switches screens
        /// this list is used to find the start control for the newly selected screen
        /// </summary>
        public ScreenStartControl[] ScreenStartControls;

        /// <summary>
        /// Last active screen
        /// </summary>
        private GameObject LastActiveScreen;

        /// <summary>
        /// Unity Awake callback
        /// </summary>
        private void Start()
        {
            var startScreen = ScreenNavigationManager.StartingScreen;
            LastActiveScreen = startScreen;
            SelectStartControlForScreen(startScreen);
        }

        /// <summary>
        /// Unity Update callback
        /// </summary>
        private void Update()
        {
            // if for whatever reason the current selected gameobject becomes null
            // try to reselect it
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                SelectStartControlForScreen(LastActiveScreen);
            }
        }

        /// <summary>
        /// Tied the the screen navigation manager to respond to screen changes
        /// </summary>
        public void OnScreenChanged(GameObject screen)
        {
            LastActiveScreen = screen;
            SelectStartControlForScreen(screen);
        }

        /// <summary>
        /// Reselect the proper game object when a screen's orientation is changed
        /// </summary>
        public void OnScreenOrientationChanged()
        {
            SelectStartControlForScreen(LastActiveScreen);
        }

        /// <summary>
        /// Select a start control when navigating to a different screen
        /// </summary>
        private void SelectStartControlForScreen(GameObject screen)
        {
            if(screen == null)
            {
                return;
            }

            foreach (var control in ScreenStartControls)
            {
                if (control.Screen == screen)
                {
                    var startControl = GetStartControlForScreen(control);

                    EventSystem.current.SetSelectedGameObject(null);
                    EventSystem.current.SetSelectedGameObject(startControl);
                    
                    if (startControl != null)
                    {
                        startControl.GetComponent<Selectable>().Select();
                    }

                    break;
                }
            }
        }

        /// <summary>
        /// Gets the start control for a specific screen
        /// </summary>
        private static GameObject GetStartControlForScreen(ScreenStartControl control)
        {
            var currentScreenSize = new Vector2(Screen.width, Screen.height);

            var startControl = currentScreenSize.x < currentScreenSize.y
                ? control.StartControlProviderBase.GetPortraitStartControl()
                : control.StartControlProviderBase.GetLandscapeStartControl();

            return startControl;
        }

        /// <summary>
        /// Get the cached axis event
        /// </summary>
        private AxisEventData GetAxistEvent()
        {
            if (AxisEventData == null)
            {
                AxisEventData = new AxisEventData(EventSystem.current);
            }

            AxisEventData.Reset();
            return AxisEventData;
        }

        /// <summary>
        /// Move the UI navigation
        /// </summary>
        public void Move(GameObject targetObject, MoveDirection moveDir, Vector2 moveVector)
        {
            var axisEventData = GetAxistEvent();
            axisEventData.moveDir = moveDir;
            axisEventData.moveVector = moveVector;
            ExecuteEvents.Execute(targetObject, axisEventData, ExecuteEvents.moveHandler);
        }

        /// <summary>
        /// Navigate up on the active screen
        /// </summary>
        public void NavigateUp()
        {
            var targetObject = EventSystem.current.currentSelectedGameObject;

            if (targetObject != null)
            {
                Move(targetObject, MoveDirection.Up, new Vector2(0f, 1f));
            }
        }

        /// <summary>
        /// Navigate down on the active screen
        /// </summary>
        public void NavigateDown()
        {
            var targetObject = EventSystem.current.currentSelectedGameObject;

            if (targetObject != null)
            {
                Move(targetObject, MoveDirection.Down, new Vector2(0f, -1f));
            }
        }

        /// <summary>
        /// Navigate left on the active screen
        /// </summary>
        public void NavigateLeft()
        {
            var targetObject = EventSystem.current.currentSelectedGameObject;

            if (targetObject != null)
            {
                Move(targetObject, MoveDirection.Left, new Vector2(-1f, 0f));
            }
        }

        /// <summary>
        /// Navigate right on the active screen
        /// </summary>
        public void NavigateRight()
        {
            var targetObject = EventSystem.current.currentSelectedGameObject;

            if (targetObject != null)
            {
                Move(targetObject, MoveDirection.Right, new Vector2(1f, 0f));
            }
        }

        /// <summary>
        /// Give OK input on the active screen
        /// </summary>
        public void OK()
        {
            var targetObject = EventSystem.current.currentSelectedGameObject;

            if (targetObject != null)
            {
                ExecuteEvents.Execute(targetObject, new BaseEventData(EventSystem.current), ExecuteEvents.submitHandler);
            }
        }

        /// <summary>
        /// Give BACK input on the active screen
        /// </summary>
        public void Back()
        {
            BackPressed.Invoke();
        }
    }
}
