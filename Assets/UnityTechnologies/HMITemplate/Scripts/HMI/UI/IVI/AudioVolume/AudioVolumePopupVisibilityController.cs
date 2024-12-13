using UnityEngine;

namespace HMI.UI.IVI.AudioVolume
{
    /// <summary>
    /// Volume Popup Visibility Controller
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class AudioVolumePopupVisibilityController : MonoBehaviour
    {
        /// <summary>
        /// Animator component, used to play fade in/out
        /// </summary>
        private Animator Animator;

        /// <summary>
        /// How long the volume popup will stay in view until it will hide once activated
        /// </summary>
        public float HideDelay = 5f;

        /// <summary>
        /// The actual volume popup
        /// </summary>
        public AudioVolumePopupUI VolumePopupUI;

        /// <summary>
        /// Countdown timer;
        /// </summary>
        private float Countdown;

        /// <summary>
        /// Unity Awake callback
        /// </summary>
        private void Awake()
        {
            enabled = false;
            Animator = GetComponent<Animator>();
        }

        /// <summary>
        /// Unity Update callback
        /// </summary>
        private void Update()
        {
            if (Countdown > 0f)
            {
                Countdown -= Time.deltaTime;

                if (Countdown < 0f)
                {
                    FadeOut();
                    enabled = false;
                }
            }
        }

        /// <summary>
        /// Respond to a Volume Change event
        /// </summary>
        public void OnVolumeChanged(int volume)
        {        
            Countdown = HideDelay;

            if (!enabled)
            {
                FadeIn();
                enabled = true;
            }
        }

        /// <summary>
        /// Fade In The volume popup controller
        /// </summary>
        public void FadeIn()
        {
            Animator.Play("Fade in");
        }

        /// <summary>
        /// Fade out the volume popup controller
        /// </summary>
        public void FadeOut()
        {
            Animator.Play("Fade out");
        }
    }
}