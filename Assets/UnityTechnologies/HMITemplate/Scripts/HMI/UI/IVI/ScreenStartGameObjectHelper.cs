using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace HMI.UI.IVI
{
    /// <summary>
    /// Selects the start game object when enabled
    /// </summary>
    public class ScreenStartGameObjectHelper : MonoBehaviour
    {
        public Selectable Starter;

        public UnityEvent AdditionalActionsOnEnable;

        /// <summary>
        /// Screen is opened
        /// </summary>
        public void OnEnable()
        {
            if (EventSystem.current != null)
            {
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(Starter.gameObject);
                Starter.Select();
                AdditionalActionsOnEnable.Invoke();
            }
        }

        /// <summary>
        /// Function to change the starter at runtime
        /// </summary>
        /// <param name="NewStarter"></param>
        public void ChangeStarter(Selectable NewStarter)
        {
            Starter = NewStarter;
        }
    }
}
