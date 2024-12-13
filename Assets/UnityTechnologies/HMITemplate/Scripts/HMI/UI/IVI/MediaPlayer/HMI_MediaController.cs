using System.Collections.Generic;
using TMPro;
using HMI.Media;
using UnityEngine;
using UnityEngine.UI;

namespace HMI.IVI.UI
{
    /// <summary>
    /// This class controlls the UI Widgets for the media player and its different pieces.
    /// </summary>
    public class HMI_MediaController : MonoBehaviour
    {
        /// <summary>
        /// Script link to the Media Player
        /// </summary>
        [Header("Music Player")]
        public MusicPlayer MusicPlayerScript;

        public bool MediaScreenIsActive { get; set; }

        /// <summary>
        /// Tracks currently playing album
        /// </summary>
        private int CurrentAlbumIndex;

        /// <summary>
        /// Name of the Album currently browsing/viewing. Note, can be different than album currently playing
        /// </summary>
        private string ViewingAlbum = "";

        /// <summary>
        /// Runtime bool which is set to true when coroutines to download music have started
        /// </summary>
        private bool StartAlbumDownload = false;

        /// <summary>
        /// Parent Object for where Track Entries are instantiated (Landscape Orientation)
        /// </summary>
        [Header("Tracklist")]
        public GameObject TrackEntryWidgetLandscape;

        /// <summary>
        /// Track entry prefab to be instantiated
        /// </summary>
        public GameObject TrackEntryPrefabLandscape;

        /// <summary>
        /// List of instantiated Track Entries
        /// </summary>
        private List<GameObject> TrackEntriesListLandscape;

        /// <summary>
        /// Parent Object for where Track Entries are instantiated (Portrait Orientation)
        /// </summary>
        public GameObject TrackEntryWidgetPortrait;

        /// <summary>
        /// Track entry prefab to be instantiated
        /// </summary>
        public GameObject TrackEntryPrefabPortrait;

        /// <summary>
        /// List of instantiated Track Entries
        /// </summary>
        private List<GameObject> TrackEntriesListPortrait;

        /// <summary>
        /// Parent Object for where Playlist Entries are instantiated
        /// </summary>
        [Header("Playlist")]
        public GameObject PlaylistEntryWidget;

        /// <summary>
        /// Playlist entry prefab to be instantiated
        /// </summary>
        public GameObject PlaylistEntryPrefab;

        /// <summary>
        /// List of instantiated Playlist Entries
        /// </summary>
        private List<GameObject> PlaylistEntriesList;

        /// <summary>
        /// Image component for the Album Cover Art (Landscape Orientation) 
        /// </summary>
        [Header("Album")]
        public Image AlbumCoverLandscape;

        /// <summary>
        /// TextMeshPro component which displays an album's Artist's name
        /// </summary>
        public TextMeshProUGUI ArtistNameTextLandscape;

        /// <summary>
        /// TextMeshPro component which displays an album's title
        /// </summary>
        public TextMeshProUGUI AlbumTitleTextLandscape;

        /// <summary>
        /// Image component for the Album Cover Art (Portrait Orientation) 
        /// </summary>
        public Image AlbumCoverPortrait;

        /// <summary>
        /// TextMeshPro component which displays an album's Artist's name
        /// </summary>
        public TextMeshProUGUI ArtistNameTextPortrait;

        /// <summary>
        /// TextMeshPro component which displays an album's title
        /// </summary>
        public TextMeshProUGUI AlbumTitleTextPortrait;

        /// <summary>
        /// Awake looks for the MediaPlayerScript if one was not manually linked looks for the game object "Media Player"
        /// </summary>
        private void Awake()
        {
            if (MusicPlayerScript == null)
            {
                MusicPlayerScript = GameObject.Find("MediaPlayer").GetComponent<MusicPlayer>();
            }
        }

