using System;
using System.Collections;
using HMI.IVI.UI;
using UnityEngine;
using UnityEngine.Networking;
using HMI.Services;

namespace HMI.Media
{
    /// <summary>
    /// Music player
    /// </summary>
    public class MusicPlayer : MonoBehaviour
    {
        /// <summary>
        /// A class to hold the information for the current song data that is selected
        /// </summary>
        [Serializable]
        public class CurrentSongInformation
        {
            public int CurrentSongIndex = 0;
            public string CurrentSongPath = "";
            public string AlbumName;
            public string ArtistName;
            public string SongName;
        }

        /// <summary>
        /// The Audio source that will play the AudioClip
        /// </summary>
        public AudioSource AudioSource;

        /// <summary>
        /// The GetMusicFromSteamingAssetsFolder script in scene that holds the album and song list data
        /// </summary>
        public GetMusicFromSteamingAssetsFolder MusicList;

        /// <summary>
        /// The delegate script in scene that holds events and delegates
        /// </summary>
        public DelegateManager DelegameManagerScript;

        /// <summary>
        /// A public class of CurrentSongInformation that is used when using the music player
        /// </summary>
        public CurrentSongInformation CurrentSongData = new CurrentSongInformation();

        /// <summary>
        /// Sets the default song to the audio source and sets the CurrentSongData data
        /// </summary>
        private void Start()
        {
            CheckForDefaultSongAndSet();
        }

        /// <summary>
        /// Loops through MusicList.MusicInStreamingAssets and if it isnt an empty list, will SetCurrentSongData and SetAudioFromPath
        /// </summary>
        private void CheckForDefaultSongAndSet()
        {
            if (MusicList.MusicInStreamingAssets.Count != 0)
            {
                if (MusicList.MusicInStreamingAssets[0].Songs.Count != 0)
                {
                    SetAudioFromPath(MusicList.MusicInStreamingAssets[0].Songs[0].SongPath);
                    SetCurrentSongData(MusicList.MusicInStreamingAssets[0].AlbumName, MusicList.MusicInStreamingAssets[0].Songs[0].SongPath, 0);

                    //Update UI
                    if (DelegameManagerScript != null)
                    {
                        DelegameManagerScript.SetNewSongOnPlayback();
                    }
                }
            }
        }

        /// <summary>
        /// Starts the process to play an audio from a provided path
        /// </summary>
        /// <param name="path"> the song path</param>
        public void PlayAudioFromPath(string path)
        {
            StartCoroutine(StreamAudio("file://" + path));
        }

