using HMI.UI.Skins.Data;
using UnityEngine;

namespace HMI.UI.Skins
{
    /// <summary>
    /// UI Skin for car shader
    /// </summary>
    public class UISkinCarShader : UISkinBase
    {
        /// <summary>
        /// The skin data for the text
        /// </summary>
        public UISkinCarShaderData SkinData;

        /// <summary>
        /// Renderers affected by this skin
        /// </summary>
        private Renderer[] Renderers;

        /// <summary>
        /// Unity Awake callback
        /// </summary>
        public override void Awake()
        {
            Renderers = GetComponentsInChildren<Renderer>();
            base.Awake();
        }

        public override UISkinDataBase GetSkinData()
        {
            return SkinData;
        }

        public override bool SetSkinData(UISkinDataBase data)
        {
            var uiSkinData = data as UISkinCarShaderData;

            if (uiSkinData == null)
            {
                Debug.LogWarning("skin data is of wrong type, expected UISkinCarShaderData");
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

            if (Renderers != null)
            {
                foreach (var renderer in Renderers)
                {
                    Material material;
                    material = renderer.sharedMaterial;
                    material.SetColor("_Color", SkinData.Fresnel1Color);
                    material.SetColor("_Fresnel1Color", SkinData.Fresnel1Color);
                    material.SetColor("_Fresnel2Color", SkinData.Fresnel2Color);
                }
            }
        }
    }
}
