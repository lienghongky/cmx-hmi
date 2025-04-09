using HMI.ChargingStations.Behaviours;
using HMI.Vehicles.Behaviours.Base;
using UnityEngine;

namespace HMI.Vehicles.Behaviours.Mocks
{
    /// <summary>
    /// Example Mock implementation of a battery of an electric vehicle
    /// </summary>
    public class MockBattery : BatteryBase
    {
        [SerializeField]
        private BatteryChargingStatus MockChargingStatus;
        public override BatteryChargingStatus ChargingStatus => MockChargingStatus;

        [SerializeField]
        private bool MockIsEmpty;
        public override bool IsEmpty => MockIsEmpty;

        [SerializeField]
        private bool MockIsFull;
        public override bool IsFull => MockIsFull;

        [SerializeField]
        private float MockCurrentRelativeBatteryLevel;
        public override float CurrentRelativeBatteryLevel => MockCurrentRelativeBatteryLevel;

        [SerializeField]
        private float MockCurrentBatteryLevel;
        public override float CurrentBatteryLevel => MockCurrentBatteryLevel;

        [SerializeField]
        private float MockMaxRange;
        public override float MaxRange => MockMaxRange;

        [SerializeField]
        private float MockRemainingRange;
        public override float RemainingRange => MockRemainingRange;

        [SerializeField]
        private float MockCurrentEnergyUsage;
        public override float CurrentEnergyUsage => MockCurrentEnergyUsage;

        [SerializeField]
        private float MockCurrentEnergyProduction;
        public override float CurrentEnergyProduction => MockCurrentEnergyProduction;

        [SerializeField]
        private float MockCurrentEnergyConsumption;
        public override float CurrentEnergyConsumption => MockCurrentEnergyConsumption;

        public override void PlugIn(ChargingStation station)
        {
        }

        public override void Unplug(ChargingStation station)
        {
        }
    }
}
