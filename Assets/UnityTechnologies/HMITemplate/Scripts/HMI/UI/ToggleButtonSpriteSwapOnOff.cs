using UnityEngine;
using UnityEngine.UI;

namespace HMI.UI
{
    /// <summary>
    /// This Script will check for if a Toggle on the same gameobject as it isOn or not isOn and then change its sprite to reflect.  
    /// </summary>
    [RequireComponent(typeof(Toggle))]
    public class ToggleButtonSpriteSwapOnOff : MonoBehaviour
    {
        /// <summary>
        /// The sprite to be set if the Toggle isOn
        /// </summary>
        public Sprite ToggleOnSprite;

        /// <summary>
        /// The Sprite to be set if the Toggle is not on 
        /// </summary>
        public Sprite ToggleOffSprite;

        /// <summary>
        /// A variable to get the Toggle component 
        /// </summary>
        private Toggle ToggleComponent;

        /// <summary>
        /// Unity Start callback
        /// </summary>
        private void Start()
        {
            ToggleComponent = GetComponent<Toggle>();
        }

        /// <summary>
        /// This method will be called from the Toggle component on boolean change of "isOn".
        /// </summary>
        public void SetToggleSprite()
        {
            if (ToggleComponent.isOn)
            {
                GetComponent<Image>().sprite = ToggleOnSprite;
            }
            else
            {
                GetComponent<Image>().sprite = ToggleOffSprite;
            }
        }
    }
}