using HMI.Vehicles.Behaviours.Base;
using HMI.Vehicles.Services;
using TMPro;
using UnityEngine;

namespace HMI.UI.Cluster
{
    /// <summary>
    /// Info element that displays battery level in text
    /// </summary>
    public class ClusterBatteryLevelText : MonoBehaviour
    {
        /// <summary>  
        /// Battery that this progress bar is attached to  
        /// </summary>
        private BatteryBase BatteryDataSource;

        /// <summary>
        /// Text with fill percentage
        /// </summary>
        public TMP_Text Text;

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
            var txtColorSmall = ColorUtility.ToHtmlStringRGB(Text.color);
            txtColorSmall += "AA";

            var batteryLevel = Mathf.Clamp01(BatteryDataSource.CurrentRelativeBatteryLevel) * 100;
            Text.text = $@"{batteryLevel:F0}<font=""DinLight_Glow"" material=""DinLight_NoGlow""><size=60%><#{txtColorSmall}>%</size></font>";
        }
    }
}
