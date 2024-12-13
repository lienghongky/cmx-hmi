using HMI.Services.Contacts.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HMI.UI.IVI.Contacts
{
    /// <summary>
    /// Shows the contact name and profile picture on the toggle button prefab
    /// </summary>
    [RequireComponent(typeof(Toggle))]
    public class ContactUpdater : MonoBehaviour
    {
        /// <summary>
        /// The contact this updates is connected
        /// </summary>
        public ContactData Contact;

        /// <summary>
        /// Present the profile image of the contact
        /// </summary>
        public Image ProfileImage;
        
        /// <summary>
        /// Presents the name of the contact
        /// </summary>
        public TMP_Text Name;

        /// <summary>
        /// Unity Awake callback
        /// </summary>
        private void Awake()
        {
            ProfileImage.sprite = Contact.ProfilePicture;
            Name.text = Contact.Name;
        }
    }
}
