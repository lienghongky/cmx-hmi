using UnityEngine;
using UnityEngine.EventSystems;

namespace Util
{
    /// <summary>
    /// Adds an eventsystem if none is active across all scenes
    /// This prevents duplicate eventsystem
    /// </summary>
    public class AutoEventSystem : MonoBehaviour
    {
        public GameObject EventSystemPrefab;

        /// <summary>
        /// Unity Awake callback
        /// </summary>
        private void Awake()
        {
            var eventsystem = FindObjectsOfType<EventSystem>();
            if (eventsystem == null || eventsystem.Length == 0)
            {
                Instantiate(EventSystemPrefab);
            }

            Destroy(gameObject);
        }
    }
}
