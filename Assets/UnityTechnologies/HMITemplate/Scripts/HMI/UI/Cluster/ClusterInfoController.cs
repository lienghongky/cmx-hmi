using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HMI.UI.Cluster
{
    /// <summary>
    /// The cluster contains info screens on the right side
    /// This scripts controls what information screen is shown
    /// </summary>
    public class ClusterInfoController : MonoBehaviour
    {
        /// <summary>
        /// Current active information screen
        /// </summary>
        public ClusterInfoScreenController ActiveScreen;

        /// <summary>
        /// All information screens that are controlled by this controller
        /// </summary>
        public List<ClusterInfoScreenController> InfoScreens;

        /// <summary>
        /// How much delay is applied before the next screen fades in
        /// after the last one has faded out
        /// </summary>
        private const float FadeFromToDelay = 0.35f;

        /// <summary>
        /// Unity Start delegate
        /// </summary>
        private void Start()
        {
            if (InfoScreens == null || InfoScreens.Count == 0)
            {
                return;
            }

            if (ActiveScreen == null)
            {
                ActiveScreen = InfoScreens[0];
            }

            foreach (var screen in InfoScreens)
            {
                screen.Hide();
            }
        }

        /// <summary>
        /// Show the active screen
        /// </summary>
        public void ShowActiveScreen()
        {
            if (ActiveScreen != null)
            {
                ActiveScreen.ShowFromLeft();
            }
        }

        /// <summary>
        /// Show the next cluster info screen
        /// </summar
        public void Next()
        {
            _ = StartCoroutine(NextFunc());
        }

        /// <summary>
        /// Show the previous cluster info screen
        /// </summary>
        public void Previous()
        {
            _ = StartCoroutine(PreviousFunc());
        }

        /// <summary>
        /// Show the next cluster info screen
        /// </summary>
        private IEnumerator NextFunc()
        {
            var activeScreenIndex = InfoScreens.IndexOf(ActiveScreen);
            activeScreenIndex++;

            if (activeScreenIndex >= InfoScreens.Count)
            {
                activeScreenIndex = 0;
            }

            var oldScreen = ActiveScreen;
            ActiveScreen = InfoScreens[activeScreenIndex];
            oldScreen.HideToRight();
            triggerCamera();
            

            yield return new WaitForSeconds(FadeFromToDelay);

            ActiveScreen.ShowFromLeft();
        }
        

        /// <summary>
        /// Show the previous cluster info screen
        /// </summary>
        private IEnumerator PreviousFunc()
        {
            var activeScreenIndex = InfoScreens.IndexOf(ActiveScreen);
            activeScreenIndex--;

            if (activeScreenIndex < 0)
            {
                activeScreenIndex = InfoScreens.Count - 1;
            }

            var oldScreen = ActiveScreen;
            ActiveScreen = InfoScreens[activeScreenIndex];
            oldScreen.HideToLeft();
            triggerCamera();

            yield return new WaitForSeconds(FadeFromToDelay);

            ActiveScreen.ShowFromRight();
        }
        void triggerCamera(){
            
                if (ActiveScreen.VisualsContainer != null)
                {
                
                    try{
                        //  find a nested child in the visual container
                        Transform nestedChild = ActiveScreen.VisualsContainer.transform.GetChild(0).GetChild(0);
                        Debug.Log(nestedChild);
                        if (nestedChild != null)
                        {
                            nestedChild.GetComponent<CameraScript>().AskCameraPermissionAndInitialize();
                        }
                    }catch{
                        Debug.Log("No nested child found");
                    }
                }
        }
    }
    
}
