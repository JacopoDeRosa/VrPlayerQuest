using System;
using UnityEngine;

namespace Subtitles
{
    [Serializable]
    public class SubtitleDescriptor
    {
        public string speaker; 
        public string content;
        public Color speakerColor;
        public float timeOfActivation; 
        public float lifeTime; 
        public bool hasActivated;
        


        public void SetActivated(bool active)
        {
            hasActivated = active;
        }

        public bool ShouldActivate(float totalTime)
        {
            return totalTime >= timeOfActivation;
        }

        public bool ShouldDie(float totalTime)
        {
            return totalTime >= timeOfActivation + lifeTime;
        }
    }
}