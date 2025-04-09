using HMI.Vehicles.Services;
using UnityEditor;

namespace HMI.Vehicles.Behaviours
{
    /// <summary>
    /// Custom inspector for the HVAC Service its main purpose is to show the details of the used
    /// data scriptable objects in the inspector so the user does not have to search in a projectfolder
    /// </summary>
    [CustomEditor(typeof(HVACService))]
    public class HVACServiceEditor : Editor
    {
        /// <summary>
        /// Show the climate zone data details in the inspector
        /// </summary>
        private bool ShowClimateZones;

        /// <summary>
        /// Show the heating data details in the inspector
        /// </summary>
        private bool ShowHeatingSystems;

        /// <summary>
        /// Show the blowers data details in the inspector
        /// </summary>
        private bool ShowBlowers;

        /// <summary>
        /// On Inspector GUI callback
        /// </summary>
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var hvac = (HVACService)target;

            if (hvac.Data != null)
            {
                var editor = CreateEditor(hvac.Data);
                editor.DrawDefaultInspector();

                EditorGUILayout.Space();

                if (ShowClimateZones = EditorGUILayout.Foldout(ShowClimateZones, "Climate Zones"))
                {
                    foreach (var climate in hvac.Data.ClimateZonesData)
                    {
                        var childEditor = CreateEditor(climate);
                        childEditor.DrawDefaultInspector();
                        EditorGUILayout.Space();
                    }
                }

                EditorGUILayout.Space();

                if (ShowHeatingSystems = EditorGUILayout.Foldout(ShowHeatingSystems, "Heating Systems"))
                {
                    foreach (var heatingSystem in hvac.Data.HeatingSystemsData)
                    {
                        var childEditor = CreateEditor(heatingSystem);
                        childEditor.DrawDefaultInspector();
                        EditorGUILayout.Space();
                    }
                }

                EditorGUILayout.Space();

                if (ShowBlowers = EditorGUILayout.Foldout(ShowBlowers, "Blowers"))
                {
                    foreach (var blowers in hvac.Data.BlowersData)
                    {
                        var childEditor = CreateEditor(blowers);
                        childEditor.DrawDefaultInspector();
                        EditorGUILayout.Space();
                    }
                }
            }
        }
    }
}
