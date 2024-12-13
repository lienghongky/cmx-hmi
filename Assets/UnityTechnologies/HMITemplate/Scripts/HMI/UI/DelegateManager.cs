using UnityEngine;

namespace HMI.Services
{

    public class DelegateManager : MonoBehaviour
    {
        /// <summary>
        /// Setting up the Delegate
        /// </summary>
        public delegate void SetButtonsInactive();

        /// <summary>
        /// Creating an event for the SetButtonsInnactive delegate
        /// </summary>
        public static event SetButtonsInactive ButtonClickDelegateInactive;

        /// <summary>
        /// Setting up the delegate
        /// </summary>
        public delegate void SetButtonsActive();

        /// <summary>
        /// Creating an event for the SetButtonsActive delegate
        /// </summary>
        public static event SetButtonsActive ButtonClickDelegateActive;

        /// <summary>
        /// Setting up the delegate
        /// </summary>
        public delegate void SetScreenLandscape();

        /// <summary>
        /// Creating an event for the SetScreenLandscape delegate
        /// </summary>
        public static event SetScreenLandscape OrientationDelegateLandscape;

        /// <summary>
        /// Setting up the delegate
        /// </summary>
        public delegate void SetScreenPortrait();

        /// <summary>
        /// Creating an event for the SetScreenPortrait delegate
        /// </summary>
        public static event SetScreenPortrait OrientationDelegatePortrait;

        /// <summary>
        /// Setting up the delegate
        /// </summary>
        public delegate void SetNewSong();

        /// <summary>
        /// Creating an event for the SetNewSong delegate
        /// </summary>
        public static event SetNewSong NewSongDelegate;

        /// <summary>
        /// Setting up the delegate
        /// </summary>
        public delegate void PlayPauseSong();

        /// <summary>
        /// Creating an event for the PlayPauseSong delegate
        /// </summary>
        public static event PlayPauseSong PlayPauseSongDelegate;

        /// <summary>
        /// Button Interaction Methods - Inactive
        /// </summary>
        public void SetButtonInnactive()
        {
            ButtonClickDelegateInactive();
        }

        /// <summary>
        /// Button Interaction Methods - Active
        /// </summary>
        public void SetButtonActive()
        {
            ButtonClickDelegateActive();
        }

        /// <summary>
        /// Set Screen Orientation to Landscape
        /// </summary>
        public void SetScreenOrientationToLandscape()
        {
            OrientationDelegateLandscape();
        }

        /// <summary>
        /// Set Screen Orientation to Portrait
        /// </summary>
        public void SetScreenOrientationToPortrait()
        {
            OrientationDelegatePortrait();
        }

        /// <summary>
        /// Media & Playback Methods - Set new song on playback
        /// </summary>
        public void SetNewSongOnPlayback()
        {
            NewSongDelegate();
        }

        /// <summary>
        /// Media & Playback Methods - play/pause song on playback
        /// </summary>
        public void PlayPauseSongOnPlayback()
        {
            PlayPauseSongDelegate();
        }
    }

}