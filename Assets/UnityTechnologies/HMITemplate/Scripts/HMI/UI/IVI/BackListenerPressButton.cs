using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace HMI.UI.IVI
{
    /// <summary>
    /// Interprets the back key press interaction as a press on this button
    /// </summary>
    public class BackListenerPressButton : MonoBehaviour
    {
        /// <summary>
        /// Respond to back key press
        /// </summary>
        public void OnBackPress()
        {
            if(isActiveAndEnabled)
            {
                var button = GetComponent<Button>();

                if(button != null)
                {
                    button.OnSubmit(new BaseEventData(EventSystem.current));
                }
                else
                {
                    var toggle = GetComponent<Toggle>();

                    if(toggle != null)
                    {
                        toggle.OnSubmit(new BaseEventData(EventSystem.current));
                    }
                }
                
            }
        }
    }
}
