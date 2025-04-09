using HMI.Vehicles.Behaviours.Base;
using HMI.Vehicles.Services;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using UnityEngine;

namespace HMI.Clusters.UI.Graph
{
    /// <summary>
    /// Draws a cluster graph that shows the energy consumption of the vehicle
    /// </summary>
    public class ClusterGraph : MonoBehaviour
    {
        /// <summary>
        /// Source information to plot on the graph
        /// </summary>
        private EnergyConsumptionLoggerBase LoggerDataSource;

        /// <summary>
        /// The line of the graph
        /// </summary>
        public LineRenderer LineRenderer;

        /// <summary>
        /// A second line that acts as a shadow of the main line
        /// </summary>
        public LineRenderer ShadowLineRenderer;

        /// <summary>
        /// Prefab for the dots of the graph
        /// </summary>
        public GameObject DotPrefab;

        /// <summary>
        /// Line prefab for the axis grid lines
        /// </summary>
        public GameObject LinePrefab;

        /// <summary>
        /// Label prefab for the labels on the axis grid lines
        /// </summary>
        public GameObject LabelPrefab;

        /// <summary>
        /// Gradient sprite for the small lines
        /// </summary>
        public Sprite LineGradientSprite;

        /// <summary>
        /// Sprite for the small axis lines
        /// </summary>
        public Sprite AxisLineSprite;

        /// <summary>
        /// Color for the big axis lines
        /// </summary>
        public Color HorizontalAxisBigLineColor = Color.white;

        /// <summary>
        /// Color for the small axis lines
        /// </summary>
        public Color HorizontalAxisSmallLineColor = Color.white;

        /// <summary>
        /// Color for the axis labels
        /// </summary>
        public Color LabelColor = Color.white;

        /// <summary>
        /// Color for the bottom axis line
        /// </summary>
        public Color AxisBottomLineColor = Color.white;

        /// <summary>
        /// Color for the current energy usage label
        /// </summary>
        public Color ColorEnergyUsageLabelColor = Color.black;

        /// <summary>
        /// Color for the current energy usage background
        /// </summary>
        public Color ColorEnergyUsageBackgroundColor = Color.white;

        /// <summary>
        /// Indicator of the current energy usage shown at the front of the graph
        /// </summary>
        public GameObject CurrentEnergyUsageIndicator;

        /// <summary>
        /// Width of the graph
        /// </summary>
        public float Width;

        /// <summary>
        /// Height of the graph
        /// </summary>
        public float Height;

        /// <summary>
        /// Maximum input value for the height of the graph
        /// </summary>
        public float MaxValue = 30f;

        /// <summary>
        /// Minimum input value for the height of the graph
        /// </summary>
        public float MinValue = -20f;

        /// <summary>
        /// Width of the
        /// </summary>
        public float IndicationLineWidth = 0.03f;

        /// <summary>
        /// Width at the start of the graph line
        /// </summary>
        public float LineStartWidth = 0.02f;

        /// <summary>
        /// Width at the end of the graph line
        /// </summary>
        public float LineEndWidth = 0.02f;


