using UnityEngine;

namespace HMI.UI.Skins.Data
{
    /// <summary>
    /// UI Skin gradient data
    /// </summary>
    [CreateAssetMenu(fileName = "Gradient Skin.asset", menuName = "HMI/Skins/Gradient Skin", order = 1)]
    public class UISkinGradientData : UISkinDataBase
    {
        /// <summary>
        /// Start color of the gradient   
        /// </summary>
        public Color StartColor = Color.white;

        /// <summary>
        /// End color of the gradient   
        /// </summary>
        public Color EndColor = Color.black;

        public override void Interpolate(UISkinDataBase from, UISkinDataBase to, float u)
        {
            var fromCast = (UISkinGradientData)from;
            var toCast = (UISkinGradientData)to;

            StartColor = Color.Lerp(fromCast.StartColor, toCast.StartColor, u);
            EndColor = Color.Lerp(fromCast.EndColor, toCast.EndColor, u);
        }
    }
}
