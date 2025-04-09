using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using HMI.Services;

namespace HMI.IVI.UI
{
    /// <summary>
    /// This Class is used in the creation of IVI Screens & navigating between them.
    /// </summary>
    public class ScreenNavigation : MonoBehaviour
    {
        /// <summary>
        /// This Sub-Class is a container class for each screen, and any extra actions to invoke when you navigate to it
        /// </summary>
        [Serializable]
        public class MainScreens
        {
            /// <summary>
            /// Screen identifier
            /// </summary>
            public string Name;

            /// <summary>
            /// Game object that contains the screen
            /// </summary>
            public GameObject Screen;

            /// <summary>
            /// Additional action to take when the screen is navigated to
            /// </summary>
            public UnityEvent AdditionalAction;

            /// <summary>
            /// Additional action to take when the screen is navigated away from
            /// </summary>
            public UnityEvent AdditionalActionOnDeactivate;
        }

        /// <summary>
        /// Enumerator of the various animation curve types. Used with screen transitions.
        /// </summary>
        [Serializable]
        public enum TransitionCurveType { Linear, EaseIn, EaseOut, EaseInOut, Constant, Custom }

        /// <summary>
        /// DelegateEventManager script link
        /// </summary>
        [Header("Event Delegate Manager")]
        public DelegateManager DelegateManagerScript;

        /// <summary>
        /// List of "MainScreen" class instances
        /// </summary>
        [Header("Main Screen Navigation")]
        [Tooltip("Add Screens here that will be navigated")]
        public List<MainScreens> MyScreensList = new List<MainScreens>();

        /// <summary>
        /// GameObject for which screen to be uses as the starting screen.
        /// </summary>
        [Tooltip("Add the gameobject of the screen to start on that is in the MyScreens list")]
        public GameObject StartingScreen;

        /// <summary>
        /// Index for the currently active screen within MyScreensList.
        /// </summary>
        private int ScreenIndex;

        /// <summary>
        /// Index for the Screen to transition to within MyScreensList.
        /// </summary>
        private int ScreenTarget;

        /// <summary>
        /// List of HMI_WidgetGroup scripts, which are on each of GameObjects that are on each MainScreens.Screen
        /// </summary>
        public HMI_WidgetGroup[] WidgetGroups { get; set; }

        /// <summary>
        /// Addtional Actions list to invoke when the screen's orientation is set to Landscape
        /// </summary>
        [Header("Screen Orientation")]
        public UnityEvent ActionsOnOrientationLandscape;

        /// <summary>
        /// Addtional Actions list to invoke when the screen's orientation is set to Portrait
        /// </summary>
        public UnityEvent ActionsOnOrientationPortrait;

        /// <summary>
        /// Vector2 useds as a quick reference to screen size
        /// </summary>
        private Vector2 CurrentScreenSize;

        /// <summary>
        /// Boolean set to true when screen is Landscape orientation, false for Portrait orientation
        /// </summary>
        public bool IsLandscape = true;

        /// <summary>
        /// Runtime bool used to check if the screen orientation has been changed
        /// </summary>
        private bool OrientationChanged = false;

        /// <summary>
        /// Float (in seconds) for the amount of time for a screen transition to occur
        /// </summary>
        [Header("Screen Transition Parameters")]
        [Tooltip("Time, in seconds, for screen transition to occur")]
        public float TransitionTime;

        /// <summary>
        /// TransitioncurveType enumerator
        /// </summary>
        [Tooltip("Type of curve to apply to transition")]
        public TransitionCurveType TransitionCurve;

        /// <summary>
        /// Animation Curve used if the TrasitionCurve enumerator is set to "Custom"
        /// </summary>
        [Tooltip("Curve to use when 'Custom' is the selected curve type")]
        public AnimationCurve CustomCurve;

        /// <summary>
        /// Bool which is true when screens are in the middle of transitioning
        /// </summary>
        public static bool IsTransitioning = false;

        /// <summary>
        /// Array of booleans for tracking the currently active screen
        /// </summary>
        private bool[] ActiveScreens;

        /// <summary>
        /// Activate all screens and sets CurrentScreensize & IsLandscape
        /// </summary>
        private void Awake()
        {
            TurnOnAllScreens();

            CurrentScreenSize = new Vector2(Screen.width, Screen.height);

            if (CurrentScreenSize.x < CurrentScreenSize.y)
            {
                IsLandscape = false;
            }
        }

        /// <summary>
        /// Start runs functions to set up script links, 
        /// open the starting screen, and invoke orientation actions.
        /// </summary>
        private void Start()
        {
            SetupScreenGroups();
            OpenStartingScreen();
            InvokeOrientationActions();
        }

