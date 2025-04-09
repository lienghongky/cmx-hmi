using System;
using UnityEngine;

namespace HMI.IVI.UI
{
    /// <summary>
    /// Class for organizing and controlling a group of child RectTransforms, which make up the components of a screen
    /// </summary>
    public class HMI_WidgetGroup : MonoBehaviour
    {
        /// <summary>
        /// This struct houses all of the data from a Rect Transform. This data is used to interpolate between multiple different Rect Transforms.
        /// </summary>
        [Serializable]
        public struct WidgetGroup
        {
            public string WidgetName;
            public GameObject WidgetGameObject;
            public bool DoNotInterpolate;
            public bool IgnoreAlphaFade;
            public CanvasGroup CanvasGroup { get; set; }
            public RectTransform RectTransform { get; set; }
            public Vector2 DefaultPosition { get; set; }
            public Vector2 DefaultSize { get; set; }
            public Vector2 DefaultAnchorMax { get; set; }
            public Vector2 DefaultAnchorMin { get; set; }
            public Vector2 DefaultPivot { get; set; }
            public Vector3 DefaultRotation { get; set; }
            public Vector3 DefaultScale { get; set; }
        }

        /// <summary>
        ///Struct array for Widgets used with Landscape(Horozontal) orientation.
        /// </summary>
        [Header("Widget Groups")]
        [Tooltip("Array of Widgets to be used in Landscape Orientation")]
        public WidgetGroup[] WidgetGroupsLandscape;

        /// <summary>
        ///Struct array for Widgets used with Portrait(Vertical) orientation
        [Tooltip("Array of Widgets to be used in Portrait Orientation")]
        public WidgetGroup[] WidgetGroupsPortrait;

        /// <summary>
        /// Struct array for the currently active orientation group
        /// </summary>
        public WidgetGroup[] ActiveGroups { get; set; }

        /// <summary>
        /// Struct array of Widgets to transition to
        /// </summary>
        private WidgetGroup[] TargetGroups;

        /// <summary>
        /// Orientation boolean, lets the script know that it is Ladscape orientation, True by default
        /// </summary>
        public bool IsLandscape = true;

        /// <summary>
        /// Field for linking to a (child) SubscreenNavigation script
        /// </summary>
        [Header("SubScreen Script")]
        public SubscreenNavigation Subscreen;

        /// <summary>
        /// Runtime bool for tracking the activation status of the subscreen
        /// </summary>
        public bool SubscreenActive { get; set; }

        /// <summary>
        /// runtime bool for tracking if this widget is part of a subscreen
        /// </summary>
        public bool IsSubscreenGroup { get; set; }

        /// <summary>
        /// Runtime variable set to true if the WidgetGroup is iun the middle of a transition
        /// </summary>
        private bool IsTransitioning = false;

        /// <summary>
        /// Runtime bool for tracking if the widget group is transitioning in (true) or out (false)
        /// </summary>
        private bool IsTransitioningIn;

        /// <summary>
        /// Runtime float for length of time, in seconds, a transition happens in. 
        /// </summary>
        private float TransitionTime = 1f;

        /// <summary>
        /// Runtime float for storing the time a transition was initiated
        /// </summary>
        private float TransitionStartTime;

        /// <summary>
        /// Animation curve used during a transition
        /// </summary>
        private AnimationCurve TransitionCurve;

        /// <summary>
        /// Function to set up the starting values and orientation of the WidgetGroup and assign the default values to the struct arrays
        /// </summary>
        /// <param name="Orientation"></param>
        public void InitializeWidgetGroup(bool Orientation)
        {
            IsLandscape = Orientation;

            if (WidgetGroupsLandscape != null)
            {
                WidgetGroupsLandscape = StoreDefaultWidgetValues(WidgetGroupsLandscape);

                for (var i = 0; i < WidgetGroupsLandscape.Length; i++)
                {
                    if (!WidgetGroupsLandscape[i].WidgetGameObject.activeSelf)
                    {
                        WidgetGroupsLandscape[i].WidgetGameObject.SetActive(true);
                    }
                }
            }

            if (WidgetGroupsPortrait != null)
            {
                WidgetGroupsPortrait = StoreDefaultWidgetValues(WidgetGroupsPortrait);

                for (var i = 0; i < WidgetGroupsPortrait.Length; i++)
                {
                    if (!WidgetGroupsPortrait[i].WidgetGameObject.activeSelf)
                    {
                        WidgetGroupsPortrait[i].WidgetGameObject.SetActive(true);
                    }
                }
            }

            if (IsLandscape)
            {
                ActiveGroups = WidgetGroupsLandscape;
            }
            else
            {
                ActiveGroups = WidgetGroupsPortrait;
            }

            SetNonActiveGroupInactive();
        }

