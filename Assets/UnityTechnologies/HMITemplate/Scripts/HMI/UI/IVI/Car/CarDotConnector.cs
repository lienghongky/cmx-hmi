using TMPro;
using UnityEngine;
using Util;

namespace HMI.UI.IVI.Car
{
    /// <summary>
    /// A Car dot connector connects a 2d ui elements to a 3d object on the car
    /// </summary>
    [ExecuteInEditMode]
    public class CarDotConnector : MonoBehaviour
    {
        /// <summary>
        /// Indicates if the dot label will be aligned to the left, 
        /// or to the right of the 3d object it is attached to
        /// </summary>
        public enum DotAlignment
        {
            Left,
            Right
        }

        /// <summary>
        /// Width of the dot label
        /// </summary>
        public float Width = 400f;

        /// <summary>
        /// Indicates if the dot label will be aligned to the left, 
        /// or to the right of the 3d object it is attached to
        /// </summary>
        public DotAlignment Alignment = DotAlignment.Right;

        /// <summary>
        /// Dark background of the dot label
        /// </summary>
        public RectTransform LineDark;

        /// <summary>
        /// Light background of the dot label
        /// </summary>
        public RectTransform LineLight;

        /// <summary>
        /// Text show on the dot label
        /// </summary>
        public RectTransform Text;

        /// <summary>
        /// Unity OnEnable callback
        /// </summary>
        private void OnEnable()
        {
            UpdateObjects();
        }

        /// <summary>
        /// Unity Update callback
        /// </summary>
        private void Update()
        {
            UpdateObjects();
        }

        private void UpdateObjects()
        {
            if (Alignment == DotAlignment.Left)
            {
                Text.pivot = new Vector2(1f, 0.5f);
                LineLight.localScale = new Vector3(1f, 0.7f, 1f);
                LineDark.pivot = new Vector2(1f, 0.5f);

                Text.GetComponent<TMP_Text>().alignment = TextAlignmentOptions.Left;

                SetHorizontalPosition(Text, -50f);
                SetHorizontalPosition(LineLight, -50f);
                SetHorizontalPosition(LineDark, -50f);
            }
            else
            {
                Text.pivot = new Vector2(0f, 0.5f);
                LineLight.localScale = new Vector3(-1f, 0.7f, 1f);
                LineDark.pivot = new Vector2(0f, 0.5f);

                Text.GetComponent<TMP_Text>().alignment = TextAlignmentOptions.Right;

                SetHorizontalPosition(Text, 50f);
                SetHorizontalPosition(LineLight, 50f);
                SetHorizontalPosition(LineDark, 50f);
            }

            if (LineLight != null)
            {
                LineLight.SetWidth(Width);
            }

            if (LineDark != null)
            {
                LineDark.SetWidth(Width);
            }

            if (Text != null)
            {
                Text.SetWidth(Width);
            }
        }

        /// <summary>
        /// Set the horizontal position of the ui element
        /// </summary>
        private void SetHorizontalPosition(RectTransform transform, float position)
        {
            var pos = transform.anchoredPosition;
            pos.x = position;
            transform.anchoredPosition = pos;
        }
    }
}