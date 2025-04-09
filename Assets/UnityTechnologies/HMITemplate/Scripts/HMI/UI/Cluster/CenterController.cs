using UnityEngine;
using UnityEngine.Playables;

namespace HMI.UI.Cluster
{
    /// <summary>
    /// Controller for the center part of the cluster
    /// </summary>
    public class CenterController : MonoBehaviour
    {
        /// <summary>
        /// Timeline to move the center of the cluster from Adas to Map Mode
        /// </summary>
        public PlayableAsset AdasToMap;
        /// <summary>
        /// Timeline to move the center of the cluster from Map to Adas Mode
        /// </summary>
        public PlayableAsset MapToAdas;

        public PlayableAsset AdasToBEV;

        /// <summary>
        /// Director that plays timelines
        /// </summary>
        public PlayableDirector Director;

        /// <summary>
        /// Is the center of the cluster currently showing the Adas?
        /// </summary>
        private int Adas = 0;

        /// <summary>
        /// Next screen
        /// </summary>
        public void Next()
        {
            Switch();
        }

        /// <summary>
        /// Previous screen
        /// </summary>
        public void Previous()
        {
            Switch(-1);
        }

        public void SwitchTo(int adas){
            this.Adas = adas;
            Switch();
        }

        /// <summary>
        /// Switch Screens
        /// </summary>
        private void Switch(int alpha = 1)
        {
            if (this.Adas < 0) this.Adas = 2;
            if (this.Adas > 2) this.Adas = 0;
            switch (Adas)
            {
                case 0:
                    Adas += alpha;
                    Director.Play(AdasToBEV);
                    break;
                case 1:
                    Adas += alpha;
                    Director.Play(AdasToMap);
                    break;
                case 2:
                    Adas += alpha;
                    Director.Play(MapToAdas);
                    break;
            }

        }
       
    }
}
