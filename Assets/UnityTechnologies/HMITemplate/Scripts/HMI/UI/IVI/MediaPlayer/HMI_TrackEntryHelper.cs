using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HMI.IVI.UI
{
    /// <summary>
    /// Class which controls the TracklistEntry prefab conponents & settings
    /// </summary>
    public class HMI_TrackEntryHelper : MonoBehaviour
    {
        /// <summary>
        /// List of TextMeshPro objects used for displaying the song's name
        /// </summary>
        public List<TMP_Text> SongNameTexts = new List<TMP_Text>();

        /// <summary>
        /// List of TextMeshPro objects used for displaying the song's track number
        /// </summary>
        public List<TMP_Text> TrackNumberTexts = new List<TMP_Text>();

        /// <summary>
        /// List of TextMeshPro objects used for displaying the song's length
        /// </summary>
        public List<TMP_Text> TrackLengthTexts = new List<TMP_Text>();

        /// <summary>
        /// Internal int to easily track which song in the album this instance corresponds to
        /// </summary>
        private int SongIndex;

        /// <summary>
        /// MediaController script link
        /// </summary>
        private HMI_MediaController MediaController;

        /// <summary>
        /// Toggle component of the TrackEntry
        /// </summary>
        private Toggle Toggle;

        /// <summary>
        /// Awake function assigns the MediaController & Toggle
        /// </summary>
        private void Awake()
        {
            if (GameObject.Find("MediaController") != null)
            {
                MediaController = GameObject.Find("MediaController").GetComponent<HMI_MediaController>();
            }
            else
            {
                Debug.LogWarning("::" + gameObject.name + "::  Can not find GameObject 'MediaController' in the heirachy");
            }

            Toggle = GetComponent<Toggle>();
        }

        /// <summary>
        /// Function to set the various text fields of the prefab. Name, TrackNumber, & Length. Also sets the song index.
        /// </summary>
        public void SetTrackEntryText(string TextSong, int Index, string TextNumber, int TextLength)
        {
            SongIndex = Index;

            foreach (var txt in SongNameTexts)
            {
                txt.text = TextSong;
            }

            foreach (var txt in TrackNumberTexts)
            {
                txt.text = TextNumber;
            }

            SetTrackLenghText(TextLength);
        }

        /// <summary>
        /// Function to overwrite/replace a songs TrackLength text fields
        /// </summary>
        public void SetTrackLenghText(int TextLength)
        {
            if (TextLength > 0)
            {
                var minutes = Mathf.FloorToInt(TextLength / 60);
                var seconds = Mathf.FloorToInt(TextLength % 60);

                var l = $"{minutes:0}:{seconds:00}";

                foreach (var txt in TrackLengthTexts)
                {
                    txt.text = l;
                }
            }
            else
            {
                foreach (var txt in TrackLengthTexts)
                {
                    txt.text = "~";
                }
            }
        }

        /// <summary>
        /// Function to set the prefab's Toggle component's Interactable parameter
        /// </summary>
        public void AllowInteraction(bool OnOff)
        {
            Toggle.interactable = OnOff;
        }

        /// <summary>
        /// Function called when the value of Toggle is changed via click
        /// </summary>
        public void ClickTracklistToggle()
        {
            if (Toggle.isOn && Toggle.interactable)
            {
                MediaController.ClickTrackEntryButton(SongIndex);
            }
        }
    }
}