        /// <summary>
        /// Start populates the playlist based on album-0, song-0 and then updates the UI to match
        /// </summary>
        private void Start()
        {
            PopulatePlaylist();
            BeginTracklistPopulation(0);
            UpdatePlayingAibumInformation(MusicPlayerScript.MusicList.MusicInStreamingAssets[0].AlbumName);
        }

        /// <summary>
        /// Update is only used to refresh tracklist entry length. It checks StartAlbumDownload, and if the current album has been downloaded
        /// </summary>
        private void Update()
        {
            if (StartAlbumDownload && MusicPlayerScript.MusicList.ReturnAlbumDownloadComplete(CurrentAlbumIndex))
            {
                StartAlbumDownload = false;
                UpdateTrackEntryPlaytimes();
            }
        }

        /// <summary>
        /// Function To populate PlaylistEntriesList with playlist entries. Creates entries for how many albums are retrieved from the MusicPlayer
        /// </summary>
        private void PopulatePlaylist()
        {
            PlaylistEntriesList = new List<GameObject>();

            for (var i = 0; i < MusicPlayerScript.MusicList.MusicInStreamingAssets.Count; i++)
            {
                PlaylistEntriesList.Add(CreatePlaylistEntry(i));
            }

            SetToggleNavigation(PlaylistEntriesList);
        }

        /// <summary>
        /// Function which instantiates a prefab, sets it's parent, position & text, and then returns the gameobject. 
        /// </summary>
        private GameObject CreatePlaylistEntry(int AlbumIndex)
        {
            var Entry = Instantiate(PlaylistEntryPrefab, PlaylistEntryWidget.transform);
            Entry.transform.SetParent(PlaylistEntryWidget.transform);

            var h = Entry.GetComponent<RectTransform>().rect.height;

            Entry.GetComponent<RectTransform>().anchoredPosition = new Vector2(Entry.GetComponent<RectTransform>().anchoredPosition.x, AlbumIndex * -(h + (h / 8)));
            Entry.GetComponent<Toggle>().group = PlaylistEntryWidget.GetComponent<ToggleGroup>();
            Entry.GetComponent<HMI_PlaylistEntryHelper>().SetNameText(MusicPlayerScript.MusicList.MusicInStreamingAssets[AlbumIndex].AlbumName);

            return Entry;
        }

        /// <summary>
        /// Builds the navigation map for generated lists
        /// </summary>
        /// <param name="ObjectList"></param>
        private void SetToggleNavigation(List<GameObject> ObjectList)
        {
            if(ObjectList.Count > 1)
            {
                for (int i = 0; i < ObjectList.Count; i++)
                {
                    var ToggleNav = new Navigation();
                    ToggleNav.mode = Navigation.Mode.Explicit;

                    if (i == 0)
                    {
                        ToggleNav.selectOnUp = ObjectList[ObjectList.Count - 1].GetComponent<Toggle>();
                        ToggleNav.selectOnLeft = ObjectList[ObjectList.Count - 1].GetComponent<Toggle>();
                        ToggleNav.selectOnDown = ObjectList[i + 1].GetComponent<Toggle>();
                        ToggleNav.selectOnRight = ObjectList[i + 1].GetComponent<Toggle>();
                    }
                    else if( i > 0 && i < ObjectList.Count - 1)
                    {
                        ToggleNav.selectOnUp = ObjectList[i - 1].GetComponent<Toggle>();
                        ToggleNav.selectOnLeft = ObjectList[i - 1].GetComponent<Toggle>();
                        ToggleNav.selectOnDown = ObjectList[i + 1].GetComponent<Toggle>();
                        ToggleNav.selectOnRight = ObjectList[i + 1].GetComponent<Toggle>();
                    }
                    else
                    {
                        ToggleNav.selectOnUp = ObjectList[i - 1].GetComponent<Toggle>();
                        ToggleNav.selectOnLeft = ObjectList[i - 1].GetComponent<Toggle>();
                        ToggleNav.selectOnDown = ObjectList[0].GetComponent<Toggle>();
                        ToggleNav.selectOnRight = ObjectList[0].GetComponent<Toggle>();
                    }

                    ObjectList[i].GetComponent<Toggle>().navigation = ToggleNav;               
                }
            }

            if(ObjectList.Count == 1)
            {
                var ToggleNav = new Navigation();
                ToggleNav.mode = Navigation.Mode.Explicit;

                ToggleNav.selectOnUp = ObjectList[0].GetComponent<Toggle>();
                ToggleNav.selectOnLeft = ObjectList[0].GetComponent<Toggle>();
                ToggleNav.selectOnDown = ObjectList[0].GetComponent<Toggle>();
                ToggleNav.selectOnRight = ObjectList[0].GetComponent<Toggle>();
            }

        }

