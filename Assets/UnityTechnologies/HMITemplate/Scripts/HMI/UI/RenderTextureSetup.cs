using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class creates a render texture, sources the data from a camera, and displays it as a UI element.
/// Used for supersampling anti-aliasing on select parts of the screen (like the 3D car)
/// </summary>

namespace HMI.Utilities
{
    public class RenderTextureSetup : MonoBehaviour
    {
        /// <summary>
        /// The camera the Render Texture will recieve its texture data from.
        /// </summary>
        public Camera RenderTextureCamera = null;

        /// <summary>
        /// The Raw Image UI gameobject that will display the Render Texture.
        /// </summary>
        public RawImage targetImage = null;

        /// <summary>
        /// The options available for applying Anti-Aliasing to the Render Texture.
        /// </summary>
        public enum EnumAntiAliasingAmount { None, AntiAliasing2x, AntiAliasing4x }

        /// <summary>
        /// Variable for supersamling settings (increased render texture size).
        /// </summary>
        [Range(0.1f, 2.0f)] public float ResolutionScale = 2.0f;

        /// <summary>
        /// Variable that stores which Anti-Aliasing option is selected.
        /// </summary>
        public EnumAntiAliasingAmount AntiAliasing = EnumAntiAliasingAmount.AntiAliasing2x;

        /// <summary>
        /// Variable used to check against the Anti-Aliasing variable to see if its value has changed during runtime.
        /// </summary>
        private EnumAntiAliasingAmount CurrentAntiAliasing;

        /// <summary>
        /// This is the value that is acuatlly applied to the RenderTexture's Anti-Aliasing value.
        /// </summary>
        private int AntiAliasingAmount = 1;

        /// <summary>
        /// The render texture created at runtime will be stored in this variable.
        /// </summary>
        private RenderTexture RenderTexture = null;

        /// <summary>
        /// Stores what the current screen size is to check if it have changed.
        /// </summary>
        private int CurrentScreenWidth;

        private int CurrentScreenHeight;

        /// <summary>
        /// Stores what the current supersampling settings are to check if they have changed.
        /// </summary>
        private float CurrentResolutionScale;

        /// <summary>
        /// Creates and assign the Render Texture to the appropriate gameobjects.  Stores the current screen width and height values.
        /// </summary>
        private void Start()
        {
            CreateAndAssignRenderTexture();
            CurrentScreenWidth = Screen.width;
            CurrentScreenHeight = Screen.height;
            CurrentResolutionScale = ResolutionScale;
        }

        /// <summary>
        /// Unity Update callback
        /// </summary>
        private void Update()
        {
            UpdateRenderTextureFOV();
        }

        /// <summary>
        /// Updates the render texture if properties have changed during runtime.
        /// </summary>
        private void UpdateRenderTextureFOV()
        {
            if (CurrentScreenWidth != Screen.width
                || CurrentScreenHeight != Screen.height
                || CurrentAntiAliasing != AntiAliasing
                || CurrentResolutionScale != ResolutionScale
               )
            {
                CurrentScreenWidth = Screen.width;
                CurrentScreenHeight = Screen.height;
                CurrentResolutionScale = ResolutionScale;
                RenderTexture.Release();
                CreateAndAssignRenderTexture();
            }
        }

        /// <summary>
        /// Creates a render texture and assigns it to the appropriate gameobjects.
        /// </summary>
        private void CreateAndAssignRenderTexture()
        {
            int width = (int)(Screen.width * ResolutionScale);
            int height = (int)(Screen.height * ResolutionScale);
            //Debug.Log("Creating render target with size: " + width + ", " + height);
            RenderTexture = new RenderTexture(width, height, 16, RenderTextureFormat.DefaultHDR) { name = "Car" };
            UpdateAntiAliasingAmount();
            RenderTexture.antiAliasing = AntiAliasingAmount;
            RenderTexture.filterMode = FilterMode.Bilinear;
            RenderTexture.Create();
            RenderTextureCamera.targetTexture = RenderTexture;
            targetImage.texture = RenderTexture;
        }

        /// <summary>
        /// Sets the appropriate amount of Anti-Aliasing depending on which Anti-Aliasing option is selected.
        /// </summary>
        private void UpdateAntiAliasingAmount()
        {
            switch (AntiAliasing)
            {
                case EnumAntiAliasingAmount.None:
                    AntiAliasingAmount = 1;
                    break;
                case EnumAntiAliasingAmount.AntiAliasing2x:
                    AntiAliasingAmount = 2;
                    break;
                case EnumAntiAliasingAmount.AntiAliasing4x:
                    AntiAliasingAmount = 4;
                    break;
            }

            CurrentAntiAliasing = AntiAliasing;
        }
    }

}