namespace HMI.Vehicles.Data.Interfaces
{
    /// <summary>
    /// Provider that provides a certain number of levels
    /// </summary>
    public interface ILevelProvider
    {
        /// <summary>
        /// Name of the provider
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Number of levels on the provider
        /// </summary>
        int Levels { get; }

        /// <summary>
        /// Current level of the provider
        /// </summary>
        int CurrentLevel { get; set; }

        /// <summary>
        /// Increase level
        /// </summary>
        void Increase();

        /// <summary>
        /// Decrease level
        /// </summary>
        void Decrease();
    }
}