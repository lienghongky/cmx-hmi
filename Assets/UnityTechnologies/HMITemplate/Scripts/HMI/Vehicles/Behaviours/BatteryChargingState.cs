namespace HMI.Vehicles.Behaviours
{
    /// <summary>
    /// The charging state of the Battery
    /// </summary>
    public enum BatteryChargingStateType
    {
        /// <summary>
        /// The battery is not charging
        /// </summary>
        NotCharging,
        /// <summary>
        /// The battery is fully drained
        /// </summary>
        Empty,
        /// <summary>
        /// Battery is currently charging
        /// </summary>
        Charging,
        /// <summary>
        /// The battery is fully charged
        /// </summary>
        FullyCharged,
    }
}
