using UnityEngine;
using Util;

namespace HMI.UI.Skins.Data
{
    /// <summary>
    /// UI Skin graph data
    /// </summary>
    [CreateAssetMenu(fileName = "Graph Skin.asset", menuName = "HMI/Skins/Graph Skin", order = 1)]
    public class UISkinGraphData : UISkinDataBase
    {
        /// <summary>
        /// Color for the big axis lines
        /// </summary>
        public Color HorizontalAxisBigLineColor;

        /// <summary>
        /// Color for the small axis lines
        /// </summary>
        public Color HorizontalAxisSmallLineColor;

        /// <summary>
        /// Color for the axis labels
        /// </summary>
        public Color LabelTextColor;

        /// <summary>
        /// Color for the bottom axis line
        /// </summary>
        public Color AxisBottomLineColor;
                
        /// <summary>
        /// Color for the current energy usage background
        /// </summary>
        public Color CurrentLevelBackgroundColor;

        /// <summary>
        /// Color for the current energy usage label
        /// </summary>
        public Color CurrentLevelTextColor;

        /// <summary>
        /// Gradient used to color the dots on the line
        /// </summary>
        public Gradient DotGradient;

        /// <summary>
        /// Gradient uses to color the graph line
        /// </summary>
        public Gradient LineGradient;

        /// <summary>
        /// Gradient used to color the graph shadow line
        /// </summary>
        public Gradient ShadowLineGradient;

        public override void Interpolate(UISkinDataBase from, UISkinDataBase to, float u)
        {
            var fromCast = (UISkinGraphData)from;
            var toCast = (UISkinGraphData)to;
            HorizontalAxisBigLineColor = Color.Lerp(fromCast.HorizontalAxisBigLineColor, toCast.HorizontalAxisBigLineColor, u);
            HorizontalAxisSmallLineColor = Color.Lerp(fromCast.HorizontalAxisSmallLineColor, toCast.HorizontalAxisSmallLineColor, u);
            LabelTextColor = Color.Lerp(fromCast.LabelTextColor, toCast.LabelTextColor, u);
            AxisBottomLineColor = Color.Lerp(fromCast.AxisBottomLineColor, toCast.AxisBottomLineColor, u);
            CurrentLevelBackgroundColor = Color.Lerp(fromCast.CurrentLevelBackgroundColor, toCast.CurrentLevelBackgroundColor, u);
            CurrentLevelTextColor = Color.Lerp(fromCast.CurrentLevelTextColor, toCast.CurrentLevelTextColor, u);
            DotGradient.Lerp(fromCast.DotGradient, toCast.DotGradient, u);
            LineGradient.Lerp(fromCast.LineGradient, toCast.LineGradient, u);
            ShadowLineGradient.Lerp(fromCast.ShadowLineGradient, toCast.ShadowLineGradient, u);
        }
    }
}
