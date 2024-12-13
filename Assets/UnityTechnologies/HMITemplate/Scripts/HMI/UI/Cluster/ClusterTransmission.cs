using HMI.Vehicles.Behaviours.Base;
using HMI.Vehicles.Services;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace HMI.UI.Cluster
{
    /// <summary>
    /// Visualizes the gearbox of the vehicle
    /// </summary>
    public class ClusterTransmission : MonoBehaviour
    {
        /// <summary>
        /// The large text representation of the current gear
        /// </summary>
        public TextMeshPro BigText;

        /// <summary>
        /// The small text representation of other gears supported by the vehicle
        /// </summary>
        public List<TextMeshPro> SmallTexts = new List<TextMeshPro>();

        /// <summary>
        /// Source of information
        /// </summary>
        private TransmissionBase TransmissionDataSource;

        /// <summary>
        /// Current Gear shown as big on cluster transmission
        /// </summary>
        private string CurrentBigGear;

        /// <summary>
        /// Unity Awake callback
        /// </summary>
        private void Awake()
        {
            TransmissionDataSource = VehicleService.GetTransmission();
        }

        /// <summary>
        /// Unity Start callback
        /// </summary>
        private void Start()
        {
            CurrentBigGear = TransmissionDataSource.CurrentGear.Gear;
            BigText.text = CurrentBigGear;
            var supportedGears = TransmissionDataSource.GearNames;

            // the number of texts on the cluster needs to match the gears supported by the gearbox
            if (supportedGears.Count != SmallTexts.Count + 1)
            {
                throw new InvalidOperationException("No matching number of texts setup for transmission system");
            }

            AssignGears(CurrentBigGear, supportedGears);
        }

        /// <summary>
        /// Unity Update Event
        /// </summary>
        private void Update()
        {
            var currentGear = TransmissionDataSource.CurrentGear.Gear;
            if (CurrentBigGear != currentGear)
            {
                SwapGearTexts(currentGear);
            }
        }

        /// <summary>
        /// Swap the ui labels of the gears
        /// </summary>
        private void SwapGearTexts(string currentGear)
        {
            foreach (var smallText in SmallTexts)
            {
                if (smallText.text == currentGear)
                {
                    smallText.text = BigText.text;
                    break;
                }
            }

            BigText.text = currentGear;
            CurrentBigGear = currentGear;
        }

        /// <summary>
        /// Assign the gears in the gearbox to a specific text
        /// </summary>
        private void AssignGears(string currentGear, IList<string> supportedGears)
        {
            var curIdx = 0;

            for (var i = 0; i < SmallTexts.Count; i++)
            {
                // current gear is assigned to the big text
                if (supportedGears[curIdx] == currentGear)
                {
                    curIdx++;
                }

                SmallTexts[i].text = supportedGears[curIdx];
                curIdx++;
            }
        }
    }
}