        /// <summary>
        /// Function which is the starting call to begin populating TracklistEntriesList with track entries. Sets the current album, & clears any old prefabs & data from another album.
        /// </summary>
        /// <param name="AlbumIndex"></param>
        private void BeginTracklistPopulation(int AlbumIndex)
        {
            //Set current Album Index
            CurrentAlbumIndex = AlbumIndex;

            //Remove old entries
            ClearTracklistEntriesList();

            //Create New Lists
            TrackEntriesListLandscape = new List<GameObject>();
            TrackEntriesListPortrait = new List<GameObject>();

            for (var i = 0; i < MusicPlayerScript.MusicList.MusicInStreamingAssets[AlbumIndex].Songs.Count; i++)
            {
                //Lookup Song Information
                MusicPlayerScript.MusicList.PopulateAdditionalSongInformation(MusicPlayerScript.MusicList.MusicInStreamingAssets[AlbumIndex].Songs[i].SongPath, MusicPlayerScript.MusicList.MusicInStreamingAssets[AlbumIndex].AlbumName);
            }

            //Enable Start Album download
            StartAlbumDownload = true;

            //Populate
            PopulateTracklistEntries();
        }

        /// <summary>
        /// Function to run throguh both TrackEntriesListLandscape & TrackEntriesListPortrait to delete any gameobjects and clear their data
        /// </summary>
        private void ClearTracklistEntriesList()
        {
            if (TrackEntriesListLandscape != null)
            {
                if (TrackEntriesListLandscape.Count > 0)
                {
                    for (var i = 0; i < TrackEntriesListLandscape.Count; i++)
                    {
                        Destroy(TrackEntriesListLandscape[i]);
                    }

                    TrackEntriesListLandscape.Clear();
                }
            }

            if (TrackEntriesListPortrait != null)
            {
                if (TrackEntriesListPortrait.Count > 0)
                {
                    for (var i = 0; i < TrackEntriesListPortrait.Count; i++)
                    {
                        Destroy(TrackEntriesListPortrait[i]);
                    }

                    TrackEntriesListPortrait.Clear();
                }
            }
        }

        /// <summary>
        /// Function To populate TrackEntriesListLandscape & TrackEntriesListPortrait  with track entries. Creates entries for how many songs are retrieved from the MusicPlayer.
        /// </summary>
        private void PopulateTracklistEntries()
        {
            for (var i = 0; i < MusicPlayerScript.MusicList.MusicInStreamingAssets[CurrentAlbumIndex].Songs.Count; i++)
            {
                TrackEntriesListLandscape.Add(CreateTracklistEntry(CurrentAlbumIndex, i, true));
                TrackEntriesListPortrait.Add(CreateTracklistEntry(CurrentAlbumIndex, i, false));
            }

            if (MusicPlayerScript.MusicList.ReturnAlbumDownloadComplete(CurrentAlbumIndex))
            {
                AllowTracklistInteraction(true);
            }

            SetToggleNavigation(TrackEntriesListLandscape);
            SetToggleNavigation(TrackEntriesListPortrait);
        }

