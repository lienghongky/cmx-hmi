using HMI.Vehicles.Data;
using UnityEngine;

namespace HMI.Vehicles.Services
{
    /// <summary>
    /// Singleton implementation of the HVAC system
    /// </summary>
    public class HVACService : MonoBehaviour
    {
        public HVACData Data;
        private static HVACData Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = Data;
            }
        }

        public static HVACData GetHVAC()
        {
            return Instance;
        }
    }
}
