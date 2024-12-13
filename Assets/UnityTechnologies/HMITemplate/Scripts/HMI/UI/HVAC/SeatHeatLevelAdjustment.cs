using HMI.Vehicles.Data;
using HMI.Vehicles.Services;
using UnityEngine;

namespace HMI.UI.HVAC_Interaction
{
    /// <summary>
    /// Adjust the seat heating of a specific seat
    /// </summary>
    public class SeatHeatLevelAdjustment : MonoBehaviour
    {
        /// <summary>
        /// Identifier for the driver seat
        /// </summary>
        private const string LeftSeat = "DriverSeat";

        /// <summary>
        /// Identifier for the passenger seat
        /// </summary>
        private const string RightSeat = "PassengerSeat";

        /// <summary>
        /// What side of data is targeted
        /// </summary>
        public TargetSeatSide TargetSeat;

        /// <summary>
        /// HVAC data source
        /// </summary>
        private HVACData HvacDataSource;

        /// <summary>
        /// Unity Awake callback
        /// </summary>
        private void Awake()
        {
            HvacDataSource = HVACService.GetHVAC();
        }
        /// <summary>
        /// Update the hvac sprite climate zone with the next level
        /// </summary>
        public void NextLevel()
        {
            HeatingData heater = null;

            switch (TargetSeat)
            {
                case TargetSeatSide.Left:
                    heater = HvacDataSource.GetHeatingSystem(LeftSeat);
                    break;
                case TargetSeatSide.Right:
                    heater = HvacDataSource.GetHeatingSystem(RightSeat);
                    break;
                default:
                    Debug.LogWarning("Target Climate Zone was not found.");
                    break;
            }

            heater.Increase();
        }
    }
}
