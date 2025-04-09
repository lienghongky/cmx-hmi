using HMI.Clusters.Enums;
using HMI.Units.Data;
using HMI.Vehicles.Data;
using HMI.Vehicles.Services;
using Util;
using UnityEngine;

namespace HMI.UI
{
    /// <summary>
    /// Visualizes the temperature on the label
    /// </summary>
    public class TemperatureLabel : MonoBehaviour
    {
        /// <summary>
        /// What type of temperature to display on the label
        /// </summary>
        public enum TemperatureSourceType
        {
            Outside,
            LeftClimateZoneTemperature,
            RightClimateZoneTemperature
        }

        /// <summary>
        /// Configuration of units (fahrenheit/celsius)
        /// </summary>
        public UnitConfiguration UnitConfiguration;

        /// <summary>
        /// Source of information
        /// </summary>
        private HVACData HvacDataSource;

        /// <summary>
        /// Text to visualize the temperature
        /// </summary>
        public TMPro.TMP_Text Text;

        /// <summary>
        /// Source data for this temperature field
        /// </summary>
        public TemperatureSourceType TemperatureSource;

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
        /// Unity Update Event
        /// </summary>
        private void Update()
        {
            var temperature = GetTemperature();

            if (UnitConfiguration.TemperatureType == TemperatureType.Celsius)
            {
                RenderCelsius(temperature);
            }
            else
            {
                RenderFahrenheit(UnitConversion.CelsiusToFahrenheit(temperature));
            }
        }

        /// <summary>
        /// Get the temperature from the source
        /// </summary>
        private float GetTemperature()
        {
            return TemperatureSource switch
            {
                TemperatureSourceType.Outside => HvacDataSource.TemperatureOutside,
                TemperatureSourceType.LeftClimateZoneTemperature => HvacDataSource.GetClimateZone("LeftClimateZone").Temperature,
                TemperatureSourceType.RightClimateZoneTemperature => HvacDataSource.GetClimateZone("RightClimateZone").Temperature,
                _ => HvacDataSource.TemperatureOutside,
            };
        }

        /// <summary>
        /// Render cluster temperature in celsius
        /// </summary>
        /// <param name="temperature">Temperature in celsius</param>
        private void RenderCelsius(float temperature)
        {
            var txtColorSmall = ColorUtility.ToHtmlStringRGB(Text.color);
            txtColorSmall += "AA";

            temperature = Mathf.Round(temperature);
            Text.text = @$"{temperature}<font=""DinLight_Glow"" material=""DinLight_NoGlow""><size=60%><#{txtColorSmall}>°C</size></font>";
        }

        /// <summary>
        /// Render cluster temperature in fahrenheit
        /// </summary>
        /// <param name="temperature">Temperature in fahrenheit</param>
        private void RenderFahrenheit(float temperature)
        {
            var txtColorSmall = ColorUtility.ToHtmlStringRGB(Text.color);
            txtColorSmall += "AA";

            temperature = Mathf.Round(temperature);
            Text.text = @$"{temperature}<font=""DinLight_Glow"" material=""DinLight_NoGlow""><size=60%><#{txtColorSmall}>°F</size></font>";
        }
    }
}
