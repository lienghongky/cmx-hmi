using HMI.Vehicles.Behaviours.Base;
using HMI.Vehicles.Services;
using System.Collections.Generic;
using UnityEngine;

namespace HMI.UI.Cluster.Car
{
    /// <summary>
    /// Lane underneath the vehicle
    /// </summary>
    public class Lane : MonoBehaviour
    {
        /// <summary>
        /// The vehicle used to determine how fast the lane is moving along
        /// </summary>
        private VehicleBase VehicleDataSource;

        /// <summary>
        /// The dot grid on the lane
        /// </summary>
        public SpriteRenderer DotGrid;

        /// <summary>
        /// All the lane lines indicating the edges of the lanes
        /// </summary>
        public List<SpriteRenderer> LaneLines = new List<SpriteRenderer>();

        /// <summary>
        /// Speed lines that give a sense of speed
        /// </summary>
        public List<SpriteRenderer> SpeedLines = new List<SpriteRenderer>();

        /// <summary>
        /// Each speed line material gets a random offset to make the lines less uniform
        /// </summary>
        private readonly List<float> SpeedLineOffsets = new List<float>();

        /// <summary>
        /// current uv position of the dotgrid
        /// </summary>
        private float DotOffset = 0f;

        /// <summary>
        /// how fast the dot grid is moving based on the vehicle speed
        /// </summary>
        private readonly float DotSpeedMultiplier = 0.6f;

        /// <summary>
        /// current uv position of the lane lines and speed lines
        /// </summary>
        private float LaneOffset = 0f;

        /// <summary>
        /// how fast the lane lines are moving based on the vehicle speed
        /// </summary>
        private readonly float LaneSpeedMultiplier = 0.03f;

        /// <summary>
        /// Minimum tiling of the lane/speed line material
        /// Used for stretching the lines when speeding up/slowing down
        /// </summary>
        private readonly float TilingMin = 1f;

        /// <summary>
        /// Maximum tiling of the lane/speed line material
        /// Used for stretching the lines when speeding up/slowing down
        /// </summary>
        private readonly float TilingMax = 3f;

        /// <summary>
        /// The speed at which the lines reach maximum stretch in km/h
        /// </summary>
        private readonly float MaxSpeedStretch = 180f;

        /// <summary>
        /// Color for the speed lines
        /// </summary>
        public Color SpeedLineColor;

        /// <summary>
        /// Color for the lane lines
        /// </summary>
        public Color LaneLineColor;

        /// <summary>
        /// Color for the dot grid
        /// </summary>
        public Color DotGridColor;

        /// <summary>
        /// Alpha for the lane
        /// </summary>
        public float Alpha = 1f;

        /// <summary>
        /// Alpha group for the lane glow
        /// </summary>
        public SpriteGroupAlpha LaneGlow;

        /// <summary>
        /// Unity Awake callback
        /// </summary>
        private void Awake()
        {
            VehicleDataSource = VehicleService.GetVehicle();

            for (var i = 0; i < SpeedLines.Count; i++)
            {
                SpeedLineOffsets.Add(Random.Range(0f, 10f));
            }
        }

        /// <summary>
        /// Unity Update callback
        /// </summary>
        private void Update()
        {
            var speed = VehicleDataSource.CurrentSpeed;
            UpdateDotGrid(speed);
            var tiling = UpdateLane(speed);
            UpdateLaneMaterials(tiling, Mathf.Abs(speed) / MaxSpeedStretch);
            UpdateSpeedLineMaterials(tiling);
            UpdateGlowAlpha();
        }

        /// <summary>
        /// Update the dot grid on the lane
        /// </summary>
        private void UpdateDotGrid(float speed)
        {
            DotOffset += speed * Time.deltaTime * DotSpeedMultiplier;
            DotGrid.material.SetFloat("_Offset", DotOffset);

            var color = DotGridColor;
            color.a *= Alpha;

            DotGrid.color = color;
        }

        /// <summary>
        /// Update Lane offset and tiling
        /// </summary>
        private float UpdateLane(float speed)
        {
            // the speed lines and lane lines stretch a bit as the car is speeding up
            // this is determines by the tilng
            var tiling = Mathf.Abs(speed);
            tiling = Mathf.SmoothStep(TilingMax, TilingMin, tiling / MaxSpeedStretch);
            var relative = tiling / TilingMax;

            // offset the lane based on the vehicle speed
            LaneOffset += speed * Time.deltaTime * LaneSpeedMultiplier * relative;
            return tiling;
        }

        /// <summary>
        /// Update Speed Line Materials
        /// </summary>
        private void UpdateSpeedLineMaterials(float tiling)
        {
            var color = SpeedLineColor;
            color.a *= Alpha;

            for (var i = 0; i < SpeedLines.Count; i++)
            {
                var speedLine = SpeedLines[i];
                speedLine.material.SetFloat("_Offset", LaneOffset + SpeedLineOffsets[i]);
                speedLine.material.SetVector("_Tiling", new Vector4(1f, tiling));
                speedLine.color = color;
            }
        }

        /// <summary>
        /// Update Lane Materials
        /// </summary>
        private void UpdateLaneMaterials(float tiling, float blur)
        {
            var color = LaneLineColor;
            color.a *= Alpha;

            foreach (var laneline in LaneLines)
            {
                laneline.material.SetFloat("_Offset", LaneOffset);
                laneline.material.SetVector("_Tiling", new Vector4(1f, tiling));
                laneline.material.SetFloat("_Blur", blur);
                laneline.color = color;
            }
        }

        /// <summary>
        /// Update the alpha of the glows on the lane, used for fade in/out
        /// </summary>
        private void UpdateGlowAlpha()
        {
            LaneGlow.Alpha = Alpha;
        }
    }
}
