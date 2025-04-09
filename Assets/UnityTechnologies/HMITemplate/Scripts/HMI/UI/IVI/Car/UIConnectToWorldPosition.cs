using UnityEngine;

namespace HMI.UI.IVI.Car
{
    /// <summary>
    /// Connects a ui element to a world position anchor. 
    /// This way 2d elements can be connected to objects in 3d space.
    /// </summary>
    [ExecuteInEditMode]
    [RequireComponent(typeof(RectTransform))]
    public class UIConnectToWorldPosition : MonoBehaviour
    {
        /// <summary>
        /// World anchor that the ui elements is connected
        /// </summary>
        public Transform WorldAnchor;

        /// <summary>
        /// Camera that is used for the world to screen transformation
        /// </summary>
        public Camera Camera;

        /// <summary>
        /// Transform of the ui element
        /// </summary>
        private RectTransform RectTransform;

        /// <summary>
        /// Parent canvas
        /// </summary>
        private Canvas ParentCanvas;

        /// <summary>
        /// Unity Awake callback
        /// </summary>
        private void Awake()
        {
            ParentCanvas = GetComponentInParent<Canvas>();
            RectTransform = GetComponent<RectTransform>();
            if (Camera == null)
            {
                Debug.Log("No camera set, using main " + this.name);
                Camera = Camera.main;
            }
        }

        /// <summary>
        /// Unity Update callback
        /// </summary>
        private void Update()
        {
            if (WorldAnchor != null && Camera != null)
            {
                var screenPos = Camera.WorldToScreenPoint(WorldAnchor.position);
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    (RectTransform)ParentCanvas.transform, screenPos, ParentCanvas.worldCamera, out var movePos);

                RectTransform.position = ParentCanvas.transform.TransformPoint(movePos);
            }
        }

        /// <summary>
        /// Unity DrawGizmos callback
        /// </summary>
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            if (WorldAnchor != null)
            {
                Gizmos.DrawWireSphere(WorldAnchor.position, 0.25f);
                Gizmos.DrawLine(transform.position, WorldAnchor.position);
            }
        }
    }
}
