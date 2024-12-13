using HMI.ChargingStations.Behaviours;
using HMI.Vehicles.Behaviours.Base;
using HMI.Vehicles.Data;
using Util;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HMI.Vehicles.Behaviours
{
    /// <summary>
    /// Battery of an electric vehicle
    /// </summary>
    [RequireComponent(typeof(VehicleBase))]
    [RequireComponent(typeof(ElectricVehicleDataProvider))]
    public class Battery : BatteryBase
    {
        /// <summary>
        /// The electric vehicle this battery is installed in
        /// </summary>
        private VehicleBase Vehicle;

        /// <summary>
        /// The profile/configuration of the battery
        /// </summary>
        private BatteryData BatteryData;

        /// <summary>
        /// The energy consumption profile of the vehicle
        /// </summary>
        private EnergyConsumptionData EnergyConsumptionData;

        /// <summary>
        /// Internal charging status of the battery
        /// </summary>
        private BatteryChargingStatus ChargingStatusInternal = new BatteryChargingStatus();

        /// <summary>
        /// The charging status of the vehicle
        /// </summary>
        public override BatteryChargingStatus ChargingStatus
        {
            get { return ChargingStatusInternal; }
        }

        /// <summary>
        /// The battery is empty
        /// </summary>
        public override bool IsEmpty
        {
            get { return ChargingStatusInternal.ChargingState == BatteryChargingStateType.Empty; }
        }

        /// <summary>
        /// The battery is full
        /// </summary>
        public override bool IsFull
        {
            get { return ChargingStatusInternal.ChargingState == BatteryChargingStateType.FullyCharged; }
        }

        /// <summary>
        /// All the influencer that can either charge or consume from the battery
        /// </summary>
        private List<EnergyInfluencer> EnergyInfluencers = new List<EnergyInfluencer>();

        /// <summary>
        /// Returns the current battery level within [0.0..1.0] (range is inclusive) (Read Only).
        /// </summary>
        public override float CurrentRelativeBatteryLevel
        {
            get { return CurrentBatteryLevel / BatteryData.Capacity; }
        }

        /// <summary>
        /// Internal field for the readonly property
        /// </summary>
        private float CurrentBatteryLevelInternal;

        /// <summary>
        /// Returns the current battery level in Kwh (Read Only).
        /// </summary>
        public override float CurrentBatteryLevel { get { return CurrentBatteryLevelInternal; } }

        /// <summary>
        /// Returns the maximum range if the battery would be fully charged (Read Only).
        /// </summary>
        public override float MaxRange { get { return EnergyConsumptionData.HistoricalKmPerKwh * BatteryData.Capacity; } }

        /// <summary>
        /// Returns the range of the battery based on its current charge (Read Only).
        /// </summary>
        public override float RemainingRange { get { return EnergyConsumptionData.HistoricalKmPerKwh * CurrentBatteryLevel; } }

        /// <summary>
        /// Internal field for the readonly property
        /// </summary>
        private float CurrentEnergyUsageInternal;

        /// <summary>
        /// Retrieve the current battery usage in Kwh (negative means consumption, positive production).
        /// </summary>
        public override float CurrentEnergyUsage
        {
            get { return CurrentEnergyUsageInternal; }
        }

        /// <summary>
        /// Internal field for the readonly property
        /// </summary>
        private float CurrentEnergyProductionInternal;

        /// <summary>
        /// Retrieve the current battery energy producers in Kwh.
        /// </summary>
        public override float CurrentEnergyProduction
        {
            get { return CurrentEnergyProductionInternal; }
        }

        /// <summary>
        /// Internal field for the readonly property
        /// </summary>
        private float CurrentEnergyConsumptionInternal;

        /// <summary>
        /// Retrieve the current battery energy consumers in Kwh.
        /// </summary>
        public override float CurrentEnergyConsumption
        {
            get { return CurrentEnergyConsumptionInternal; }
        }

        /// <summary>
        /// Plug the battery into a charging station
        /// </summary>
        public override void PlugIn(ChargingStation station)
        {
            if (EnergyInfluencers.Contains(station))
            {
                return;
            }

            ChargingStatusInternal.IsPluggedIn = true;
            EnergyInfluencers.Add(station);
        }

        /// <summary>
        /// Unplug the battery from a charging station
        /// </summary>
        public override void Unplug(ChargingStation station)
        {
            if (!EnergyInfluencers.Contains(station))
            {
                return;
            }

            ChargingStatusInternal.IsPluggedIn = false;
            EnergyInfluencers.Remove(station);
        }

        /// <summary>
        /// Unity Awake
        /// </summary>
        private void Awake()
        {
            var dataProvider = GetComponent<ElectricVehicleDataProvider>();
            EnergyConsumptionData = dataProvider.ElectricVehicleData.EnergyConsumption;
            BatteryData = dataProvider.ElectricVehicleData.Battery;
            Vehicle = GetComponent<VehicleBase>();
            EnergyInfluencers = GetComponentsInChildren<EnergyInfluencer>().ToList();

            CurrentBatteryLevelInternal = BatteryData.StartCapacity;

            if (CurrentBatteryLevel > BatteryData.Capacity)
            {
                CurrentBatteryLevelInternal = BatteryData.Capacity;
            }
        }

        /// <summary>
        /// Unity Update
        /// </summary>
        private void Update()
        {
            UpdateEnergyInfluencers();
            UpdateBatteryStatus();
        }

        /// <summary>
        /// Update the battery level by applying any influencers
        /// </summary>
        private void UpdateEnergyInfluencers()
        {
            var totalInfluence = 0f;
            var totalConsumers = 0f;
            var totalProducers = 0f;

            foreach (var influencer in EnergyInfluencers)
            {
                var influence = influencer.GetCurrentEnergyInfluence(Vehicle, this);
                totalInfluence += influence;

                if (influence < 0f)
                {
                    totalConsumers -= influence;
                }
                else
                {
                    totalProducers += influence;
                }
            }

            CurrentEnergyUsageInternal = totalInfluence;
            CurrentEnergyProductionInternal = totalProducers;
            CurrentEnergyConsumptionInternal = totalConsumers;

            // consumption is in KWH, convert to seconds
            var kws = UnitConversion.HoursPerSecond * CurrentEnergyUsage;
            CurrentBatteryLevelInternal += kws * EnergyConsumptionData.ConsumptionMultiplier * Time.deltaTime;
            CurrentBatteryLevelInternal = Mathf.Clamp(CurrentBatteryLevel, 0, BatteryData.Capacity);
        }

        /// <summary>
        /// Update the status of the battery
        /// </summary>
        private void UpdateBatteryStatus()
        {
            if (Mathf.Approximately(CurrentBatteryLevel, 0))
            {
                ChargingStatusInternal.ChargingState = BatteryChargingStateType.Empty;
            }
            else if (Mathf.Approximately(CurrentBatteryLevel, BatteryData.Capacity))
            {
                ChargingStatusInternal.ChargingState = BatteryChargingStateType.FullyCharged;
            }
            else if (ChargingStatusInternal.IsPluggedIn)
            {
                ChargingStatusInternal.ChargingState = BatteryChargingStateType.Charging;
            }
            else
            {
                ChargingStatusInternal.ChargingState = BatteryChargingStateType.NotCharging;
            }
        }
    }
}