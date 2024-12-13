using UnityEngine;

namespace HMI.Services.Contacts.Data
{
    /// <summary>
    /// Contacts data
    /// </summary>
    [CreateAssetMenu(fileName = "Contact.asset", menuName = "HMI/Services/Contact", order = 1)]
    public class ContactData : ScriptableObject
    {
        /// <summary>
        /// Profile picture of the contact
        /// </summary>
        public Sprite ProfilePicture;

        /// <summary>
        /// Name of the contact
        /// </summary>
        public string Name;

        /// <summary>
        /// Phone number of the contact
        /// </summary>
        public string PhoneNumber;

        /// <summary>
        /// Email address of the contact
        /// </summary>
        public string Email;

        /// <summary>
        /// Birthday of the contact
        /// </summary>
        public string Birthday;

        /// <summary>
        /// Group this contacts is grouped under
        /// </summary>
        public string Group;
    }
}