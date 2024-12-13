using UnityEngine;

/// <summary>
/// Allows the user to close the application by pressing escape
/// </summary>

namespace HMI.Utilities
{
    public class EscapeToQuitApplication : MonoBehaviour
    {
        /// <summary>
        /// Unity Update callback
        /// </summary>
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }

}