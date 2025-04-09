using UnityEditor;
using UnityEngine;

namespace HMI.UI.Skins.Data.Editor
{
    /// <summary>
    /// Custom editor for UISkinColorPairsData
    /// UISKinColorPairs is a list of SpriteRenderer-Color pairs.
    /// It allows for easy color assignment to a set of spriterenderers
    /// </summary>
    [CustomEditor(typeof(UISkinColorPairsData))]
    public class UISkinColorPairsDataEditor : UnityEditor.Editor
    {
        /// <summary>
        /// Last selected SpriteGroupColorAlpha to load color pairs from
        /// </summary>
        private SpriteGroupColorAlpha LastSelected = null;

        /// <summary>
        /// On Inspector GUI callback
        /// </summary>
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            var pairs = (UISkinColorPairsData)target;

            LastSelected = (SpriteGroupColorAlpha)EditorGUILayout.ObjectField(
                LastSelected,
                typeof(SpriteGroupColorAlpha),
                true
                );

            if (LastSelected != null)
            {
                if (GUILayout.Button("Load colors"))
                {
                    LoadColors(pairs, LastSelected);
                }
            }
        }

        /// <summary>
        /// Load the colors to the data set
        /// </summary>
        private static void LoadColors(UISkinColorPairsData target, SpriteGroupColorAlpha source)
        {
            target.Colors.Clear();

            foreach (var pair in source.SpriteColorPairs)
            {
                target.Colors.Add(pair.Color);
            }

            EditorUtility.SetDirty(target);
        }
    }
}
