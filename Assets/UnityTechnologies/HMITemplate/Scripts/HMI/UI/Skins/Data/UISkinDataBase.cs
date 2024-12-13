using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HMI.UI.Skins.Data
{
    /// <summary>
    /// Base class of skin data
    /// </summary>
    public abstract class UISkinDataBase : ScriptableObject
    {
        /// <summary>
        /// When any property changes, apply the change to all elements in the scene 
        /// that use this skin data element
        /// </summary>
        public void OnValidate()
        {
            ApplyDataToSkinsInScene();
        }

        /// <summary>
        /// Apply skin to any element in the scene that use this element
        /// </summary>
        private void ApplyDataToSkinsInScene()
        {
            var uiSkins = GetUISkinsInActiveScene();
            var skinsConnectedToData = uiSkins.Where(x => x.GetSkinData() == this);

            foreach (var connectedSkins in skinsConnectedToData)
            {
                connectedSkins.ApplySkin();
            }
        }

        /// <summary>
        /// Get all skins in the scene
        /// </summary>
        /// <returns></returns>
        private IEnumerable<UISkinBase> GetUISkinsInActiveScene()
        {
            var scene = SceneManager.GetActiveScene();

            if (!scene.isLoaded)
            {
                return Enumerable.Empty<UISkinBase>();
            }

            return scene
                .GetRootGameObjects()
                .SelectMany(gameObject => gameObject.GetComponentsInChildren<UISkinBase>());
        }

        /// <summary>
        /// Interpolate two skin data elements
        /// </summary>
        public abstract void Interpolate(UISkinDataBase from, UISkinDataBase to, float u);
    }
}
