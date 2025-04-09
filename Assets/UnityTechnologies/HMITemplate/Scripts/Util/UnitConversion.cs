namespace Util
{
    /// <summary>
    /// Utility methods for unit conversions
    /// </summary>
    public static class UnitConversion
    {
        /// <summary>
        /// How many celsius units are in a single unit of fahrenheit
        /// </summary>
        public const float CelsiusPerFahrenheit = 5f / 9f;

        /// <summary>
        /// How many fahrenheit units are in a single unit of celsius
        /// </summary>
        public const float FahrenheitPerCelsius = 1f / CelsiusPerFahrenheit;

        /// <summary>
        /// Seconds per hour
        /// </summary>
        public const int SecondsPerHours = 3600;

        /// <summary>
        /// Number of horus in a second
        /// </summary>
        public const float HoursPerSecond = 1f / SecondsPerHours;

        /// <summary>
        /// Kilometer to mile
        /// </summary>
        private const float KmToMi = 1f / MiToKm;

        /// <summary>
        /// Mile to kilometer
        /// </summary>
        private const float MiToKm = 1.609344f;

        /// <summary>
        /// Converts a Kilometers value to the same value in Miles
        /// </summary>
        public static float KilometersToMiles(float km)
        {
            return km * KmToMi;
        }

        /// <summary>
        /// Converts a Miles value to the same value in Kilometers
        /// </summary>
        public static float MilesToKilometers(float mi)
        {
            return mi * MiToKm;
        }

        /// <summary>
        /// Converts a temperature in Fahrenheit to the same value in Celsius
        /// </summary>
        public static float FahrenheitToCelsius(float f)
        {
            return (f - 32f) * CelsiusPerFahrenheit;
        }

        /// <summary>
        /// Converts a temperature in Celsius to the same value in Fahrenheit
        /// </summary>
        public static float CelsiusToFahrenheit(float c)
        {
            return c * FahrenheitPerCelsius + 32f;
        }
    }
}
