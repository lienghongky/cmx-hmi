using HMI.Units.Data;
using HMI.Vehicles.Behaviours.Base;
using HMI.Vehicles.Services;
using TMPro;
using UnityEngine;
using Util;

namespace HMI.UI.Cluster
{
    /// <summary>
    /// Info element that displays battery level in text
    /// </summary>
    public class ClusterRangeText : MonoBehaviour
    {
        /// <summary>
        /// Configure what range type to display
        /// </summary>
        public enum RangeType
        {
            RemainingRange,
            MaximumRange
        }

        /// <summary>
        /// Used for unit configuration of the text
        /// </summary>
        public UnitConfiguration UnitConfiguration;

        /// <summary>
        /// Leading text of the cluster range
        /// </summary>
        public string LeadingText;

        /// <summary>  
        /// Battery that this progress bar is attached to  
        /// </summary>
        private BatteryBase BatteryDataSource;

        /// <summary>
        /// Text with range
        /// </summary>
        public TMP_Text Text;

        /// <summary>
        /// Type of range to display
        /// </summary>
        public RangeType RangeDisplayType = RangeType.RemainingRange;

        /// <summary>
        /// Unity Awake callback
        /// </summary>
        private void Awake()
        {
            BatteryDataSource = VehicleService.GetBattery();
        }

        /// <summary>
        /// Unity Update callback
        /// </summary>
        private void Update()
        {
            var range = RangeDisplayType == RangeType.RemainingRange ? BatteryDataSource.RemainingRange : BatteryDataSource.MaxRange;
            var txtColorSmall = ColorUtility.ToHtmlStringRGB(Text.color);
            txtColorSmall += "AA";

            if (UnitConfiguration.UnitOfLength == Clusters.Enums.UnitOfLengthType.Miles)
            {
                range = UnitConversion.KilometersToMiles(range);
                Text.text = $@"{LeadingText} {range:F0}<font=""DinLight_Glow"" material=""DinLight_NoGlow""><size=60%><#{txtColorSmall}>mi</size></font>";
            }
            else
            {
                Text.text = $@"{LeadingText} {range:F0}<font=""DinLight_Glow"" material=""DinLight_NoGlow""><size=60%><#{txtColorSmall}>km</size></font>";
            }
        }
    }
}
