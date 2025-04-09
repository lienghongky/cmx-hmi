using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HMI.Utilities
{

    [RequireComponent(typeof(Text))]
    public class FPSCounter : MonoBehaviour
    {

        Text text;

        public int
            numberOfFramesForAverage = 30; //how many frames should the fps counter look back to show the average?

        public string numberFormatting = "0.0";
        public float[] frameTimes;
        int currentFrameIndex = 0;
        bool hasReachedEnoughFramesForAverage;

        //default text
        //current
        string curMs = "-";

        string curFps = "-";

        //average
        string avgMs = "-";

        string avgFps = "-";

        //slowest
        string sMs = "-";

        string sFps = "-";

        //fastest
        string fMs = "-";
        string fFps = "-";

        void Start()
        {
            text = GetComponent<Text>();
            frameTimes = new float[numberOfFramesForAverage];
        }

        void Update()
        {
            //add current frame time to list
            if (currentFrameIndex >= numberOfFramesForAverage - 1)
            {
                currentFrameIndex = 0;
            }
            else
            {
                currentFrameIndex++;
            }

            frameTimes[currentFrameIndex] = Time.deltaTime;

            //calculate average
            float sum = 0;
            float avg = 0;
            for (int i = 0; i < numberOfFramesForAverage; i++)
            {
                sum += frameTimes[i];
            }

            avg = sum / numberOfFramesForAverage;
            //get slowest
            float slowest = 0.0f;
            for (int i = 0; i < numberOfFramesForAverage; i++)
            {
                if (frameTimes[i] > slowest)
                {
                    slowest = frameTimes[i];
                }
            }

            //get fastest
            float fastest = Mathf.Infinity;
            for (int i = 0; i < numberOfFramesForAverage; i++)
            {
                if (frameTimes[i] < fastest)
                {
                    fastest = frameTimes[i];
                }
            }

            //prepare values
            //current
            curMs = (Time.deltaTime * 1000.0f).ToString(numberFormatting);
            curFps = (1.0 / Time.deltaTime).ToString(numberFormatting);
            if (!hasReachedEnoughFramesForAverage)
            {
                if (currentFrameIndex >= numberOfFramesForAverage - 1)
                {
                    hasReachedEnoughFramesForAverage = true;
                }
            }
            else
            {
                //average
                avgMs = (avg * 1000.0f).ToString(numberFormatting);
                avgFps = (1.0 / avg).ToString(numberFormatting);
                //slowest
                sMs = (slowest * 1000.0f).ToString(numberFormatting);
                sFps = (1.0 / slowest).ToString(numberFormatting);
                //fastest
                fMs = (fastest * 1000.0f).ToString(numberFormatting);
                fFps = (1.0 / fastest).ToString(numberFormatting);
            }

            text.text =
                "\t\tcurrent\taverage\tslowest\tfastest"
                +
                "\n ms \t" + curMs + "\t\t" + avgMs + "\t\t" + sMs + "\t\t" + fMs
                +
                "\n fps \t" + curFps + "\t\t" + avgFps + "\t\t" + sFps + "\t\t" + fFps
                ;
        }
    }

}