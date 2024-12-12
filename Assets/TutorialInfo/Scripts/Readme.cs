using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HMI.Utilities
{


    public class Readme : ScriptableObject
    {
        public Texture2D icon;
        public string title;
        public Section[] sections;
        public bool loadedLayout;
        public Scene[] scenes;

        [Serializable]
        public class Section
        {
            public string heading, text, linkText, url;
        }
    }

}