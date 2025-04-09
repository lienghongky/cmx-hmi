using System.Collections.Generic;
using UnityEngine;

namespace Util
{
    /// <summary>
    /// Interpolates between two Unity Gradients
    /// </summary>
    public static class GradientExtensions
    {
        /// <summary>
        /// Linear Interpolation
        /// </summary>
        /// <param name="input">Input gradient that will be interpolated</param>
        /// <param name="from">Start gradient</param>
        /// <param name="to">End gradient</param>
        /// <param name="u">How far the interpolation has progressed [0..1.0]</param>
        public static void Lerp(this Gradient input, Gradient from, Gradient to, float u)
        {
            var keyframes = CreateKeyframesFromGradients(from, to);
            var colorKeys = new GradientColorKey[keyframes.Count];
            var alphaKeys = new GradientAlphaKey[keyframes.Count];
            var idx = 0;

            foreach (var keyframe in keyframes)
            {
                var color = Color.Lerp(from.Evaluate(keyframe), to.Evaluate(keyframe), u);
                colorKeys[idx] = new GradientColorKey(color, keyframe);
                alphaKeys[idx] = new GradientAlphaKey(color.a, keyframe);
                idx++;
            }

            input.SetKeys(colorKeys, alphaKeys);
        }

        /// <summary>
        /// Places all keyframes from the input gradients in a hashset
        /// </summary>
        private static HashSet<float> CreateKeyframesFromGradients(Gradient from, Gradient to)
        {
            var keyframes = new HashSet<float>();

            foreach (var key in from.colorKeys)
            {
                keyframes.Add(key.time);
            }

            foreach (var key in to.colorKeys)
            {
                keyframes.Add(key.time);
            }

            foreach (var key in from.alphaKeys)
            {
                keyframes.Add(key.time);
            }

            foreach (var key in to.alphaKeys)
            {
                keyframes.Add(key.time);
            }

            return keyframes;
        }
    }
}