using UnityEngine;

namespace HMI.UI.Skins.Data
{
    /// <summary>
    /// UI Skin text data
    /// </summary>
    [CreateAssetMenu(fileName = "Text Skin.asset", menuName = "HMI/Skins/Text Skin", order = 1)]
    public class UISkinTextData : UISkinDataBase
    {
        /// <summary>
        /// Text color for texts
        /// </summary>
        [Tooltip("Text color")]
        public Color TextColor;

        /// <summary>
        /// Material for texts
        /// </summary>
        [Tooltip("Material for texts")]
        public Material Material;

        public override void Interpolate(UISkinDataBase from, UISkinDataBase to, float u)
        {
            var fromCast = (UISkinTextData)from;
            var toCast = (UISkinTextData)to;

            TextColor = Color.Lerp(fromCast.TextColor, toCast.TextColor, u);
        }
    }
}
