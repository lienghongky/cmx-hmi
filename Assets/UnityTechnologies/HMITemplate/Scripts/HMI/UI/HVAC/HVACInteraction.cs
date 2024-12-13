using HMI.Clusters.Enums;
using HMI.Units.Data;
using HMI.Vehicles.Data;
using HMI.Vehicles.Services;
using Util;
using UnityEngine;
using UnityEngine.UI;

namespace HMI.Services
{
    /// <summary>
    /// Ties the UI Interaction to the HVAC Data Source
    /// </summary>
    public class HVACInteraction : MonoBehaviour
    {
        /// <summary>
        /// Left Climate Zone Identifier
        /// </summary>
        private const string LeftClimateZoneId = "LeftClimateZone";

        /// <summary>
        /// Right Climate Zone Identifier
        /// </summary>
        private const string RightClimateZoneId = "RightClimateZone";

        /// <summary>
        /// Configuration of units (fahrenheit/celsius)
        /// </summary>
        public UnitConfiguration UnitConfiguration;

        /// <summary>
        /// Data Source for the HVAC Interaction
        /// </summary>
        private HVACData HvacDataSource;

        /// <summary>
        /// Air In Toggle Button
        /// </summary>
        public Toggle AirIn;

        /// <summary>
        /// Air Recirculate Toggle Button
        /// </summary>
        public Toggle AirRecirculate;

        /// <summary>
        /// Sync Toggle Button
        /// </summary>
        public Toggle Sync;

        /// <summary>
        /// Left Seat Massage Toggle Button
        /// </summary>
        public Toggle LeftSeatVentilation;

        /// <summary>
        /// Right Seat Massage Toggle Button
        /// </summary>
        public Toggle RightSeatVentilation;

        /// <summary>
        /// Unity Awake callback
        /// </summary>
        private void Awake()
        {
            if (HvacDataSource == null)
            {
                HvacDataSource = HVACService.GetHVAC();
            }
        }

        /// <summary>
        /// Increase the left climate zone temperature by one unit
        /// (could be fahrenheit, or celsius, depending on the settings)
        /// </summary>
        public void IncreaseLeftClimateZoneTemperatureByOne()
        {
            var zone = HvacDataSource.GetClimateZone(LeftClimateZoneId);
            zone.IncreaseTemperature(GetOneUnit());
        }

        /// <summary>
        /// Decrease the left climate zone temperature by one unit
        /// (could be fahrenheit, or celsius, depending on the settings)
        /// </summary>
        public void DecreaseLeftClimateZoneTemperatureByOne()
        {
            var zone = HvacDataSource.GetClimateZone(LeftClimateZoneId);
            zone.DecreaseTemperature(GetOneUnit());
        }

        /// <summary>
        /// Increase the right climate zone temperature by one unit
        /// (could be fahrenheit, or celsius, depending on the settings)
        /// </summary>
        public void IncreaseRightClimateZoneTemperatureByOne()
        {
            var zone = HvacDataSource.GetClimateZone(RightClimateZoneId);
            zone.IncreaseTemperature(GetOneUnit());
        }

        /// <summary>
        /// Decrease the right climate zone temperature by one unit
        /// (could be fahrenheit, or celsius, depending on the settings)
        /// </summary>
        public void DecreaseRightClimateZoneTemperatureByOne()
        {
            var zone = HvacDataSource.GetClimateZone(RightClimateZoneId);
            zone.DecreaseTemperature(GetOneUnit());
        }

        /// <summary>
        /// Determine how much a single unit increase means, since the base data is in celsius
        /// </summary>
        private float GetOneUnit()
        {
            if (UnitConfiguration.TemperatureType == TemperatureType.Celsius)
            {
                return 1f;
            }
            else
            {
                return UnitConversion.CelsiusPerFahrenheit;
            }
        }

