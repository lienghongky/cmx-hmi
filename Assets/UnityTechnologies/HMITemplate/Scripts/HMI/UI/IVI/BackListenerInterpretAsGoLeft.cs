using HMI.UI.IVI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HMI.UI.IVI
{
    /// <summary>
    /// Interprets the back key press interaction as a press to the left
    /// </summary>
    [RequireComponent(typeof(CanvasGroup))]
    public class BackListenerInterpretAsGoLeft : MonoBehaviour
    {
        /// <summary>
        /// Input controller
        /// </summary>
        public IVIInputController InputController;

        /// <summary>
        /// Respond to back key press
        /// </summary>
        public void OnBackPress()
        {
            if (isActiveAndEnabled && GetComponent<CanvasGroup>().interactable)
            {
                InputController.NavigateLeft();
            }
        }
    }
}
