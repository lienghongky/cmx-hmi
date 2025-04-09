using TMPro;
using HMI.Media;
using UnityEngine;
using UnityEngine.UI;
using HMI.Services;

namespace HMI.UI.IVI
{
    /// <summary>
    /// Class which controls the PlayheadUI Prefab conponents & settings
    /// </summary>
    public class HMI_PlayheadUI : MonoBehaviour
    {
        /// <summary>
        /// Rect Transform for the Playbar's mask. Used for showing percentage of a song's playback visually.
        /// </summary>
        [Header("Playbar Graphics")]
        public RectTransform PlaybarMask;

        /// <summary>
        /// Rect Transform for the whole width of the playbar
        /// </summary>
        public RectTransform PlaybarEmpty;

        /// <summary>
        /// GameObject which contains the Playbutton Play Icon
        /// </summary>
        public GameObject PlayIcon;

        /// <summary>
        /// GameObject which contains the Playbutton PauseIcon
        /// </summary>
        public GameObject PauseIcon;

        /// <summary>
        /// TextMeshPro component which displays how long a song has been playing
        /// </summary>
        [Header("Playbar Text")]
        public TextMeshProUGUI TimePlayedText;

        /// <summary>
        /// TextMeshPro component which displays a track's length
        /// </summary>
        public TextMeshProUGUI TrackLengthText;

        /// <summary>
        /// TextMeshPro component which displays a song's name
        /// </summary>
        public TextMeshProUGUI SongNameText;

        /// <summary>
        /// Image component for the Album Cover Art
        /// </summary>
        [Header("Album Art")]
        public Image AlbumArt;

        /// <summary>
        /// MusicPlayerScript link
        /// </summary>
        private MusicPlayer MusicPlayerScript;

        /// <summary>
        /// bool to ignore any delegate events that are sent
        /// </summary>
        [Header("Delegate Settings")]
        public bool IgnoreDelegateEvents = false;

        /// <summary>
        /// Awake Function assigns the MusicPlayerScript and subscribes to the newSongDelegate delegate event
        /// </summary>
        private void Awake()
        {
            if (GameObject.Find("MediaPlayer") != null)
            {
                MusicPlayerScript = GameObject.Find("MediaPlayer").GetComponent<MusicPlayer>();
            }
            else
            {
                Debug.LogWarning("::" + gameObject.name + "::  Can not find GameObject 'MediaPlayer' in the heirachy");
            }

            if (!IgnoreDelegateEvents)
            {
                DelegateManager.NewSongDelegate += SetNewSong;
            }
        }

        /// <summary>
        /// Update checks to see if the MusicPlayer is currently playing, and if it is, calls a function to update the UI components
        /// </summary>
        private void Update()
        {
            if (MusicPlayerScript != null)
            {
                if (MusicPlayerScript.AudioSource.isPlaying)
                {
                    UpdatePlayheadUI();

                    if (PlayIcon.activeSelf)
                    {
                        UpdatePlaybuttonIcon();
                    }
                }
                else
                {
                    if (!PlayIcon.activeSelf)
                    {
                        UpdatePlaybuttonIcon();
                    }
                }
            }
        }

        /// <summary>
        /// Function to update UI compnents for the Playhead UI. Playbar size, & TimePlayed text
        /// </summary>
        public void UpdatePlayheadUI()
        {
            var percent = MusicPlayerScript.AudioSource.time / MusicPlayerScript.AudioSource.clip.length;
            float TimePlayed = Mathf.RoundToInt(MusicPlayerScript.AudioSource.time);

            PlaybarMask.sizeDelta = new Vector2(Mathf.Lerp(0, PlaybarEmpty.rect.width, percent), PlaybarMask.rect.height);

            var minutes = Mathf.FloorToInt(TimePlayed / 60);
            var seconds = Mathf.FloorToInt(TimePlayed % 60);
            TimePlayedText.text = minutes.ToString("") + ":" + seconds.ToString("00");
        }

        /// <summary>
        /// Function to update the playhead UI for a new song to play. Changes TrackName, SongLength, and the AlbumArt. Called via delegate event.
        /// </summary>
        private void SetNewSong()
        {
            if (MusicPlayerScript != null)
            {
                SongNameText.text = GetMusicFromSteamingAssetsFolder.NameFromDisk(MusicPlayerScript.CurrentSongData.CurrentSongPath, MusicPlayerScript.CurrentSongData.AlbumName);

                var minutes = 0;
                var seconds = 0;

                if (MusicPlayerScript.AudioSource.clip != null)
                {
                    minutes = Mathf.FloorToInt(MusicPlayerScript.AudioSource.clip.length / 60);
                    seconds = Mathf.FloorToInt(MusicPlayerScript.AudioSource.clip.length % 60);
                }

                TrackLengthText.text = minutes.ToString("0") + ":" + seconds.ToString("00");

                AlbumArt.sprite = MusicPlayerScript.MusicList.MusicInStreamingAssets[MusicPlayerScript.MusicList.ReturnAlbumIndex(MusicPlayerScript.CurrentSongData.AlbumName)].AlbumArt;
            }
        }

        /// <summary>
        /// Function to set the playbutton Icon based on if music is playing
        /// </summary>
        private void UpdatePlaybuttonIcon()
        {
            if (MusicPlayerScript.AudioSource.isPlaying)
            {
                PlayIcon.SetActive(false);
                PauseIcon.SetActive(true);
            }
            else
            {
                PlayIcon.SetActive(true);
                PauseIcon.SetActive(false);
            }
        }

        /// <summary>
        /// Function is called when the playbutton is clicked.
        /// </summary>
        public void PlayPauseSongButton()
        {
            MusicPlayerScript.PlayPauseAudioSource();
        }
    }
}