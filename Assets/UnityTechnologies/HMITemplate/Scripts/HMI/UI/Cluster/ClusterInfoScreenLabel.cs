using TMPro;
using UnityEngine;

namespace HMI.UI.Cluster
{
    /// <summary>
    /// This label displays what info screen the info screen on the cluster is currently showing
    /// </summary>
    [RequireComponent(typeof(TextMeshPro))]
    public class ClusterInfoScreenLabel : MonoBehaviour
    {
        /// <summary>
        /// Data source for the info screens
        /// </summary>
        public ClusterInfoController ScreenDataSource;

        /// <summary>
        /// Label that shows the currently active info screen
        /// </summary>
        private TextMeshPro Label;

        /// <summary>
        /// Unity Awake callback
        /// </summary>
        private void Awake()
        {
            Label = GetComponent<TextMeshPro>();
        }

        /// <summary>
        /// Unity Update callback
        /// </summary>
        private void Update()
        {
            Label.text = ScreenDataSource?.ActiveScreen?.ScreenName;
        }
    }
}
