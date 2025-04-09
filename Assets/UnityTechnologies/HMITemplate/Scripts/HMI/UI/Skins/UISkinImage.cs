using HMI.UI.Skins.Data;
using UnityEngine;
using UnityEngine.UI;

namespace HMI.UI.Skins
{
    /// <summary>
    /// Skin for a UI Image
    /// </summary>
    [RequireComponent(typeof(Image))]
    public class UISkinImage : UISkinBase
    {
        /// <summary>
        /// The skin data for a sprite
        /// </summary>
        public UISkinSpriteData SkinData;

        public override UISkinDataBase GetSkinData()
        {
            return SkinData;
        }

        public override bool SetSkinData(UISkinDataBase data)
        {
            var uiSkinSpriteData = data as UISkinSpriteData;

            if (uiSkinSpriteData == null)
            {
                Debug.LogWarning("skin data is of wrong type, expected UISkinSpriteData");
                return false;
            }

            SkinData = uiSkinSpriteData;
            ApplySkin();
            return true;
        }

        public override void ApplySkin()
        {
            if (SkinData == null)
            {
                return;
            }

            var spriteRenderer = GetComponent<Image>();
            spriteRenderer.color = SkinData.Color;
        }
    }
}
