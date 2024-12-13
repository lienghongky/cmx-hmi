using UnityEngine;

namespace HMI.UI.Skins.Data
{
    /// <summary>
    /// UI Skin for car shader data
    /// </summary>
    [CreateAssetMenu(fileName = "Car Shader Skin.asset", menuName = "HMI/Skins/Car Shader Skin", order = 1)]
    public class UISkinCarShaderData : UISkinDataBase
    {
        /// <summary>
        /// Base color of the car shader
        /// </summary>
        public Color NormalColor;

        /// <summary>
        /// Fresnel layer 1 color
        /// </summary>
        public Color Fresnel1Color;

        /// <summary>
        /// Fresnel layer 2 color
        /// </summary>
        public Color Fresnel2Color;

        public override void Interpolate(UISkinDataBase from, UISkinDataBase to, float u)
        {
            var fromCast = (UISkinCarShaderData)from;
            var toCast = (UISkinCarShaderData)to;

            NormalColor = Color.Lerp(fromCast.NormalColor, toCast.NormalColor, u);
            Fresnel1Color = Color.Lerp(fromCast.Fresnel1Color, toCast.Fresnel1Color, u);
            Fresnel2Color = Color.Lerp(fromCast.Fresnel2Color, toCast.Fresnel2Color, u);
        }
    }
}
