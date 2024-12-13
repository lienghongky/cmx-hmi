using HMI.Vehicles.Behaviours;
using HMI.Vehicles.Behaviours.Base;

namespace HMI.UI.Cluster
{
    /// <summary>
    /// A single turning indicator light on the cluster
    /// </summary>
    public class TurningSignalIndicator : ClusterIndicator
    {
        /// <summary>
        /// Car system that is used as input for the turning signals
        /// </summary>
        public CarSystems TurningSignals;

        /// <summary>
        /// Which side of turning signal input this indicator listens to
        /// </summary>
        public TurningSignalType Side;

        /// <summary>
        /// Listener to hook up the indicator to a boolean value change event
        /// </summary>
        /// <param name="value">value the indicator used to determine to turn on or off</param>
        public override void OnValueChangeListener(bool value)
        {
            if (TurningSignals.Data == null)
            {
                return;
            }

            if (Side == TurningSignalType.Left)
            {
                if (TurningSignals.Data.IsLeftTurningSignalOn || TurningSignals.Data.AreHazardLightsOn)
                {
                    if (!IsTurnedOn)
                    {
                        TurnOn();
                    }
                }
                else if (IsTurnedOn)
                {
                    TurnOff();
                }
            }
            else
            {
                if (TurningSignals.Data.IsRightTurningSignalOn || TurningSignals.Data.AreHazardLightsOn)
                {
                    if (!IsTurnedOn)
                    {
                        TurnOn();
                    }
                }
                else if (IsTurnedOn)
                {
                    TurnOff();
                }
            }
        }
    }
}
