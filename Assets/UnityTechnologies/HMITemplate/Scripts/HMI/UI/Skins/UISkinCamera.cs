using HMI.UI.Skins.Data;
using UnityEngine;

namespace HMI.UI.Skins
{
    /// <summary>
    /// UI Skin for Cameras
    /// </summary>
    [RequireComponent(typeof(Camera))]
    public class UISkinCamera : UISkinBase
    {
        /// <summary>
        /// The skin data for a camera
        /// </summary>
        public UISkinCameraData SkinData;

        public override UISkinDataBase GetSkinData()
        {
            return SkinData;
        }

        public override bool SetSkinData(UISkinDataBase data)
        {
            var uiSkinCameraData = data as UISkinCameraData;

            if (uiSkinCameraData == null)
            {
                Debug.LogWarning("skin data is of wrong type, expected UISkinCameraData");
                return false;
            }

            SkinData = uiSkinCameraData;
            ApplySkin();
            return true;
        }

        public override void ApplySkin()
        {
            if (SkinData == null)
            {
                return;
            }

            GetComponent<Camera>().backgroundColor = SkinData.BackgroundColor;
        }
    }
}
