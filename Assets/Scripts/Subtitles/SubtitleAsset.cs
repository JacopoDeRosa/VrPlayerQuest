using System;
using UnityEngine;

namespace Subtitles
{
    [Serializable]
    public class SubtitleAsset
    {
        public SubtitleDescriptor[] subtitles;

        public SubtitleAsset()
        {
            subtitles = new SubtitleDescriptor[2];
        }
        
        public void ResetActivation()
        {
            foreach (var descriptor in subtitles)
            {
                descriptor.SetActivated(false);
            }
        }

    }
}