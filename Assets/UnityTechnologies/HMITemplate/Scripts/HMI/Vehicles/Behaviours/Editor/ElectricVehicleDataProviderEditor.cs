using UnityEditor;
using UnityEngine;

namespace HMI.Vehicles.Behaviours
{
    /// <summary>
    /// Custom editor for the electric vehicle data provider that gives
    /// insight in the data in the project folder in the inspector of the script
    /// </summary>
    [CustomEditor(typeof(ElectricVehicleDataProvider))]
    public class ElectricVehicleDataProviderEditor : Editor
    {
        /// <summary>
        /// Show the drag data details in the inspector
        /// </summary>
        private bool ShowDragData;

        /// <summary>
        /// Show the drag data details in the inspector
        /// </summary>
        private bool ShowEngineData;

        /// <summary>
        /// Show the drag data details in the inspector
        /// </summary>
        private bool ShowBrakeData;

        /// <summary>
        /// Show the transmission data details in the inspector
        /// </summary>
        private bool ShowTransmissionData;

        /// <summary>
        /// Show the odometer data details in the inspector
        /// </summary>
        private bool ShowOdometerData;

        /// <summary>
        /// Show the battery data details in the inspector
        /// </summary>
        private bool ShowBattery;

        /// <summary>
        /// Show the energy consumption data details in the inspector
        /// </summary>
        private bool ShowEnergyConsumption;

        /// <summary>
        /// Show the regenerative braking data details in the inspector
        /// </summary>
        private bool ShowRegenerativeBraking;

        /// <summary>
        /// Show the logger data details in the inspector
        /// </summary>
        private bool ShowLogger;

        /// <summary>
        /// Show adas
        /// </summary>
        private bool ShowAdas;

        /// <summary>
        /// Unity OnInspectorGUI callback
        /// </summary>
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var provider = (ElectricVehicleDataProvider)target;

            if (provider.ElectricVehicleData != null)
            {
                DrawInspector(provider.ElectricVehicleData.Drag, "Drag", ref ShowDragData);
                DrawInspector(provider.ElectricVehicleData.Engine, "Engine", ref ShowEngineData);
                DrawInspector(provider.ElectricVehicleData.Brake, "Brakes", ref ShowBrakeData);
                DrawInspector(provider.ElectricVehicleData.Transmission, "Transmission", ref ShowTransmissionData);
                DrawInspector(provider.ElectricVehicleData.Odometer, "Odometer", ref ShowOdometerData);
                DrawInspector(provider.ElectricVehicleData.Battery, "Battery", ref ShowBattery);
                DrawInspector(provider.ElectricVehicleData.EnergyConsumption, "Energy Consumption", ref ShowEnergyConsumption);
                DrawInspector(provider.ElectricVehicleData.RegenerativeBraking, "Regenerative Braking", ref ShowRegenerativeBraking);
                DrawInspector(provider.ElectricVehicleData.EnergyConsumptionLogger, "Energy Logger", ref ShowLogger);
                DrawInspector(provider.ElectricVehicleData.SpeedControlData, "Adas", ref ShowAdas);
            }
        }

        /// <summary>
        /// Draw inspector for scriptable object
        /// </summary>
        private void DrawInspector(ScriptableObject so, string foldOutLabel, ref bool foldOutState)
        {
            if (so != null)
            {
                if (foldOutState = EditorGUILayout.Foldout(foldOutState, foldOutLabel))
                {
                    var childEditor = CreateEditor(so);
                    childEditor.DrawDefaultInspector();
                }
            }
        }
    }
}
