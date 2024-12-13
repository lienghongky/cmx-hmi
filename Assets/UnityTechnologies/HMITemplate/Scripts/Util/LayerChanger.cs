using UnityEngine;

namespace Util
{
    /// <summary>
    /// Changes the layer of all child objects to the input layer
    /// </summary>
    public class LayerChanger : MonoBehaviour
    {
        /// <summary>
        /// Changes the layer of all child objects to the input layer
        /// </summary>
        public void SetLayer(string layer)
        {
            var layerIndex = LayerMask.NameToLayer(layer);
            gameObject.layer = layerIndex;

            foreach (var childTransform in GetComponentsInChildren<Transform>(true))
            {
                childTransform.gameObject.layer = layerIndex;
            }
        }
    }
}
