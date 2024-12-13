using UnityEngine;

namespace HMI.UI
{
    /// <summary>
    /// Controls the alpha of all child sprites
    /// </summary>
    public class SpriteGroupAlpha : MonoBehaviour
    {
        /// <summary>
        /// Child sprite renderers
        /// </summary>
        private SpriteRenderer[] SpriteRenderers;

        /// <summary>
        /// Alpha for every child sprite renderer
        /// </summary>
        public float Alpha = 1f;

        /// <summary>
        /// Unity Awake callback
        /// </summary>
        private void Awake()
        {
            SpriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        }

        /// <summary>
        /// Unity Update callback
        /// </summary>
        private void Update()
        {
            foreach (var renderer in SpriteRenderers)
            {
                var c = renderer.color;
                c.a = Alpha;
                renderer.color = c;
            }
        }
    }
}
