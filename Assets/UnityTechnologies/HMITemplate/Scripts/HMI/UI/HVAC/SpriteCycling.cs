using HMI.UI.HVAC_Interaction;
using HMI.Vehicles.Data;
using HMI.Vehicles.Services;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HMI.UI
{

    public class SpriteCycling : MonoBehaviour
    {
        /// <summary>
        /// The sprite cycle class that holds a name and the desired sprite
        /// </summary>
        [Serializable]
        public class SpriteCycleClass
        {
            /// <summary>
            /// Identifier
            /// </summary>
            public string Name;

            /// <summary>
            /// Desired sprite
            /// </summary>
            public Sprite DesiredSprite;
        }

        /// <summary>
        /// Identifier for the driver seat
        /// </summary>
        private const string LeftSeat = "DriverSeat";

        /// <summary>
        /// Identifier for the passenger seat
        /// </summary>
        private const string RightSeat = "PassengerSeat";

        /// <summary>
        /// Hvac data source
        /// </summary>
        private HVACData HvacDataSource;

        /// <summary>
        /// The public variable for the user to choose in the inspector
        /// </summary>
        public TargetSeatSide CurrentSeat;

        /// <summary>
        /// The target image you want to change
        /// </summary>
        public Image TargetImageToChange;

        /// <summary>
        /// Public list that users can populate with data
        /// </summary>
        public List<SpriteCycleClass> CyclingSpritesList = new List<SpriteCycleClass>();

        /// <summary>
        /// used to determine if level changed and graphics require an update
        /// </summary>
        private int LastLevel = -1;

        /// <summary>
        /// Unity Awake callback
        /// </summary>
        private void Awake()
        {
            HvacDataSource = HVACService.GetHVAC();
        }

        /// <summary>
        /// Unity Update callback
        /// </summary>
        private void Update()
        {
            var currentLevel = GetHeatLevelData();
            if (currentLevel != LastLevel)
            {
                UpdateLevel(currentLevel);
            }
        }

        /// <summary>
        /// Sets the sprite to a specific position in the list
        /// </summary>
        /// <param name="newLevel"> Desired position in the list </param>
        private void UpdateLevel(int newLevel)
        {
            if (newLevel > -1 && newLevel < CyclingSpritesList.Count)
            {
                TargetImageToChange.sprite = CyclingSpritesList[newLevel].DesiredSprite;
            }
            else
            {
                Debug.LogWarning(@"Your Desired sprite to pull fell outside of the bounds of the CyclingClassList. 
                \nPlease confirm the value of the sprite you intend to pull. 
                \nYour requestedSprite: " + newLevel +
                                 "; Count of sprites in the class: " + CyclingSpritesList.Count);
            }

            LastLevel = newLevel;
        }

        /// <summary>
        /// Returns current value of the chosen seat heater from the Hvac script
        /// </summary>
        /// <returns></returns>
        private int GetHeatLevelData()
        {
            HeatingData heater;

            switch (CurrentSeat)
            {
                case TargetSeatSide.Left:
                    heater = HvacDataSource.GetHeatingSystem(LeftSeat);
                    return heater.CurrentLevel;
                case TargetSeatSide.Right:
                    heater = HvacDataSource.GetHeatingSystem(RightSeat);
                    return heater.CurrentLevel;
                default:
                    heater = HvacDataSource.GetHeatingSystem(LeftSeat);
                    return heater.CurrentLevel;
            }
        }
    }

}
