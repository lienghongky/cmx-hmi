using UnityEngine;

namespace HMI.Vehicles.Data
{
    /// <summary>
    /// Drag data of the vehicle, the drag curve determines how much drag is applied to the vehicle at a specified speed
    /// </summary>
    [CreateAssetMenu(fileName = "DragData.asset", menuName = "HMI/Vehicle/Drag", order = 1)]
    public class DragData : ScriptableObject
    {
        /// <summary>
        /// Data how much drag is applied to the vehicle at a certain speed
        /// </summary>
        [Tooltip("Non-normalized data how much drag is applied to the vehicle at a certain speed")]
        public AnimationCurve DragCurve;

        /// <summary>
        /// Calculate the drag based on a provided speed
        /// </summary>
        /// <param name="currentSpeed">Current speed of the vehicle</param>
        /// <param name="dt">Deltatime</param>
        /// <returns>New speed with drag applied</returns>
        public float CalculateDrag(float currentSpeed, float dt)
        {
            var cv = 1f - DragCurve.Evaluate(currentSpeed);
            // over a second the dragspeed would be dragged down to this new speed
            var dragSpeed = cv * currentSpeed;
            return Mathf.Lerp(currentSpeed, dragSpeed, dt);
        }
    }
}
