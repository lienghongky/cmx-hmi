using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace HMI.Media
{
    /// <summary>
    /// SETTING UP NEW MUSIC:
    /// In the Assets>StreamingAssets > Music folder:
    /// Make a folder with your album name there
    /// In that folder drop your album art (.jpg or .png) and your music (currently supported .wav and .ogg)
    /// Then when you push play, this script will grab that music and album art and populate a class that you can pull info from
    /// </summary>
    public class GetMusicFromSteamingAssetsFolder : MonoBehaviour
    {
        /// <summary>
        /// Class for MusicAlbumAndSongs
        /// Contains: Album name, album art, and list of songs in the form of string location paths
        /// </summary>
        [Serializable]
        public class MusicAlbumAndSongs
        {
            public string AlbumName;
            public Sprite AlbumArt;
            public bool AlbumDownloadComplete;
            public List<SongInformation> Songs = new List<SongInformation>();
        }

        /// <summary>
        /// Song information
        /// </summary>
        [Serializable]
        public class SongInformation
        {
            public string SongName;
            public string SongPath;
            public float SongLength = -1;
        }

        /// <summary>
        /// List of MusicAlbumAndSongs in the streaming assets folder
        /// </summary>
        //[HideInInspector]
        public List<MusicAlbumAndSongs> MusicInStreamingAssets;

        /// <summary>
        /// Default album sprite to be used if a sprite isnt in the album folder to represent album art
        /// </summary>
        public Sprite PlaceholderAlbum;

        /// <summary>
        /// Number of songs loaded
        /// </summary>
        private int NumSongsLoaded = 0;

        /// <summary>
        /// On awake, populate the music in the streaming assets folder to our MusicInStreamingAssets list
        /// </summary>
        private void Awake()
        {
            PopulateMusicInStreamingAssets();
        }

        /// <summary>
        /// Creates an array of all directories in the streaming asset music folder which represent Albums
        /// For each album in the string array we created, create a new MusicAlbumAndSongs class, and populate its data
        /// Then add that class to the list MusicInStreamingAssets
        /// </summary>
        private void PopulateMusicInStreamingAssets()
        {
            var AllAlbums = GetAllDirectoriesAtPath(StreamingAssetPathToMusicFolder());
            for (var i = 0; i < AllAlbums.Length; i++)
            {
                //Debug.Log("Album name: " + AllAlbums[i]);
                var NewAlbum = new MusicAlbumAndSongs
                {
                    AlbumName = NameFromDisk(AllAlbums[i]),
                    AlbumArt = ReturnAlbumArtInMusicDirectory(AllAlbums[i]),
                    AlbumDownloadComplete = false,
                    Songs = GetAllsongFilesInDirectory(AllAlbums[i])
                };
                MusicInStreamingAssets.Add(NewAlbum);
            }

        }

        /// <summary>
        /// Find the artwork in the album folder and return it
        /// if no artwork is found, then return the PlaceholderAlbum
        /// </summary>
        /// <param name="path"> the string path to the album folder</param>
        private Sprite ReturnAlbumArtInMusicDirectory(string path)
        {
            foreach (var fileName in Directory.GetFiles(path))
            {
                if (fileName.Contains(".jpg") || fileName.Contains(".png"))
                {
                    return ReturnCreatedSpriteFromRequest(fileName);
                }
            }

            return PlaceholderAlbum;
        }

        /// <summary>
        /// Gets all directories in a given path
        /// </summary>
        /// <param name="path">the music folder path located in streaming assets</param>
        /// <returns> returns an array of directories </returns>
        private string[] GetAllDirectoriesAtPath(string path)
        {
            return Directory.GetDirectories(path);
        }

        /// <summary>
        /// Gets all song files in a given directory
        /// </summary>
        /// <param name="path">the path to a directory that contains the songs</param>
        /// <returns> returns a list of songs in the directory</returns>
        private List<SongInformation> GetAllsongFilesInDirectory(string path)
        {
            var SongList = new List<SongInformation>();

            foreach (var fileName in Directory.GetFiles(path))
            {
                
                if (CheckSupportedFileTypes(fileName))
                {
                    var NewSong = new SongInformation
                    {
                        SongName = NameFromDisk(fileName, NameFromDisk(path)),
                        SongPath = fileName
                    };

                    SongList.Add(NewSong);
                }
            }

            return SongList;
        }

        private bool CheckSupportedFileTypes(string fileName)
        {
            if (fileName.Contains(".mp3") || fileName.Contains(".wav") || fileName.Contains(".ogg"))
            {
                if (!fileName.Contains(".meta"))
                {
                    return true;
                }   
            }
            
            return false;
        }

        /// <summary>
        /// Populate additional song information
        /// </summary>
        public void PopulateAdditionalSongInformation(string path, string AlbumName)
        {
            if (MusicInStreamingAssets[ReturnAlbumIndex(AlbumName)].AlbumDownloadComplete == false)
            {
                var SongInfo = MusicInStreamingAssets[ReturnAlbumIndex(AlbumName)].Songs[ReturnSongIndex(AlbumName, path)];
                if (SongInfo.SongLength <= 0)
                {
                    StartCoroutine(RequestSongDownloadThenReturnLength(path, SongInfo, AlbumName));
                }
            }
        }

        /// <summary>
        /// Find out the length of a downloaded song
        /// </summary>
        private IEnumerator RequestSongDownloadThenReturnLength(string path, SongInformation SongInfo, string AlbumNanme)
        {
            //var www = UnityWebRequestMultimedia.GetAudioClip("file://" + path, MusicPlayer.ReturnAudioType(path));

            // yield return www.SendWebRequest();

            var downloadHandler = new DownloadHandlerAudioClip("file://" + path, MusicPlayer.ReturnAudioType(path));
            downloadHandler.compressed = false;
            downloadHandler.streamAudio = true;
            var www = new UnityWebRequest("file://" + path, UnityWebRequest.kHttpVerbGET, downloadHandler, null);

            yield return www.SendWebRequest();


            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);
            }
            else
            {
                var myClip = DownloadHandlerAudioClip.GetContent(www);
               
                SongInfo.SongLength = myClip.length;

                NumSongsLoaded++;

                if (NumSongsLoaded == MusicInStreamingAssets[ReturnAlbumIndex(AlbumNanme)].Songs.Count)
                {
                    SongDownloadForAlbumComplete(ReturnAlbumIndex(AlbumNanme));
                }
                
               Destroy(myClip);
               
            }
        }

        /// <summary>
        /// Song download completed
        /// </summary>
        private void SongDownloadForAlbumComplete(int AlbumIndex)
        {
            NumSongsLoaded = 0;
            MusicInStreamingAssets[AlbumIndex].AlbumDownloadComplete = true;
        }

        /// <summary>
        /// Album download completed
        /// </summary>
        public bool ReturnAlbumDownloadComplete(int AlbumIndex)
        {
            return MusicInStreamingAssets[AlbumIndex].AlbumDownloadComplete;
        }

        /// <summary>
        /// Gets a name to return from a path
        /// </summary>
        /// <param name="filePath"> the path that you would like the string cleaned from</param>
        /// <returns>Returns a file name from the path</returns>
        public static string NameFromDisk(string filePath)
        {
            var newName = filePath;
            var removePath = newName.Replace(StreamingAssetPathToMusicFolder(), "");
            newName = removePath.Replace(@"\", "");
            newName = newName.Replace("/", "");
            newName = newName.Replace(".mp4", "");
            newName = newName.Replace(".mp3", "");
            newName = newName.Replace(".wav", "");
            newName = newName.Replace(".ogg", "");

            return newName;
        }

        /// <summary>
        /// Gets a name to return from a path
        /// </summary>
        public static string NameFromDisk(string filePath, string AlbumName)
        {
            var newName = filePath;
            var removePath = newName.Replace(StreamingAssetPathToMusicFolder(), "");
            newName = removePath.Replace(@"\", "");
            newName = newName.Replace("/", "");
            newName = newName.Replace(".mp4", "");
            newName = newName.Replace(".mp3", "");
            newName = newName.Replace(".wav", "");
            newName = newName.Replace(".ogg", "");
            newName = newName.Replace(AlbumName, "");
            return newName;
        }

        /// <summary>
        /// Returns the path to the the Music folder located in streaming assets folder in assets
        /// </summary>
        private static string StreamingAssetPathToMusicFolder()
        {
            return Application.streamingAssetsPath + "/Music";
        }

        /// <summary>
        /// Creates and returns a sprite from a path to a png or jpg file.
        /// </summary>
        /// <param name="path">path to the png or jpg file</param>
        /// <returns>returns a created sprite</returns>
        public Sprite ReturnCreatedSpriteFromRequest(string path)
        {
            var texture = LoadPNG(path);
            var rect = new Rect(0, 0, texture.width, texture.height);
            var mySprite = Sprite.Create(texture, rect, new Vector2(.5f, .5f));
            mySprite.name = NameFromDisk(path);
            return mySprite;
        }

        /// <summary>
        /// Loads a png from a file path
        /// </summary>
        /// <param name="filePath">the filepath in which the png resides</param>
        /// <returns>returns a texture 2D</returns>
        public static Texture2D LoadPNG(string filePath)
        {
            Texture2D tex = null;
            byte[] fileData;

            if (File.Exists(filePath))
            {
                fileData = File.ReadAllBytes(filePath);
                tex = new Texture2D(2, 2);
                tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
            }
            return tex;
        }

        /// <summary>
        /// Returns the index of a directory album in MusicInStreamingAssets
        /// </summary>
        /// <param name="AlbumName">The name of the album you would like to find in the list</param>
        /// <returns>returns the int index of the file in the list</returns>
        public int ReturnAlbumIndex(string AlbumName)
        {
            var index = -1;
            for (var i = 0; i < MusicInStreamingAssets.Count; i++)
            {
                if (MusicInStreamingAssets[i].AlbumName == AlbumName)
                {
                    index = i;
                }
            }
            return index;
        }

        /// <summary>
        /// Returns the song index in MusicInStreamingAssets[ReturnAlbumIndex(AlbumName)].Songs
        /// </summary>
        /// <param name="AlbumName"> The album name</param>
        /// <param name="SongPath"> the song path</param>
        /// <returns>returns the index in the list MusicInStreamingAssets[ReturnAlbumIndex(AlbumName)].Songs</returns>
        public int ReturnSongIndex(string AlbumName, string SongPath)
        {
            var index = -1;
            for (var i = 0; i < MusicInStreamingAssets[ReturnAlbumIndex(AlbumName)].Songs.Count; i++)
            {
                if (MusicInStreamingAssets[ReturnAlbumIndex(AlbumName)].Songs[i].SongPath == SongPath)
                {
                    index = i;
                }
            }
            return index;
        }
    }
}
