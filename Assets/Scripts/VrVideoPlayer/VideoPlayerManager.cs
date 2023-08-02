using System.IO;
using Manifest;
using Network;
using Subtitles;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Video;

namespace VrVideoPlayer
{
    public class VideoPlayerManager : MonoBehaviour
    {
        [SerializeField] private VrDeviceManager _vrDeviceManager;
        [SerializeField] private PlayableDirector _intro, _outro;
        [SerializeField] private RenderTexture _texture;
        [SerializeField] private SubtitleController _subtitleController;
    
    
        private AppManifest _manifest;
        private VideoPlayer _player;

        private void Start()
        { 
            _vrDeviceManager.onUserPresenceChanged += OnUserPresenceChanged;
            _manifest = AppManifest.Instance;
            _player = GetComponent<VideoPlayer>();
            _player.loopPointReached += OnVideoOver;
            OnUserPresenceChanged(true);
        }

        private void OnUserPresenceChanged(bool userPresence)
        {
            _texture.Release();
        
            void OnCallback(bool success, VisitorInZone visitor)
            {
                if (success)
                {
                    PlayVideo(_manifest.DefaultVideo, visitor.language);
                }
                else
                {
                    PlayDefaultVideo();
                }
            }
        
            if (userPresence == true)
            {
                StartCoroutine(Networking.GetVisitorInZoneRoutine(OnCallback));
            }
            else
            {
                _player.Stop();
            }
        }

        public void PlayVideo(int id, string lang)
        {
            string videoUrl = _manifest.GetDataByID(id).GetVideoUrlByLanguage(lang);
            string subUrl = _manifest.GetDataByID(id).GetSubtitleByLanguage(lang);
            PlayVideoFromUrl(videoUrl, subUrl);
        }

        public void PlayDefaultVideo()
        {
            PlayVideoFromUrl(_manifest.GetDefaultData().GetVideoUrlByLanguage("it"), _manifest.GetDefaultData().GetSubtitleByLanguage("it"));
        }

        private void PlayVideoFromUrl(string url, string subUrl)
        {
            _intro.Play();

#if UNITY_EDITOR
            _player.url = "file://" + Application.persistentDataPath + "/Video/" + url;
#else
        _player.url =  Application.persistentDataPath + "/Video/" + url;
#endif
            _player.Play();
            
            string subPath = Application.persistentDataPath + "/" + subUrl;

            Debug.Log("Sub Path is: " + subPath);
            if (File.Exists(subPath))
            {
                string subs = File.ReadAllText(subPath);

                SubtitleAsset subtitleAsset = JsonUtility.FromJson<SubtitleAsset>(subs);

                _subtitleController.RegisterNewAsset(subtitleAsset);
            }
        }

        private void OnVideoOver(VideoPlayer player)
        {
            _outro.Play();
            _texture.Release();
        }
    }
}
