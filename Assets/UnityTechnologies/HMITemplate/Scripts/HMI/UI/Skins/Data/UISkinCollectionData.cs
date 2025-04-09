using System.Collections.Generic;
using UnityEngine;

namespace HMI.UI.Skins.Data
{
    /// <summary>
    /// A collection of skin data items, used to create a complete skin for a scene
    /// </summary>
    [CreateAssetMenu(fileName = "Skin Collection.asset", menuName = "HMI/Skins/Skin Collection", order = 1)]
    public class UISkinCollectionData : ScriptableObject
    {
        /// <summary>
        /// Collection of skin data items
        /// </summary>
        [Tooltip("Collection of skin data items")]
        public List<UISkinDataBase> Collection = new List<UISkinDataBase>();

        /// <summary>
        /// Automatically update the scene if the collection changes
        /// </summary>
        [Tooltip("Automatically update the scene if the collection changes")]
        public bool AutoUpdate = false;

        /// <summary>
        /// Creates a deep clone of the current skin
        /// </summary>
        public UISkinCollectionData Clone()
        {
            var clone = Instantiate(this);

            for (var i = 0; i < clone.Collection.Count; i++)
            {
                var child = clone.Collection[i];
                var childClone = Instantiate(child);
                childClone.name = child.name;
                clone.Collection[i] = childClone;
            }

            return clone;
        }
    }
}
