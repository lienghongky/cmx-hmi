using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HMI.UI.IVI.AudioVolume
{
    /// <summary>
    /// Controls the volume popup visuals
    /// </summary>
    public class AudioVolumePopupUI : MonoBehaviour
    {
        /// <summary>
        /// The source to set the volume on;
        /// </summary>
        public AudioSource AudioSource;

        /// <summary>
        /// Volume label
        /// </summary>
        public TMP_Text VolumeLabel;

        /// <summary>
        /// Progressbar
        /// </summary>
        public RectTransform ProgressBar;
                
        [Header("Volume Icon")]
        /// <summary>
        /// The Image component which renderes the volume sprites
        /// </summary>
        public Image VolumeIcon;

        /// <summary>
        /// Icon to use wehn volume is zero
        /// </summary>
        public Sprite VolumeSprite_Silent;

        /// <summary>
        /// Sprite to use when volume is between 0 and 25%
        /// </summary>
        public Sprite VolumeSprite_Low;

        /// <summary>
        /// Sprite to use when volume is between 25 and 75%
        /// </summary>
        public Sprite VolumeSprite_Medium;

        /// <summary>
        /// Sprite to use when volume is between 75 and 100%
        /// </summary>
        public Sprite VolumeSprite_High;

        /// <summary>
        /// Unity Start callback
        /// </summary>
        public void Start()
        {
            UpdateVolume(Mathf.Clamp(Mathf.RoundToInt(AudioSource.volume * 100), 0, 100));
        }

        /// <summary>
        /// Respond to a Volume Change event
        /// </summary>
        /// <param name="volume">volume 0-100</param>
        public void OnVolumeChanged(int volume)
        {
            UpdateVolume(volume);
        }

        /// <summary>
        /// Update the volume
        /// </summary>
        private void UpdateVolume(int volume)
        {
            UpdateVolumeLabel(volume);
            UpdateVolumeBar(volume);
            UpdateVolumeIcon(volume);
        }

        /// <summary>
        /// Update the volume label
        /// </summary>
        private void UpdateVolumeLabel(int volume)
        {
            VolumeLabel.text = volume.ToString();
        }

        /// <summary>
        /// Update the volume bar
        /// </summary>
        private void UpdateVolumeBar(int volume)
        {
            float VolumePercent = volume / 100f;

            ProgressBar.sizeDelta = new Vector2(VolumePercent * ProgressBar.transform.parent.GetComponent<RectTransform>().rect.width, ProgressBar.sizeDelta.y);
        }

        /// <summary>
        /// Update the volume icon
        /// </summary>
        private void UpdateVolumeIcon(int volume)
        {
            if(volume <= 0)
            {
                VolumeIcon.sprite = VolumeSprite_Silent;
            }
            else if (volume > 0 && volume < 25)
            {
                VolumeIcon.sprite = VolumeSprite_Low;
            }
            else if (volume >= 25 && volume <= 75)
            {
                VolumeIcon.sprite = VolumeSprite_Medium;
            }
            else if(volume > 75)
            {
                VolumeIcon.sprite = VolumeSprite_High;
            }
        }
    }
}