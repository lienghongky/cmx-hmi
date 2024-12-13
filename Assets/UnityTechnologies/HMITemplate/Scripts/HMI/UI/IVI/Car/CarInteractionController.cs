using UnityEngine;
using UnityEngine.Events;

namespace HMI.UI.IVI.Car
{
    /// <summary>
    /// Controls the car, its camera and the ui on the various IVI screens
    /// </summary>
    public class CarInteractionController : MonoBehaviour
    {
        /// <summary>
        /// Animator for the camera that is pointed at the car
        /// </summary>
        public Animator CarCameraAnimator;

        /// <summary>
        /// Animator for the car to drive the car around
        /// </summary>
        public Animator CarAnimator;

        /// <summary>
        /// Contains all the connector ui elements that show various options on the car
        /// </summary>
        public GameObject Connectors;

        /// <summary>
        /// Contains the return button to close a screen that is opened when pressing a connector
        /// </summary>
        public GameObject ReturnButton;

        /// <summary>
        /// Event that users can respnd to when the car drives into view
        /// </summary>
        public UnityEvent OnDriveIntoView;
        
        /// <summary>
        /// Event that users can respnd to when the car drives out of view
        /// </summary>
        public UnityEvent OnDriveOutOfView;

        /// <summary>
        /// Unity Start callback
        /// </summary>
        public void Start()
        {
            DriveIntoView();
        }

        /// <summary>
        /// Car drives into view
        /// </summary>
        public void DriveIntoView()
        {
            CarCameraAnimator.Play("CarCameraStartup");
            CarAnimator.Play("CarMoveIn");
            OnDriveIntoView.Invoke();
        }

        /// <summary>
        /// Car drives out of view
        /// </summary>
        public void DriveOutOfView()
        {
            CarCameraAnimator.SetBool("ReturnToDefault", true);

            if (CarAnimator.isActiveAndEnabled)
            {
                CarAnimator.Play("CarMoveOut");
            }

            OnDriveOutOfView.Invoke();
        }

        /// <summary>
        /// Move the car view to the battery
        /// </summary>
        public void GoToBatteryView()
        {
            CarCameraAnimator.SetBool("ReturnToDefault", false);
            CarCameraAnimator.Play("DefaultToBattery");
        }

        /// <summary>
        /// Move the car view to the interior
        /// </summary>
        public void GoToInteriorView()
        {
            CarCameraAnimator.SetBool("ReturnToDefault", false);
            CarCameraAnimator.Play("DefaultToInterior");
        }

        /// <summary>
        /// Move the car view to the trunk
        /// </summary>
        public void GoToTrunkView()
        {
            CarCameraAnimator.SetBool("ReturnToDefault", false);
            CarCameraAnimator.Play("DefaultToTrunk");
        }

        /// <summary>
        /// Move the car view to the doors
        /// </summary>
        public void GoToDoorsView()
        {
            CarCameraAnimator.SetBool("ReturnToDefault", false);
            CarCameraAnimator.Play("DefaultToDoors");
        }

        /// <summary>
        /// Move the car view to the engine
        /// </summary>
        public void GoToEngineView()
        {
            CarCameraAnimator.SetBool("ReturnToDefault", false);
            CarCameraAnimator.Play("DefaultToEngine");
        }

        /// <summary>
        /// Move the car view to charging view
        /// </summary>
        public void GoToChargingView()
        {
            CarCameraAnimator.SetBool("ReturnToDefault", false);
            CarCameraAnimator.Play("DefaultToCharging");
            CarAnimator.Play("CarIdle");
        }

        /// <summary>
        /// Move the car view to the default view
        /// </summary>
        public void GoToDefaultView()
        {
            CarCameraAnimator.SetBool("ReturnToDefault", true);
        }
    }
}
