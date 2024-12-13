using HMI.Services.Contacts.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HMI.UI.IVI.Contacts
{
    /// <summary>
    /// Updates the details page of a contact
    /// </summary>
    [RequireComponent(typeof(Toggle))]
    public class ContactDetailsUpdater : MonoBehaviour
    {
        /// <summary>
        /// Toggle button this details page is tied to
        /// </summary>
        private Toggle Toggle;

        /// <summary>
        /// Contact data source
        /// </summary>
        public ContactData Contact;

        /// <summary>
        /// Presents the name of the contact
        /// </summary>
        public TMP_Text Name;

        /// <summary>
        /// Presents the phone number of the contact
        /// </summary>
        public TMP_Text Phone;

        /// <summary>
        /// Presents the email address of the contact
        /// </summary>
        public TMP_Text Email;

        /// <summary>
        /// Presents the birthday of the contact
        /// </summary>
        public TMP_Text Birthday;

        /// <summary>
        /// Presents the group of the contact
        /// </summary>
        public TMP_Text Group;

        /// <summary>
        /// Unity OnEnable callback
        /// </summary>
        private void OnEnable()
        {
            UpdateContact();
        }

        /// <summary>
        /// Unity Awkae callback
        /// </summary>
        private void Awake()
        {
            Toggle = GetComponent<Toggle>();
            UpdateContact();
        }

        /// <summary>
        /// Update the contact information
        /// </summary>
        public void UpdateContact()
        {
            if (Toggle.isOn)
            {
                Name.text = Contact.Name;
                Phone.text = Contact.PhoneNumber;
                Email.text = Contact.Email;
                Birthday.text = Contact.Birthday;
                Group.text = Contact.Group;
            }
        }
    }
}