        /// <summary>
        /// Update is used only when IsTransitioning is true.
        /// </summary>
        public void Update()
        {
            if (IsTransitioning)
            {
                if (IsTransitioningIn)
                {
                    TransitionLerpIn();
                }
                else
                {
                    TransitionLerpOut();
                }
            }
        }

        /// <summary>
        /// Function to initiate interpolation between a widget array, and a target widget array
        /// </summary>
        public void BeginTransition(bool inOut, float time, AnimationCurve ac, WidgetGroup[] Targets)
        {
            IsTransitioning = true;
            IsTransitioningIn = inOut;
            TransitionTime = time;
            TransitionStartTime = Time.time;
            TransitionCurve = ac;

            TargetGroups = new WidgetGroup[ActiveGroups.Length];

            if (ActiveGroups.Length == Targets.Length)
            {
                for (var i = 0; i < ActiveGroups.Length; i++)
                {
                    TargetGroups[i] = Targets[i];
                }
            }
            else if (ActiveGroups.Length > Targets.Length)
            {
                for (var i = 0; i < ActiveGroups.Length; i++)
                {
                    if (i <= Targets.Length - 1)
                    {
                        TargetGroups[i] = Targets[i];
                    }
                    else
                    {
                        TargetGroups[i] = new WidgetGroup
                        {
                            DefaultPosition = Vector2.zero,
                            DefaultSize = Vector2.zero,
                            DefaultAnchorMax = new Vector2(.5f, .5f),
                            DefaultAnchorMin = new Vector2(.5f, .5f),
                            DefaultPivot = new Vector2(.5f, .5f),
                            DefaultRotation = Vector3.zero,
                            DefaultScale = Vector3.one
                        };
                    }
                }
            }
            else
            {
                for (var i = 0; i < ActiveGroups.Length; i++)
                {
                    TargetGroups[i] = Targets[i];
                }
            }

            for (var i = 0; i < ActiveGroups.Length; i++)
            {
                for (var i2 = 0; i2 < Targets.Length; i2++)
                {
                    if (ActiveGroups[i].WidgetName == Targets[i2].WidgetName)
                    {
                        TargetGroups[i] = Targets[i2];
                    }
                }
            }

            OverrideTargetByName(Targets);

            if (SubscreenActive)
            {
                Subscreen.WidgetGroups[Subscreen.GetScreenIndex()].BeginTransition(false, Subscreen.TransitionTime, Subscreen.GetAnimationCurve(), Targets);
            }

        }

        /// <summary>
        /// Function to overwrite a target widget based on Name
        /// </summary>
        /// <param name="Targets"></param>
        private void OverrideTargetByName(WidgetGroup[] Targets)
        {
            for (var i = 0; i < TargetGroups.Length; i++)
            {
                for (var i2 = 0; i2 < Targets.Length; i2++)
                {
                    if (ActiveGroups[i].WidgetName == Targets[i2].WidgetName)
                    {
                        TargetGroups[i] = Targets[i2];
                    }
                }
            }
        }

