using UnityEngine;

namespace Util
{
    /// <summary>
    /// Extensions for the UI RectTransform
    /// </summary>
    public static class RectTransformExtensions
    {
        /// <summary>
        /// Adjust the size of the rect transform
        /// </summary>
        public static void SetSize(this RectTransform me, Vector2 size)
        {
            var oldSize = me.rect.size;
            var deltaSize = size - oldSize;
            me.offsetMin -= new Vector2(deltaSize.x * me.pivot.x, deltaSize.y * me.pivot.y);
            me.offsetMax += new Vector2(deltaSize.x * (1f - me.pivot.x), deltaSize.y * (1f - me.pivot.y));
        }

        /// <summary>
        /// Adjust the width of the rect transform
        /// </summary>
        public static void SetWidth(this RectTransform me, float size)
        {
            SetSize(me, new Vector2(size, me.rect.size.y));
        }

        /// <summary>
        /// Adjust the height of the rect transform
        /// </summary>
        public static void SetHeight(this RectTransform me, float size)
        {
            SetSize(me, new Vector2(me.rect.size.x, size));
        }
    }
}
