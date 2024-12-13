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
    /// Activates/deactivates set of objects based on selection
    /// </summary>
    public class SelectedHelper : MonoBehaviour, ISelectHandler, IDeselectHandler
    {
        /// <summary>
        /// Set of objects to activate when object is selected
        /// </summary>
        public List<GameObject> SelectSet = new List<GameObject>();

        /// <summary>
        /// Set of objects to deactivate when object is selected
        /// </summary>
        public List<GameObject> DeselectSet = new List<GameObject>();

        /// <summary>
        /// Extra Actions to call when a toggle's isOn is set to true
        /// </summary>
        public UnityEvent AddtionalActionOnToggleIsOn;

        /// <summary>
        /// Selectable selected
        /// </summary>
        public UnityEvent Selected;

        /// <summary>
        /// Selectable deselected
        /// </summary>
        public UnityEvent Deselected;

        /// <summary>
        /// Is the control selected?
        /// </summary>
        public bool IsSelected { get; private set; } = false;

        /// <summary>
        /// Unity Awake callback
        /// </summary>
        private void Awake()
        {
            Deselect();
        }

        /// <summary>
        /// Unity OnEnable callback
        /// </summary>
        private void OnEnable()
        {
            if (EventSystem.current != null)
            {
                if (EventSystem.current.currentSelectedGameObject != gameObject)
                {
                    Deselect();
                }
                else
                {
                    Select();
                }
            }
        }

        /// <summary>
        /// Selectable selected callback
        /// </summary>
        public void OnSelect(BaseEventData data)
        {
            Select();
        }

        /// <summary>
        /// Selectable deselected callback
        /// </summary>
        public void OnDeselect(BaseEventData data)
        {
            Deselect();
        }

        /// <summary>
        /// Select
        /// </summary>
        private void Select()
        {
            IsSelected = true;
            SetState(SelectSet, true);
            SetState(DeselectSet, false);
            Selected.Invoke();
        }

        /// <summary>
        /// Deselect
        /// </summary>
        private void Deselect()
        {
            IsSelected = false;
            SetState(SelectSet, false);
            SetState(DeselectSet, true);
            Deselected.Invoke();
        }

        /// <summary>
        /// Set the state of a set of objects
        /// </summary>
        private void SetState(IEnumerable<GameObject> set, bool state)
        {
            foreach (var obj in set)
            {
                obj.SetActive(state);
            }
        }

        /// <summary>
        /// Invoke additional actions, only if the supplied toggle component is On
        /// </summary>
        /// <param name="ToggleComponent"></param>
        public void AddtionActionsOnToggle(Toggle ToggleComponent)
        {
            if (ToggleComponent.isOn)
            {
                AddtionalActionOnToggleIsOn.Invoke();
            }
        }
    }
}
