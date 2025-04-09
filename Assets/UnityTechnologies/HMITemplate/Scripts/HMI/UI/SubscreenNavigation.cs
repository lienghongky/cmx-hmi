using UnityEngine;

namespace HMI.IVI.UI
{
    /// <summary>
    /// This class is an extended version of the ScreenNavigation class. It is inteded to be used for screens/popups within screens.
    /// </summary>
    [RequireComponent(typeof(CanvasGroup))]
    public class SubscreenNavigation : ScreenNavigation
    {
        /// <summary>
        /// Canvasgroup for the Subscreen gameobject
        /// </summary>
        private CanvasGroup SubscreenCanvasGroup;

        /// <summary>
        /// HMI_WidgetGroup of the parent gameobject. 
        /// </summary>
        private HMI_WidgetGroup ParentScreenGroup;

        /// <summary>
        /// Gameobject for the Desired Screen when transitioning between subscreens
        /// </summary>
        private GameObject DesiredSubscreen;

        /// <summary>
        /// Awake assigns the canvas group component on the subscreen gameobjet, and links to the parent game object's HMI_WidgetGroup script
        /// </summary>
        private void Awake()
        {
            SubscreenCanvasGroup = GetComponent<CanvasGroup>();

            SubscreenCanvasGroup.alpha = 0;
            SubscreenCanvasGroup.interactable = false;

            ParentScreenGroup = GetComponentInParent<HMI_WidgetGroup>();
        }

        /// <summary>
        /// Function to transition to the next subscreen. Index Wraps.
        /// </summary>
        public void GoToNextSubscreen()
        {
            if (GetScreenIndex() < MyScreensList.Count - 1)
            {
                DesiredSubscreen = MyScreensList[GetScreenIndex() + 1].Screen;
            }
            else
            {
                DesiredSubscreen = MyScreensList[0].Screen;
            }

            NavigateToSpecificScreen(DesiredSubscreen);
        }

        /// <summary>
        /// Function to transition to the previous subscreen. Index Wraps.
        /// </summary>
        public void GoToPreviousSubscreen()
        {
            if (GetScreenIndex() == 0)
            {
                DesiredSubscreen = MyScreensList[MyScreensList.Count - 1].Screen;
            }
            else
            {
                DesiredSubscreen = MyScreensList[GetScreenIndex() - 1].Screen;
            }

            NavigateToSpecificScreen(DesiredSubscreen);
        }

        /// <summary>
        /// Function to transition to a specified subscreen
        /// </summary>
        /// <param name="Screen"></param>
        public void GoToSpecificSubscreen(GameObject Screen)
        {
            if (Screen != null)
            {
                DesiredSubscreen = Screen;

                NavigateToSpecificScreen(DesiredSubscreen);
            }
        }

        /// <summary>
        /// Function to Open the subscreen on the desired screen
        /// </summary>
        public void OpenSubscreen(GameObject Screen)
        {
            for (var i = 0; i < WidgetGroups.Length; i++)
            {
                WidgetGroups[i].IsSubscreenGroup = true;
            }

            SubscreenCanvasGroup.alpha = 1;
            SubscreenCanvasGroup.interactable = true;
            SubscreenCanvasGroup.blocksRaycasts = true;

            GoToSpecificSubscreen(Screen);

            ParentScreenGroup.SubscreenActive = true;
            ParentScreenGroup.SetGroupInactive();
        }

        /// <summary>
        /// Function to close the subscreen. used when changing main screens
        /// </summary>
        public void CloseSubscreen()
        {
            SubscreenCanvasGroup.alpha = 0;
            SubscreenCanvasGroup.interactable = false;
            SubscreenCanvasGroup.blocksRaycasts = false;

            for (var i = 0; i < WidgetGroups.Length; i++)
            {
                WidgetGroups[i].SetGroupInactive();
            }

            ParentScreenGroup.SubscreenActive = false;
        }

        /// <summary>
        /// Function to close the subscreen when returning to the parent screen
        /// </summary>
        /// <param name="ParentScreen"></param>
        public void ReturnToParentScreen(GameObject ParentScreen)
        {
            ParentScreen.GetComponentInParent<ScreenNavigation>().ScreenStartTransition();

            WidgetGroups[GetScreenIndex()].BeginTransition(false, TransitionTime, GetAnimationCurve(), ParentScreen.GetComponentInParent<ScreenNavigation>().WidgetGroups[ParentScreen.GetComponentInParent<ScreenNavigation>().GetScreenIndex()].ActiveGroups);
        }
    }
}