        /// <summary>
        /// Interoplates a widget group IN, Target to Widget
        /// </summary>
        private void TransitionLerpIn()
        {
            var PercentComplete = (Time.time - TransitionStartTime) / TransitionTime;
            var ItemsComplete = 0;

            for (var i = 0; i < ActiveGroups.Length; i++)
            {
                if (PercentComplete < 1f)
                {
                    if (!ActiveGroups[i].DoNotInterpolate)
                    {
                        if (ActiveGroups[i].DefaultPosition != TargetGroups[i].DefaultPosition)
                        {
                            ActiveGroups[i].RectTransform.anchoredPosition = Vector2.Lerp(TargetGroups[i].DefaultPosition, ActiveGroups[i].DefaultPosition, TransitionCurve.Evaluate(PercentComplete));
                        }

                        if (ActiveGroups[i].DefaultSize != TargetGroups[i].DefaultSize)
                        {
                            ActiveGroups[i].RectTransform.sizeDelta = Vector2.Lerp(TargetGroups[i].DefaultSize, ActiveGroups[i].DefaultSize, TransitionCurve.Evaluate(PercentComplete));
                        }

                        if (ActiveGroups[i].DefaultAnchorMax != TargetGroups[i].DefaultAnchorMax)
                        {
                            ActiveGroups[i].RectTransform.anchorMax = Vector2.Lerp(TargetGroups[i].DefaultAnchorMax, ActiveGroups[i].DefaultAnchorMax, TransitionCurve.Evaluate(PercentComplete));
                        }

                        if (ActiveGroups[i].DefaultAnchorMin != TargetGroups[i].DefaultAnchorMin)
                        {
                            ActiveGroups[i].RectTransform.anchorMin = Vector2.Lerp(TargetGroups[i].DefaultAnchorMin, ActiveGroups[i].DefaultAnchorMin, TransitionCurve.Evaluate(PercentComplete));
                        }

                        if (ActiveGroups[i].DefaultPivot != TargetGroups[i].DefaultPivot)
                        {
                            ActiveGroups[i].RectTransform.pivot = Vector2.Lerp(TargetGroups[i].DefaultPivot, ActiveGroups[i].DefaultPivot, TransitionCurve.Evaluate(PercentComplete));
                        }

                        if (ActiveGroups[i].DefaultRotation != TargetGroups[i].DefaultRotation)
                        {
                            ActiveGroups[i].RectTransform.localEulerAngles = Vector2.Lerp(TargetGroups[i].DefaultRotation, ActiveGroups[i].DefaultRotation, TransitionCurve.Evaluate(PercentComplete));
                        }

                        if (ActiveGroups[i].DefaultScale != TargetGroups[i].DefaultScale)
                        {
                            ActiveGroups[i].RectTransform.localScale = Vector2.Lerp(TargetGroups[i].DefaultScale, ActiveGroups[i].DefaultScale, TransitionCurve.Evaluate(PercentComplete));
                        }
                    }
                    else
                    {
                        SetGroupToDefault(i);
                    }

                    if (!ActiveGroups[i].IgnoreAlphaFade)
                    {
                        ActiveGroups[i].CanvasGroup.alpha = Mathf.Lerp(0, 1, TransitionCurve.Evaluate(PercentComplete));
                    }
                }
                else
                {
                    SetGroupToDefault(i);

                    ActiveGroups[i].CanvasGroup.alpha = 1;

                    ItemsComplete++;
                }
            }

            if (ItemsComplete == ActiveGroups.Length)
            {
                IsTransitioning = false;
                SetGroupInteractable();

                if (IsSubscreenGroup)
                {
                    transform.parent.GetComponent<SubscreenNavigation>().ScreenTransitionEnded();
                }
                else
                {
                    transform.parent.GetComponent<ScreenNavigation>().ScreenTransitionEnded();
                }
            }
        }

