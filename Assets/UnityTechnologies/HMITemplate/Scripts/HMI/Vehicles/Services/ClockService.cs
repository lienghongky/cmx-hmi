using HMI.Vehicles.Data;
using HMI.Vehicles.Data.Interfaces;
using UnityEngine;

namespace HMI.Vehicles.Services
{
    /// <summary>
    /// Singleton implementation of the Clock
    /// </summary>
    public class ClockService : MonoBehaviour
    {
        public ClockData Clock;
        private static IClock Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = Clock;
            }
        }
        public static IClock GetClock()
        {
            return Instance;
        }
    }
}
