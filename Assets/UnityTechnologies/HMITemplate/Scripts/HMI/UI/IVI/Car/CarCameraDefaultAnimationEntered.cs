using UnityEngine;
using UnityEngine.Events;

namespace HMI.UI.IVI.Car
{
    /// <summary>
    /// Triggered by animator when default animation is entered by the car
    /// </summary>
    public class CarCameraDefaultAnimationEntered : MonoBehaviour
    {
        /// <summary>
        /// On Default animation entered event
        /// </summary>
        public UnityEvent OnDefaultAnimationEntered;

        /// <summary>
        /// Callback from animation to trigger OnDefaultAnimationEntered event
        /// </summary>
        public void OnDefaultEntered()
        {
            OnDefaultAnimationEntered.Invoke();
        }
    }
}
