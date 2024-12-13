using HMI.UI.Skins.Data;
using UnityEngine;
using UnityEngine.UI;

namespace HMI.UI.Skins
{
    /// <summary>
    /// UI skin for buttons
    /// </summary>
    public class UISkinButton : UISkinBase
    {
        /// <summary>
        /// The skin data for the button
        /// </summary>
        public UISkinButtonData SkinData;

        public override UISkinDataBase GetSkinData()
        {
            return SkinData;
        }

        public override bool SetSkinData(UISkinDataBase data)
        {
            var uiSkinData = data as UISkinButtonData;

            if (uiSkinData == null)
            {
                Debug.LogWarning("skin data is of wrong type, expected UiSkinButtonData");
                return false;
            }

            SkinData = uiSkinData;
            ApplySkin();
            return true;
        }

        public override void ApplySkin()
        {
            if (SkinData == null)
            {
                return;
            }

            var button = GetComponent<Selectable>();

            var colorBlock = button.colors;
            colorBlock.normalColor = SkinData.NormalColor;
            colorBlock.highlightedColor = SkinData.HighlightedColor;
            colorBlock.pressedColor = SkinData.PressedColor;
            colorBlock.selectedColor = SkinData.SelectedColor;
            colorBlock.disabledColor = SkinData.DisabledColor;
            button.colors = colorBlock;
        }
    }
}
