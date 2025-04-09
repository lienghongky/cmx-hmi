using System;
using UnityEngine;

namespace HMI.UI
{
    /// <summary>
    /// A pair containing a renderer and color
    /// </summary>
    [Serializable]
    public class SpriteRendererColorPair
    {
        /// <summary>
        /// Renderer that will have the color assigned
        /// </summary>
        public SpriteRenderer Renderer;

        /// <summary>
        /// Color for the renderer
        /// </summary>
        public Color Color;
    }
}
