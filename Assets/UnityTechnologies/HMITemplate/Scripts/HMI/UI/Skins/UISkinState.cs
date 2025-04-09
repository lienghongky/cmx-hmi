using HMI.UI.Skins.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HMI.UI.Skins
{
    /// <summary>
    /// Skin State
    /// </summary>
    public class UISkinState : UISkinBase
    {
        /// <summary>
        /// Is the state active
        /// </summary>
        public bool IsStateActive;

        /// <summary>
        /// Target to apply the skin data to
        /// </summary>
        public UISkinBase Target;

        /// <summary>
        /// Skin data associated with this skin
        /// </summary>
        public UISkinDataBase SkinData;

        /// <summary>
        /// Activate this state
        /// </summary>
        public void ActivateState()
        {
            IsStateActive = true;
            ApplySkin();
        }

        /// <summary>
        /// Deactivate this state
        /// </summary>
        public void DeactivateState()
        {
            IsStateActive = false;
            ApplySkin();
        }

        /// <summary>
        /// Apply the state
        /// </summary>
        public override void ApplySkin()
        {
            if (IsStateActive)
            {
                if (Target != null)
                {
                    Target.SetSkinData(SkinData);
                    Target.ApplySkin();
                }
            }
        }

        /// <summary>
        /// Get skin data
        /// </summary>
        public override UISkinDataBase GetSkinData()
        {
            return SkinData;
        }

        /// <summary>
        /// Set skin data
        /// </summary>
        public override bool SetSkinData(UISkinDataBase data)
        {
            SkinData = data;

            if (Target != null)
            {
                return Target.SetSkinData(data);
            }
            else
            {
                return true;
            }
        }
    }
}
