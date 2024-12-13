using UnityEngine;

namespace HMI.UI
{
    /// <summary>
    /// Sets the object position based the screen orientation
    /// </summary>
    public class MatchPositionToScreenOrientation : MonoBehaviour
    {
        /// <summary>
        /// Position of the object if the screen has landscape dimensions
        /// </summary>
        public Vector3 LandscapePosition;

        /// <summary>
        /// Position of the object if the screen has portrait dimensions
        /// </summary>
        public Vector3 PortraitPosition;

        /// <summary>
        /// Unity Update callback
        /// </summary>
        private void Update()
        {
            var currentScreenSize = new Vector2(Screen.width, Screen.height);

            if (currentScreenSize.x > currentScreenSize.y)
            {
                ToLandscape();
            }
            else
            {
                ToPortrait();
            }
        }

        /// <summary>
        /// Switch object to landscape position
        /// </summary>
        private void ToLandscape()
        {
            transform.localPosition = LandscapePosition;
        }

        /// <summary>
        /// Switch object to portrait position
        /// </summary>
        private void ToPortrait()
        {
            transform.localPosition = PortraitPosition;
        }
    }
}
