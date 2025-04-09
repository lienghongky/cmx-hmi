using HMI.UI.Skins.Data;
using UnityEngine;
using UnityEngine.UI;

namespace HMI.UI.Skins
{
    /// <summary>
    /// UI skin for cluster glass layer
    /// </summary>
    public class UISkinClusterGlass : UISkinBase
    {
        /// <summary>
        /// The skin data for a sprite
        /// </summary>
        public UISkinClusterGlassData SkinData;

        public override UISkinDataBase GetSkinData()
        {
            return SkinData;
        }

        public override bool SetSkinData(UISkinDataBase data)
        {
            var uiSkinClusterGlassData = data as UISkinClusterGlassData;

            if (uiSkinClusterGlassData == null)
            {
                Debug.LogWarning("skin data is of wrong type, expected UISkinClusterGlassData");
                return false;
            }

            SkinData = uiSkinClusterGlassData;
            ApplySkin();
            return true;
        }

        public override void ApplySkin()
        {
            if (SkinData == null)
            {
                return;
            }

            var renderer = GetComponent<Renderer>();
            Material material;

            if (renderer != null)
            {
                material = GetMaterial(renderer);
            }
            else
            {
                var image = GetComponent<Image>();
                material = image.material;
            }

            material.SetColor("_MultiplyColorStart", SkinData.MultiplyStartColor);
            material.SetColor("_MultiplyColorEnd", SkinData.MultiplyEndColor);
            material.SetColor("_AddColorStart", SkinData.AddStartColor);
            material.SetColor("_AddColorEnd", SkinData.AddEndColor);
        }

        /// <summary>
        /// Get the material from the renderer
        /// </summary>
        private static Material GetMaterial(Renderer renderer)
        {
            Material material;

            if (Application.isPlaying)
            {
                material = renderer.material;
            }
            else
            {
                material = renderer.sharedMaterial;
            }

            return material;
        }
    }
}
