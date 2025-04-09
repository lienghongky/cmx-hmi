using UnityEditor;
using UnityEngine;

namespace HMI.UI.Skins.Data.Editor
{
    /// <summary>
    /// Custom editor for UISkin Collection Data.
    /// A UI Skin Collection Data is a complete skin
    /// that consists of a list of objects that inherit from UISkinDataBase
    /// </summary>
    [CustomEditor(typeof(UISkinCollectionData))]
    public class UISkinCollectionDataEditor : UnityEditor.Editor
    {
        /// <summary>
        /// Unity OnEnable callback
        /// </summary>
        public void OnEnable()
        {
            // if auto update is enabled on this skin, load all elements in this folder into the skin collection
            var collection = (UISkinCollectionData)target;
            if (collection.AutoUpdate)
            {
                LoadElements(collection);
            }
        }

        /// <summary>
        /// On Inpsector GUI callback
        /// </summary>
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            var collection = (UISkinCollectionData)target;
            if (!collection.AutoUpdate)
            {
                if (GUILayout.Button("Load elements"))
                {
                    LoadElements(collection);
                }
            }
        }

        /// <summary>
        /// Finds all assets inheriting from UISkinDataBase and fill them to the collection in this skin
        /// </summary>
        private static void LoadElements(UISkinCollectionData collection)
        {
            var path = AssetDatabase.GetAssetPath(collection);
            var directory = System.IO.Path.GetDirectoryName(path);
            var assets = AssetDatabase.FindAssets("t:UISkinDataBase", new[] { directory });

            collection.Collection.Clear();

            foreach (var guid in assets)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(guid);
                collection.Collection.Add(AssetDatabase.LoadAssetAtPath<UISkinDataBase>(assetPath));
            }

            EditorUtility.SetDirty(collection);
        }
    }
}