        /// <summary>
        /// Check to see if the screensize is different from CurrentScreenSize, 
        /// if true updates CurrentScreenSize and begins orientation transition
        /// </summary>
        private void Update()
        {
            if (CurrentScreenSize != new Vector2(Screen.width, Screen.height))
            {
                CurrentScreenSize = new Vector2(Screen.width, Screen.height);

                StartScreenOrientationTransition(CurrentScreenSize);
            }
        }

        /// <summary>
        /// Fill out & initialize the WidgetGroups list with all HMI_WidgetGroup scripts found within MyScreensList
        /// </summary>
        private void SetupScreenGroups()
        {
            if (MyScreensList.Count > 0)
            {
                WidgetGroups = new HMI_WidgetGroup[MyScreensList.Count];
                for (var i = 0; i < MyScreensList.Count; i++)
                {
                    if (MyScreensList[i].Screen.GetComponent<HMI_WidgetGroup>() != null)
                    {
                        WidgetGroups[i] = MyScreensList[i].Screen.GetComponent<HMI_WidgetGroup>();
                        WidgetGroups[i].InitializeWidgetGroup(IsLandscape);

                    }
                    else
                    {
                        Debug.LogError("You are missing a UIScreenGroup script component at " + MyScreensList[i].Name + ".");
                    }
                }
            }
            else
            {
                Debug.LogError("On ScreenNavigationManager gameobject in your heirarchy, the screen navigation script has 0 in the screens list./n please add screens to this list.");
            }
        }

        /// <summary>
        /// Set all gameobjects within MyScreensList to active
        /// </summary>
        private void TurnOnAllScreens()
        {
            for (var i = 0; i < MyScreensList.Count; i++)
            {
                if (!MyScreensList[i].Screen.activeSelf)
                {
                    MyScreensList[i].Screen.SetActive(true);
                }
            }
        }

        /// <summary>
        /// Sets StartingScreen visibility and interactability
        /// </summary>
        private void OpenStartingScreen()
        {
            if (MyScreensList.Count != 0)
            {

                ActiveScreens = new bool[MyScreensList.Count];

                if (StartingScreen != null)
                {
                    for (var i = 0; i < MyScreensList.Count; i++)
                    {
                        if (StartingScreen == MyScreensList[i].Screen)
                        {
                            ActiveScreens[i] = true;
                            ScreenIndex = i;                    
                        }
                        else
                        {
                            ActiveScreens[i] = false;
                            WidgetGroups[i].SetGroupInactive();
                        }
                    }
                }
                else
                {
                    Debug.LogWarning("Your Starting screen variable is null, please fill the StartingScreen variable with the starting screen that is currently in your MyScreens List. For now I will start on the first position screen in your list");

                    ScreenIndex = 0;

                    for (var i = 0; i < MyScreensList.Count; i++)
                    {
                        ActiveScreens[i] = false;
                        WidgetGroups[i].SetGroupInactive();
                    }

                }
            }
            else
            {
                Debug.LogWarning("MyScreens List is empty, please include screens that you would like to navigate. In the Hierarchy, select SceneManager, then look at UINavigationScript, select the dropdown for MyScreens, increase the list count from 0 to your intended count, then from the hierarchy drag the parent gameobject to the new list item.");
            }

            if (!IsLandscape)
            {
                DelegateManagerScript.SetScreenOrientationToPortrait();
            }
        }

        /// <summary>
        /// Begin a transition between screens
        /// </summary>
        public void NavigateToSpecificScreen(GameObject DesiredScreen)
        {
            if (DesiredScreen != null && !IsTransitioning)
            {
                if (CheckIfDesiredScreenExists(DesiredScreen))
                {
                    for (var i = 0; i < MyScreensList.Count; i++)
                    {
                        if (DesiredScreen == MyScreensList[i].Screen && ActiveScreens[i] == false)
                        {
                            MyScreensList[ScreenIndex].AdditionalActionOnDeactivate.Invoke();
                            MyScreensList[i].AdditionalAction.Invoke();                         
                            ScreenTarget = i;
                            ScreenStartTransition();
                        }
                    }
                }
                else
                {
                    Debug.LogWarning("Your Desired screen (Gameobject) for your button's desired navigation was not matched to a Gameobject listed in MyScreens");
                }
            }
        }