        /// <summary>
        /// Get audio clip from path and loads as an audio type of the file
        /// If no connection error, then creates the audioclip, sets the audio clip name, and calls PlayMusicClip using the clip
        /// </summary>
        /// <param name="path"></param>
        private IEnumerator StreamAudio(string path)
        {
            using var www = UnityWebRequestMultimedia.GetAudioClip(path, ReturnAudioType(path));
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);
            }
            else
            {
                var myClip = DownloadHandlerAudioClip.GetContent(www);
                myClip.name = DownloadHandlerAudioClip.GetContent(www).name;
                PlayMusicClip(myClip);
            }
        }

        /// <summary>
        /// Starts the process to set an audio from a provided path
        /// </summary>
        /// <param name="path"></param>
        public void SetAudioFromPath(string path)
        {
            StartCoroutine(SetAudio(path));
        }

        /// <summary>
        /// Get audio clip from path and loads as an audio type of the file
        /// If no connection error, then creates the audioclip, sets the audio clip name to audiosource
        /// </summary>
        /// <param name="path"></param>
        private IEnumerator SetAudio(string path)
        {
            using var www = UnityWebRequestMultimedia.GetAudioClip("file://" + path, ReturnAudioType(path));
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);
            }
            else
            {
                var myClip = DownloadHandlerAudioClip.GetContent(www);
                AudioSource.clip = myClip;
            }
        }

        /// <summary>
        /// Sets the audioSource.clip to Myclip, then plays that clip on the audioSource.
        /// </summary>
        /// <param name="MyClip">Created AudioClip</param>
        private void PlayMusicClip(AudioClip MyClip)
        {
            AudioSource.clip = MyClip;
            AudioSource.Play();
        }

        /// <summary>
        /// If audioSource.clip isnt null, then it will check if its playing or not
        /// if audioSource is not playing then we will Play
        /// if audioSource is playing then we will Pause
        /// </summary>
        public void PlayPauseAudioSource()
        {
            if (AudioSource.clip != null)
            {
                if (!AudioSource.isPlaying)
                {
                    AudioSource.Play();
                }
                else
                {
                    AudioSource.Pause();
                }
            }
        }

        /// <summary>
        /// Checks the path file type and returns relevant audiotypes
        /// </summary>
        /// <param name="path">path of file to check</param>
        /// <returns></returns>
        public static AudioType ReturnAudioType(string path)
        {

            if (path.Contains(".wav"))
            {
                return AudioType.WAV;
            }
            if (path.Contains(".ogg"))
            {
                return AudioType.OGGVORBIS;
            }
            if (path.Contains(".mp3"))
            {
                return AudioType.MPEG;
            }

            return AudioType.UNKNOWN;
        }

        /// <summary>
        /// Cycles to next song in list
        /// if song would cycle to an index greater than the list count then will set song to first in list
        /// plays the new song in the list
        /// sets the current song data
        /// </summary>
        public void NextSong()
        {
            var CurrentIndexInSong = MusicList.ReturnSongIndex(CurrentSongData.AlbumName, CurrentSongData.CurrentSongPath); //ReturnIndexOfSongInList();
            if (CurrentIndexInSong > -1)
            {
                CurrentIndexInSong++;
                if (CurrentIndexInSong > (ReturnSongCountInCurrentAlbum() - 1))
                {
                    CurrentIndexInSong = 0;
                }
                var NewPath = ReturnNewPathInCurrentAlbum(CurrentIndexInSong);
                if (!string.IsNullOrEmpty(NewPath))
                {
                    PlayAudioFromPath(NewPath);
                    SetCurrentSongData(CurrentSongData.AlbumName, NewPath, CurrentIndexInSong);

                    //Update UI
                    if (DelegameManagerScript != null)
                    {
                        DelegameManagerScript.SetNewSongOnPlayback();
                    }

                    GameObject.Find("MediaController").GetComponent<HMI_MediaController>().UpdateActiveEntry(CurrentIndexInSong);

                }
            }
        }

        /// <summary>
        /// Cycles to previous song in list
        /// if song would cycle to an index less than 0 then will set the song to the last song in the list
        /// plays the new song in the list
        /// sets the current song data
        /// </summary>
        public void PreviousSong()
        {
            var CurrentIndexInSong = MusicList.ReturnSongIndex(CurrentSongData.AlbumName, CurrentSongData.CurrentSongPath);//ReturnIndexOfSongInList();
            if (CurrentIndexInSong > -1)
            {
                CurrentIndexInSong--;
                if (CurrentIndexInSong < 0)
                {
                    CurrentIndexInSong = (ReturnSongCountInCurrentAlbum() - 1);
                }
                var NewPath = ReturnNewPathInCurrentAlbum(CurrentIndexInSong);
                if (!string.IsNullOrEmpty(NewPath))
                {
                    PlayAudioFromPath(NewPath);
                    SetCurrentSongData(CurrentSongData.AlbumName, NewPath, CurrentIndexInSong);

                    //Update UI
                    if (DelegameManagerScript != null)
                    {
                        DelegameManagerScript.SetNewSongOnPlayback();
                    }

                    GameObject.Find("MediaController").GetComponent<HMI_MediaController>().UpdateActiveEntry(CurrentIndexInSong);
                }
            }
        }

        /// <summary>
        /// Sets the current song data with the parameters
        /// </summary>
        /// <param name="AlbumName"> the name of the directory representing the album name</param>
        /// <param name="SongPath"> the string path to the song</param>
        /// <param name="CurrentSongIndex">the current index of the song in the music list</param>
        private void SetCurrentSongData(string AlbumName, string SongPath, int CurrentSongIndex)
        {
            CurrentSongData.AlbumName = GetMusicFromSteamingAssetsFolder.NameFromDisk(AlbumName);
            //CurrentSongData.ArtistName = GetMusicFromSteamingAssetsFolder.NameFromDisk
            CurrentSongData.SongName = GetMusicFromSteamingAssetsFolder.NameFromDisk(SongPath);
            CurrentSongData.CurrentSongPath = SongPath;
            CurrentSongData.CurrentSongIndex = CurrentSongIndex;
        }

        /// <summary>
        /// Returns the song cound in the current directory album
        /// </summary>
        /// <returns></returns>
        private int ReturnSongCountInCurrentAlbum()
        {
            for (var i = 0; i < MusicList.MusicInStreamingAssets.Count; i++)
            {
                if (CurrentSongData.AlbumName == MusicList.MusicInStreamingAssets[i].AlbumName)
                {
                    return MusicList.MusicInStreamingAssets[i].Songs.Count;
                }
            }
            return 0;
        }

        /// <summary>
        /// Returns the new path in the current album
        /// </summary>
        /// <param name="DesiredSongIndex">the desired song index of the song in the list</param>
        /// <returns> Returns the path to the new song</returns>
        private string ReturnNewPathInCurrentAlbum(int DesiredSongIndex)
        {
            for (var i = 0; i < MusicList.MusicInStreamingAssets.Count; i++)
            {
                if (CurrentSongData.AlbumName == MusicList.MusicInStreamingAssets[i].AlbumName)
                {
                    return MusicList.MusicInStreamingAssets[i].Songs[DesiredSongIndex].SongPath;
                }
            }

            return null;
        }

        /// <summary>
        /// Sets the album to the new album name
        /// Updates the current song data
        /// If the audiosource is currently playing, then will set and play the default first song
        /// If the audiosource is not currently playing, then will set the audio
        /// </summary>
        /// <param name="AlbumName">the name of the directory that represents the album</param>
        public void SetAlbum(string AlbumName)
        {
            CurrentSongData.AlbumName = MusicList.MusicInStreamingAssets[MusicList.ReturnAlbumIndex(AlbumName)].AlbumName;
            SetCurrentSongData(CurrentSongData.AlbumName, ReturnNewPathInCurrentAlbum(0), 0);
            if (AudioSource.isPlaying)
            {
                PlayAudioFromPath(CurrentSongData.CurrentSongPath);
            }
            else
            {
                SetAudioFromPath(CurrentSongData.CurrentSongPath);
            }

            //Update UI
            if (DelegameManagerScript != null)
            {
                DelegameManagerScript.SetNewSongOnPlayback();
            }
        }

        /// <summary>
        /// Set album with desired song and play
        /// </summary>
        /// <param name="AlbumName">string directory album name</param>
        /// <param name="DesiredSongIndex"> index of desired song to set</param>
        public void SetAlbumWithDesiredSongAndPlay(string AlbumName, int DesiredSongIndex)
        {
            CurrentSongData.AlbumName = MusicList.MusicInStreamingAssets[MusicList.ReturnAlbumIndex(AlbumName)].AlbumName;
            SetCurrentSongData(CurrentSongData.AlbumName, ReturnNewPathInCurrentAlbum(DesiredSongIndex), DesiredSongIndex);

            PlayAudioFromPath(CurrentSongData.CurrentSongPath);

            //Update UI
            if (DelegameManagerScript != null)
            {
                DelegameManagerScript.SetNewSongOnPlayback();
            }
        }
    }
}
