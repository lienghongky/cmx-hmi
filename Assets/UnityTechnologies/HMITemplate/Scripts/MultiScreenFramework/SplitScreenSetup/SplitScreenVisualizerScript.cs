using UnityEngine;

namespace MultiScreenFramework.SplitScreenSetup
{
    /// <summary>
    /// requires the SplitScreenSetupConfiguration script
    /// </summary>
    [RequireComponent(typeof(SplitScreenSetupConfiguration))]
    public class SplitScreenVisualizerScript : MonoBehaviour
    {
        /// <summary>
        /// The SplitScreenSetupConfiguration script
        /// </summary>
        public SplitScreenSetupConfiguration SplitScreenScript;
    }
}