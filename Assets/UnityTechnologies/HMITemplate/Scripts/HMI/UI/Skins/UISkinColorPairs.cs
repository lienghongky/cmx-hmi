using HMI.UI.Skins.Data;
using UnityEngine;

namespace HMI.UI.Skins
{
    /// <summary>
    /// Applies colors to a collection of sprite renderers
    /// </summary>
    [RequireComponent(typeof(SpriteGroupColorAlpha))]
    public class UISkinColorPairs : UISkinBase
    {
        /// <summary>
        /// The skin data for a sprite
        /// </summary>
        public UISkinColorPairsData SkinData;

        public override UISkinDataBase GetSkinData()
        {
            return SkinData;
        }

        public override bool SetSkinData(UISkinDataBase data)
        {
            var uiSkinColorPairsData = data as UISkinColorPairsData;

            if (uiSkinColorPairsData == null)
            {
                Debug.LogWarning("skin data is of wrong type, expected UISkinColorPairsData");
                return false;
            }

            SkinData = uiSkinColorPairsData;
            ApplySkin();
            return true;
        }

        public override void ApplySkin()
        {
            if (SkinData == null)
            {
                return;
            }

            var spriteGroup = GetComponent<SpriteGroupColorAlpha>();

            if (spriteGroup.SpriteColorPairs.Count != SkinData.Colors.Count)
            {
                Debug.LogError("mismatch in pairs");
            }

            for (var i = 0; i < spriteGroup.SpriteColorPairs.Count; i++)
            {
                if (i >= SkinData.Colors.Count)
                {
                    break;
                }

                spriteGroup.SpriteColorPairs[i].Color = SkinData.Colors[i];
            }
        }
    }
}
