using HMI.UI.Skins.Data;
using UnityEngine;

namespace HMI.UI.Skins
{
    /// <summary>
    /// Base class for all UI Skin elements
    /// An UI skin is single object that can be driven by UI scriptable object skin data
    /// </summary>
    public abstract class UISkinBase : MonoBehaviour
    {
        /// <summary>
        /// get the Skin Data that will be applied to this skinnable object
        /// </summary>
        public abstract UISkinDataBase GetSkinData();

        /// <summary>
        /// Set the skin data for this ui skinnable object
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public abstract bool SetSkinData(UISkinDataBase data);

        /// <summary>
        /// Apply the skin to this skinnable object
        /// </summary>
        public abstract void ApplySkin();

        /// <summary>
        /// Unity Awake callback
        /// </summary>
        public virtual void Awake()
        {
            ApplySkin();
        }

        /// <summary>
        ///  Unity OnEnable callback
        /// </summary>
        public virtual void OnEnable()
        {
            ApplySkin();
        }

        /// <summary>
        ///  Unity Validate callback
        /// </summary>
        public virtual void OnValidate()
        {
            ApplySkin();
        }

        /// <summary>
        ///  Unity Update callback
        /// </summary>
        public virtual void Update()
        {
            if (Application.isEditor)
            {
                ApplySkin();
            }
        }
    }
}
