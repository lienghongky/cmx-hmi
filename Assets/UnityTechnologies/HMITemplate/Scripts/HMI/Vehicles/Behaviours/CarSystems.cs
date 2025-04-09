using HMI.Vehicles.Data;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

namespace HMI.Vehicles.Behaviours
{
    /// <summary>
    /// Simple representation of internal car systems that have an on/off state
    /// </summary>
    public class CarSystems : MonoBehaviour
    {
        /// <summary>
        /// Dataset for the car system
        /// </summary>
        public CarSystemsData Data;

        /// <summary>
        /// Parking brake state changed event
        /// </summary>
        public UnityEvent<bool> OnIsParkingBrakeOnChanged;

        /// <summary>
        /// Parking brake malfunctioning state changed event
        /// </summary>
        public UnityEvent<bool> OnIsParkingBrakeMalfunctioningChanged;

        /// <summary>
        /// Tire pressure low state changed event
        /// </summary>
        public UnityEvent<bool> OnIsTirePressureLowChanged;

        /// <summary>
        /// Seatbelt not fastened state changed event
        /// </summary>
        public UnityEvent<bool> OnIsAnySeatbeltNotFastenedChanged;

        /// <summary>
        /// ABS Malfuntioning state changed event
        /// </summary>
        public UnityEvent<bool> OnIsABSMalfunctioningChanged;

        /// <summary>
        /// ESC active state changed event
        /// </summary>
        public UnityEvent<bool> OnIsESCActiveChanged;

        /// <summary>
        /// Airbags activate state changed event
        /// </summary>
        public UnityEvent<bool> OnAreAirbagsActivatedChanged;

        /// <summary>
        /// Any door or trunk open state changed event
        /// </summary>
        public UnityEvent<bool> OnIsAnyDoorOrTrunkOpenChanged;

        /// <summary>
        /// Left turning signal state changed event
        /// </summary>
        public UnityEvent<bool> OnIsLeftTurningSignalOnChanged;

        /// <summary>
        /// Right turning signal state changed event
        /// </summary>
        public UnityEvent<bool> OnIsRightTurningSignalOnChanged;

        /// <summary>
        /// Hazard light state changed event
        /// </summary>
        public UnityEvent<bool> OnAreHazardLightsOnChanged;

        /// <summary>
        /// Front fog light state changed event
        /// </summary>
        public UnityEvent<bool> OnAreFrontFogLightsOnChanged;

        /// <summary>
        /// Parking light state changed event
        /// </summary>
        public UnityEvent<bool> OnAreParkingLightsOnChanged;

        /// <summary>
        /// High beam lights state changed event
        /// </summary>
        public UnityEvent<bool> OnAreHighBeamHeadlightsOnChanged;

        /// <summary>
        /// Low beam light state changed event
        /// </summary>
        public UnityEvent<bool> OnAreLowBeamHeadlightsOnChanged;

        /// <summary>
        /// Brake system malfunction state changed event
        /// </summary>
        public UnityEvent<bool> OnIsBrakeSystemMalfunctioningChanged;

        /// <summary>
        /// Holds the state of each individual boolean of the CarSystemsData (initially loaded through reflection)
        /// </summary>
        private readonly List<bool> ActivationList = new List<bool>();

        /// <summary>
        /// Reference to the field on the CarSystemsData (loaded through reflection)
        /// </summary>
        private readonly List<FieldInfo> DataReferences = new List<FieldInfo>();

        /// <summary>
        /// Ties the boolean on the CarSystemsData to an explicit event on this car systems data file
        /// if the carsystems data file is ever extended with new booleans, then to respond to state changes
        /// an explicit unity event will need to be added. This allows for maximum flexibility while still being
        /// able to define explicit unity events that the user can connect to through the editor
        /// </summary>
        private readonly List<UnityEvent<bool>> EventReferences = new List<UnityEvent<bool>>();

        /// <summary>
        /// Unity script is awake
        /// </summary>
        private void Awake()
        {
            // through reflection load all boolean fields into the data references list
            // copy their state on the data object in the project folder to the activation list
            if (Data != null)
            {
                var booleans = Data.GetType().GetFields().Where(x => x.FieldType == typeof(bool));

                foreach (var boolField in booleans)
                {
                    ActivationList.Add((bool)boolField.GetValue(Data));
                    DataReferences.Add(boolField);
                }
            }

            var events = GetType().GetFields().Where(x => x.FieldType == typeof(UnityEvent<bool>));

            for (var i = 0; i < DataReferences.Count; i++)
            {
                var eventFound = false;

                foreach (var @event in events)
                {
                    var nameData = DataReferences[i].Name;
                    var matchingEventName = "On" + nameData + "Changed";
                    var eventName = @event.Name;

                    if (eventName == matchingEventName)
                    {
                        EventReferences.Add((UnityEvent<bool>)@event.GetValue(this));
                        eventFound = true;
                        break;
                    }
                }

                if (!eventFound)
                {
                    Debug.LogError("mismatch between supported carsystem events and data fields");
                }
            }
        }

        /// <summary>
        /// Unity Start callback
        /// </summary>
        private void Start()
        {
            for (var i = 0; i < EventReferences.Count; i++)
            {
                var val = (bool)DataReferences[i].GetValue(Data);
                EventReferences[i].Invoke(val);
            }
        }

        /// <summary>
        /// Unity Update callback
        /// </summary>
        private void Update()
        {
            for (var i = 0; i < ActivationList.Count; i++)
            {
                var val = (bool)DataReferences[i].GetValue(Data);

                if (ActivationList[i] != val)
                {
                    ActivationList[i] = val;
                    EventReferences[i].Invoke(val);
                }
            }
        }
    }
}
