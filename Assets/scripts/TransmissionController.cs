using UnityEngine;
using UnityEngine.EventSystems;
using HMI.Vehicles.Services;
using System.Collections.Generic;
using UnityEngine.UI;

namespace HMI.Vehicles.Behaviours
{

    public class TransmissionController : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private VehicleController vehicleController;
        public GameObject gearContainer;
        public List<GameObject> buttons; 
    
        void Start()
        {
            vehicleController = GameObject.FindObjectOfType<VehicleController>();

            if (vehicleController == null)
            {
                Debug.LogError("VehicleController not found in the scene.");
            }
            foreach (GameObject button in buttons)
            {
                button.GetComponent<Button>().onClick.AddListener(() => {
                    ShiftGearWithButton(button);
                    Invoke("hideGearContainer", 1f);
                });
            }
            
        }


        void Update()
        {

            if (this.gearContainer?.activeInHierarchy == false){
                // Handle geer update
                handleGeerUpdate();
            }

        }

        void hideGearContainer(){
           this.gearContainer?.SetActive(false);
        }
        void handleGeerUpdate(){
            if (vehicleController == null) return;
            foreach (GameObject button in buttons)
            {
        
                if(button.name == vehicleController.CurrentGear.Gear){
                    button.SetActive(false);
                }else{
                    button.SetActive(true);
                }
            }
        }
        void HandleInputDown(Vector3 inputPosition)
        {

            gearContainer.SetActive(true);





            // // Convert screen position (mouse or touch) to a ray
            // Ray ray = Camera.main.ScreenPointToRay(inputPosition);
            // Debug.Log("Camera.main: "+Camera.main.name);
            // // Draw ray 
            // Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 2f);
            // if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("Cluster")))
            // {
            //     Debug.Log("Hit detected"+hit.transform.name);
            //     // Check if the hit object is a child of the TransmissionController
            //     Transform hitTransform = hit.transform;
            //     if (hitTransform.parent == transform)
            //     {
            //         // Set the gear based on the child's name
            //         ShiftGear(hitTransform.name);
            //     }
            // }else{
            //     Debug.Log("No hit detected");
            // }
        }

        void ShiftGear(string gearName)
        {
            if (vehicleController == null) {
                Debug.LogError("VehicleController not found in the scene.");
                return;
            }
            if (gearName != "D"){
                VehicleController.AdasCancel();
            }
            

            vehicleController.SetGear(gearName);

            Debug.Log($"Shifted to gear: {gearName}");
        }

        public void ShiftGearWithButton(GameObject button)
        {
            ShiftGear(button.name);
        }
    }

}