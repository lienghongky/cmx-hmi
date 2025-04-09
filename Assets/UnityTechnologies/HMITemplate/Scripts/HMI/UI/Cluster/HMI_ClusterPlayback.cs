using System.Collections;
using System.Collections.Generic;
using HMI.Media;
using TMPro;
using UnityEngine;
using HMI.Services;

namespace HMI.UI.Cluster
{
    /// <summary>
    /// This Class is used to set the MediaPlayback information in the Cluster Scene.
    /// </summary>
    public class HMI_ClusterPlayback : MonoBehaviour
    {
        /// <summary>
        /// TextMeshPro gameobject used for the song's name
        /// </summary>
        [Header("Text Objects")]
        public TextMeshPro SongNameText;

        /// <summary>
        /// TextMeshPro gameobject used for the songs length of playtime
        /// </summary>
        public TextMeshPro TrackLengthText;

        /// <summary>
        /// TextMeshPro gameobject used for the current playback time of a song
        /// </summary>
        public TextMeshPro PlayingText;

        /// <summary>
        /// SpriteRenderer gameobject used for displaying the play/pause icons
        /// </summary>
        [Header("Playback Objects")]
        public SpriteRenderer PlayStatus;

        /// <summary>
        /// Sprite image for the Play Icon
        /// </summary>
        public Sprite PlaySprite;

        /// <summary>
        /// Sprite Image for the Pause Icon
        /// </summary>
        public Sprite PauseSprite;

        /// <summary>
        /// Animation Controller of the playback bar, which displays normalized playback progress
        /// </summary>
        [Header("Playback Animator")]
        public Animator PlaybackAnimationController;

        /// <summary>
        /// MusicPlayerScript contains all of the nessicary music information. The GameObject with this script is located in the IVI scene
        /// </summary>
        private MusicPlayer MusicPlayerScript;

        /// <summary>
        /// Boolean to ignore delegate events
        /// </summary>
        [Header("Delegate Settings")]
        public bool IgnoreDelegateEvents = false;

        /// <summary>
        /// Update function checks to see if the music player script is set. If it isn't it tries to find it.
        /// Calls functions to update the playback position/text and to update the PlayStatus icon
        /// </summary>
        private void Update()
        {
            if (MusicPlayerScript != null)
            {
                if (MusicPlayerScript.AudioSource.isPlaying)
                {
                    UpdateClusterPlaybackUI();
                }

                UpdateTrackLengthText();

                UpdatePlayStatusSprite();
            }
            else
            {
                if (GameObject.Find("MediaPlayer") != null)
                {
                    MusicPlayerScript = GameObject.Find("MediaPlayer").GetComponent<MusicPlayer>();
                    SetNewSong();

                    if (!IgnoreDelegateEvents)
                    {
                        DelegateManager.NewSongDelegate += SetNewSong;
                    }
                }
            }
        }

        /// <summary>
        /// Function to update the playback bar. 
        /// </summary>
        private void UpdateClusterPlaybackUI()
        {
            var percent = MusicPlayerScript.AudioSource.time / MusicPlayerScript.AudioSource.clip.length;
            float TimePlayed = Mathf.RoundToInt(MusicPlayerScript.AudioSource.time);

            var minutes = Mathf.FloorToInt(TimePlayed / 60);
            var seconds = Mathf.FloorToInt(TimePlayed % 60);
            PlayingText.text = minutes.ToString("") + ":" + seconds.ToString("00");

            PlaybackAnimationController.Play("Progress", -1, percent);
            PlaybackAnimationController.Update(0f);
        }

        /// <summary>
        /// Function to set which sprite is used for the PlayStatus renderer
        /// </summary>
        private void UpdatePlayStatusSprite()
        {
            if (MusicPlayerScript.AudioSource.isPlaying)
            {
                if(PlayStatus.sprite = PlaySprite)
                {
                    PlayStatus.sprite = PauseSprite;
                }
            }
            else
            {
                if (PlayStatus.sprite = PauseSprite)
                {
                    PlayStatus.sprite = PlaySprite;
                }
            }
        }

        /// <summary>
        /// Function usedf for updating a song's length text, called if the music player is not playing a song.
        /// </summary>
        private void UpdateTrackLengthText()
        {
            if (MusicPlayerScript.MusicList.ReturnAlbumDownloadComplete(MusicPlayerScript.MusicList.ReturnAlbumIndex(MusicPlayerScript.CurrentSongData.AlbumName)))
            {
                if (TrackLengthText.text == "0:00")
                {
                    var minutes = Mathf.FloorToInt(MusicPlayerScript.AudioSource.clip.length / 60);
                    var seconds = Mathf.FloorToInt(MusicPlayerScript.AudioSource.clip.length % 60);
                    TrackLengthText.text = minutes.ToString("0") + ":" + seconds.ToString("00");
                }
            }
        }

        /// <summary>
        /// Function called on Delegate Event, Sets a songs information in the cluster playback ui. Usuys same delegate as IVI Playback_UI
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

            }
        }
    }
}   