        /// <summary>
        /// Interoplates a widget group OUT, Widget to Target
        /// </summary>
        private void TransitionLerpOut()
        {
            var PercentComplete = (Time.time - TransitionStartTime) / TransitionTime;
            var ItemsComplete = 0;

            for (var i = 0; i < ActiveGroups.Length; i++)
            {
                if (PercentComplete < 1f)
                {
                    if (!ActiveGroups[i].DoNotInterpolate)
                    {
                        if (ActiveGroups[i].DefaultPosition != TargetGroups[i].DefaultPosition)
                        {
                            ActiveGroups[i].RectTransform.anchoredPosition = Vector2.Lerp(ActiveGroups[i].DefaultPosition, TargetGroups[i].DefaultPosition, TransitionCurve.Evaluate(PercentComplete));
                        }

                        if (ActiveGroups[i].DefaultSize != TargetGroups[i].DefaultSize)
                        {
                            ActiveGroups[i].RectTransform.sizeDelta = Vector2.Lerp(ActiveGroups[i].DefaultSize, TargetGroups[i].DefaultSize, TransitionCurve.Evaluate(PercentComplete));
                        }

                        if (ActiveGroups[i].DefaultAnchorMax != TargetGroups[i].DefaultAnchorMax)
                        {
                            ActiveGroups[i].RectTransform.anchorMax = Vector2.Lerp(ActiveGroups[i].DefaultAnchorMax, TargetGroups[i].DefaultAnchorMax, TransitionCurve.Evaluate(PercentComplete));
                        }

                        if (ActiveGroups[i].DefaultAnchorMin != TargetGroups[i].DefaultAnchorMin)
                        {
                            ActiveGroups[i].RectTransform.anchorMin = Vector2.Lerp(ActiveGroups[i].DefaultAnchorMin, TargetGroups[i].DefaultAnchorMin, TransitionCurve.Evaluate(PercentComplete));
                        }

                        if (ActiveGroups[i].DefaultPivot != TargetGroups[i].DefaultPivot)
                        {
                            ActiveGroups[i].RectTransform.pivot = Vector2.Lerp(ActiveGroups[i].DefaultPivot, TargetGroups[i].DefaultPivot, TransitionCurve.Evaluate(PercentComplete));
                        }

                        if (ActiveGroups[i].DefaultRotation != TargetGroups[i].DefaultRotation)
                        {
                            ActiveGroups[i].RectTransform.localEulerAngles = Vector2.Lerp(ActiveGroups[i].DefaultRotation, TargetGroups[i].DefaultRotation, TransitionCurve.Evaluate(PercentComplete));
                        }

                        if (ActiveGroups[i].DefaultScale != TargetGroups[i].DefaultScale)
                        {
                            ActiveGroups[i].RectTransform.localScale = Vector2.Lerp(ActiveGroups[i].DefaultScale, TargetGroups[i].DefaultScale, TransitionCurve.Evaluate(PercentComplete));
                        }
                    }
                    else
                    {
                        SetGroupToDefault(i);
                    }

                    if (!ActiveGroups[i].IgnoreAlphaFade)
                    {
                        ActiveGroups[i].CanvasGroup.alpha = Mathf.Lerp(1, 0, TransitionCurve.Evaluate(PercentComplete));
                    }
                }
                else
                {
                    SetGroupToDefault(i);

                    ActiveGroups[i].CanvasGroup.alpha = 0;

                    ItemsComplete++;
                }
            }
            if (ItemsComplete == ActiveGroups.Length)
            {
                IsTransitioning = false;

                if (SubscreenActive && Subscreen != null)
                {
                    SubscreenActive = false;
                    Subscreen.CloseSubscreen();
                }

                SetGroupInactive();
            }
        }

        /// <summary>
        /// Function to store the values of a widget's RectTransform in the default value variables
        /// </summary>
        public WidgetGroup[] StoreDefaultWidgetValues(WidgetGroup[] Widgets)
        {
            for (var i = 0; i < Widgets.Length; i++)
            {
                Widgets[i].CanvasGroup = Widgets[i].WidgetGameObject.GetComponent<CanvasGroup>();
                Widgets[i].RectTransform = Widgets[i].WidgetGameObject.GetComponent<RectTransform>();
                Widgets[i].DefaultPosition = Widgets[i].RectTransform.anchoredPosition;
                Widgets[i].DefaultSize = Widgets[i].RectTransform.sizeDelta;
                Widgets[i].DefaultAnchorMax = Widgets[i].RectTransform.anchorMax;
                Widgets[i].DefaultAnchorMin = Widgets[i].RectTransform.anchorMin;
                Widgets[i].DefaultPivot = Widgets[i].RectTransform.pivot;
                Widgets[i].DefaultRotation = Widgets[i].RectTransform.localEulerAngles;
                Widgets[i].DefaultScale = Widgets[i].RectTransform.localScale;
            }
            return Widgets;
        }

