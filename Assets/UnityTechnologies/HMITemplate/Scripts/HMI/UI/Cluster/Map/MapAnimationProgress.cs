using UnityEngine;

namespace HMI.UI.Cluster.Map
{
    /// <summary>
    /// The vehicle on the map is moving based on a path-based animation
    /// The route itself has a material that shows active/inactive colors
    /// based on how far the vehicle has progress along the map
    /// This script applies the progress to the route based on how far
    /// in the animation the vehicle is
    /// </summary>
    public class MapAnimationProgress : MonoBehaviour
    {
        /// <summary>
        /// The animator that is animating the vehicle along the path
        /// </summary>
        public Animator VehicleAnimator;

        /// <summary>
        /// The material of the route that will be updated
        /// </summary>
        public Material RouteMaterial;

        /// <summary>
        /// Unity update event
        /// </summary>
        private void Update()
        {
            var animState = VehicleAnimator.GetCurrentAnimatorStateInfo(0);
            var currentTime = animState.normalizedTime % 1;
            RouteMaterial.SetFloat("_Progress", currentTime);
        }
    }
}
