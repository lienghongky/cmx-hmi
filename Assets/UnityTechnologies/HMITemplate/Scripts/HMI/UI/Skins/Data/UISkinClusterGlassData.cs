using UnityEngine;

namespace HMI.UI.Skins.Data
{
    /// <summary>
    /// UI Skin cluster glass data
    /// </summary>
    [CreateAssetMenu(fileName = "Cluster Glass Skin.asset", menuName = "HMI/Skins/Cluster Glass Skin", order = 1)]
    public class UISkinClusterGlassData : UISkinDataBase
    {
        /// <summary>
        /// Start color of the multiply gradient   
        /// </summary>
        public Color MultiplyStartColor = Color.white;

        /// <summary>
        /// End color of the multiply gradient   
        /// </summary>
        public Color MultiplyEndColor = Color.black;

        /// <summary>
        /// Start color of the add gradient   
        /// </summary>
        public Color AddStartColor = Color.white;

        /// <summary>
        /// End color of the add gradient   
        /// </summary>
        public Color AddEndColor = Color.black;

        public override void Interpolate(UISkinDataBase from, UISkinDataBase to, float u)
        {
            var fromCast = (UISkinClusterGlassData)from;
            var toCast = (UISkinClusterGlassData)to;

            MultiplyStartColor = Color.Lerp(fromCast.MultiplyStartColor, toCast.MultiplyStartColor, u);
            MultiplyEndColor = Color.Lerp(fromCast.MultiplyEndColor, toCast.MultiplyEndColor, u);
            AddStartColor = Color.Lerp(fromCast.AddStartColor, toCast.AddStartColor, u);
            AddEndColor = Color.Lerp(fromCast.AddEndColor, toCast.AddEndColor, u);
        }
    }
}