        /// <summary>
        /// Color of this ui element (using m_ to match unity spriterenderer property names)
        /// </summary>
        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "using m_ to match unity spriterenderer property names")]
        public Color m_Color;

        /// <summary>
        /// Gradient used to color the dots on the line
        /// </summary>
        public Gradient DotGradient;

        /// <summary>
        /// Gradient uses to color the graph line
        /// </summary>
        public Gradient LineGradient;

        /// <summary>
        /// Gradient used to color the graph shadow line
        /// </summary>
        public Gradient ShadowLineGradient;

        /// <summary>
        /// Bottom line of the axis
        /// </summary>
        public SpriteRenderer AxisBottomLine;

        /// <summary>
        /// Number of dots/axis shown on the graph
        /// </summary>
        public int NumDots = 10;

        /// <summary>
        /// Size for the small dots on the graph line
        /// </summary>
        public float SmallDotScale = 0.05f;

        /// <summary>
        /// Size for the large dots on the graph line
        /// </summary>
        public float LargeDotScale = 0.1f;

        /// <summary>
        /// how far below the graph the axis is presented
        /// </summary>
        private readonly float AxisOffset = 0.2f;

        /// <summary>
        /// How far below the graph the labels of the axis are presented
        /// </summary>
        private readonly float LabelOffset = .85f;

        /// <summary>
        /// How far in the past in Km the graphs shows information
        /// </summary>
        private readonly float DisplayDistance = 2f;

        /// <summary>
        /// Number of segments on the graph line
        /// </summary>
        private readonly int NumSegments = 100;

        /// <summary>
        /// The curve contains the data from the logger and is used as a spline interpolator
        /// </summary>
        private readonly AnimationCurve InputCurve = new AnimationCurve();

        /// <summary>
        /// Pool of axis grid lines connected to the dots
        /// </summary>
        private readonly List<GameObject> GridLinePool = new List<GameObject>();

        /// <summary>
        /// Pool of dots on the graph line
        /// </summary>
        private readonly List<GameObject> DotPool = new List<GameObject>();

        /// <summary>
        /// Pool of axis lines
        /// </summary>
        private readonly List<SpriteRenderer> AxisLinePool = new List<SpriteRenderer>();

        /// <summary>
        /// Pool of axis labels
        /// </summary>
        private readonly List<TMPro.TextMeshPro> AxisLabelsPool = new List<TMPro.TextMeshPro>();

        /// <summary>
        /// Background of the current energy usage.
        /// </summary>
        private List<SpriteRenderer> CurrentEnergyUsageBackgroundRenderers;

        /// <summary>
        /// Label of the indicator
        /// </summary>
        private TMPro.TextMeshPro CurrentEnergyUsageLabel;

        /// <summary>
        /// Unity Awake event
        /// </summary>
        private void Awake()
        {
            LoggerDataSource = VehicleService.GetEnergyConsumptionLogger();
            CreateAxis();
            CreateDotPool();
            CreateLinePool();
            CurrentEnergyUsageLabel = CurrentEnergyUsageIndicator.GetComponentInChildren<TMPro.TextMeshPro>();
            CurrentEnergyUsageBackgroundRenderers = CurrentEnergyUsageIndicator.GetComponentsInChildren<SpriteRenderer>().ToList();
        }

        /// <summary>
        /// Unity Update Event
        /// </summary>
        private void Update()
        {
            Render();
        }

        /// <summary>
        /// Create the axis lines, including the labels
        /// </summary>
        private void CreateAxis()
        {
            var intervalBetweenDots = DisplayDistance / (NumDots - 1);

            for (var i = 0; i < NumDots; i++)
            {
                var relativePosition = (float)i / (NumDots - 1);
                var currentDistance = i * intervalBetweenDots;
                var isSmallLine = IsSmallAxisLine(i);
                var axisLinePosition = new Vector3(relativePosition * Width, -AxisOffset, 0f);

                CreateAxisLine(isSmallLine, axisLinePosition);

                if (!isSmallLine)
                {
                    var labelPosition = new Vector3(relativePosition * Width, -LabelOffset, 0f);
                    var labelValue = -(DisplayDistance - currentDistance) * 100;
                    CreateAxisLabel(labelPosition, labelValue.ToString("N0"));
                }
            }
        }

        /// <summary>
        /// Determine if the axis line is a small line
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private bool IsSmallAxisLine(int index)
        {
            return index % 2 == 1;
        }

        /// <summary>
        /// Places a small vertical line below the graph that indicates distance travelled
        /// </summary>
        private void CreateAxisLine(bool isSmallLine, Vector3 position)
        {
            var line = Instantiate(LinePrefab);
            line.transform.parent = LineRenderer.transform;
            line.transform.localPosition = position;

            var renderer = line.GetComponentInChildren<SpriteRenderer>();
            renderer.sprite = AxisLineSprite;

            AxisLinePool.Add(renderer);

            if (isSmallLine)
            {
                line.transform.localScale = new Vector3(IndicationLineWidth, 0.1f, IndicationLineWidth);
                renderer.color = HorizontalAxisSmallLineColor;
            }
            else
            {
                line.transform.localScale = new Vector3(IndicationLineWidth, 0.2f, IndicationLineWidth);
                renderer.color = HorizontalAxisBigLineColor;
            }
        }

        /// <summary>
        /// Creates one of the labels on the horizontal axis at the bottom of the graph
        /// </summary>
        private void CreateAxisLabel(Vector3 position, string text)
        {
            var label = Instantiate(LabelPrefab);
            label.transform.SetParent(LineRenderer.transform);
            label.transform.localPosition = position;

            var tmpText = label.GetComponent<TMPro.TextMeshPro>();
            tmpText.color = LabelColor;
            tmpText.text = text;
            AxisLabelsPool.Add(tmpText);
        }

        /// <summary>
        /// Create a pool for the dots on the graph
        /// </summary>
        private void CreateDotPool()
        {
            for (var i = 0; i < NumDots; i++)
            {
                var dot = Instantiate(DotPrefab);
                dot.transform.SetParent(LineRenderer.transform);

                var isSmallDot = i % 2 == 1;

                if (isSmallDot)
                {
                    dot.transform.localScale = new Vector3(SmallDotScale, SmallDotScale, SmallDotScale);
                }
                else
                {
                    dot.transform.localScale = new Vector3(LargeDotScale, LargeDotScale, LargeDotScale);
                }

                DotPool.Add(dot);
            }
        }

        /// <summary>
        /// Create a pool for the vertical axis lines that are connected to the dots
        /// </summary>
        private void CreateLinePool()
        {
            for (var i = 0; i < NumDots; i++)
            {
                var line = Instantiate(LinePrefab);
                line.transform.parent = LineRenderer.transform;
                GridLinePool.Add(line);
            }
        }

        /// <summary>
        /// Render the graph
        /// </summary>
        private void Render()
        {
            // an input curve is filled with the values from the energy usage log
            // the curve is used to take advantage of the spline interpolation
            UpdateCurveWithInputDataFromLog();

            var latestKmTraveled = InputCurve.keys.LastOrDefault();

            UpdateGraphLines(latestKmTraveled.time);
            UpdateGraphDotsAndConnectedLines(latestKmTraveled.time);
            UpdateCurrentEnergyUsage();
            UpdateAxisAlpha();
        }

        /// <summary>
        /// Update the curve with input data from the log
        /// </summary>
        private void UpdateCurveWithInputDataFromLog()
        {
            var entries = LoggerDataSource.LogEntries;
            InputCurve.keys = null;

            foreach (var entry in entries)
            {
                InputCurve.AddKey((float)entry.Distance, -entry.EnergyUsage);
            }
        }

        /// <summary>
        /// Update the lines of the graph
        /// </summary>
        private void UpdateGraphLines(float latestKmTraveled)
        {
            var interval = DisplayDistance / NumSegments;

            LineRenderer.colorGradient = LineGradient;
            LineRenderer.positionCount = NumSegments + 1;
            LineRenderer.startWidth = LineStartWidth;
            LineRenderer.endWidth = LineEndWidth;
            var color = LineRenderer.material.color;
            color.a = m_Color.a;
            LineRenderer.material.color = color;

            ShadowLineRenderer.colorGradient = ShadowLineGradient;
            ShadowLineRenderer.positionCount = NumSegments + 1;
            color = ShadowLineRenderer.material.color;
            color.a = m_Color.a;
            ShadowLineRenderer.material.color = color;

            for (var i = 0; i < NumSegments + 1; i++)
            {
                var distanceTraveledOffset = (NumSegments - i) * interval;
                var kWh = InputCurve.Evaluate(latestKmTraveled - distanceTraveledOffset);

                var x = (float)i / NumSegments * Width;
                var y = (kWh - MinValue) / (MaxValue - MinValue) * Height;
                var position = new Vector3(x, y);

                LineRenderer.SetPosition(i, position);
                ShadowLineRenderer.SetPosition(i, position);
            }
        }

        /// <summary>
        /// Update the label and background that indicates the current energy usage
        /// </summary>
        private void UpdateCurrentEnergyUsage()
        {
            // keys are in kWh/100km
            var latest = InputCurve.keys.LastOrDefault();
            var kWhPerKm = latest.value / 100;
            var whPerKm = kWhPerKm * 1000;

            CurrentEnergyUsageLabel.text = $@"{whPerKm:N0}<font=""DinLight_Glow"" material=""DinLight_NoGlow""><size=60%>Wh/km</size></font>";
            var color = ColorEnergyUsageLabelColor;
            color *= m_Color;
            CurrentEnergyUsageLabel.color = color;

            var backgroundColor = ColorEnergyUsageBackgroundColor;
            backgroundColor *= m_Color;
            foreach (var renderer in CurrentEnergyUsageBackgroundRenderers)
            {
                renderer.color = backgroundColor;
            }
        }

        /// <summary>
        /// Update the alpha of the axis
        /// </summary>
        private void UpdateAxisAlpha()
        {
            var color = LabelColor;
            color.a *= m_Color.a;

            foreach (var label in AxisLabelsPool)
            {
                label.color = color;
            }

            var smallColor = HorizontalAxisSmallLineColor;
            smallColor.a *= m_Color.a;

            var bigColor = HorizontalAxisBigLineColor;
            bigColor.a *= m_Color.a;

            for (var i = 0; i < AxisLinePool.Count; i++)
            {
                if (IsSmallAxisLine(i))
                {
                    AxisLinePool[i].color = smallColor;
                }
                else
                {
                    AxisLinePool[i].color = bigColor;
                }
            }

            color = AxisBottomLineColor;
            color.a *= m_Color.a;
            AxisBottomLine.color = color;
        }

        /// <summary>
        /// Update the dots on the graph and the vertical lines connected to the dots
        /// </summary>
        private void UpdateGraphDotsAndConnectedLines(float latestKmTraveled)
        {
            var interval = DisplayDistance / (NumDots - 1);
            var position = Vector3.zero;

            for (var i = 0; i < NumDots; i++)
            {
                var relativePosition = (float)i / (NumDots - 1);
                var distanceTraveledOffset = (NumDots - 1 - i) * interval;
                var kWh = InputCurve.Evaluate(latestKmTraveled - distanceTraveledOffset);
                var height = (kWh - MinValue) / (MaxValue - MinValue) * Height;

                position = new Vector3(relativePosition * Width, height, 0f);

                UpdateDots(DotPool[i], relativePosition, position);
                UpdateVerticalLineConnectedToDot(GridLinePool[i], position, relativePosition, height + AxisOffset);
            }

            // set the current energy indicator position to the last dot on the line
            CurrentEnergyUsageIndicator.transform.localPosition = position;
        }

        /// <summary>
        /// Update a single dot on the graph
        /// </summary>
        private void UpdateDots(GameObject dot, float relativePosition, Vector3 position)
        {
            dot.transform.localPosition = position;
            var color = DotGradient.Evaluate(relativePosition + 0.1f);
            color.a *= m_Color.a;
            dot.GetComponent<SpriteRenderer>().color = color;
        }

        /// <summary>
        /// Update the presentation of the vertical line that is connected to a dot 
        /// </summary>
        private void UpdateVerticalLineConnectedToDot(GameObject line, Vector3 position, float relativePosition, float height)
        {
            line.transform.localScale = new Vector3(IndicationLineWidth, height, IndicationLineWidth);
            line.transform.localPosition = position;

            var renderer = line.GetComponentInChildren<SpriteRenderer>();
            renderer.sprite = LineGradientSprite;

            var color = renderer.color;
            color.a = (relativePosition * 0.4f + 0.05f) * m_Color.a;
            renderer.color = color;
        }
    }
}