        /// <summary>
        /// Start screen transition animation
        /// </summary>
        public void ScreenStartTransition()
        {
            DelegateManagerScript.SetButtonInnactive();

            IsTransitioning = true;

            if (ScreenIndex != ScreenTarget)
            {
                WidgetGroups[ScreenIndex].BeginTransition(false, TransitionTime, GetAnimationCurve(), WidgetGroups[ScreenTarget].ActiveGroups);
                WidgetGroups[ScreenTarget].BeginTransition(true, TransitionTime, GetAnimationCurve(), WidgetGroups[ScreenIndex].ActiveGroups);
            }
            else
            {
                if (OrientationChanged)
                {
                    if (IsLandscape)
                    {
                        WidgetGroups[ScreenIndex].BeginTransition(true, TransitionTime, GetAnimationCurve(), WidgetGroups[ScreenIndex].WidgetGroupsPortrait);
                    }
                    else
                    {
                        WidgetGroups[ScreenIndex].BeginTransition(true, TransitionTime, GetAnimationCurve(), WidgetGroups[ScreenIndex].WidgetGroupsLandscape);
                    }

                    WidgetGroups[ScreenIndex].SetGroupInactive();
                    OrientationChanged = false;
                }
                else
                {
                    WidgetGroups[ScreenIndex].BeginTransition(true, TransitionTime, GetAnimationCurve(), WidgetGroups[ScreenIndex].ActiveGroups);
                }
            }
        }

        /// <summary>
        /// Called via child WidgetGroup scripts, to notify that a screen transition has completed.
        /// </summary>
        public void ScreenTransitionEnded()
        {
            DelegateManagerScript.SetButtonActive();

            IsTransitioning = false;

            if (ScreenIndex != ScreenTarget)
            {
                ActiveScreens[ScreenIndex] = false;
                ActiveScreens[ScreenTarget] = true;

                ScreenIndex = ScreenTarget;
            }
        }

        /// <summary>
        /// Starts a screen orientation change
        /// </summary>
        public void StartScreenOrientationTransition(Vector2 CurrentScreen)
        {
            OrientationChanged = true;
            
            ScreenTarget = ScreenIndex;

            if (CurrentScreen.x > CurrentScreen.y && !IsLandscape)
            {
                IsLandscape = true;
                DelegateManagerScript.SetScreenOrientationToLandscape();
            }

            else if (CurrentScreen.x < CurrentScreen.y && IsLandscape)
            {
                IsLandscape = false;
                DelegateManagerScript.SetScreenOrientationToPortrait();
            }

            for (var i = 0; i < WidgetGroups.Length; i++)
            {
                WidgetGroups[i].SetOrientation(IsLandscape);
            }

            ScreenStartTransition();
            InvokeOrientationActions();
        }

        /// <summary>
        /// Invokes additional actions during screen orientation transition, based on the value of IsLandscape
        /// </summary>
        private void InvokeOrientationActions()
        {
            if (IsLandscape)
            {
                ActionsOnOrientationLandscape.Invoke();
            }
            else
            {
                ActionsOnOrientationPortrait.Invoke();
            }
        }

        /// <summary>
        /// Checks if a Screen exists within MyScreensList
        /// </summary>
        private bool CheckIfDesiredScreenExists(GameObject goDesiredScreen)
        {
            for (var i = 0; i < MyScreensList.Count; i++)
            {
                if (goDesiredScreen == MyScreensList[i].Screen)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Function to check the value of ScreenIndex
        /// </summary>
        public int GetScreenIndex()
        {
            return ScreenIndex;
        }

        /// <summary>
        /// Function to check the value of the Trasitioncurve enum and return an appropriate animation curve
        /// </summary>
        public AnimationCurve GetAnimationCurve()
        {
            var ac = new AnimationCurve();

            switch (TransitionCurve)
            {
                case TransitionCurveType.EaseIn:
                    ac = AnimationCurve.EaseInOut(0, 0, 1, 1);
                    ac.keys[1].inTangent = 0;
                    break;
                case TransitionCurveType.EaseOut:
                    ac = AnimationCurve.EaseInOut(0, 0, 1, 1);
                    ac.keys[0].outTangent = 0;
                    break;
                case TransitionCurveType.EaseInOut:
                    ac = AnimationCurve.EaseInOut(0, 0, 1, 1);
                    break;
                case TransitionCurveType.Linear:
                    ac = AnimationCurve.Linear(0, 0, 1, 1);
                    break;
                case TransitionCurveType.Constant:
                    ac = AnimationCurve.Constant(0, 1, 1);
                    break;
                case TransitionCurveType.Custom:
                    ac = CustomCurve;
                    break;
            }

            return ac;
        }
    }
}

