using UnityEngine;

namespace Util
{
    /// <summary>
    /// Adds an audiolistener if none is active across all scenes
    /// This prevents duplicate audiolisteners
    /// </summary>
    public class AutoAudioListener : MonoBehaviour
    {
        /// <summary>
        /// Unity Awake callback
        /// </summary>
        private void Awake()
        {
            var audioListener = FindObjectsOfType<AudioListener>();
            if (audioListener == null || audioListener.Length == 0)
            {
                gameObject.AddComponent<AudioListener>();
            }
        }
    }
}
