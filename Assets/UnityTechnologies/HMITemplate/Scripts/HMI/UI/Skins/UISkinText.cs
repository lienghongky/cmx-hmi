using HMI.UI.Skins;
using HMI.UI.Skins.Data;
using TMPro;
using UnityEngine;

namespace Assets.UnityTechnologies.HMITemplate.Scripts.HMI.UI.Skins
{
    /// <summary>
    /// Apply an UI skin to a text
    /// </summary>
    [RequireComponent(typeof(TextMeshPro))]
    public class UISkinText : UISkinBase
    {
        /// <summary>
        /// The skin data for the text
        /// </summary>
        public UISkinTextData SkinData;

        public override UISkinDataBase GetSkinData()
        {
            return SkinData;
        }

        public override bool SetSkinData(UISkinDataBase data)
        {
            var uiSkinTextData = data as UISkinTextData;

            if (uiSkinTextData == null)
            {
                Debug.LogWarning("skin data is of wrong type, expected UISkinTextData");
                return false;
            }

            SkinData = uiSkinTextData;
            ApplySkin();
            return true;
        }

        public override void ApplySkin()
        {
            if (SkinData == null)
            {
                return;
            }

            var text = GetComponent<TextMeshPro>();
            text.color = SkinData.TextColor;

            if (SkinData.Material != null)
            {
                text.fontMaterial = SkinData.Material;
            }
        }
    }
}