        /// <summary>
        /// Function which instantiates a prefab, sets it's parent, position & text, and then returns the gameobject.
        /// </summary>
        private GameObject CreateTracklistEntry(int AlbumIndex, int SongIndex, bool IsLandscape)
        {
            GameObject WidgetParent;
            GameObject PrefabToUse;

            if (IsLandscape)
            {
                WidgetParent = TrackEntryWidgetLandscape;
                PrefabToUse = TrackEntryPrefabLandscape;
            }
            else
            {
                WidgetParent = TrackEntryWidgetPortrait;
                PrefabToUse = TrackEntryPrefabPortrait;
            }

            var Entry = Instantiate(PrefabToUse, WidgetParent.transform);

            Entry.transform.SetParent(WidgetParent.transform);

            var h = Entry.GetComponent<RectTransform>().rect.height;

            Entry.GetComponent<RectTransform>().anchoredPosition = new Vector2(Entry.GetComponent<RectTransform>().anchoredPosition.x, SongIndex * -(h + (h / 10)));

            Entry.GetComponent<Toggle>().group = WidgetParent.GetComponent<ToggleGroup>();

            MusicPlayerScript.MusicList.PopulateAdditionalSongInformation(MusicPlayerScript.MusicList.MusicInStreamingAssets[AlbumIndex].Songs[SongIndex].SongPath,
                MusicPlayerScript.MusicList.MusicInStreamingAssets[AlbumIndex].AlbumName);

            var SongName = MusicPlayerScript.MusicList.MusicInStreamingAssets[AlbumIndex].Songs[SongIndex].SongName;
            var TrackLength = Mathf.RoundToInt(MusicPlayerScript.MusicList.MusicInStreamingAssets[AlbumIndex].Songs[SongIndex].SongLength);
            Entry.GetComponent<HMI_TrackEntryHelper>().SetTrackEntryText(SongName, SongIndex, (1 + SongIndex).ToString(), TrackLength);

            if (MusicPlayerScript.CurrentSongData.CurrentSongIndex == SongIndex && MusicPlayerScript.MusicList.ReturnAlbumIndex(MusicPlayerScript.CurrentSongData.AlbumName) == CurrentAlbumIndex)
            {
                Entry.GetComponent<Toggle>().isOn = true;
            }

            return Entry;
        }

        /// <summary>
        /// Function to update all TrackLength text fields for all playlist entries
        /// </summary>
        private void UpdateTrackEntryPlaytimes()
        {
            for (var i = 0; i < TrackEntriesListLandscape.Count; i++)
            {
                var TrackLength = Mathf.RoundToInt(MusicPlayerScript.MusicList.MusicInStreamingAssets[CurrentAlbumIndex].Songs[i].SongLength);
                TrackEntriesListLandscape[i].GetComponent<HMI_TrackEntryHelper>().SetTrackLenghText(TrackLength);

                if (i == TrackEntriesListLandscape.Count - 1)
                {
                    AllowTracklistInteraction(true);
                }
            }

            for (var i = 0; i < TrackEntriesListPortrait.Count; i++)
            {
                var TrackLength = Mathf.RoundToInt(MusicPlayerScript.MusicList.MusicInStreamingAssets[CurrentAlbumIndex].Songs[i].SongLength);
                TrackEntriesListPortrait[i].GetComponent<HMI_TrackEntryHelper>().SetTrackLenghText(TrackLength);

                if (i == TrackEntriesListPortrait.Count - 1)
                {
                    AllowTracklistInteraction(true);
                }
            }
        }

        /// <summary>
        /// Function to set the toggle state for TracklistEntries to match the currently playing song
        /// </summary>
        /// <param name="SongIndex"></param>
        public void UpdateActiveEntry(int SongIndex)
        {
            if (MusicPlayerScript.MusicList.ReturnAlbumIndex(MusicPlayerScript.CurrentSongData.AlbumName) == CurrentAlbumIndex)
            {
                AllowTracklistInteraction(false);

                for (var i = 0; i < TrackEntriesListLandscape.Count; i++)
                {
                    if (i == SongIndex)
                    {
                        TrackEntriesListLandscape[i].GetComponent<Toggle>().isOn = true;
                    }
                    else
                    {
                        TrackEntriesListLandscape[i].GetComponent<Toggle>().isOn = false;
                    }
                }

                //Portrait
                for (var i = 0; i < TrackEntriesListPortrait.Count; i++)
                {
                    if (i == SongIndex)
                    {
                        TrackEntriesListPortrait[i].GetComponent<Toggle>().isOn = true;
                    }
                    else
                    {
                        TrackEntriesListPortrait[i].GetComponent<Toggle>().isOn = false;
                    }
                }

                AllowTracklistInteraction(true);
            }
        }

