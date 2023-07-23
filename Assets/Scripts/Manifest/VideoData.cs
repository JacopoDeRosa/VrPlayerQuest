using System;

namespace Manifest
{
    [Serializable]
    public class VideoData
    {
        public string VideoTitle;
        public LocalizedUrl[] UrlsByLanguage = new LocalizedUrl[2];
        public LocalizedUrl[] SubtitlesByLanguage = new LocalizedUrl[2];


        public string GetVideoUrlByLanguage(string language)
        {
            foreach (LocalizedUrl localUrl in UrlsByLanguage)
            {
                if (localUrl.Language == language)
                {
                    return localUrl.Url;
                }
            }

            return null;
        }
        public string GetSubtitleByLanguage(string language)
        {
            foreach (LocalizedUrl localUrl in SubtitlesByLanguage)
            {
                if (localUrl.Language == language)
                {
                    return localUrl.Url;
                }
            }

            return null;
        }
    }
}
