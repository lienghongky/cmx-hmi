using UnityEngine;

namespace HMI.UI.Skins.Data
{
    /// <summary>
    /// UI Skin sprite data
    /// </summary>
    [CreateAssetMenu(fileName = "Sprite Skin.asset", menuName = "HMI/Skins/Sprite Skin", order = 1)]
    public class UISkinSpriteData : UISkinDataBase
    {
        /// <summary>
        /// Color of the sprite
        /// </summary>
        public Color Color = Color.white;

        public override void Interpolate(UISkinDataBase from, UISkinDataBase to, float u)
        {
            var fromCast = (UISkinSpriteData)from;
            var toCast = (UISkinSpriteData)to;

            Color = Color.Lerp(fromCast.Color, toCast.Color, u);
        }
    }
}
