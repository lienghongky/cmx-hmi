using HMI.UI.Skins.Data;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HMI.UI.Skins
{
    /// <summary>
    /// Swaps between skins
    /// </summary>
    public class UISkinCollectionSwapper : MonoBehaviour
    {
        /// <summary>
        /// Target that the skin will be applied to
        /// </summary>
        [Tooltip("Target that the skin will be applied to")]
        public GameObject Target;

        /// <summary>
        /// Previously assigned skin
        /// </summary>
        private UISkinCollectionData LastAssignedSkin = null;

        /// <summary>
        /// Skin that will be applied to the target
        /// </summary>
        [Tooltip("Skin that will be applied to the target")]
        public UISkinCollectionData SkinCollectionData = null;

        /// <summary>
        /// When in playmode the skins will interpolation over time
        /// </summary>
        public float InterpolationTime = 1f;

        /// <summary>
        /// Has the skin been applied once and is the script fully initialized
        /// </summary>
        private bool IsInitialized = false;

        /// <summary>
        /// Timer used to interpolate between skins
        /// </summary>
        private float InterpolationTimer;

        /// <summary>
        /// True if the swapper is interpolating
        /// </summary>
        private bool IsInterpolating;

        /// <summary>
        /// When interpolating a clone of the skin is made of the from state
        /// This clone is then used to store the interpolation data
        /// </summary>
        private UISkinCollectionData InterpolationClone;

        /// <summary>
        /// used to find matching elements quickly during interpolation in the from set
        /// </summary>
        private readonly Dictionary<string, UISkinDataBase> From = new Dictionary<string, UISkinDataBase>();

        /// <summary>
        /// used to find matching elements quickly during interpolation in the to set
        /// </summary>
        private readonly Dictionary<string, UISkinDataBase> To = new Dictionary<string, UISkinDataBase>();

        /// <summary>
        /// Set a new skin to swap to
        /// </summary>
        /// <returns>true if succesfully assigned</returns>
        public bool SetSkin(UISkinCollectionData skin)
        {
            if (!IsMatchingSkin(LastAssignedSkin, skin))
            {
                return false;
            }
            else
            {
                SkinCollectionData = skin;
                RefreshSkin();
                return true;
            }
        }

        /// <summary>
        /// Unity Validate callback
        /// </summary>
        private void OnValidate()
        {
            RefreshSkin();
        }

        /// <summary>
        /// Unity Awake callback
        /// </summary>
        private void Awake()
        {
            ApplySkinToScene(Target, LastAssignedSkin, SkinCollectionData);
            LastAssignedSkin = SkinCollectionData;
            IsInitialized = true;
        }

        /// <summary>
        /// Unity Update callback
        /// </summary>
        private void Update()
        {
            if (!IsInterpolating)
            {
                enabled = false;
            }
            else
            {
                UpdateInterpolation();
            }
        }

        /// <summary>
        /// Update the interpolation
        /// </summary>
        private void UpdateInterpolation()
        {
            InterpolationTimer += Time.deltaTime;

            // completed interpolation
            if (InterpolationTimer >= InterpolationTime)
            {
                IsInterpolating = false;
                ApplySkinToScene(Target, InterpolationClone, SkinCollectionData);
                InterpolationClone = null;
                LastAssignedSkin = SkinCollectionData;
            }
            // continue interpolation
            else
            {
                var u = InterpolationTimer / InterpolationTime;
                foreach (var element in InterpolationClone.Collection)
                {
                    element.Interpolate(From[element.name], To[element.name], u);
                }
            }
        }

        /// <summary>
        /// Refresh the skin
        /// </summary>
        private void RefreshSkin()
        {
            if (Target == null)
            {
                return;
            }

            if (SkinCollectionData == null)
            {
                return;
            }

            if (LastAssignedSkin == SkinCollectionData)
            {
                return;
            }
            else if (LastAssignedSkin != null && !IsMatchingSkin(SkinCollectionData, LastAssignedSkin))
            {
                Debug.LogError("The new skin collection is invalid.");
                return;
            }
            else
            {
                if (Application.isPlaying)
                {
                    if (IsInitialized && LastAssignedSkin != null)
                    {
                        InterpolateSkin();
                    }
                }
                else
                {
                    ApplySkinToScene(Target, LastAssignedSkin, SkinCollectionData);
                    LastAssignedSkin = SkinCollectionData;
                }
            }
        }

        /// <summary>
        /// Check if two skins have matching properties and can thus be swapped
        /// </summary>
        private static bool IsMatchingSkin(UISkinCollectionData newSkin, UISkinCollectionData oldSkin)
        {
            if (newSkin == null)
            {
                return false;
            }

            if (newSkin.Collection.Count != oldSkin.Collection.Count)
            {
                Debug.Log("The new skin collection has a different element count than the old one.");
                return false;
            }

            var names = new HashSet<string>(newSkin.Collection.Select(x => x.name));

            if (!ElementsExistInSet(names, oldSkin.Collection))
            {
                return false;
            }

            names = new HashSet<string>(oldSkin.Collection.Select(x => x.name));

            if (!ElementsExistInSet(names, newSkin.Collection))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Interpolate between two skins
        /// </summary>
        private void InterpolateSkin()
        {
            ResetInterpolationState();
            InterpolationClone = LastAssignedSkin.Clone();
            ApplySkinToScene(Target, LastAssignedSkin, InterpolationClone);
        }

        /// <summary>
        /// Reset the interpolation state
        /// </summary>
        private void ResetInterpolationState()
        {
            InterpolationTimer = 0f;
            IsInterpolating = true;
            enabled = true;

            From.Clear();
            foreach (var element in LastAssignedSkin.Collection)
            {
                From.Add(element.name, element);
            }

            To.Clear();
            foreach (var element in SkinCollectionData.Collection)
            {
                To.Add(element.name, element);
            }
        }

        /// <summary>
        /// Apply skin to target
        /// </summary>
        private static void ApplySkinToScene(GameObject target, UISkinCollectionData lastSkin, UISkinCollectionData newSkin)
        {
            if (newSkin == null || newSkin.Collection == null)
            {
                return;
            }

            var skinnables = target.GetComponentsInChildren<UISkinBase>(true);

            foreach (var skinnable in skinnables)
            {
                var skinData = skinnable.GetSkinData();

                if (skinData == null)
                {
                    Debug.LogWarning($"SkinData null on {skinnable.name}");
                    continue;
                }

                if (lastSkin != null && !lastSkin.Collection.Contains(skinData))
                {
                    Debug.LogWarning($"Skinnable {skinnable.name} has skin data that was not found in the assigned skin");
                    continue;
                }

                var element = newSkin.Collection.FirstOrDefault(x => x.name == skinData.name);
                if (element != null)
                {
                    skinnable.SetSkinData(element);
                }
            }
        }

        /// <summary>
        /// Check if all elements in the set exist in the hashset 
        /// </summary>
        private static bool ElementsExistInSet(HashSet<string> names, IEnumerable<UISkinDataBase> elements)
        {
            foreach (var element in elements)
            {
                if (!names.Contains(element.name))
                {
                    Debug.LogWarning($"Collection invalid, name not in collection: {element.name}");
                    return false;
                }
            }

            return true;
        }
    }
}