        /// <summary>
        /// Function to update the AlbumCoverArt, Artist Name & Album Title, to the currently playing album
        /// </summary>
        /// <param name="AlbumName"></param>
        public void UpdatePlayingAibumInformation(string AlbumName)
        {
            AlbumCoverLandscape.sprite = MusicPlayerScript.MusicList.MusicInStreamingAssets[MusicPlayerScript.MusicList.ReturnAlbumIndex(AlbumName)].AlbumArt;
            ArtistNameTextLandscape.text = MusicPlayerScript.CurrentSongData.ArtistName;
            AlbumTitleTextLandscape.text = AlbumName;

            AlbumCoverPortrait.sprite = MusicPlayerScript.MusicList.MusicInStreamingAssets[MusicPlayerScript.MusicList.ReturnAlbumIndex(AlbumName)].AlbumArt;
            ArtistNameTextPortrait.text = MusicPlayerScript.CurrentSongData.ArtistName;
            AlbumTitleTextPortrait.text = AlbumName;
        }

        /// <summary>
        /// Function which is called when a playlist entry button is pressed. Begins tracklist population.
        /// </summary>
        /// <param name="AlbumName"></param>
        public void ClickPlaylistButton(string AlbumName)
        {
            BeginTracklistPopulation(MusicPlayerScript.MusicList.ReturnAlbumIndex(AlbumName));
            ViewingAlbum = AlbumName;

            if (MediaScreenIsActive)
            {
                if(Screen.width > Screen.height)
                {
                    if(ViewingAlbum != MusicPlayerScript.CurrentSongData.AlbumName)
                    {
                        TrackEntriesListLandscape[0].GetComponent<Toggle>().Select();

                    }
                    else
                    {
                        TrackEntriesListLandscape[MusicPlayerScript.CurrentSongData.CurrentSongIndex].GetComponent<Toggle>().Select();
                    }
                }
                else
                {
                    if(ViewingAlbum != MusicPlayerScript.CurrentSongData.AlbumName)
                    {
                        TrackEntriesListPortrait[0].GetComponent<Toggle>().Select();

                    }
                    else
                    {
                        TrackEntriesListPortrait[MusicPlayerScript.CurrentSongData.CurrentSongIndex].GetComponent<Toggle>().Select();

                    }
                }
            }
        }

        /// <summary>
        /// Function which is called when a tracklist enrty button is pressed. Plays song.
        /// </summary>
        public void ClickTrackEntryButton(int SongIndex)
        {
            MusicPlayerScript.SetAlbumWithDesiredSongAndPlay(ViewingAlbum, SongIndex);

            UpdateActiveEntry(SongIndex);
            UpdatePlayingAibumInformation(ViewingAlbum);
        }

        /// <summary>
        /// Function which sets 'Interactable" for all tracklist entries.
        /// </summary>
        private void AllowTracklistInteraction(bool OnOff)
        {
            for (var i = 0; i < TrackEntriesListLandscape.Count; i++)
            {
                TrackEntriesListLandscape[i].GetComponent<HMI_TrackEntryHelper>().AllowInteraction(OnOff);
            }

            for (var i = 0; i < TrackEntriesListPortrait.Count; i++)
            {
                TrackEntriesListPortrait[i].GetComponent<HMI_TrackEntryHelper>().AllowInteraction(OnOff);
            }
        }

        public void MediaPlayerOnBackPressed()
        {
            if (MediaScreenIsActive)
            {
                PlaylistEntriesList[CurrentAlbumIndex].GetComponent<Toggle>().Select();
            }
        }
    }
}