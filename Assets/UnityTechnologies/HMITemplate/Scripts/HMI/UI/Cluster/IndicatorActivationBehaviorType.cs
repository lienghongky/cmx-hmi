namespace HMI.UI.Cluster
{
    /// <summary>
    /// Determines what kind of behavior an indicator will have when activated
    /// </summary>
    public enum IndicatorActivationBehaviorType
    {
        /// <summary>
        /// Once activated the indicators stays on
        /// </summary>
        Steady,

        /// <summary>
        /// Once activated the indicator blinks
        /// </summary>
        Blink
    }
}
