using HMI.Units.Data;
using HMI.Vehicles.Data;
using TMPro;
using UnityEngine;
using Util;

namespace HMI.UI.Cluster.Adas
{
    /// <summary>
    /// Visualization of the adas system on the cluster
    /// </summary>
    public class AdasVisualization : MonoBehaviour
    {
        /// <summary>
        /// Used to determine if labels should be rendered in miles or km/h
        /// </summary>
        public UnitConfiguration UnitConfiguration;

        /// <summary>
        /// Speed control data source
        /// </summary>
        public AdasSpeedControlData DataSource;

        /// <summary>
        /// On symbol
        /// </summary>
        public GameObject AdasOn;

        /// <summary>
        /// Off symbol
        /// </summary>
        public GameObject AdasOff;

        /// <summary>
        /// On text
        /// </summary>
        public TMP_Text AdasTextOn;

        /// <summary>
        /// Off text
        /// </summary>
        public TMP_Text AdasTextOff;

        /// <summary>
        /// On text
        /// </summary>
        public TMP_Text AdasTextUnitOn;

        /// <summary>
        /// Off text
        /// </summary>
        public TMP_Text AdasTextUnitOff;

        /// <summary>
        /// Unity Update callback
        /// </summary>
        public void LateUpdate()
        {
            UpdateGoalSpeedText();
            AdasOn.SetActive(DataSource.IsSpeedControlActive);
            AdasOff.SetActive(!DataSource.IsSpeedControlActive);
        }

        /// <summary>
        /// Update the goal speed text
        /// </summary>
        private void UpdateGoalSpeedText()
        {
            var speed = DataSource.GoalSpeed;

            if (UnitConfiguration.UnitOfLength == Clusters.Enums.UnitOfLengthType.Miles)
            {
                speed = UnitConversion.KilometersToMiles(speed);
                AdasTextUnitOn.text = "MPH";
                AdasTextUnitOff.text = "MPH";
            }
            else
            {
                AdasTextUnitOn.text = "KMH";
                AdasTextUnitOff.text = "KMH";
            }

            var speedText = speed.ToString("F0");
            AdasTextOn.text = speedText;
            AdasTextOff.text = speedText;
        }
    }
}
