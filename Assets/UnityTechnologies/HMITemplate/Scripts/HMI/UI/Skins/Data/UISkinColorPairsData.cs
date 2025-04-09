using System.Collections.Generic;
using UnityEngine;

namespace HMI.UI.Skins.Data
{
    /// <summary>
    /// Skin for a collection of color pairs
    /// </summary>
    [CreateAssetMenu(fileName = "Color Pairs Skin.asset", menuName = "HMI/Skins/Color Pairs Skin", order = 1)]
    public class UISkinColorPairsData : UISkinDataBase
    {
        /// <summary>
        /// Colors to apply to the pairs
        /// </summary>
        public List<Color> Colors = new List<Color>();

        public override void Interpolate(UISkinDataBase from, UISkinDataBase to, float u)
        {
            var fromCast = (UISkinColorPairsData)from;
            var toCast = (UISkinColorPairsData)to;

            for (var i = 0; i < Colors.Count; i++)
            {
                Colors[i] = Color.Lerp(fromCast.Colors[i], toCast.Colors[i], u);
            }
        }
    }
}
