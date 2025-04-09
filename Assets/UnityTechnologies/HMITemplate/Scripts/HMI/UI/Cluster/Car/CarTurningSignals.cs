using HMI.Vehicles.Behaviours;
using UnityEngine;

namespace HMI.UI.Cluster.Car
{
    /// <summary>
    /// Turning signals on the 3d car
    /// </summary>
    public class CarTurningSignals : MonoBehaviour
    {
        /// <summary>
        /// Data source for the turning signals
        /// </summary>
        public CarSystems TurningSignalsDataSource;

        /// <summary>
        /// Object that contains the left turning signal
        /// </summary>
        public GameObject LeftTurningSignal;

        /// <summary>
        /// Object that contains the right turning signal
        /// </summary>
        public GameObject RightTurningSignal;

        /// <summary>
        /// Interval between blinks
        /// </summary>
        public float BlinkInterval = 1f;

        /// <summary>
        /// Is the left turning signal blinking
        /// </summary>
        private bool IsLeftBlinking = false;

        /// <summary>
        /// Is the right turning signal blinking
        /// </summary>
        private bool IsRightBlinking = false;

        private void Update()
        {
            UpdateSignal(
                LeftTurningSignal,
                isSignalOn: TurningSignalsDataSource.Data.IsLeftTurningSignalOn ||
                TurningSignalsDataSource.Data.AreHazardLightsOn,
                isBlinking: ref IsLeftBlinking,
                blinkingMethod: "BlinkLeft"
                );

            UpdateSignal(
              RightTurningSignal,
              isSignalOn: TurningSignalsDataSource.Data.IsRightTurningSignalOn ||
              TurningSignalsDataSource.Data.AreHazardLightsOn,
              isBlinking: ref IsRightBlinking,
              blinkingMethod: "BlinkRight"
              );
        }

        /// <summary>
        /// Update signal behaviour
        /// </summary>
        private void UpdateSignal(
            GameObject target,
            bool isSignalOn,
            ref bool isBlinking,
            string blinkingMethod)
        {
            if (isSignalOn)
            {
                if (!isBlinking)
                {
                    InvokeRepeating(blinkingMethod, 0, BlinkInterval);
                    isBlinking = true;
                }
            }
            else
            {
                CancelInvoke(blinkingMethod);
                target.SetActive(false);
                isBlinking = false;
            }
        }

        /// <summary>
        /// Turn on/off the left turning signal
        /// </summary>
        public void BlinkLeft()
        {
            if (LeftTurningSignal.gameObject.activeSelf)
            {
                LeftTurningSignal.gameObject.SetActive(false);
            }
            else
            {
                LeftTurningSignal.gameObject.SetActive(true);
            }
        }

        /// <summary>
        /// Turn on/off the right turning signal
        /// </summary>
        public void BlinkRight()
        {
            if (RightTurningSignal.gameObject.activeSelf)
            {
                RightTurningSignal.gameObject.SetActive(false);
            }
            else
            {
                RightTurningSignal.gameObject.SetActive(true);
            }
        }
    }
}