        /// <summary>
        /// Callback for UI Air Recirculation Button Press
        /// </summary>
        public void AirRecirculationButtonPress()
        {
            if (HvacDataSource.IsRecirculationActivated)
            {
                HvacDataSource.IsRecirculationActivated = false;
                AirRecirculate.isOn = false;
            }
            else
            {
                HvacDataSource.IsRecirculationActivated = true;
                AirRecirculate.isOn = true;
            }
        }

        /// <summary>
        /// Callback for UI Air In Button Press
        /// </summary>
        public void AirInButtonPress()
        {
            if (HvacDataSource.IsAirInActivated)
            {
                HvacDataSource.IsAirInActivated = false;
                AirIn.isOn = false;
            }
            else
            {
                HvacDataSource.IsAirInActivated = true;
                AirIn.isOn = true;
            }
        }

        /// <summary>
        /// Callback for Sync Button Press
        /// </summary>
        public void SyncButtonPress()
        {
            if (HvacDataSource.IsSyncButtonActivated)
            {
                HvacDataSource.IsSyncButtonActivated = false;
                Sync.isOn = false;
            }
            else
            {
                HvacDataSource.IsSyncButtonActivated = true;
                Sync.isOn = true;
            }
        }

        /// <summary>
        /// Callback for Left Seat Massage Button Press
        /// </summary>
        public void LeftSeatMassagePress()
        {
            if (HvacDataSource.IsLeftSeatVentilationActivated)
            {
                HvacDataSource.IsLeftSeatVentilationActivated = false;
                LeftSeatVentilation.isOn = false;
            }
            else
            {
                HvacDataSource.IsLeftSeatVentilationActivated = true;
                LeftSeatVentilation.isOn = true;
            }
        }

        /// <summary>
        /// Callback for Right Seat Massage Button Press
        /// </summary>
        public void RightSeatMassagePress()
        {
            if (HvacDataSource.IsRightSeatVentilationActivated)
            {
                HvacDataSource.IsRightSeatVentilationActivated = false;
                RightSeatVentilation.isOn = false;
            }
            else
            {
                HvacDataSource.IsRightSeatVentilationActivated = true;
                RightSeatVentilation.isOn = true;
            }
        }

        /// <summary>
        /// Update code will check for if there is an gui interaction script, if there is then it will update the Gui graphics
        /// to match the bools selected in the Hvac services editor window.
        /// </summary>
        private void Update()
        {
            if (LeftSeatVentilation.isOn != HvacDataSource.IsLeftSeatVentilationActivated)
            {
                if (LeftSeatVentilation != null)
                {
                    HvacDataSource.IsLeftSeatVentilationActivated = !HvacDataSource.IsLeftSeatVentilationActivated;
                    LeftSeatVentilation.isOn = !LeftSeatVentilation.isOn;
                }
            }

            if (RightSeatVentilation.isOn != HvacDataSource.IsRightSeatVentilationActivated)
            {
                if (RightSeatVentilation != null)
                {
                    HvacDataSource.IsRightSeatVentilationActivated = !HvacDataSource.IsRightSeatVentilationActivated;
                    RightSeatVentilation.isOn = !RightSeatVentilation.isOn;

                }
            }

            if (AirRecirculate.isOn != HvacDataSource.IsRecirculationActivated)
            {
                if (AirRecirculate != null)
                {
                    HvacDataSource.IsRecirculationActivated = !HvacDataSource.IsRecirculationActivated;
                    AirRecirculate.isOn = !AirRecirculate.isOn;
                }
            }

            if (AirIn.isOn != HvacDataSource.IsAirInActivated)
            {
                if (AirIn != null)
                {
                    HvacDataSource.IsAirInActivated = !HvacDataSource.IsAirInActivated;
                    AirIn.isOn = !AirIn.isOn;
                }
            }

            if (Sync.isOn != HvacDataSource.IsSyncButtonActivated)
            {
                if (Sync != null)
                {
                    HvacDataSource.IsSyncButtonActivated = !HvacDataSource.IsSyncButtonActivated;
                    Sync.isOn = !Sync.isOn;
                }
            }
        }
    }

}