using UnityEngine;
using UnityEngine.Playables;
using Util;

namespace HMI.UI.Cluster
{
    /// <summary>
    /// A controller for single screen of the cluster info panel (right side of the cluster)
    /// Used to show/hide this information screen
    /// </summary>
    [RequireComponent(typeof(PlayableDirector))]
    public class ClusterInfoScreenController : MonoBehaviour
    {
        /// <summary>
        /// The screen name
        /// </summary>
        public string ScreenName;

        /// <summary>
        /// Animation that shows the info screen, coming in from the left
        /// </summary>
        public PlayableAsset ShowFromLeftAnimation;

        /// <summary>
        /// Animation that hides the info screen, moving away to the left
        /// </summary>
        public PlayableAsset HideToLeftAnimation;

        /// <summary>
        /// Animation that shows the info screen, coming in from the right
        /// </summary>
        public PlayableAsset ShowFromRightAnimation;

        /// <summary>
        /// Animation that hides the info screen, moving away to the right
        /// </summary>
        public PlayableAsset HideToRightAnimation;

        /// <summary>
        /// The gameobject that contains all the visual elements of this screen
        /// </summary>
        public GameObject VisualsContainer;

        /// <summary>
        /// The director that controls this info screen
        /// </summary>
        private PlayableDirector Director;

        /// <summary>
        /// Unity Awake delegate
        /// </summary>
        private void Awake()
        {
            VisualsContainer.GetComponent<LayerChanger>().SetLayer("Invisible");
            Director = GetComponent<PlayableDirector>();
        }

        /// <summary>
        /// Instantly hides the infoscreen
        /// </summary>
        public void Hide()
        {
            VisualsContainer.GetComponent<LayerChanger>().SetLayer("Invisible");
        }

        /// <summary>
        /// Show the cluster info element, moving it from the left
        /// </summary>
        public void ShowFromLeft()
        {
            Director.Play(ShowFromLeftAnimation);
        }

        /// <summary>
        /// Show the cluster info element, moving it from the right
        /// </summary>
        public void ShowFromRight()
        {
            Director.Play(ShowFromRightAnimation);
        }

        /// <summary>
        /// Hide the cluster info element, moving it to the left
        /// </summary>
        public void HideToLeft()
        {
            Director.Play(HideToLeftAnimation);
        }

        /// <summary>
        /// Hide the cluster info element, moving it to the right
        /// </summary>
        public void HideToRight()
        {
            Director.Play(HideToRightAnimation);
        }
    }
}