        /// <summary>
        /// function to set the values of the RectTransform in the active widget group to the stored defaults
        /// </summary>
        private void SetGroupToDefault(int i)
        {
            if (ActiveGroups[i].RectTransform.anchoredPosition != ActiveGroups[i].DefaultPosition)
            {
                ActiveGroups[i].RectTransform.anchoredPosition = ActiveGroups[i].DefaultPosition;
            }

            if (ActiveGroups[i].RectTransform.sizeDelta != ActiveGroups[i].DefaultSize)
            {
                ActiveGroups[i].RectTransform.sizeDelta = ActiveGroups[i].DefaultSize;
            }

            if (ActiveGroups[i].RectTransform.anchorMax != ActiveGroups[i].DefaultAnchorMax)
            {
                ActiveGroups[i].RectTransform.anchorMax = ActiveGroups[i].DefaultAnchorMax;
            }

            if (ActiveGroups[i].RectTransform.anchorMin != ActiveGroups[i].DefaultAnchorMin)
            {
                ActiveGroups[i].RectTransform.anchorMin = ActiveGroups[i].DefaultAnchorMin;
            }

            if (ActiveGroups[i].RectTransform.pivot != ActiveGroups[i].DefaultPivot)
            {
                ActiveGroups[i].RectTransform.pivot = ActiveGroups[i].DefaultPivot;
            }

            if (ActiveGroups[i].RectTransform.localEulerAngles != ActiveGroups[i].DefaultRotation)
            {
                ActiveGroups[i].RectTransform.localEulerAngles = ActiveGroups[i].DefaultRotation;
            }

            if (ActiveGroups[i].RectTransform.localScale != ActiveGroups[i].DefaultScale)
            {
                ActiveGroups[i].RectTransform.localScale = ActiveGroups[i].DefaultScale;
            }
        }

        /// <summary>
        /// Function to set internal ScreenOrientation variables, swaps active widget groups between landscape & portrait
        /// </summary>
        public void SetOrientation(bool Landscape)
        {
            if (WidgetGroupsLandscape != null && WidgetGroupsPortrait != null)
            {
                if (Landscape)
                {
                    IsLandscape = true;
                    ActiveGroups = WidgetGroupsLandscape;
                }
                else
                {
                    IsLandscape = false;
                    ActiveGroups = WidgetGroupsPortrait;
                }
            }
        }

        /// <summary>
        /// Function to set a WidgetGroup's CanvasGroup Raycasting & Interactable OFF. Alpha to 0. Also sets all inactive other widgets off as well
        /// </summary>
        public void SetGroupInactive()
        {
            for (var i = 0; i < ActiveGroups.Length; i++)
            {
                ActiveGroups[i].CanvasGroup.alpha = 0;
                ActiveGroups[i].CanvasGroup.interactable = false;
                ActiveGroups[i].CanvasGroup.blocksRaycasts = false;
            }

            SetNonActiveGroupInactive();
        }

        /// <summary>
        /// Function to set all widgetGroups except for the active one, to non interactable
        /// </summary>
        private void SetNonActiveGroupInactive()
        {
            if (WidgetGroupsLandscape != null && WidgetGroupsPortrait != null)
            {
                WidgetGroup[] InactiveWidgets;

                if (IsLandscape)
                {
                    InactiveWidgets = WidgetGroupsPortrait;
                }
                else
                {
                    InactiveWidgets = WidgetGroupsLandscape;
                }

                for (var i = 0; i < InactiveWidgets.Length; i++)
                {
                    InactiveWidgets[i].CanvasGroup.alpha = 0;
                    InactiveWidgets[i].CanvasGroup.interactable = false;
                    InactiveWidgets[i].CanvasGroup.blocksRaycasts = false;
                }
            }
        }

        /// <summary>
        /// Function to set the ActiveGroup's CanvasGroup Raycasting & Interactable ON
        /// </summary>
        private void SetGroupInteractable()
        {
            for (var i = 0; i < ActiveGroups.Length; i++)
            {
                if (ActiveGroups[i].RectTransform == null)
                {
                    Debug.LogWarning("NULL rect");
                }

                ActiveGroups[i].CanvasGroup.interactable = true;
                ActiveGroups[i].CanvasGroup.blocksRaycasts = true;
            }
        }
    }
}