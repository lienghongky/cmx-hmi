using System.Linq;
using UnityEngine;

namespace HMI.Vehicles.Data
{
    /// <summary>
    /// Set of car system entries
    /// </summary>
    [CreateAssetMenu(fileName = "Car Systems.asset", menuName = "HMI/Vehicle/Car Systems", order = 1)]
    public class CarSystemsData : ScriptableObject
    {
        public bool IsParkingBrakeOn;
        public bool IsParkingBrakeMalfunctioning;
        public bool IsTirePressureLow;
        public bool IsAnySeatbeltNotFastened;
        public bool IsABSMalfunctioning;
        public bool IsESCActive;
        public bool AreAirbagsActivated;
        public bool IsAnyDoorOrTrunkOpen;
        public bool IsLeftTurningSignalOn;
        public bool IsRightTurningSignalOn;
        public bool AreHazardLightsOn;
        public bool AreFrontFogLightsOn;
        public bool AreParkingLightsOn;
        public bool AreHighBeamHeadlightsOn;
        public bool AreLowBeamHeadlightsOn;
        public bool IsBrakeSystemMalfunctioning;

        public void Switch(string option)
        {
            var field =
                GetType()
                .GetFields()
                .Where(x => x.FieldType == typeof(bool) && x.Name == option.ToString())
                .FirstOrDefault();

            if (field != null)
            {
                var val = (bool)field.GetValue(this);
                val = !val;
                field.SetValue(this, val);
            }
        }
    }
}
