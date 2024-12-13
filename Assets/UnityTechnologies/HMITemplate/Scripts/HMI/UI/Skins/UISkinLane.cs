using HMI.UI.Cluster.Car;
using HMI.UI.Skins.Data;
using UnityEngine;

namespace HMI.UI.Skins
{
    /// <summary>
    /// UI Skin for a lane
    /// </summary>
    [RequireComponent(typeof(Lane))]
    public class UISkinLane : UISkinBase
    {
        /// <summary>
        /// The skin data for the text
        /// </summary>
        public UISkinLaneData SkinData;

        public override UISkinDataBase GetSkinData()
        {
            return SkinData;
        }

        public override bool SetSkinData(UISkinDataBase data)
        {
            var uiSkinLaneData = data as UISkinLaneData;

            if (uiSkinLaneData == null)
            {
                Debug.LogWarning("skin data is of wrong type, expected UISkinLaneData");
                return false;
            }

            SkinData = uiSkinLaneData;
            ApplySkin();
            return true;
        }

        public override void ApplySkin()
        {
            if (SkinData == null)
            {
                return;
            }

            var lane = GetComponent<Lane>();

            lane.DotGridColor = SkinData.DotGridColor;
            lane.LaneLineColor = SkinData.LaneLineColor;
            lane.SpeedLineColor = SkinData.SpeedLineColor;
        }
    }
}
