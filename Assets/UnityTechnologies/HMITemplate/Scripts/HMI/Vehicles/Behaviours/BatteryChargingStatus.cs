namespace HMI.Vehicles.Behaviours
{
    /// <summary>
    /// Charging status of the battery
    /// </summary>
    public struct BatteryChargingStatus
    {
        /// <summary>
        /// Is the battery plugged in for charging
        /// </summary>
        public bool IsPluggedIn { get; set; }

        /// <summary>
        /// The current charging state of the battery
        /// </summary>
        public BatteryChargingStateType ChargingState { get; set; }
    }
}
