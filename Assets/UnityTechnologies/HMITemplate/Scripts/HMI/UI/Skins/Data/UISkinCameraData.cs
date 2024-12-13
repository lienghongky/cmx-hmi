using UnityEngine;

namespace HMI.UI.Skins.Data
{
    /// <summary>
    /// Skin for a camera
    /// </summary>
    [CreateAssetMenu(fileName = "Camera Skin.asset", menuName = "HMI/Skins/Camera Skin", order = 1)]
    public class UISkinCameraData : UISkinDataBase
    {
        /// <summary>
        /// Background color for the camera
        /// </summary>
        public Color BackgroundColor;

        public override void Interpolate(UISkinDataBase from, UISkinDataBase to, float u)
        {
            var fromCast = (UISkinCameraData)from;
            var toCast = (UISkinCameraData)to;

            BackgroundColor = Color.Lerp(fromCast.BackgroundColor, toCast.BackgroundColor, u);
        }
    }
}
