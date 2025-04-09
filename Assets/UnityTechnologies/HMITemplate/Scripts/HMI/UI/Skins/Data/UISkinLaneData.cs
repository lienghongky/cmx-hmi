using UnityEngine;

namespace HMI.UI.Skins.Data
{
    /// <summary>
    /// Skin data for the lane of the center of the cluster
    /// </summary>
    [CreateAssetMenu(fileName = "Lane Skin.asset", menuName = "HMI/Skins/Lane Skin", order = 1)]
    public class UISkinLaneData : UISkinDataBase
    {
        /// <summary>
        /// Color for the speed line
        /// </summary>
        public Color SpeedLineColor;

        /// <summary>
        /// Color for the lane lines
        /// </summary>
        public Color LaneLineColor;

        /// <summary>
        /// Color for the dot grid
        /// </summary>
        public Color DotGridColor;

        public override void Interpolate(UISkinDataBase from, UISkinDataBase to, float u)
        {
            var fromCast = (UISkinLaneData)from;
            var toCast = (UISkinLaneData)to;

            SpeedLineColor = Color.Lerp(fromCast.SpeedLineColor, toCast.SpeedLineColor, u);
            LaneLineColor = Color.Lerp(fromCast.LaneLineColor, toCast.LaneLineColor, u);
            DotGridColor = Color.Lerp(fromCast.DotGridColor, toCast.DotGridColor, u);
        }
    }
}
