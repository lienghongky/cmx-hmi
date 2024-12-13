using HMI.UI.Skins.Data;
using UnityEngine;

namespace HMI.UI.Skins
{
    /// <summary>
    /// UI Skin for a 2-color gradient
    /// </summary>
    [RequireComponent(typeof(Renderer))]
    public class UISkinGradient : UISkinBase
    {
        /// <summary>
        /// The skin data for a sprite
        /// </summary>
        public UISkinGradientData SkinData;

        public override UISkinDataBase GetSkinData()
        {
            return SkinData;
        }

        public override bool SetSkinData(UISkinDataBase data)
        {
            var uiSkinGradientData = data as UISkinGradientData;

            if (uiSkinGradientData == null)
            {
                Debug.LogWarning("skin data is of wrong type, expected UISkinGradientData");
                return false;
            }

            SkinData = uiSkinGradientData;
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

            if (Application.isPlaying)
            {
                material = renderer.material;
            }
            else
            {
                material = renderer.sharedMaterial;
            }

            material.SetColor("_StartColor", SkinData.StartColor);
            material.SetColor("_EndColor", SkinData.EndColor);
        }
    }
}
