using HMI.UI.Skins.Data;
using UnityEngine;

namespace HMI.UI.Skins
{
    /// <summary>
    ///Applies sprite/color settings to a mesh
    /// </summary>
    [RequireComponent(typeof(Renderer))]
    public class UISkinMesh : UISkinBase
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

            var renderer = GetComponent<Renderer>();

            Material[] materials;

            materials = renderer.sharedMaterials;

            if(materials != null){
                for (var i = 0; i < materials.Length; i++)
                {
                    materials[i].color = SkinData.Color;
                }
            }
        }
    }
}
