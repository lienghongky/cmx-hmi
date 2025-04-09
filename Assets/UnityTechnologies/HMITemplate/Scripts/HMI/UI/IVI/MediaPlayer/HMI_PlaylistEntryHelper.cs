using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HMI.IVI.UI
{
    /// <summary>
    /// Class which controls the PlaylistEntry prefab components & settings
    /// </summary>
    public class HMI_PlaylistEntryHelper : MonoBehaviour
    {
        /// <summary>
        /// Array of TextMeshPro Text components
        /// </summary>
        private TMP_Text[] AlbumNameTexts;

        /// <summary>
        /// String for storing the album's name
        /// </summary>
        private string AlbumName;

        /// <summary>
        /// MediaController script link
        /// </summary>
        private HMI_MediaController MediaController;

        /// <summary>
        /// Awake function assigns the MediaController & AlbumNameTexts
        /// </summary>
        private void Awake()
        {
            if (GameObject.Find("MediaController") != null)
            {
                MediaController = GameObject.Find("MediaController").GetComponent<HMI_MediaController>();
            }
            else
            {
                Debug.LogWarning("::" + gameObject.name + "::  Can not find GameObject 'MediaController' in the hierachy");
            }

            AlbumNameTexts = gameObject.GetComponentsInChildren<TextMeshProUGUI>();
        }

        /// <summary>
        /// Function to set the NameText field of the prefab, and assigns AlbumName
        /// </summary>
        /// <param name="TextToUse"></param>
        public void SetNameText(string TextToUse)
        {
            foreach (var entry in AlbumNameTexts)
            {
                entry.text = TextToUse;
            }

            AlbumName = TextToUse;
        }

        /// <summary>
        /// Function called when the value of Toggle is changed via click
        /// </summary>
        public void ClickPlaylistToggle()
        {
            MediaController.ClickPlaylistButton(AlbumName);
        }
    }
}
