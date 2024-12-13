using UnityEditor;

namespace HMI.Vehicles.Behaviours
{
    /// <summary>
    /// Custom Editor for the car systems
    /// </summary>
    [CustomEditor(typeof(CarSystems))]
    public class CarSystemsEditor : Editor
    {
        /// <summary>
        /// Cache of the editor for better performance
        /// </summary>
        private Editor CachedEditor;

        /// <summary>
        /// Unity OnEnable callback
        /// </summary>
        private void OnEnable()
        {
            CachedEditor = null;
        }

        /// <summary>
        /// Unity OnInspectorGUI callback
        /// </summary>
        public override void OnInspectorGUI()
        {
            var carSystem = (CarSystems)target;

            if (carSystem.Data != null)
            {
                if (CachedEditor == null)
                {
                    CachedEditor = CreateEditor(carSystem.Data);
                }

                CachedEditor.DrawDefaultInspector();
            }

            DrawDefaultInspector();
        }
    }
}
