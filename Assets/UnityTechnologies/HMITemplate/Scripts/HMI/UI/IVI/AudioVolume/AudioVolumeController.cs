using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace HMI.UI.IVI.AudioVolume
{
    /// <summary>
    /// Controls the audio volume
    /// </summary>
    public class AudioVolumeController : MonoBehaviour
    {
        /// <summary>
        /// How much the volume will increase/decrease on each press/knob turn
        /// </summary>
        private const float Increment = 0.05f;

        /// <summary>
        /// The source to set the volume on;
        /// </summary>
        public AudioSource AudioSource;

        /// <summary>
        /// The audio volume changed
        /// </summary>
        public UnityEvent<int> OnVolumeChanged;

        /// <summary>
        /// Increase the audio volume
        /// </summary>
        public void IncreaseVolume()
        {
            var volume = AudioSource.volume;
            volume += Increment;
            UpdateVolume(volume);
        }

        /// <summary>
        /// Decrease the audio volume
        /// </summary>
        public void DecreaseVolume()
        {
            var volume = AudioSource.volume;
            volume -= Increment;
            UpdateVolume(volume);
        }

        /// <summary>
        /// Update the volume
        /// </summary>
        private void UpdateVolume(float volume)
        {
            volume = Mathf.Clamp01(volume);
            AudioSource.volume = volume;
            OnVolumeChanged.Invoke(Mathf.Clamp(Mathf.RoundToInt(volume * 100), 0, 100));
        }
    }
}