using System.Collections.Generic;
using UnityEngine;

namespace HMI.UI
{
    /// <summary>
    /// Controls the colors and alpha of all child sprites
    /// </summary>
    public class SpriteGroupColorAlpha : MonoBehaviour
    {
        /// <summary>
        /// Colors for all child sprite renderers
        /// </summary>
        public List<SpriteRendererColorPair> SpriteColorPairs = new List<SpriteRendererColorPair>();

        /// <summary>
        /// Alpha multiplier for every child sprite renderer
        /// </summary>
        public float Alpha = 1f;

        /// <summary>
        /// Unity Update callback
        /// </summary>
        private void Update()
        {
            foreach (var pair in SpriteColorPairs)
            {
                var c = pair.Color;
                c.a *= Alpha;
                pair.Renderer.color = c;
            }
        }
    }
}
