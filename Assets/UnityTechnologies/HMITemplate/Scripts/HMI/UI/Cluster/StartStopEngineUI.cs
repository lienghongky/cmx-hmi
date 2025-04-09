using HMI.Vehicles.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HMI.UI.Cluster
{
    /// <summary>
    /// Handles the start/stop engine ui components
    /// </summary>
    public class StartStopEngineUI : MonoBehaviour
    {
        /// <summary>
        /// Animator for the start/stop animation
        /// </summary>
        public Animator EngineStartStopAnimator;       
        
        /// <summary>
        /// Unity Start callback
        /// </summary>
        private void Start()
        {
            var engine = VehicleService.GetEngine();
            engine.EngineStateChanged.AddListener(OnEngineStartStopChanged);

            if (!engine.IsEngineOn)
            {
                EngineStartStopAnimator.SetBool("Activate", true);
            }
        }

        /// <summary>
        /// Unity Destroy callback
        /// </summary>
        private void OnDestroy()
        {
            var engine = VehicleService.GetEngine();
            if (engine != null)
            {
                engine.EngineStateChanged.RemoveListener(OnEngineStartStopChanged);
            }
        }

        /// <summary>
        /// Engine start/stop changed
        /// </summary>
        public void OnEngineStartStopChanged()
        {
            var engine = VehicleService.GetEngine();
            
            if(engine.IsEngineOn)
            {
                ShowEngineOnUI();
            }
            else
            {
                ShowEngineOffUI();
            }
        }

        /// <summary>
        /// Show engine off ui
        /// </summary>
        private void ShowEngineOffUI()
        {
            EngineStartStopAnimator.SetBool("Activate", true);
        }

        /// <summary>
        /// Show engine on ui
        /// </summary>
        private void ShowEngineOnUI()
        {
            EngineStartStopAnimator.SetBool("Deactivate", true);
        }
    }
}
