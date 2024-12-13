using HMI.Clusters.UI.Graph;
using HMI.UI.Skins.Data;
using UnityEngine;

namespace HMI.UI.Skins
{
    /// <summary>
    /// UI skin for the info graph
    /// </summary>
    [RequireComponent(typeof(ClusterGraph))]
    public class UISkinGraph : UISkinBase
    {
        /// <summary>
        /// The skin data for the text
        /// </summary>
        public UISkinGraphData SkinData;

        public override UISkinDataBase GetSkinData()
        {
            return SkinData;
        }

        public override bool SetSkinData(UISkinDataBase data)
        {
            var uiSkinGraphData = data as UISkinGraphData;

            if (uiSkinGraphData == null)
            {
                Debug.LogWarning("skin data is of wrong type, expected UISkinGraphData");
                return false;
            }

            SkinData = uiSkinGraphData;
            ApplySkin();
            return true;
        }

        public override void ApplySkin()
        {
            if (SkinData == null)
            {
                return;
            }

            var graph = GetComponent<ClusterGraph>();

            graph.HorizontalAxisBigLineColor = SkinData.HorizontalAxisBigLineColor;
            graph.HorizontalAxisSmallLineColor = SkinData.HorizontalAxisSmallLineColor;
            graph.LabelColor = SkinData.LabelTextColor;
            graph.AxisBottomLineColor = SkinData.AxisBottomLineColor;
            graph.ColorEnergyUsageBackgroundColor = SkinData.CurrentLevelBackgroundColor;
            graph.ColorEnergyUsageLabelColor = SkinData.CurrentLevelTextColor;
            graph.DotGradient = SkinData.DotGradient;
            graph.LineGradient = SkinData.LineGradient;
            graph.ShadowLineGradient = SkinData.ShadowLineGradient;
        }
    }
}