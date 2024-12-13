using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using HMI.Services;

namespace HMI.IVI.Buttons
{
    /// <summary>
    /// Assists in easily managing toggle states when the toggle button has various sub-gameobjects
    /// </summary>
    public class HMI_ToggleHelper : MonoBehaviour
    {
        /// <summary>
        /// Individual Button Component
        /// </summary>
        [Serializable]
        public struct ButtonComponents
        {
            public string Name;
            public GameObject Graphic;
            public Image Image { get; set; }
            public TextMeshProUGUI Text { get; set; }
            public Color HighlightedColor;
            public Color BaseColor;
            public Sprite ActiveSprite;
            public Sprite InactiveSprite;
        }

        /// <summary>
        /// Button Components
        /// </summary>
        [Tooltip("Button Components")]
        public ButtonComponents[] Components;

        /// <summary>
        /// Determine if the button listens to global events
        /// </summary>
        public bool IgnoreToggleDelegateEvents;

        /// <summary>
        /// Unity Awake callback
        /// </summary>
        private void Awake()
        {
            if (Components != null)
            {
                for (var i = 0; i < Components.Length; i++)
                {
                    //Image
                    if (Components[i].Graphic.GetComponent<Image>() != null)
                    {
                        Components[i].Image = Components[i].Graphic.GetComponent<Image>();
                    }

                    //TextMeshPro
                    if (Components[i].Graphic.GetComponent<TextMeshProUGUI>() != null)
                    {
                        Components[i].Text = Components[i].Graphic.GetComponent<TextMeshProUGUI>();
                    }
                }

            }
        }

        /// <summary>
        /// Unity Start callback
        /// </summary>
        private void Start()
        {
            if (!IgnoreToggleDelegateEvents)
            {
                // Subscribing to the SetButtonActive method to be called whenever buttonClickDelegateActive is Invoked 
                DelegateManager.ButtonClickDelegateActive += SetButtonActive;

                // Subscribing to the SetButtonInactive method to be called whenever buttonClickDelegateActive is Invoked 
                DelegateManager.ButtonClickDelegateInactive += SetButtonInactive;
            }
        }

        /// <summary>
        /// Setting the interactable element of the button that this script is attached to Off
        /// </summary>
        private void SetButtonInactive()
        {
            if (this != null)
            {
                GetComponent<Toggle>().interactable = false;
            }
        }

        /// <summary>
        /// Setting the interactable element of the button that this script is attached to On
        /// </summary>
        private void SetButtonActive()
        {
            if (this != null)
            {
                GetComponent<Toggle>().interactable = true;
            }
        }

        /// <summary>
        /// Updates the selected navi button visuals
        /// </summary>
        public void NavigationButtonSelection(Toggle SelectedNaviButton)
        {
            for (var i = 0; i < Components.Length; i++)
            {
                if (SelectedNaviButton.isOn)
                {
                    //Highlight Image
                    if (Components[i].Image != null)
                    {
                        //Image
                        Components[i].Image.color = Components[i].HighlightedColor;

                        //Active Sprite
                        if (Components[i].ActiveSprite != null)
                        {
                            Components[i].Image.sprite = Components[i].ActiveSprite;
                        }
                    }

                    //Highlight TextMeshPro
                    if (Components[i].Text != null)
                    {
                        Components[i].Text.color = Components[i].HighlightedColor;
                    }
                }
                else
                {
                    //Off Color
                    if (Components[i].Image != null)
                    {
                        Components[i].Image.color = Components[i].BaseColor;

                        //Inactive Sprite
                        if (Components[i].ActiveSprite != null)
                        {
                            Components[i].Image.sprite = Components[i].InactiveSprite;
                        }
                    }

                    //Off TextMeshPro
                    if (Components[i].Text != null)
                    {
                        Components[i].Text.color = Components[i].BaseColor;
                    }
                }
            }
        }

        /// <summary>
        /// Function to check the toggle's ability to be toggled, and then toggle it.
        /// </summary>
        public void TryToggle()
        {
            if (this != null)
            {
                if(GetComponent<Toggle>().interactable == true && GetComponent<Toggle>().isOn == false)
                {
                    GetComponent<Toggle>().isOn = true;
                }
            }
        }
    }
}

