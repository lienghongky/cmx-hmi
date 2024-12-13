using UnityEngine;

namespace HMI.UI.Skins.Data
{
    /// <summary>
    /// UI Skin button data
    /// </summary>
    [CreateAssetMenu(fileName = "Button Skin.asset", menuName = "HMI/Skins/Button Skin", order = 1)]
    public class UISkinButtonData : UISkinDataBase
    {
        /// <summary>
        /// Normal button color
        /// </summary>
        public Color NormalColor;

        /// <summary>
        /// Highlighted button color
        /// </summary>
        public Color HighlightedColor;

        /// <summary>
        /// Pressed button color
        /// </summary>
        public Color PressedColor;

        /// <summary>
        /// Selected button color
        /// </summary>
        public Color SelectedColor;

        /// <summary>
        /// Disabled button color
        /// </summary>
        public Color DisabledColor;

        public override void Interpolate(UISkinDataBase from, UISkinDataBase to, float u)
        {
            var fromCast = (UISkinButtonData)from;
            var toCast = (UISkinButtonData)to;

            NormalColor = Color.Lerp(fromCast.NormalColor, toCast.NormalColor, u);
            HighlightedColor = Color.Lerp(fromCast.HighlightedColor, toCast.HighlightedColor, u);
            PressedColor = Color.Lerp(fromCast.PressedColor, toCast.PressedColor, u);
            SelectedColor = Color.Lerp(fromCast.SelectedColor, toCast.SelectedColor, u);
            DisabledColor = Color.Lerp(fromCast.DisabledColor, toCast.DisabledColor, u);
        }
    }
}
